using SpotifyGraphQL.Application.UseCases;
using SpotifyGraphQL.Domain.Entities;

namespace SpotifyGraphQL.Api.GraphQL;

public sealed record SimilarTrack(string TrackId, string TrackName, string Artists, string Genre, double Score);

[ExtendObjectType(typeof(Query))]
public sealed class SimilarityQuery
{
    public async Task<IReadOnlyList<SimilarTrack>> SimilarTracks(
        string trackId,
        [Service] TrackQueries queries,
        int top = 10,
        bool sameGenreOnly = true,
        CancellationToken ct = default)
    {
        var all = await queries.GetAllAsync(ct);
        var seed = all.FirstOrDefault(t => t.TrackId == trackId);
        if (seed is null) return Array.Empty<SimilarTrack>();

        IEnumerable<Track> candidates = all.Where(t => t.TrackId != seed.TrackId);

        if (sameGenreOnly)
            candidates = candidates.Where(t => string.Equals(t.TrackGenre, seed.TrackGenre, StringComparison.OrdinalIgnoreCase));

        double Score(Track a, Track b)
        {
            var d =
                Math.Abs(a.Danceability - b.Danceability) * 1.2 +
                Math.Abs(a.Energy - b.Energy) * 1.2 +
                Math.Abs(a.Acousticness - b.Acousticness) * 0.8 +
                Math.Abs(a.Instrumentalness - b.Instrumentalness) * 0.6 +
                Math.Abs(a.Valence - b.Valence) * 0.8 +
                Math.Abs(a.Tempo - b.Tempo) / 200.0;
            return 1.0 / (1.0 + d);
        }

        var results = candidates
            .Select(t => new SimilarTrack(t.TrackId, t.TrackName, t.Artists, t.TrackGenre, Score(seed, t)))
            .OrderByDescending(x => x.Score)
            .ThenByDescending(x => x.TrackId)
            .Take(Math.Clamp(top, 1, 50))
            .ToList()
            .AsReadOnly();

        return results;
    }
}