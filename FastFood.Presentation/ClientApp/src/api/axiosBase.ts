import axios from "axios";

const apiUrl = import.meta.env.VITE_APP_API_URL;

const axiosBase = axios.create({
	baseURL: apiUrl,
	headers: { "Content-Type": "application/json" },
	timeout: 5000,
});

export default axiosBase;
