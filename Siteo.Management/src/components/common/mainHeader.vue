<template>
<el-menu :default-active="$route.path" mode="horizontal" :router="true" :loading="loading">
    <el-menu-item index="/home">
        Home
    </el-menu-item>
    <template v-for="(menu,index) in modules">
        <el-submenu v-if="menu.ChildModules" :key="index" :index="menu.Key">
            <template slot="title">{{menu.Name}}</template>
            <el-menu-item :key="index*1000 + index2" v-for="(subMenu, index2) in menu.ChildModules" :index="subMenu.Key">{{subMenu.Name}}</el-menu-item>
        </el-submenu>
        <el-menu-item v-else :key="index" :index="menu.Key">
            {{menu.Name}}
        </el-menu-item>
    </template>

    
</el-menu>
</template>

<script>
import { get } from "../../util/apiUtil.js";
export default {
    name: "mainHeader",
    data() {
        return {
            loading:false,
            modules:[]
        };
    },
    mounted(){
        this.loading = true;
        get("/AdminUserApi/GetModuleList").then(response => {
            if(response.Status === 1){
                this.modules = response.Data.Modules;
            }

             this.loading = false;
        })
    },
    methods: {
    }
}
</script>
<style scoped>
    .menu-item{
        text-decoration: none;
    }

</style>
