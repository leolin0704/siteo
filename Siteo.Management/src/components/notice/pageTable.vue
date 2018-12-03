<template>
    <baseTable
    :data="data"
    @selection-change="handleSelectionChange"
    @row-action="handleAction"
    tableId="tblNotice"
    >
        <el-table-column
        label="Title"
        >
            <template slot-scope="scope">
                <span at-key="Title">{{scope.row.Title}}</span>
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
  name: "noticePageTable",
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
