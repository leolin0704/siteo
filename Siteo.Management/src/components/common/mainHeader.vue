<template>
<el-menu :default-active="$route.path" mode="horizontal" @select="handleSelect"  :router="true" >
    <template v-for="(menu,index) in menus">
        <el-menu-item :key="index" :index="menu.url">
            {{menu.name}}
        </el-menu-item>
    </template>
</el-menu>
</template>

<script>
import menus from '../../config/modules.js';
export default {
    name: "mainHeader",
    data() {
        return {
            menus
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
