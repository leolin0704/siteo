<template>
<div>
    <el-row :gutter="10">
        <el-col :span="8">
            <loginInfo></loginInfo>
        </el-col>
        <el-col :span="16">
           <notice></notice>
        </el-col>
    </el-row>
    <el-row :gutter="10">
        <el-col :span="24">
            <el-card shadow="hover" class="card">
                <div slot="header">
                    <span>用户反馈</span>
                </div>
                <ul>
                    <li>网站不错</li>
                    <li>网站不错</li>
                    <li>网站不错</li>
                    <li>网站不错</li>
                    <li>网站不错</li>
                </ul>
            </el-card>
        </el-col>
    </el-row>
</div>
</template>

<script>
import notice from "../components/notice.vue";
import loginInfo from "../components/loginInfo.vue";


export default {
    name: "home",
    data() {
        return {
        }
    },
    components: {
        notice,
        loginInfo
    }
};
</script>

<style lang="scss">
.card {
    height: 300px;
    margin-bottom: 10px;
}

.avatar-box {
    border-bottom: solid 2px #ccc;
    padding: 10px;

    * {
        line-height: 80px;
        float: left;
        margin: 0 20px 0 0;
        padding: 0;
    }

    .avatar {
        width: 80px;
        height: 80px;
    }

    img {
        border-radius: 50%;
    }

    &:after {
        content: "";
        display: block;
        clear: both;
    }
}

.login-info {
    line-height: 30px;
    padding: 10px;
}
</style>
