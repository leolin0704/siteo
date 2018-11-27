<template>
<el-form :inline="true" :model="queryForm">
    <el-form-item>
        <el-input at-key="txtBaseQuery" placeholder="Please input query condition..." v-model="queryForm.value" class="query-with-type">
            <el-select v-if="conditionTypes && conditionTypes.length" v-model="queryForm.type" slot="prepend" placeholder="Please choose...">
                <el-option v-for="type in conditionTypes" :key="type.Value" :label="type.Label" :value="type.Value"></el-option>
            </el-select>
            <el-button at-key="btnBaseQuery" slot="append" type="primary" @click="handleQuery">QUERY</el-button>
        </el-input>
    </el-form-item>
<el-form-item>
    <el-button at-key="btnBaseQueryReset" @click="handleReset" >RESET</el-button>
</el-form-item>
</el-form>
</template>
<script>
import mainHeader from "./mainHeader";
export default {
  name: "baseQuery",
  props:{
    conditionTypes:{
        type:Array,
        default:() => []
    }
  },
  data(){
      return {
          queryForm:{
            value:"",
            type:""
          }
      };
  },
  created(){
    var [defaultType] = this.conditionTypes;
    this.queryForm.type = defaultType;
  },
  components: {
  },
  methods:{
      handleQuery() {
            this.$emit("query", {
                keywords: this.queryForm.value,
                searchType: this.queryForm.type
            });
      },
      handleReset() {
          var [defaultType] = this.conditionTypes;

          if(this.queryForm.type === defaultType && this.queryForm.value === ""){
              return;
          }

          this.queryForm.value = "";
          this.queryForm.type = defaultType;
          this.$emit("reset", {
                keywords: this.queryForm.value,
                searchType: this.queryForm.type
            });
      }
  }
};
</script>
<style scoped lang="scss">
.query-with-type {
    .el-input__inner{
        min-width: 400px;
    }

    .el-select {
        .el-input__inner{
            min-width:200px;
        }
    }

    .el-input-group__prepend {
        background-color: #fff;
    }
}
</style>
