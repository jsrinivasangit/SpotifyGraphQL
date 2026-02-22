using HotChocolate.Data;
using SpotifyGraphQL.Application.UseCases;
using SpotifyGraphQL.Domain.Entities;

namespace SpotifyGraphQL.Api.GraphQL;

public sealed class Query
{
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 25)]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<Track>> Tracks([Service] TrackQueries queries, CancellationToken ct)
        => (await queries.GetAllAsync(ct));

    public Task<Track?> TrackById(string trackId, [Service] TrackQueries queries, CancellationToken ct)
        => queries.GetByIdAsync(trackId, ct);

    public async Task<IEnumerable<string>> Genres([Service] TrackQueries queries, CancellationToken ct)
        => (await queries.GetAllAsync(ct))
            .Select(t => t.TrackGenre)
            .Where(g => !string.IsNullOrWhiteSpace(g))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(g => g);

    public async Task<IEnumerable<string>> Artists([Service] TrackQueries queries, CancellationToken ct)
        => (await queries.GetAllAsync(ct))
            .SelectMany(t => SplitArtists(t.Artists))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(a => a);

    private static IEnumerable<string> SplitArtists(string raw)
        => (raw ?? "")
            .Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Where(s => s.Length > 0);
}