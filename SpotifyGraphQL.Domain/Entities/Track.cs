namespace SpotifyGraphQL.Domain.Entities;

public sealed record Track(
    string TrackId,
    string Artists,
    string AlbumName,
    string TrackName,
    int Popularity,
    int DurationMs,
    bool Explicit,
    double Danceability,
    double Energy,
    int Key,
    double Loudness,
    int Mode,
    double Speechiness,
    double Acousticness,
    double Instrumentalness,
    double Liveness,
    double Valence,
    double Tempo,
    int TimeSignature,
    string TrackGenre
);