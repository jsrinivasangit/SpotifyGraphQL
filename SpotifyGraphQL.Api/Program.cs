using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Threading.RateLimiting;
using SpotifyGraphQL.Api.GraphQL;
using SpotifyGraphQL.Application.UseCases;
using SpotifyGraphQL.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var serviceName = "spotify-graphql-api";
var serviceVersion = typeof(Program).Assembly.GetName().Version?.ToString() ?? "1.0.0";
var environmentName = builder.Environment.EnvironmentName;

var resourceBuilder = ResourceBuilder.CreateDefault()
    .AddService(serviceName, serviceVersion: serviceVersion)
    .AddAttributes(new Dictionary<string, object>
    {
        ["deployment.environment"] = environmentName
    });

builder.Logging.SetMinimumLevel(LogLevel.Information);
builder.Logging.AddSimpleConsole(o =>
{
    o.TimestampFormat = "HH:mm:ss ";
    o.SingleLine = true;
});
builder.Logging.AddOpenTelemetry(options =>
{
    options.SetResourceBuilder(resourceBuilder);
    options.IncludeFormattedMessage = true;
    options.IncludeScopes = false;
    options.ParseStateValues = false;
    options.AddConsoleExporter();
});

builder.Services.AddInfrastructure(builder.Environment.ContentRootPath);
builder.Services.AddScoped<TrackQueries>();

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1)
            }));
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(policy => policy.Expire(TimeSpan.FromMinutes(60)));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddTypeExtension<StatsQuery>()
    .AddTypeExtension<SimilarityQuery>()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

app.UseRateLimiter();
app.UseCors("AllowReactApp");
app.UseOutputCache();

app.MapGet("/health", () => Results.Ok(new { status = "ok" }))
   .CacheOutput();

app.MapGraphQL("/graphql")
   .CacheOutput();

app.Run();
