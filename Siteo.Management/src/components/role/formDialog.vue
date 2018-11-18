<template>
<baseDialog :mode="mode" name="Role" :visible="visible" @close="handleClose" @open="handleOpen" @opened="handleOpened" @save="handleSave">
    <el-form :model="roleModel">
        <el-form-item label="Name" :label-width="formLabelWidth">
            <el-input :disabled="mode === 'view'" v-model="roleModel.Name" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="Permissions" :label-width="formLabelWidth">
            <el-checkbox-group v-model="roleModel.PermissionIDList" :disabled="mode === 'view'">
                <el-checkbox v-for="permission in Permissions" :label="permission.ID" :key="permission.ID">{{permission.Name}}</el-checkbox>
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
        return {
            roleModel: {},
            Permissions: [],
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
            if (this.mode === "add") {
                axiosPost("/RoleApi/Add", this.roleModel).then(response => {
                    if (response.Status == 1) {
                        this.$message({
                            message: response.Message,
                            type: 'success'
                        });
                        
                        this.handleClose(true);
                    } else {
                        this.$message({
                            message: response.Message,
                            type: 'warning'
                        });
                    }
                });
            } else if (this.mode === "edit") {
                    axiosPost("/RoleApi/edit", this.roleModel).then(response => {
                    if (response.Status == 1) {
                        this.$message({
                            message: response.Message,
                            type: 'success'
                        });

                        this.handleClose(true);
                    } else {
                        this.$message({
                            message: response.Message,
                            type: 'warning'
                        });
                    }
                });
            }

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
