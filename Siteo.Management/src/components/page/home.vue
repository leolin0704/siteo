<template>
<div>
    <el-row :gutter="10">
        <el-col :span="8">
            <el-card shadow="hover" class="card">
                <div slot="header">
                    <span>登录信息</span>
                </div>
                <div class="avatar-box">
                    <img :src="avatar" alt="avatar" class="avatar">
                    <h3>admin</h3>
                </div>
                <div class="login-info">
                    <el-row>
                        <el-col :span="10">上次登录时间:</el-col>
                        <el-col :span="14">2018-08-08 20:00:00</el-col>
                    </el-row>
                    <el-row>
                        <el-col :span="10">上次登录IP:</el-col>
                        <el-col :span="14">127.0.0.1</el-col>
                    </el-row>
                </div>
            </el-card>
        </el-col>
        <el-col :span="16">
            <el-card shadow="hover" class="card">
                <div slot="header">
                    <span>系统公告</span>
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
import avatar from "../../assets/images/avatar.jpg";
export default {
    name: "home",
    data() {
        return {
            avatar
        }
    },
    components: {}
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
