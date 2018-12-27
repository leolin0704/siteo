<template>
<baseDialog :mode="mode" name="Admin User" :visible="visible" @close="handleClose" @open="handleOpen" @opened="handleOpened" @save="handleSave">    
    <el-form ref="adminUserForm" :validate-on-rule-change="false" :model="adminUserModel"  :rules="rules" >
        <el-form-item label="Account" prop="Account" :label-width="formLabelWidth">
            <el-input id="txtUserAccount" :disabled="mode !== 'add'" v-model="adminUserModel.Account" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="Password" prop="Password" :label-width="formLabelWidth">
            <el-input id="txtUserPassword" type="password" :disabled="mode === 'view'" v-model="adminUserModel.Password" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="Confirm Password" prop="ConfirmPassword" :label-width="formLabelWidth">
            <el-input id="txtUserConfirmPassword" type="password" :disabled="mode === 'view'" v-model="adminUserModel.ConfirmPassword" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="Status" prop="Status" :label-width="formLabelWidth">
            <el-select id="selUserStatusList" :disabled="mode === 'view'" v-model="adminUserModel.Status" placeholder="Please select...">
                <el-option
                    at-key="optUserStatus"
                    v-for="(value, key) in adminUserStatus"
                    :key="key"
                    :label="value"
                    :value="key">
                </el-option>
            </el-select>
        </el-form-item>
        <el-form-item label="Role" prop="RoleID" :label-width="formLabelWidth">
            <el-select id="selUserRoles" :disabled="mode === 'view'" v-model="adminUserModel.RoleID" placeholder="Please select...">
                <el-option
                    at-key="optUserRole"
                    v-for="item in RoleList"
                    :key="item.ID"
                    :label="item.Name"
                    :value="item.ID">
                </el-option>
            </el-select>
        </el-form-item>
    </el-form>
</baseDialog>
</template>

<script>
import baseDialog from "../common/baseDialog";
import {adminUserStatus} from "../../config/consts.js"

export default {
    name: "viewAdminUser",
    props: {
        visible: {
            type: Boolean,
            default: false
        },
        model: {
            type: Object,
            default: null
        },
        mode: {
            type: String,
            default: "add"
        }
    },
    data() {
        return {
            adminUserModel: {},
            RoleList: {},
            formLabelWidth: "200px",
            adminUserStatus
        }
    },
    components: {
        baseDialog
    },
    mounted() {

    },
    computed:{
        rules(){

            const validateAccount = (rule, value, callback) => {
                const newValue = value.trim();

                if(!newValue){
                    callback(new Error('Account is required.'));
                    return;
                }

                if(newValue.length < 5 || newValue.length > 30){
                    callback(new Error('Account length should between 5 to 30.'));
                    return;
                }

                const uPattern = /^[a-zA-Z0-9_-]*$/;
                if (!uPattern.test(newValue)) {
                    callback(new Error('Account may only contain alphanumeric characters, underline or single hyphens.'));
                } else {
                    callback();
                }
            };

            const validatePass2 = (rule, value, callback) => {
                if (value != this.adminUserModel.Password) {
                    callback(new Error('Confirm password must be same as password.'));
                } else {
                    callback();
                }
            };

            return {
                Account: [
                    { required: true, message: 'Account is required.', trigger: 'blur' },
                    { validator: validateAccount, trigger: 'blur' }
                ],
                Password: [
                    { required: this.mode === "add", message: 'Password is required.', trigger: 'blur' },
                    { min: 5, max: 30, message: 'Password length should between 5 to 30.', trigger: 'blur' }
                ],
                ConfirmPassword: [
                    { validator: validatePass2, trigger: 'blur' }
                ],
                Status: [
                    { required: true, message: 'Status is required.', trigger: 'blur' }
                ],
                RoleID: [
                    { required: true, message: 'Role is required.', trigger: 'blur' }
                ]
            }
        }
    },
    methods: {
        loadRoles() {
            return window.axiosGet("/RoleApi/GetList?pageSize=9999&pageIndex=1&keywords=").then(response => {
                if (response.Status == 1) {
                    this.RoleList = response.Data.List;
                } else {
                    this.RoleList = {};
                }
            });

        },
        loadData() {
            if (this.mode === "edit" || this.mode === "view") {
                return window.axiosGet("/AdminUserApi/Get", {
                    id: this.model.ID
                }).then(response => {
                    if (response.Status == 1) {
                        this.adminUserModel = response.Data.Data;
                    } else {
                        this.adminUserModel = {};
                    }
                });
            } else {
                return Promise.resolve();
            }
        },
        handleSave() {
            this.$refs["adminUserForm"].validate((valid) => {
                if (valid) {
                    if (this.mode === "add") {
                        window.axiosPost("/AdminUserApi/Add", this.adminUserModel).then(response => {
                            if (response.Status == 1) {
                                this.handleClose(true);
                            }
                        });
                    } else if (this.mode === "edit") {
                            window.axiosPost("/AdminUserApi/edit", this.adminUserModel).then(response => {
                            if (response.Status == 1) {
                                this.handleClose(true);
                            }
                        });
                    }

                } else {
                    return false;
                }
            });
        },
        handleOpen() {
            
        },
        handleOpened() {
            
            let loading = this.$loading({
                lock: true,
                fullscreen: true
            });
            Promise.all([this.loadRoles(), this.loadData()]).then(res => {
                loading.close();
            });
        },
        handleClose(saved = false) {
            this.adminUserModel = {
                Account: "",
                Password:"",
                RoleID:"",
                Status:""
            };
            this.$refs["adminUserForm"].resetFields();
            this.$emit("close", saved);
        }
    }
};
</script>

<style scoped>
</style>
