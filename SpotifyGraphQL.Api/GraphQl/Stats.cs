namespace SpotifyGraphQL.Api.GraphQL;

public sealed record GenreStats(
    string Genre,
    int TrackCount,
    double AvgTempo,
    double AvgEnergy,
    double AvgDanceability,
    IReadOnlyList<TopTrack> TopByPopularity);

public sealed record TopTrack(string TrackId, string TrackName, string Artists, int Popularity);