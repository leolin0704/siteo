<script>
import baseListPage from "./baseListPage";
import EditDialog from "../components/user/formDialog";
import pageTable from "../components/user/pageTable";

export default {
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
    },
    methods:{
      handleDeleteConfirm(index, row){
           window.axiosPost("/AdminUserApi/Delete", {adminUserID:row.ID}).then(response => {
              if (response.Status == 1) {
                  this.loadData();
              }
          });
      },
      handleMultiDeleteConfirm(rows = []){
          var ids = [];
          rows.forEach( row => ids.push(row.ID));
          window.axiosPost("/AdminUserApi/MultiDelete", {adminUserIDs:ids}).then(response => {
              if (response.Status == 1) {
                  this.loadData();
              }
          });
      }
  }
};
</script>

<style lang="scss">
</style>
