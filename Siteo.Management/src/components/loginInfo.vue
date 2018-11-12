<template>
    <el-card shadow="hover" class="card" v-loading="loading">
        <div slot="header">
            <span>Welcome Back!</span>
        </div>
        <div class="avatar-box">
            <img :src="avatar" alt="avatar" class="avatar">
            <h3>{{adminUser.Account}}</h3>
        </div>
        <div class="login-info">
            <el-row>
                <el-col :span="10">Login Date:</el-col>
                <el-col :span="14">{{adminUser.LastLoginDate}}</el-col>
            </el-row>
            <el-row>
                <el-col :span="10">Login IP:</el-col>
                <el-col :span="14">{{adminUser.LastLoginIP}}</el-col>
            </el-row>
        </div>
    </el-card>
</template>
<script>
import { get } from "../util/apiUtil.js";
import avatar from "../assets/images/avatar.jpg";
export default {
    name: "loginInfo",
    data() {
        return {
            avatar,
            adminUser:{},
            loading:false
        }
    },
    components: {
    },
    mounted(){
        this.loading = true;
        get("/LoginApi/GetLoginUser").then(response => {
            if(response.Status){
                this.adminUser = response.Data.AdminUser || {};
            }

             this.loading = false;
        })
    }
};
</script>
<style scoped>
</style>
