using FluentAssertions;
using SpotifyGraphQL.Tests.Fixtures;
using Xunit;

namespace SpotifyGraphQL.Tests.Domain;

public class TrackEntityTests
{
    [Fact]
    public void Track_CreatedWithAllProperties()
    {
        var track = TrackFixtures.CreateTrack(trackId: "123", trackName: "Test Song", popularity: 85);

        track.TrackId.Should().Be("123");
        track.TrackName.Should().Be("Test Song");
        track.Popularity.Should().Be(85);
    }

    [Fact]
    public void Track_RecordEqualityWorks()
    {
        var track1 = TrackFixtures.CreateTrack(trackId: "1");
        var track2 = TrackFixtures.CreateTrack(trackId: "1");

        track1.Should().Be(track2);
    }
}
