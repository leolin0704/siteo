<template>
    <baseTable
    :data="data"
    @selection-change="handleSelectionChange"
    @row-action="handleAction"
    >
        <el-table-column
        label="Name"
        prop="Name"
        >
        </el-table-column>
        <el-table-column
        label="Create By"
        prop="CreateBy"
        >
        </el-table-column>
        <el-table-column
        label="Create Date"
        width="180">
            <template slot-scope="scope">
                <i class="el-icon-time"></i>
                <span style="margin-left: 10px">{{ scope.row.CreateDate | date }}</span>
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
      handleSelectionChange(){
          this.$emit("selection-change",arguments);
      },
      handleAction(){
          this.$emit("row-action",arguments);
      }
  }
};
</script>

<style lang="scss">
</style>
