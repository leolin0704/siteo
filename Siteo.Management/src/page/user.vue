<script>
import baseListPage from "./baseListPage";
import EditDialog from "../components/user/formDialog";
import pageTable from "../components/user/pageTable";

export default{
    extends:baseListPage,
    name: "user",
    components:{
        pageDialog:EditDialog,
        pageTable
    },
    data() {
        return {
            query:{
                api:"/AdminUserApi/GetList"
            }
        };
    }
};
</script>

<style lang="scss">
</style>
