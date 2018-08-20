<template>
<div id="app">
    <router-view>
    </router-view>
</div>
</template>

<script>
export default {
  name: "app",
  components: {
  }
};
</script>

<style lang="scss">
html,body{
  height: 100%;
  margin:0;
  padding:0;
  font-family: "Helvetica Neue",Helvetica,"Microsoft YaHei","微软雅黑",Arial,sans-serif;
  background-color: #ddd;
}
#app {
  height:100%;
  position: relative;
}

    .el-container{
        height:100%;
        
        .el-header{
          background-color: #fff; 
        }
    }

</style>
