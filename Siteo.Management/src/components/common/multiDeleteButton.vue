<template>
    <el-button :disabled="!this.selectedRows || this.selectedRows.length ===0" @click="handleMultiDelete">DELETE</el-button>
</template>

<script>
export default {
  name: "multiDeleteButton",
  props:{
      selectedRows:{
          type:Array,
          default:[]
      }
  },
  methods:{
    handleMultiDelete(){
        this.$confirm('Are you sure?', 'Prompt', {
            confirmButtonText: 'OK',
            cancelButtonText: 'CANCEL',
            type: 'warning'
        }).then(() => {
            this.$emit("delete",this.selectedRows);
        }).catch(() => {
            this.$emit("cancel",this.selectedRows);        
        });
    }
  }
};
</script>

<style lang="scss">
</style>
