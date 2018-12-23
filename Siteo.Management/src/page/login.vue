<template>
    <el-card class="login-box"  v-loading="loading">
        <div slot="header">
            <h1>Siteo Management System</h1>
        </div>
        <el-form :model="ruleForm" :rules="rules" ref="ruleForm" label-width="0px">
            <el-form-item prop="Account">
                <el-input id="txtAccount" v-model="ruleForm.Account" placeholder="Please input account...">
                    <template slot="prepend">
                        <i class="el-icon-message"></i>
                    </template>
                </el-input>
            </el-form-item>
            <el-form-item prop="Password">
                <el-input type="Password" id="txtPassword" placeholder="Please input password..." v-model="ruleForm.Password" @keyup.enter.native="submitForm('ruleForm')">
                    <template slot="prepend">
                        <i class="el-icon-view"></i>
                    </template>
                </el-input>
            </el-form-item>
            <div class="button-box">
                <el-button id="btnLogin" type="primary" @click="submitForm('ruleForm')">Login</el-button>
                <el-button id="btnReset" @click="resetForm('ruleForm')">Reset</el-button>
            </div>
        </el-form>
    </el-card>
</template>

<script>
import { mapState } from 'vuex';

export default {
    data: function () {
        return {
            loading:false,
            ruleForm: {
                Account: '',
                Password: ''
            },
            rules: {
                Account: [{
                    required: true,
                    message: 'Account is required.',
                    trigger: 'blur'
                }],
                Password: [{
                    required: true,
                    message: 'Password is required.',
                    trigger: 'blur'
                }]
            }
        }
    },
    computed: mapState({
        adminUser: state => state.login.user
    }),
    created(){
        if(this.adminUser.ID){
            this.$router.push('/home');
        }
    },
    methods: {
        submitForm(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    this.loading = true;
                    console.log(this.$store);
                    this.$store.dispatch('login/login', {
                        account: this.ruleForm.Account,
                        password: this.ruleForm.Password
                    })
                    .then((response) => {
                        if(response.Status === 1){
                            this.$router.push('/home');
                        }                       
                        this.loading = false;
                    });
                } else {
                    this.$message
                    return false;
                }
            });
        },
        resetForm(formName) {
            this.$refs[formName].resetFields();
        }
    }
}
</script>
<style scoped lang="scss">
    .login-box{
        width:600px;
        height:300px;
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
