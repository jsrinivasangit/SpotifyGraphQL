using FluentAssertions;
using Moq;
using SpotifyGraphQL.Application.Abstractions;
using SpotifyGraphQL.Application.UseCases;
using SpotifyGraphQL.Tests.Fixtures;
using Xunit;
using Microsoft.Extensions.Logging;

namespace SpotifyGraphQL.Tests.UseCases;

public class TrackQueriesTests
{
    private readonly Mock<ITrackRepository> _mockRepository;
    private readonly Mock<ILogger<TrackQueries>> _mockLogger;
    private readonly TrackQueries _trackQueries;

    public TrackQueriesTests()
    {
        _mockRepository = new Mock<ITrackRepository>();
        _mockLogger = new Mock<ILogger<TrackQueries>>();
        _trackQueries = new TrackQueries(_mockLogger.Object, _mockRepository.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllTracks()
    {
        var tracks = TrackFixtures.CreateTracks(5);
        _mockRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(tracks.AsReadOnly());

        var result = await _trackQueries.GetAllAsync(CancellationToken.None);

        result.Should().HaveCount(5);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnTrack_WhenExists()
    {
        var track = TrackFixtures.CreateTrack(trackId: "123");
        _mockRepository.Setup(r => r.GetByTrackIdAsync("123", It.IsAny<CancellationToken>()))
            .ReturnsAsync(track);

        var result = await _trackQueries.GetByIdAsync("123", CancellationToken.None);

        result.Should().NotBeNull();
        result?.TrackId.Should().Be("123");
    }

    [Fact]
    public async Task GetByIdAsync_ReturnNull_WhenNotFound()
    {
        _mockRepository.Setup(r => r.GetByTrackIdAsync("999", It.IsAny<CancellationToken>()))
            .ReturnsAsync((Track?)null);

        var result = await _trackQueries.GetByIdAsync("999", CancellationToken.None);

        result.Should().BeNull();
    }
}
