<template>
<el-container>
    <el-header style="height:61px;">
        <el-row>
            <el-col :span="24" class="header-line">
                <h1 class="title">Siteo Management</h1>
                <main-header></main-header>
                <user-info class="header-user-info"></user-info>
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
import userInfo from "./userInfo";
export default {
    name: "layout",
    components: {
        mainHeader,
        userInfo
    }
};
</script>
<style scoped lang="scss">
 .header-line {
     position: relative;

    @mixin positionItem(){
        height: 60px;
        line-height: 60px;
        position: absolute;
        margin: 0;
        padding: 0;
        top:0;
        z-index:2;
    }

    .title{
        @include positionItem();
    }
    
    &>ul{
        margin-left: 250px;
    }

    .header-user-info{
        @include positionItem();
        right:0;
        width:120px;
        text-align:right;
        white-space:nowrap;
        overflow: hidden;
    }
 }


    
</style>
