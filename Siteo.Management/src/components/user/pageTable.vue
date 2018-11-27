<template>
    <baseTable
    :data="data"
    @selection-change="handleSelectionChange"
    @row-action="handleAction"
    tableId="tblUser"
    >
        <el-table-column
        label="Account"
        >
            <template slot-scope="scope">
                <span at-key="Account">{{scope.row.Account}}</span>
            </template>
        </el-table-column>
        <el-table-column
        label="Role Name"
        >
            <template slot-scope="scope">
                <span at-key="RoleName">{{scope.row.Role.Name}}</span>
            </template>
        </el-table-column>
        <el-table-column
        label="Status"
        >
            <template slot-scope="scope">
                <span at-key="Status">{{adminUserStatus[scope.row.Status]}}</span>
            </template>
        </el-table-column>
        <el-table-column
        label="Last Login IP"
        >
            <template slot-scope="scope">
                <span at-key="LastLoginIP">{{scope.row.LastLoginIP}}</span>
            </template>
        </el-table-column>
        <el-table-column
        label="Last Login Date"
        >
        <template slot-scope="scope">
            <i class="el-icon-time"></i>
            <span at-key="LastLoginDate">{{scope.row.LastLoginDate | date}}</span>
        </template>
        </el-table-column>
    </baseTable>
</template>

<script>
import baseTable from "../common/baseTable";
import {adminUserStatus} from "../../config/consts.js";

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
        adminUserStatus
    };
  },
  components: {
    baseTable
  },
  mounted() {

  },
  methods:{
      handleSelectionChange(vals){
        this.$emit("selection-change", vals);
      },
      handleAction(p){
        this.$emit("row-action", p);
      }
  }
};
</script>

<style lang="scss">
</style>
