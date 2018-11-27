<template>
    <el-table
        :id="tableId"
        :data="data"
        stripe
        @selection-change="handleSelectionChange"
        style="width: 100%">
        <el-table-column
        type="selection"
        width="55">
        </el-table-column>
        <slot></slot>
        <el-table-column
        label="Action"
        width="180"
        >
        <template slot-scope="scope">
            <el-button :at-key="action.name" v-for="action in actions" :key="action.name"
            size="mini"
            :icon="action.icon"
            :type="action.type"
            @click="handleAction(action.name,scope.$index, scope.row)"></el-button>
        </template>
        </el-table-column>
    </el-table>
</template>
<script>
export default {
  name: "baseTable",
  props: {
    tableId:{
        type:String,
        default:""
    },
    data: {
      type: Array,
      default: () => []
    },
    actions:{
        type:Array,
        default: () => [
            {
                icon:"el-icon-search",
                name:"view"
            },
            {
                icon:"el-icon-edit",
                name:"edit",
                type:"primary"
            },
            {
                icon:"el-icon-delete",
                name:"delete",
                type:"danger"
            },
        ]
    }
  },
  data() {
    return {};
  },
  components: {},
  methods: {
    handleSelectionChange(vals) {
      this.$emit("selection-change", vals);
    },
    handleAction(name, index, row) {
      this.$emit("row-action", {name, index, row});
    }
  }
};
</script>
<style scoped lang="scss">
</style>
