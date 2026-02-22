using SpotifyGraphQL.Application.UseCases;

namespace SpotifyGraphQL.Api.GraphQL;

[ExtendObjectType(typeof(Query))]
public sealed class StatsQuery
{
    public async Task<GenreStats?> StatsByGenre(
        string genre,
        [Service] TrackQueries queries,
        int top = 5,
        CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(genre)) return null;

        var tracks = (await queries.GetAllAsync(ct))
            .Where(t => string.Equals(t.TrackGenre, genre.Trim(), StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (tracks.Count == 0) return null;

        var avgTempo = tracks.Average(t => t.Tempo);
        var avgEnergy = tracks.Average(t => t.Energy);
        var avgDance = tracks.Average(t => t.Danceability);

        var topTracks = tracks
            .OrderByDescending(t => t.Popularity)
            .Take(Math.Clamp(top, 1, 25))
            .Select(t => new TopTrack(t.TrackId, t.TrackName, t.Artists, t.Popularity))
            .ToList()
            .AsReadOnly();

        return new GenreStats(
            Genre: genre,
            TrackCount: tracks.Count,
            AvgTempo: avgTempo,
            AvgEnergy: avgEnergy,
            AvgDanceability: avgDance,
            TopByPopularity: topTracks
        );
    }
}