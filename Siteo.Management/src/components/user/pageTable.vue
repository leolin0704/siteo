<template>
    <baseTable
    :data="data"
    @selection-change="handleSelectionChange"
    @row-action="handleAction"
    >
        <el-table-column
        label="Account"
        prop="Account"
        >
        </el-table-column>
        <el-table-column
        label="Role Name"
        >
        <template slot-scope="scope">
            {{scope.row.Role.Name}}
        </template>
        </el-table-column>
        <el-table-column
        label="Status"
        >
        <template slot-scope="scope">
            {{adminUserStatus[scope.row.Status]}}
        </template>
        </el-table-column>
        <el-table-column
        label="Last Login IP"
        prop="LastLoginIP"
        >
        </el-table-column>
        <el-table-column
        label="Last Login Date"
        >
        <template slot-scope="scope">
            {{scope.row.LastLoginDate | date}}
        </template>
        </el-table-column>
        <!-- <el-table-column
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
        </el-table-column> -->
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
