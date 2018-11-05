<template>
<el-container>
    <el-header>
        <el-row>
            <el-col :span="6">
                <h1 class="title">CMS管理系统</h1>
            </el-col>
            <el-col :span="18">
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
<style scoped>
    .title{
        height: 60px;
        line-height: 60px;
        margin: 0;
        padding: 0;
    }
</style>
