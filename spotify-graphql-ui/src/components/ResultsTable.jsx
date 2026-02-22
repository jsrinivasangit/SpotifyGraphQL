import './ResultsTable.css'

function ResultsTable({ data }) {
  const flattenData = (obj, prefix = '') => {
    const result = {}
    for (const key in obj) {
      const value = obj[key]
      const newKey = prefix ? `${prefix}.${key}` : key

      if (Array.isArray(value)) {
        result[newKey] = JSON.stringify(value)
      } else if (value !== null && typeof value === 'object') {
        Object.assign(result, flattenData(value, newKey))
      } else {
        result[newKey] = value
      }
    }
    return result
  }

  const getRows = () => {
    let rows = []

    if (Array.isArray(data)) {
      rows = data.map((item) => flattenData(item))
    } else {
      for (const key in data) {
        const value = data[key]
        if (Array.isArray(value)) {
          rows = value.map((item) => flattenData(item))
          break
        } else if (value?.edges && Array.isArray(value.edges)) {
          rows = value.edges.map((edge) => flattenData(edge.node))
          break
        } else if (value?.nodes && Array.isArray(value.nodes)) {
          rows = value.nodes.map((node) => flattenData(node))
          break
        } else if (typeof value === 'object' && value !== null) {
          rows = [flattenData(value)]
          break
        }
      }
    }

    return rows
  }

  const rows = getRows()

  if (!rows.length) {
    return <div className="no-results">No results to display</div>
  }

  const columns = Object.keys(rows[0])

  return (
    <div className="results-table-container">
      <table className="results-table">
        <thead>
          <tr>
            {columns.map((col) => (
              <th key={col}>{col}</th>
            ))}
          </tr>
        </thead>
        <tbody>
          {rows.map((row, idx) => (
            <tr key={idx}>
              {columns.map((col) => (
                <td key={`${idx}-${col}`}>
                  {String(row[col] ?? '').substring(0, 100)}
                </td>
              ))}
            </tr>
          ))}
        </tbody>
      </table>
      <div className="result-count">Showing {rows.length} records</div>
    </div>
  )
}

export default ResultsTable
