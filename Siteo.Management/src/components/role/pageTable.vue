<template>
    <baseTable
    :data="data"
    @selection-change="handleSelectionChange"
    @row-action="handleAction"
    tableId="tblRole"
    >
        <el-table-column
        label="Name"
        >
            <template slot-scope="scope">
                <span at-key="Name">{{scope.row.Name}}</span>
            </template>
        </el-table-column>
        <el-table-column
        label="User Count"
        prop="UserCount"
        >
            <template slot-scope="scope">
                <span at-key="UserCount">{{scope.row.UserCount}}</span>
            </template>
        </el-table-column>
        <el-table-column
        label="Create By"
        >
            <template slot-scope="scope">
                <span at-key="CreateBy">{{scope.row.CreateBy}}</span>
            </template>
        </el-table-column>
        <el-table-column
        label="Create Date"
        width="180">
            <template slot-scope="scope">
                <i class="el-icon-time"></i>
                <span style="margin-left: 10px" at-key="CreateDate">{{ scope.row.CreateDate | date }}</span>
            </template>
        </el-table-column>
    </baseTable>
</template>

<script>
import baseTable from "../common/baseTable";

export default {
  name: "userPageTable",
  props:{
      data:{
          type:Array,
          default:() => []
      }
  },
  data() {
    return {
     
    };
  },
  components: {
    baseTable
  },
  mounted() {

  },
  methods:{
      handleSelectionChange(rows){
          this.$emit("selection-change",rows);
      },
      handleAction(p){
          this.$emit("row-action",p);
      }
  }
};
</script>

<style lang="scss">
</style>
