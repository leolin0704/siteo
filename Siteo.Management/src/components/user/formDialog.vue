<template>
<baseDialog :mode="mode" name="Admin User" :visible="visible" @close="handleClose" @open="handleOpen" @opened="handleOpened" @save="handleSave">    
    <el-form ref="adminUserForm" :model="adminUserModel"  :rules="rules" >
        <el-form-item label="Account" prop="Account" :label-width="formLabelWidth">
            <el-input :disabled="mode === 'view'" v-model="adminUserModel.Account" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="Password" prop="Password" :label-width="formLabelWidth">
            <el-input type="password" :disabled="mode === 'view'" v-model="adminUserModel.Password" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="Confirm Password" prop="ConfirmPassword" :label-width="formLabelWidth">
            <el-input type="password" :disabled="mode === 'view'" v-model="adminUserModel.ConfirmPassword" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="Status" prop="Status" :label-width="formLabelWidth">
            <el-select v-model="adminUserModel.Status" placeholder="Please select...">
                <el-option
                v-for="(value, key) in adminUserStatus"
                :key="key"
                :label="value"
                :value="key">
                </el-option>
            </el-select>
        </el-form-item>
        <el-form-item label="Role" prop="RoleID" :label-width="formLabelWidth">
            <el-select v-model="adminUserModel.RoleID" placeholder="Please select...">
                <el-option
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
        const validatePass2 = (rule, value, callback) => {
            if (value === '') {
                callback(new Error('Comfirm password is required.'));
            } else if (value !== this.adminUserModel.Password) {
                callback(new Error('Confirm password must be same as password.'));
            } else {
                callback();
            }
        };


        return {
            adminUserModel: {},
            RoleList: {},
            formLabelWidth: "200px",
            adminUserStatus,
            rules:{
                Account: [
                    { required: true, message: 'Account is required.', trigger: 'blur' },
                    { min: 5, max: 30, message: 'Account length should between 5 to 30.', trigger: 'blur' }
                ],
                Password: [
                    { required: true, message: 'Password is required.', trigger: 'blur' },
                    { min: 5, max: 30, message: 'Password length should between 5 to 30.', trigger: 'blur' }
                ],
                ConfirmPassword: [
                    { required: true, message: 'Confirm password is required.', trigger: 'blur' },
                    { validator: validatePass2, trigger: 'blur' }
                ],
                Status: [
                    { required: true, message: 'Status is required.', trigger: 'blur' }
                ],
                RoleID: [
                    { required: true, message: 'Role is required.', trigger: 'blur' }
                ],
            }
        }
    },
    components: {
        baseDialog
    },
    mounted() {

    },
    methods: {
        loadRoles() {
            return axiosGet("/RoleApi/GetList?pageSize=9999&pageIndex=1&keywords=").then(response => {
                if (response.Status == 1) {
                    this.RoleList = response.Data.List;
                } else {
                    this.RoleList = {};
                }
            });

        },
        loadData() {
            if (this.mode === "edit" || this.mode === "view") {
                return axiosGet("/AdminUserApi/Get", {
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
                        axiosPost("/AdminUserApi/Add", this.adminUserModel).then(response => {
                            if (response.Status == 1) {
                                this.$message({
                                    message: response.Message,
                                    type: 'success'
                                });
                                
                                this.handleClose(true);
                            } 
                            // else {
                            //     this.$message({
                            //         message: response.Message,
                            //         type: 'warning'
                            //     });
                            // }
                        });
                    } else if (this.mode === "edit") {
                            axiosPost("/AdminUserApi/edit", this.adminUserModel).then(response => {
                            if (response.Status == 1) {
                                this.$message({
                                    message: response.Message,
                                    type: 'success'
                                });

                                this.handleClose(true);
                            }
                            // else {
                            //     this.$message({
                            //         message: response.Message,
                            //         type: 'warning'
                            //     });
                            // }
                        });
                    }

                } else {
                    return false;
                }
            });
        },
        handleOpen() {
            this.adminUserModel = {
                Account: "",
                Password:"",
                RoleID:""
            };

            this.$refs["adminUserForm"].resetFields();
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
            this.$emit("close", saved);
        }
    }
};
</script>

<style scoped>
</style>
