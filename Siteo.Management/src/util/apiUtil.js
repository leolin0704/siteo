import web from "../config/web.js";
import axios from "axios";
import qs from "qs";

var getUrl = (url) => {

    if(!url)
        return;

    if(url.indexOf("http") === 0){
        return url;
    }else if(url.lastIndexOf("/") === (url.length - 1)){
        return  web.apiBase + url;
    }

    return web.apiBase + "/" + url;
}

var get = (url, params) => {
    return axios.get(getUrl(url), {
        params
    }).then(result => {
        if(result && result.status === 200){
            return result.data;
        }
      });
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
      });
}


export {
    get,
    post
}