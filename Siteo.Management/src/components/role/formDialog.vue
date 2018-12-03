<template>
<baseDialog :mode="mode" name="Role" :visible="visible" @close="handleClose" @open="handleOpen" @opened="handleOpened" @save="handleSave">
    <el-form ref="roleForm" :validate-on-rule-change="false" :rules="rules" :model="roleModel">
        <el-form-item label="Name" prop="Name" :label-width="formLabelWidth">
            <el-input id="txtRoleName" :disabled="mode === 'view'" v-model="roleModel.Name" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="Permissions" prop="PermissionIDList" :label-width="formLabelWidth">
            <el-checkbox-group id="txtRolePermissions" v-model="roleModel.PermissionIDList" :disabled="mode === 'view'">
                <el-checkbox at-key="chkPermission" v-for="permission in Permissions" :label="permission.ID" :key="permission.ID">{{permission.Name}}</el-checkbox>
            </el-checkbox-group>
        </el-form-item>
    </el-form>
</baseDialog>
</template>

<script>
import baseDialog from "../common/baseDialog";

export default {
    name: "viewRole",
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
        const validatePermissionIDList = (rule, value, callback) => {
            if (value === null || value.length === 0) {
                callback(new Error('Permission is required.'));
            } else {
                callback();
            }
        };

        return {
            roleModel: {},
            Permissions: [],
            rules:{
                Name: [
                    { required: true, message: 'Name is required.', trigger: 'blur' },
                    { min: 2, max: 50, message: 'Name length should between 2 to 50.', trigger: 'blur' }
                ],
                PermissionIDList:[
                    { required: true, message: 'Permission is required.', trigger: 'blur' },
                    { validator: validatePermissionIDList, trigger: 'blur' }
                ]
            },
            formLabelWidth: "100px"
        }
    },
    components: {
        baseDialog
    },
    mounted() {

    },
    methods: {
        loadPermissions() {
            return axiosGet("/RoleApi/GetPermissionList").then(response => {
                if (response.Status == 1) {
                    this.Permissions = response.Data.List;
                } else {
                    this.Permissions = [];
                }
            });

        },
        loadData() {
            if (this.mode === "edit" || this.mode === "view") {
                return axiosGet("/RoleApi/Get", {
                    id: this.model.ID
                }).then(response => {
                    if (response.Status == 1) {
                        this.roleModel = response.Data.Data;
                        if(!this.roleModel.PermissionIDList){
                            this.roleModel.PermissionIDList = [];
                        }
                    } else {
                        this.roleModel = {};
                    }
                });
            } else {
                return Promise.resolve();
            }
        },
        handleSave() {
            this.$refs["roleForm"].validate((valid) => {
                if(valid){
                    if (this.mode === "add") {
                        axiosPost("/RoleApi/Add", this.roleModel).then(response => {
                            if (response.Status == 1) {
                                this.handleClose(true);
                            }
                        });
                    } else if (this.mode === "edit") {
                            axiosPost("/RoleApi/edit", this.roleModel).then(response => {
                            if (response.Status == 1) {
                                this.handleClose(true);
                            }
                        });
                    }
                }
            })

        },
        handleOpen() {
            this.roleModel = {
                Name: "",
                PermissionIDList: []
            };
        },
        handleOpened() {
            let loading = this.$loading({
                lock: true,
                fullscreen: true
            });
            Promise.all([this.loadPermissions(), this.loadData()]).then(res => {
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
