import { post, get } from "../util/apiUtil";

const login = (account, password) => post("/LoginApi/Login", {
    Account: account,
    Password: password
});


const getLoginUser = () =>  get("/LoginApi/GetLoginUser");

const logout = () => post("/LoginApi/Logout");


export {
    login,
    getLoginUser,
    logout
}