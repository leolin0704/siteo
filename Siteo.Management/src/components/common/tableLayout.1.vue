<template>
<div>
    <el-row>
        <el-col :span="24">
            <el-card>
                <el-form :inline="true" :model="queryForm">
                <el-form-item>
                    <el-input placeholder="Please input query condition..." v-model="queryForm.queryCondition" class="query-with-type">
                        <el-select v-model="queryForm.queryConditionType" slot="prepend" placeholder="Please choose...">
                        <el-option label="Title" value="1"></el-option>
                        <el-option label="Content" value="2"></el-option>
                        <el-option label="Type" value="3"></el-option>
                        </el-select>
                        <el-button slot="append" type="primary">QUERY</el-button>
                    </el-input>
                </el-form-item>
                <el-form-item>
                    <el-button >RESET</el-button>
                </el-form-item>
                </el-form>
            </el-card>
        </el-col>
    </el-row>
    <el-row>
        <el-col :span="24">
            <el-card>
                <div slot="header" class="clearfix">
                    <el-button type="primary">ADD</el-button>
                    <el-button>DELETE</el-button>
                </div>
                <el-table
                    :data="tableData"
                    style="width: 100%">
                    <el-table-column
                    label="Create Date"
                    width="180">
                    <template slot-scope="scope">
                        <i class="el-icon-time"></i>
                        <span style="margin-left: 10px">{{ scope.row.date }}</span>
                    </template>
                    </el-table-column>
                    <el-table-column
                    label="Name"
                    >
                    <template slot-scope="scope">
                        <el-popover trigger="hover" placement="top">
                        <p>Name: {{ scope.row.name }}</p>
                        <p>Address: {{ scope.row.address }}</p>
                        <div slot="reference" class="name-wrapper">
                            <el-tag size="medium">{{ scope.row.name }}</el-tag>
                        </div>
                        </el-popover>
                    </template>
                    </el-table-column>
                    <el-table-column
                    label="Action"
                    width="180"
                    >
                    <template slot-scope="scope">
                        <el-button
                        size="mini"
                        @click="handleEdit(scope.$index, scope.row)">EDIT</el-button>
                        <el-button
                        size="mini"
                        type="danger"
                        @click="handleDelete(scope.$index, scope.row)">DELETE</el-button>
                    </template>
                    </el-table-column>
                </el-table>
            </el-card>
            <el-card class="footer-card">
                <el-pagination
                background
                layout="prev, pager, next"
                :total="1000">
                </el-pagination>
            </el-card>
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
          queryForm:{
            queryCondition:"",
            queryConditionType:""
          },
          tableData: [{
            date: '2016-05-02',
            name: 'Tiger',
            address: '上海市普陀区金沙江路 1518 弄'
            }, {
            date: '2016-05-04',
            name: 'Tiger',
            address: '上海市普陀区金沙江路 1517 弄'
            }, {
            date: '2016-05-01',
            name: 'Tiger',
            address: '上海市普陀区金沙江路 1519 弄'
            }, {
            date: '2016-05-03',
            name: 'Tiger',
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
<style scoped lang="scss">
.footer-card{
    text-align: center;
}
</style>
