const apiBase = process.env.NODE_ENV === "production" ? "http://192.168.1.222:8081" : "http://localhost:56664";
const siteBase = "/";
export default {
  apiBase,
  siteBase
}