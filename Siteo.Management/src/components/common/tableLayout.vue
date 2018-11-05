<template>
<div>
    <el-row>
        <el-col :span="24">
            <el-button>新增</el-button>
            <el-button>修改</el-button>
            <el-button>删除</el-button>
        </el-col>
    </el-row>
    <el-row>
        <el-col :span="24">
            <el-table
                :data="tableData"
                style="width: 100%">
                <el-table-column
                label="日期"
                width="180">
                <template slot-scope="scope">
                    <i class="el-icon-time"></i>
                    <span style="margin-left: 10px">{{ scope.row.date }}</span>
                </template>
                </el-table-column>
                <el-table-column
                label="姓名"
                width="180">
                <template slot-scope="scope">
                    <el-popover trigger="hover" placement="top">
                    <p>姓名: {{ scope.row.name }}</p>
                    <p>住址: {{ scope.row.address }}</p>
                    <div slot="reference" class="name-wrapper">
                        <el-tag size="medium">{{ scope.row.name }}</el-tag>
                    </div>
                    </el-popover>
                </template>
                </el-table-column>
                <el-table-column label="操作">
                <template slot-scope="scope">
                    <el-button
                    size="mini"
                    @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
                    <el-button
                    size="mini"
                    type="danger"
                    @click="handleDelete(scope.$index, scope.row)">删除</el-button>
                </template>
                </el-table-column>
            </el-table>
        </el-col>
    </el-row>
</div> 
</template>
<script>
import mainHeader from "./mainHeader";
export default {
  name: "layout",
  data(){
      return {
          tableData: [{
          date: '2016-05-02',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1518 弄'
        }, {
          date: '2016-05-04',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1517 弄'
        }, {
          date: '2016-05-01',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1519 弄'
        }, {
          date: '2016-05-03',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1516 弄'
        }]
      };
  },
  components: {
        mainHeader
  },
  methods:{
      handleEdit(index, row) {
        console.log(index, row);
      },
      handleDelete(index, row) {
        console.log(index, row);
      }
  }
};
</script>
<style scoped>
.title {
  height: 60px;
  line-height: 60px;
  margin: 0;
  padding: 0;
}
</style>
