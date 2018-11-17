<template>
    <baseDialog
        :mode="mode"
        name="Role"
        :visible="visible"
        @close="handleClose"
        @opened="handleOpen"
        @save="handleSave"
    >
        <el-form :model="roleModel">
            <el-form-item label="Name" :label-width="formLabelWidth">
                <el-input :disabled="mode === 'view'" v-model="roleModel.Name" autocomplete="off"></el-input>
            </el-form-item>
            <el-form-item label="Permissions" :label-width="formLabelWidth">
                <el-checkbox-group v-model="roleModel.Permissions" :disabled="mode === 'view'">
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
    props:{
        visible:{
            type:Boolean,
            default:false
        },
        model:{
            type:Object,
            default:null
        },
        mode:{
            type:String,
            default:"add"
        }
    },
    data() {
        return {
            roleModel:{},
            Permissions:[{
                ID:1,
                Name:"Content"
            },{
                ID:2,
                Name:"Basic Info"
            },{
                ID:3,
                Name:"System Manage"
            }],
            formLabelWidth:"100px"
        }
    },
    components: {
        baseDialog
    },
    mounted(){
        
    },
    methods:{
        loadData(id){
            const loading = this.$loading();
            setTimeout(() => {
                this.roleModel = {
                    Name:"admin",
                    Permissions:[1,2]
                };

                loading.close();
            },1000);
        },
        handleSave(){
            //TODO
            this.handleClose();
        },
        handleOpen(){
            if(this.model && this.mode === "edit"){
                this.loadData(this.model.ID);
            }
        },
        handleClose(){
            this.$emit("close");
        }
    }
};
</script>
<style scoped>
</style>
