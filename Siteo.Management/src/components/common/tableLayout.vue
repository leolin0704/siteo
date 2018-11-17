<template>
<div>
    <el-row>
        <el-col :span="24">
            <el-card>
                <slot name="query"></slot>
            </el-card>
        </el-col>
    </el-row>
    <el-row>
        <el-col :span="24">
            <el-card>
                <div slot="header" class="clearfix">
                    <slot name="button"></slot>
                </div>
                <slot name="table"></slot>
            </el-card>
            <el-card class="footer-card">
                <slot name="pager"></slot>
            </el-card>
        </el-col>
    </el-row>
</div> 
</template>
<script>
import mainHeader from "./mainHeader";
export default {
    name: "layout"
};
</script>
<style scoped lang="scss">
.footer-card{
    text-align: center;
}
</style>
