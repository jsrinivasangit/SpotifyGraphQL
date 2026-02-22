using CsvHelper.Configuration.Attributes;

namespace SpotifyGraphQL.Infrastructure.Csv;

public sealed class TrackRow
{
    [Name("unnamed1")] public string? Unnamed1 { get; set; }
    [Name("unnamed2")] public string? Unnamed2 { get; set; }

    [Name("track_id")] public string TrackId { get; set; } = "";
    [Name("artists")] public string Artists { get; set; } = "";
    [Name("album_name")] public string AlbumName { get; set; } = "";
    [Name("track_name")] public string TrackName { get; set; } = "";

    [Name("popularity")] public int Popularity { get; set; }
    [Name("duration_ms")] public int DurationMs { get; set; }
    [Name("explicit")] public bool Explicit { get; set; }

    [Name("danceability")] public double Danceability { get; set; }
    [Name("energy")] public double Energy { get; set; }
    [Name("key")] public int Key { get; set; }
    [Name("loudness")] public double Loudness { get; set; }
    [Name("mode")] public int Mode { get; set; }
    [Name("speechiness")] public double Speechiness { get; set; }
    [Name("acousticness")] public double Acousticness { get; set; }
    [Name("instrumentalness")] public double Instrumentalness { get; set; }
    [Name("liveness")] public double Liveness { get; set; }
    [Name("valence")] public double Valence { get; set; }
    [Name("tempo")] public double Tempo { get; set; }
    [Name("time_signature")] public int TimeSignature { get; set; }
    [Name("track_genre")] public string TrackGenre { get; set; } = "";
}