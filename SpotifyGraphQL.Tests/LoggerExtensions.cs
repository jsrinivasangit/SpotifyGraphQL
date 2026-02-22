using Microsoft.Extensions.Logging;
using Moq;

namespace SpotifyGraphQL.Tests;

public static class LoggerExtensions
{
    public static void VerifyLogging<T>(
        this Mock<ILogger<T>> mockLogger,
        LogLevel logLevel,
        string expectedMessage,
        Times times)
    {
        mockLogger.Verify(
            x => x.Log(
                logLevel,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains(expectedMessage)),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            times);
    }
}
