import { useState } from 'react'
import './App.css'
import QueryEditor from './components/QueryEditor'
import ResultsTable from './components/ResultsTable'
import { executeGraphQLQuery } from './services/graphqlClient'

function App() {
  const [query, setQuery] = useState(`{
  tracks(first: 10) {
    edges {
      node {
        trackId
        trackName
        artists
        albumName
        popularity
        genre: trackGenre
      }
    }
  }
}`)

  const [results, setResults] = useState(null)
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState(null)

  const handleExecute = async () => {
    setLoading(true)
    setError(null)
    setResults(null)

    try {
      const data = await executeGraphQLQuery(query)
      setResults(data)
    } catch (err) {
      setError(err.message || 'An error occurred')
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="app-container">
      <header className="app-header">
        <h1>Spotify GraphQL Explorer</h1>
      </header>

      <div className="app-content">
        <div className="editor-section">
          <QueryEditor
            query={query}
            onQueryChange={setQuery}
            onExecute={handleExecute}
            loading={loading}
          />
        </div>

        <div className="results-section">
          {error && <div className="error-message">{error}</div>}
          {loading && <div className="loading">Executing query...</div>}
          {results && <ResultsTable data={results} />}
          {!results && !loading && !error && (
            <div className="placeholder">Enter a GraphQL query and click Execute</div>
          )}
        </div>
      </div>
    </div>
  )
}

export default App
