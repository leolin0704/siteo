<template>
  <tableLayout>
    <template slot="query">
      <baseQuery @query="loadData" @reset="loadData" :conditionTypes="query.conditionTypes"></baseQuery>
    </template>
    <template slot="button">
        <el-button at-key="btnAdd" type="primary" @click="handleAdd">ADD</el-button>
        <multiDeleteButton :selectedRows="selectedRows" @delete="handleMultiDeleteConfirm" ></multiDeleteButton>
    </template>
    <template slot="table">
        <pageDialog
          :visible="dialog.viewVisible"
          :mode="dialog.mode"
          :model="dialog.viewModel"
          @close="handleDialogClose"
        ></pageDialog>
        <pageTable
          :data="result.list"
          @selection-change="handleSelectionChange"
          @row-action="handleAction"
        >
        </pageTable>
    </template>
    <template slot="pager">
      <basePager :totalCount="result.totalCount" :currentPage="query.currentPage" :pageSize="query.pageSize" @page-change="loadData"></basePager>
    </template>
  </tableLayout>
</template>

<script>
import tableLayout from "../components/common/tableLayout";
import multiDeleteButton from "../components/common/multiDeleteButton";
import basePager from "../components/common/basePager";
import baseQuery from "../components/common/baseQuery";

import { get } from "../util/apiUtil.js";
export default {
  name: "role",
  data() {
    return {
      query:{
        api:"",
        currentPage:1,
        pageSize:20,
        keywords:"",
        conditionTypes:[]
      },
      result:{
        list: [],
        totalCount:0
      },
      dialog:{
        viewVisible:false,
        viewModel:{},
        mode:"add"
      },
      selectedRows:[]
    };
  },
  components: {
    tableLayout,
    pageDialog:null,
    multiDeleteButton,
    pageTable:null,
    basePager,
    baseQuery,
    test:null
  },
  mounted() {
    this.loadData();
  },
  methods:{
    loadData(params){
      if(params){
        const {currentPage, pageSize, keywords, searchType} = params;
        if(currentPage !== undefined){
          this.query.currentPage = currentPage;
        }
        if(keywords !== undefined){
          this.query.keywords = keywords;
        }
        if(pageSize !==  undefined){
          this.query.pageSize = pageSize;
        }
        if(searchType !==  undefined){
          this.query.searchType = searchType;
        }
      }

      let loading = this.$loading({
        lock:true,
        fullscreen:true
      });
      get(this.query.api, {
        pageSize: this.query.pageSize,
        pageIndex: this.query.currentPage,
        keywords: this.query.keywords,
        searchType:this.query.searchType
      }).then(response => {
        if (response.Status == 1) {
          this.result.list = response.Data.List;
          this.result.totalCount = response.Data.TotalCount;
        }else{
          this.result.list = [];
          this.result.totalCount = 0;
        }

        loading.close();
      });
    },
    refreshData(){
      this.loadData({
        currentPage:1
      });
    },
    handleAdd(){
        this.dialog.mode = "add";
        this.dialog.viewModel = null;
        this.dialog.viewVisible = true;
    },
    handleDialogClose(saved){
      if(saved){
        this.refreshData();
      }
      this.dialog.viewModel = null;
      this.dialog.viewVisible = false;
    },
    handleSelectionChange(val){
      this.selectedRows = val;
    },
    handleDeleteConfirm(index, row){
      console.log(index, row,"Row deleted");
      //need implements.
    },
    handleMultiDeleteConfirm(selectedRows){
      console.log(selectedRows, "Row deleted");
      //need implements.
    },
    handleAction({name, index, row}){
      if(name === "delete"){
        this.$confirm('Are you sure?', 'Prompt', {
            confirmButtonText: 'OK',
            cancelButtonText: 'CANCEL',
            type: 'warning'
        })
        .then(() => {
            this.handleDeleteConfirm(index, row);
        })
        //.catch(() => {
        //});
      }else{
        this.dialog.mode = name;
        this.dialog.viewModel = row;
        this.dialog.viewVisible = true;
      }
    }
  }
};
</script>

<style lang="scss">
</style>
