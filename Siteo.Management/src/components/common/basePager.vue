<template>
    <el-pagination
        at-key="basePager"
        background
        :page-size="pageSize"
        :current-page="currentPage"
        layout="prev, pager, next"
        :total="totalCount"
        @current-change="handleChange"
        @prev-click="handleChange"
        @next-click="handleChange"
        >
    </el-pagination>
</template>
<script>
export default {
  name: "baseTable",
  props: {
    totalCount: {
      type: Number,
      default: 0
    },
    pageSize:{
      type: Number,
      default: 20
    },
    currentPage:{
      type: Number,
      default: 1
    }
  },
  data() {
    return {
    };
  },
  components: {},
  methods: {
    handleChange(currentPage) {
      this.$emit("page-change", {
        currentPage,
        pageSize:this.pageSize
      });
    }
  }
};
</script>
<style scoped lang="scss">
</style>
