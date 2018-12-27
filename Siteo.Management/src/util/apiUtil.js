import web from "../config/web.js";
import axios from "axios";
import qs from "qs";
import router from '../route/index.js';
import { Message, Notification } from 'element-ui';
import { responseStatusList } from '../config/consts.js';
import { getToken } from './tokenHelper';
import { HTMLEncode } from './commonUtil';


// 添加一个请求拦截器
axios.interceptors.request.use(function (config) {
    
    config.headers.token = getToken();


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


const transformRequest = (data) => {
    let newData = {};

    if(!data) {
        return newData;
    }
    
    for (let k in data) {
        var value = data[k];

        if(typeof(value) === "string"){
            newData[k] = HTMLEncode(value || "");
        } else if(typeof(value) === "object"){
            newData[k] = transformRequest(value);
        } else {
            newData[k] = value;
        }
    }

    return newData;
};

const getUrl = (url) => {

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
        router.push("/home");
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
        params: transformRequest(params)
    }).then(result => {
        if(result && result.status === 200){
            return result.data;
        }
      }).then(processCommonResponse);
}

var post = (url, params) => {
    return axios.post(getUrl(url),qs.stringify(transformRequest(params)),{
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