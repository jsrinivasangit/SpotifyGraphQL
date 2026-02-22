using FluentAssertions;
using Xunit;

namespace SpotifyGraphQL.Tests.Repositories;

public class CsvTrackRepositoryTests
{
    [Fact]
    public void Constructor_InitializesWithValidPath()
    {
        var csvPath = Path.Combine(Directory.GetCurrentDirectory(), "test-data.csv");
        
        var action = () => new SpotifyGraphQL.Infrastructure.Repositories.CsvTrackRepository(csvPath);

        action.Should().NotThrow();
    }
}
