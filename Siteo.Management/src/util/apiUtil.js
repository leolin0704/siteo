import web from "../config/web.js";
import axios from "axios";
import qs from "qs";
import router from '../route/index.js';

// 添加一个请求拦截器
axios.interceptors.request.use(function (config) {
    
    config.headers.token = localStorage.getItem("token");
    // Do something before request is sent
    return config;
  }, function (error) {
    // Do something with request error
    return Promise.reject(error);
  });

var getUrl = (url) => {

    if(!url)
        return;

    if(url.indexOf("http") === 0){
        return url;
    }
    
    if(url.indexOf("/") === 0){
        return  web.apiBase + url;
    }

    return web.apiBase + "/" + url;
}

var processCommonResponse = (response) => {
    if(response.Status === 3){
        router.push("/");
    }else if(response.Status === 4){
        router.push("/login");
    }

    return response;
}

var get = (url, params) => {
    return axios.get(getUrl(url), {
        params
    }).then(result => {
        if(result && result.status === 200){
            return result.data;
        }
      }).then(processCommonResponse);
}

var post = (url, params) => {
    return axios.post(getUrl(url),qs.stringify(params),{
        headers: {
          'Content-Type': 'application/x-www-form-urlencoded'
        },
      }).then(result => {
        if(result && result.status === 200){
            return result.data;
        }
      }).then(processCommonResponse);
}

window.axiosGet = get;
window.axiosPost = post;


export {
    get,
    post
}