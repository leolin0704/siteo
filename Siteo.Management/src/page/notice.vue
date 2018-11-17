<script>
import baseListPage from "./baseListPage";
import EditDialog from "../components/role/edit";
import pageTable from "../components/user/pageTable";

export default{
    extends:baseListPage,
    name: "notice",
    components:{
        pageDialog:EditDialog,
        pageTable
    },
    data() {
        return {
            query:{
                api:"/NoticeApi/GetList"
            }
        };
    }
};
</script>

<style lang="scss">
</style>
