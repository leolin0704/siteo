<script>
import pageDialog from "../components/role/formDialog";
import pageTable from "../components/role/pageTable";
import baseListPage from "./baseListPage";

import { get } from "../util/apiUtil.js";
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
           axiosPost("/RoleApi/Delete", {roleID:row.ID}).then(response => {
              if (response.Status == 1) {
                  this.$message({
                      message: response.Message,
                      type: 'success'
                  });

                  this.loadData();
              }
            //   else {
            //       this.$message({
            //           message: response.Message,
            //           type: 'warning'
            //       });
            //   }
          });
      },
      handleMultiDeleteConfirm(rows = []){
          var ids = [];
          rows.forEach( row => ids.push(row.ID));
          axiosPost("/RoleApi/MultiDelete", {roleIDs:ids}).then(response => {
              if (response.Status == 1) {
                  this.$message({
                      message: response.Message,
                      type: 'success'
                  });

                  this.loadData();
              }
            //   else {
            //       this.$message({
            //           message: response.Message,
            //           type: 'warning'
            //       });
            //   }
          });
      }
  }
};
</script>

<style lang="scss">
</style>
