import axios from 'axios'

const API_URL = 'http://localhost:5005/graphql'

export const executeGraphQLQuery = async (query, variables = {}) => {
  try {
    const response = await axios.post(API_URL, {
      query,
      variables,
    })

    if (response.data.errors) {
      throw new Error(response.data.errors[0]?.message || 'GraphQL Error')
    }

    return response.data.data
  } catch (error) {
    throw error
  }
}
