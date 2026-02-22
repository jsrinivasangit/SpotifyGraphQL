# SpotifyGraphQL Unit Tests

Minimal unit tests for the Spotify GraphQL API focusing on API-related functionalities.

## Technology Stack

- **xUnit** - Testing framework
- **Moq** - Mocking library
- **FluentAssertions** - Assertion syntax

## Running Tests

```powershell
dotnet test
```

## Test Coverage (4 tests)

### TrackQueriesTests (3 tests) - API Query Logic
- GetAllAsync returns all tracks
- GetByIdAsync returns track when exists
- GetByIdAsync returns null when not found

### CsvTrackRepositoryTests (1 test) - Data Layer
- Constructor initializes with valid path

## Quick Commands

```powershell
# Run all tests
dotnet test

# Run with verbose output
dotnet test --verbosity detailed

# Run specific test class
dotnet test --filter "ClassName=TrackQueriesTests"

# Watch mode
dotnet watch test
```

