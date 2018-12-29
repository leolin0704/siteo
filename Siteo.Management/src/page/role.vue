<script>
import pageDialog from "../components/role/formDialog";
import pageTable from "../components/role/pageTable";
import baseListPage from "./baseListPage";

export default {
  extends:baseListPage,
  name: "role",
  components:{
    pageTable,
    pageDialog
  },
  data() {
    return {
      query:{
        api:"/RoleApi/GetList"
      }
    };
  },
  methods:{
      handleDeleteConfirm(index, row){
           window.axiosPost("/RoleApi/Delete", {roleID:row.ID}).then(response => {
              if (response.Status == 1) {
                  this.loadData();
              }
          });
      },
      handleMultiDeleteConfirm(rows = []){
          var ids = [];
          rows.forEach( row => ids.push(row.ID));
          window.axiosPost("/RoleApi/MultiDelete", {roleIDs:ids}).then(response => {
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
