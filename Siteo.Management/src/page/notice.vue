<script>
import baseListPage from "./baseListPage";
import EditDialog from "../components/notice/formDialog";
import pageTable from "../components/notice/pageTable";

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
    },
    methods:{
      handleDeleteConfirm(index, row){
           window.axiosPost("/NoticeApi/Delete", {noticeID:row.ID}).then(response => {
              if (response.Status == 1) {
                  this.loadData();
              }
          });
      },
      handleMultiDeleteConfirm(rows = []){
          var ids = [];
          rows.forEach( row => ids.push(row.ID));
          window.axiosPost("/NoticeApi/MultiDelete", {noticeIDs:ids}).then(response => {
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
