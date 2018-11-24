<template>
<el-dialog :title="getTitle()" width="70%" append-to-body :visible="visible" @close="handleClose" @opened="handleOpened" @open="handleOpen">
    <slot></slot>
    <div slot="footer" class="dialog-footer">
        <slot name="button">
            <el-button v-if="mode !== 'view'" type="primary" @click="handleSave">SAVE</el-button>
            <el-button @click="handleClose">CANCEL</el-button>
        </slot>
    </div>
</el-dialog>
</template>

<script>
export default {
    name: "baseDialog",
    props: {
        visible: {
            type: Boolean,
            default: false
        },
        mode: {
            type: String,
            default: "add"
        },
        name: {
            type: String,
            default: "Result"
        }
    },
    data() {
        return {
            roleModel: {
                Name: "admin",
                Permissions: [1, 2]
            },
            Permissions: [{
                ID: 1,
                Name: "Content"
            }, {
                ID: 2,
                Name: "Basic Info"
            }, {
                ID: 3,
                Name: "System Manage"
            }],
            formLabelWidth: "100px"
        }
    },
    components: {},
    mounted() {

    },
    methods: {
        getTitle() {
            if (this.mode == "add") {
                return "Add " + this.name;
            }

            if (this.mode == "edit") {
                return "Edit " + this.name;
            }

            return "View " + this.name;
        },
        handleSave() {
            //TODO
            this.$emit("save");
        },
        handleOpened() {
            this.$emit("opened");
        },
        handleOpen() {
            this.$emit("open");
        },
        handleClose() {
            this.$emit("close");
        }
    }
};
</script>

<style scoped>
</style>
