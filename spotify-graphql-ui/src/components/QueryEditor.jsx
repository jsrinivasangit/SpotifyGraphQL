import './QueryEditor.css'

function QueryEditor({ query, onQueryChange, onExecute, loading }) {
  return (
    <div className="query-editor">
      <label htmlFor="query-input">GraphQL Query:</label>
      <textarea
        id="query-input"
        className="query-input"
        value={query}
        onChange={(e) => onQueryChange(e.target.value)}
        placeholder="Enter your GraphQL query..."
        disabled={loading}
      />
      <button
        className="execute-btn"
        onClick={onExecute}
        disabled={loading || !query.trim()}
      >
        {loading ? 'Executing...' : 'Execute'}
      </button>
    </div>
  )
}

export default QueryEditor
