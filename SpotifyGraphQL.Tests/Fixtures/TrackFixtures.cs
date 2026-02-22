using SpotifyGraphQL.Domain.Entities;

namespace SpotifyGraphQL.Tests.Fixtures;

public static class TrackFixtures
{
    public static Track CreateTrack(
        string trackId = "1",
        string trackName = "Test Song",
        string artists = "Test Artist",
        string albumName = "Test Album",
        int popularity = 75,
        int durationMs = 180000,
        bool explicit_ = false,
        double danceability = 0.7,
        double energy = 0.6,
        int key = 0,
        double loudness = -5.0,
        int mode = 1,
        double speechiness = 0.03,
        double acousticness = 0.1,
        double instrumentalness = 0.0,
        double liveness = 0.15,
        double valence = 0.7,
        double tempo = 120.0,
        int timeSignature = 4,
        string trackGenre = "pop")
    {
        return new Track(
            trackId, artists, albumName, trackName, popularity, durationMs, explicit_,
            danceability, energy, key, loudness, mode, speechiness, acousticness,
            instrumentalness, liveness, valence, tempo, timeSignature, trackGenre);
    }

    public static List<Track> CreateTracks(int count = 5)
    {
        var tracks = new List<Track>();
        for (int i = 1; i <= count; i++)
        {
            tracks.Add(CreateTrack(
                trackId: i.ToString(),
                trackName: $"Song {i}",
                popularity: 50 + i * 5,
                energy: 0.4 + (i * 0.1),
                danceability: 0.5 + (i * 0.05)));
        }
        return tracks;
    }
}
