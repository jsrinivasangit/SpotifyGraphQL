using Microsoft.Extensions.DependencyInjection;
using SpotifyGraphQL.Application.Abstractions;
using SpotifyGraphQL.Infrastructure.Repositories;

namespace SpotifyGraphQL.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string contentRootPath)
    {
        var csvPath = Path.Combine(AppContext.BaseDirectory, "Data", "spotify-tracks-dataset.csv");

        services.AddSingleton<ITrackRepository>(_ => new CsvTrackRepository(csvPath));

        return services;
    }
}