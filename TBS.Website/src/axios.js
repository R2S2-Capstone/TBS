import axios from 'axios'
export default axios.create({
    // For local
    baseURL: 'https://localhost:7000/api/v1/'
    // for prod
    // baseURL: 'https://tbs-api.ddns.net/api/v1/'
})