<template>
<el-menu :default-active="$route.path" mode="horizontal" @select="handleSelect"  :router="true" >
    <template v-for="(menu,index) in modules">
        <el-submenu v-if="menu.subModules" :key="index" :index="menu.url">
            <template slot="title">{{menu.name}}</template>
            <el-menu-item :key="index*1000 + index2" v-for="(subMenu, index2) in menu.subModules" :index="subMenu.url">{{subMenu.name}}</el-menu-item>
        </el-submenu>
        <el-menu-item v-else :key="index" :index="menu.url">
            {{menu.name}}
        </el-menu-item>
    </template>

    
</el-menu>
</template>

<script>
import modules from '../../config/modules.js';
export default {
    name: "mainHeader",
    data() {
        return {
            modules
        };
    },
    mounted(){
    },
    methods: {
        handleSelect(key, keyPath) {
            console.log(key, keyPath);
        }
    }
}
</script>
<style scoped>
    .menu-item{
        text-decoration: none;
    }

</style>
