<template>
<el-menu :default-active="activeIndex2" class="el-menu-demo" mode="horizontal" @select="handleSelect" >
    <template v-for="(menu,index) in menus">
        <el-menu-item :key="index" :index="menu.key">
            <router-link :to="menu.url" class="menu-item" >{{menu.name}}</router-link>
            </el-menu-item>
    </template>
</el-menu>
</template>

<script>
import menus from '../../config/modules.js';
const defaultKey = menus && menus.length && menus[0].key;
export default {
    name: "mainHeader",
    data() {
        return {
            activeIndex2: defaultKey,
            menus
        };
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
