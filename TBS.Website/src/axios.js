import axios from 'axios'
export default axios.create({
    baseURL: "https://localhost:5001/api/"
    // baseURL: "https://put-url-here.net/api/"
})