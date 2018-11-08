<template>

<el-card class="login-box">
    <div slot="header">
        <h1>CMS管理系统</h1>
    </div>
    <el-form :model="ruleForm" :rules="rules" ref="ruleForm" label-width="0px">
        <el-form-item prop="Account">
            <el-input v-model="ruleForm.Account" placeholder="请输入用户名...">
                <template slot="prepend">
                    <i class="el-icon-message"></i>
                </template>
            </el-input>
        </el-form-item>
        <el-form-item prop="Password">
            <el-input type="Password" placeholder="请输入密码..." v-model="ruleForm.Password" @keyup.enter.native="submitForm('ruleForm')">
                <template slot="prepend">
                    <i class="el-icon-view"></i>
                </template>
            </el-input>
        </el-form-item>
        <div class="button-box">
            <el-button type="primary" @click="submitForm('ruleForm')">Login</el-button>
            <el-button native-type="reset">Reset</el-button>
        </div>
    </el-form>
</el-card>
</template>

<script>
import { post } from "../util/apiUtil.js";

export default {
    data: function () {
        return {
            ruleForm: {
                Account: '123',
                Password: '123123'
            },
            rules: {
                Account: [{
                    required: true,
                    message: '请输入用户名',
                    trigger: 'blur'
                }],
                Password: [{
                    required: true,
                    message: '请输入密码',
                    trigger: 'blur'
                }]
            }
        }
    },
    methods: {
        submitForm(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    post("/LoginApi/Login", {
                        Account:this.ruleForm.Account,
                        Password:this.ruleForm.Password
                    }).then((response) => {
                         if(response.Status === 1){
                            this.$router.push('/');
                        }else{
                            this.$message({
                                message: 'Account or password is not correct.',
                                type: 'warning'
                            });
                        }
                    });
                    
                } else {
                    this.$message
                    return false;
                }
            });
        }
    }
}
</script>
<style scoped lang="scss">
    .login-box{
        width:600px;
        height:350px;
        position: absolute;
        top:50%;
        left:50%;
        margin: -250px 0 0 -300px;

        h1{
            margin: 10px 0;
            text-align: center;
        }

        .button-box{
            margin: 10px 0;
            text-align: center;
        }
    }
</style>
