# SpotifyGraphQL

A full-stack GraphQL application to explore and analyze Spotify track data at scale.

## Overview

This project demonstrates how GraphQL simplifies data access for analytics-heavy scenarios where filter/sort combinations change frequently.

Instead of creating many REST endpoints, clients can query exactly what they need through a single GraphQL endpoint.

## Dataset

Source: https://www.kaggle.com/datasets/yashdev01/spotify-tracks-dataset

- ~100,000 tracks
- 125 genres
- Includes fields such as popularity, energy, danceability, tempo, genre, and more

## Tech Stack

### Backend
- .NET 10
- ASP.NET Core Minimal API
- HotChocolate GraphQL (`HotChocolate.AspNetCore`, `HotChocolate.Data`)
- GreenDonut
- OpenTelemetry (console exporter)
- Microsoft.Extensions.Http.Resilience
- Middleware: Output Cache, Rate Limiter, CORS
- Logging: Microsoft.Extensions.Logging

### Frontend
- React 19
- Vite 7
- Axios

### Testing
- xUnit
- Moq
- FluentAssertions

## Solution Structure

- `SpotifyGraphQL.Api` - API host + GraphQL endpoint + middleware pipeline
- `SpotifyGraphQL.Application` - use cases and application logic
- `SpotifyGraphQL.Infrastructure` - CSV repository and DI wiring
- `SpotifyGraphQL.Domain` - domain entities
- `SpotifyGraphQL.Tests` - unit/integration-style tests
- `spotify-graphql-ui` - React UI for writing GraphQL queries and viewing results

## Features

- GraphQL endpoint at `/graphql`
- Query capabilities with filtering and sorting
- Track-level lookup by `trackId`
- Server-side output caching
- Rate limiting for API protection
- OpenTelemetry instrumentation (logs/traces/metrics)
- Health endpoint at `/health`

## How It Works

1. User enters a GraphQL query in the UI.
2. UI sends request to backend GraphQL endpoint.
3. Backend resolves query from CSV-backed repository.
4. Results are rendered dynamically in a table.

## Getting Started

## Prerequisites
- .NET SDK 10
- Node.js 18+ (recommended 20+)
- npm

## Run Backend

```bash
dotnet restore
dotnet run --project SpotifyGraphQL.Api
