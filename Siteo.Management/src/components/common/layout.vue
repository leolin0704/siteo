<template>
<el-container>
    <el-header style="height:61px;">
        <el-row>
            <el-col :span="24" class="header-line">
                <h1 class="title">Siteo Management</h1>
                <main-header></main-header>
            </el-col>
        </el-row>
    </el-header>
    <el-container>
        <el-main>
                <router-view></router-view>
        </el-main>
    </el-container>
</el-container>
</template>
<script>
import mainHeader from "./mainHeader";
export default {
    name: "layout",
    components: {
        mainHeader
    }
};
</script>
<style scoped lang="scss">
 .header-line {
     position: relative;
    .title{
        height: 60px;
        line-height: 60px;
        position: absolute;
        margin: 0;
        padding: 0;
    }
    
    &>ul{
        margin-left: 250px;
    }
 }
    
</style>
