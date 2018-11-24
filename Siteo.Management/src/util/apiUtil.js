import web from "../config/web.js";
import axios from "axios";
import qs from "qs";
import router from '../route/index.js';
import { Message, Notification } from 'element-ui';
import { responseStatusList } from '../config/consts.js';



// 添加一个请求拦截器
axios.interceptors.request.use(function (config) {
    
    config.headers.token = localStorage.getItem("token");


    config.headers.common = { ...config.headers.common, ...{
        'Access-Control-Allow-Origin' : '*',
        'Access-Control-Allow-Methods' : '*',
        'Access-Control-Allow-Headers' : '*'
      } 
    };
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
    if(response.Status === responseStatusList.NO_PERMISSION){
        router.push("/");
    }else if(response.Status === responseStatusList.NOT_LOGIN){
        router.push("/login");
    }else{
        var message = "";
        if(response.MessageList){
            message = response.MessageList.join("<br />");
        }else if(response.Message){
            message = response.Message;
        }

        if(message){
            if(response.Status === responseStatusList.SUCCESS){
                Message({
                    message,
                    type:"success"
                })
            }else{
                Notification({
                    title: 'Operation Failed',
                    dangerouslyUseHTMLString: true,
                    message,
                    type: 'warning'
                })
            }
        }
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