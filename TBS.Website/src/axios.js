import axios from 'axios'
export default axios.create({
  // For local
  baseURL: 'http://localhost:5000/api/v1/'
  // for prod
  // baseURL: 'https://tbs-api.ddns.net/api/v1/'
})