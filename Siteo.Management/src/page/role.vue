<script>
import pageDialog from "../components/role/edit";
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
  }
};
</script>

<style lang="scss">
</style>
