<template>
<el-card shadow="hover" class="card">
    <div slot="header">
        <span>System Notice</span>
        <el-button class="btn-all" type="text">All</el-button>
    </div>
    <ul>
        <li :key="notice.ID" v-for="notice in noticeList"><a href="http://www.baidu.com" >{{notice.Title}}</a></li>
    </ul>
</el-card>
</template>
<script>
import { get } from "../util/apiUtil.js";

export default {
    name: "notice",
    data() {
        return {
            noticeList:[]
        }
    },
    components: {
    },
    mounted(){
        get("/NoticeApi/GetList", {
            pageSize:5,
            pageIndex:1,
            keywords:""
        }).then(response => {
            if(response.Status === 1){
                this.noticeList = response.Data.List;
            }
        })
    }
};
</script>
<style scoped>
.btn-all{
    float: right;
    padding: 3px 0;
}

</style>
