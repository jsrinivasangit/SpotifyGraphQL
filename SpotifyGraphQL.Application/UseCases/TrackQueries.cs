using SpotifyGraphQL.Application.Abstractions;
using SpotifyGraphQL.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace SpotifyGraphQL.Application.UseCases;

public sealed class TrackQueries
{
    private readonly ITrackRepository _repo;
    private readonly ILogger<TrackQueries> _logger;

    public TrackQueries(ILogger<TrackQueries> logger, ITrackRepository repo) => (_logger, _repo) = (logger, repo);

    public Task<IReadOnlyList<Track>> GetAllAsync(CancellationToken ct) {
        _logger.LogInformation("Getting all tracks"); 
        return _repo.GetAllAsync(ct);
    }

    public Task<Track?> GetByIdAsync(string trackId, CancellationToken ct)
    {
        _logger.LogInformation("Getting track by ID: {TrackId}", trackId);
        return _repo.GetByTrackIdAsync(trackId, ct);
    }

}