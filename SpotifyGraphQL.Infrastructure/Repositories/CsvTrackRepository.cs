using System.Collections.Concurrent;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using SpotifyGraphQL.Application.Abstractions;
using SpotifyGraphQL.Domain.Entities;
using SpotifyGraphQL.Infrastructure.Csv;

namespace SpotifyGraphQL.Infrastructure.Repositories;

public sealed class CsvTrackRepository : ITrackRepository
{
    private readonly string _csvPath;
    private readonly Lazy<Task<IReadOnlyList<Track>>> _cache;
    private readonly ConcurrentDictionary<string, Track> _byId = new(StringComparer.Ordinal);

    public CsvTrackRepository(string csvPath)
    {
        _csvPath = csvPath ?? throw new ArgumentNullException(nameof(csvPath));
        _cache = new Lazy<Task<IReadOnlyList<Track>>>(LoadAsync);
    }

    public async Task<IReadOnlyList<Track>> GetAllAsync(CancellationToken ct)
        => await _cache.Value;

    public async Task<Track?> GetByTrackIdAsync(string trackId, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(trackId)) return null;
        await _cache.Value;
        _byId.TryGetValue(trackId, out var track);
        return track;
    }

    private async Task<IReadOnlyList<Track>> LoadAsync()
    {
        if (!File.Exists(_csvPath))
            throw new FileNotFoundException($"CSV file not found: {_csvPath}");

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ",",
            BadDataFound = null, 
            MissingFieldFound = null,
            DetectDelimiter = false,
            TrimOptions = TrimOptions.Trim
        };

        using var reader = new StreamReader(_csvPath);
        using var csv = new CsvReader(reader, config);

        var rows = csv.GetRecords<TrackRow>().ToList();

        //static string Clean(string s) => (s ?? "").Trim().Replace("\u014D", "o"); // ō -> o

        var tracks = rows
            .Where(r => !string.IsNullOrWhiteSpace(r.TrackId))
            .Select(r => new Track(
                TrackId: r.TrackId,
                Artists: r.Artists,
                AlbumName: r.AlbumName,
                TrackName: r.TrackName,
                Popularity: r.Popularity,
                DurationMs: r.DurationMs,
                Explicit: r.Explicit,
                Danceability: r.Danceability,
                Energy: r.Energy,
                Key: r.Key,
                Loudness: r.Loudness,
                Mode: r.Mode,
                Speechiness: r.Speechiness,
                Acousticness: r.Acousticness,
                Instrumentalness: r.Instrumentalness,
                Liveness: r.Liveness,
                Valence: r.Valence,
                Tempo: r.Tempo,
                TimeSignature: r.TimeSignature,
                TrackGenre: r.TrackGenre
            ))
            .ToList()
            .AsReadOnly();

        foreach (var t in tracks)
            _byId[t.TrackId] = t;

        return tracks;
    }
}