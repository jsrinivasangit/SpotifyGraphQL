using SpotifyGraphQL.Domain.Entities;

namespace SpotifyGraphQL.Application.Abstractions;

public interface ITrackRepository
{
    Task<IReadOnlyList<Track>> GetAllAsync(CancellationToken ct);
    Task<Track?> GetByTrackIdAsync(string trackId, CancellationToken ct);
}