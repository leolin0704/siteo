<template>

<el-card class="login-box">
    <div slot="header">
        <h1>CMS管理系统</h1>
    </div>
    <el-form :model="ruleForm" :rules="rules" ref="ruleForm" label-width="0px">
        <el-form-item prop="username">
            <el-input v-model="ruleForm.username" placeholder="请输入用户名...">
                <template slot="prepend">
                    <i class="el-icon-message"></i>
                </template>
            </el-input>
        </el-form-item>
        <el-form-item prop="password">
            <el-input type="password" placeholder="请输入密码..." v-model="ruleForm.password" @keyup.enter.native="submitForm('ruleForm')">
                <template slot="prepend">
                    <i class="el-icon-view"></i>
                </template>
            </el-input>
        </el-form-item>
        <div class="button-box">
            <el-button type="primary" @click="submitForm('ruleForm')">登录</el-button>
            <el-button native-type="reset">重置</el-button>
        </div>
    </el-form>
</el-card>
</template>

<script>
export default {
    data: function () {
        return {
            ruleForm: {
                username: 'admin@test.com',
                password: '123123'
            },
            rules: {
                username: [{
                    required: true,
                    message: '请输入用户名',
                    trigger: 'blur'
                }],
                password: [{
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
                    localStorage.setItem('ms_username', this.ruleForm.username);
                    this.$router.push('/');
                } else {
                    console.log('error submit!!');
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
