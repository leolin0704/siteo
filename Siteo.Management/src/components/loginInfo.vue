<template>
    <el-card shadow="hover" class="card">
        <div slot="header">
            <span>Welcome Back!</span>
        </div>
        <div class="avatar-box">
            <img :src="avatar" alt="avatar" class="avatar">
            <h3>{{adminUser.Account}}</h3>
        </div>
        <div class="login-info">
            <el-row>
                <el-col :span="10">Role:</el-col>
                <el-col :span="14">{{adminUser.Role && adminUser.Role.Name}}</el-col>
            </el-row>
            <el-row>
                <el-col :span="10">Login Date:</el-col>
                <el-col :span="14">{{adminUser.LastLoginDate | date}}</el-col>
            </el-row>
            <el-row>
                <el-col :span="10">Login IP:</el-col>
                <el-col :span="14">{{adminUser.LastLoginIP}}</el-col>
            </el-row>
        </div>
    </el-card>
</template>
<script>
import avatar from "../assets/images/avatar.jpg";
import { mapState } from 'vuex';
export default {
    name: "loginInfo",
    data() {
        return {
            avatar
        }
    },
    computed: mapState({
        adminUser: state => state.login.user
    })
};
</script>
<style scoped>
</style>
