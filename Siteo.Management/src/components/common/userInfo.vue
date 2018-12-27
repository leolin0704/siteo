<template>
<span class="user-info-box">
    Welcome,
    <el-popover
    placement="bottom"
    width="220"
    trigger="click">
    <div class="button-box">
        <el-button size="mini">Change Password</el-button>
        <el-button type="primary" size="mini" @click="logout()">Log out</el-button>
    </div>
    <el-button type="text" slot="reference">{{adminUser.Account}}</el-button>
    </el-popover>
</span>
</template>

<script>
import { mapState } from 'vuex';
import web from '../../config/web';

export default {
    name: "userInfo",
    data() {
        return {
        };
    },
    created () {
        if(!this.adminUser || !this.adminUser.ID) {
            this.$store.dispatch('login/getLoginUser');
        }
    },
    computed: mapState({
        adminUser: state => state.login.user
    }),
    methods: {
        logout(){
            this.$confirm('Are you sure?', 'Prompt', {
                confirmButtonText: 'OK',
                cancelButtonText: 'CANCEL',
                type: 'warning'
            })
            .then(() => {
                const loading = this.$loading({
                    lock: true
                });
                this.$store.dispatch('login/logout').then(response => {
                    if(response.Status === 1){
                        window.location = web.siteBase;
                    }
                });
            });
        }
    }
}
</script>
<style scoped lang="scss">
    .user-info-box{
        font-size: 12px;
    }

    .button-box{
        text-align:center;
    }
</style>
