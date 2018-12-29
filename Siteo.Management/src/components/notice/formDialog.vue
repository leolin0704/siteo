<template>
<baseDialog :mode="mode" name="Notice" :visible="visible" @close="handleClose" @open="handleOpen" @opened="handleOpened" @save="handleSave">
    <el-form ref="noticeForm" :validate-on-rule-change="false" :rules="rules" :model="noticeModel">
        <el-form-item label="Title" prop="Title" :label-width="formLabelWidth">
            <el-input id="txtNoticeTitle" :disabled="mode === 'view'" v-model="noticeModel.Title" autocomplete="off"></el-input>
        </el-form-item>
        <!-- <el-form-item label="Content" prop="Content" :label-width="formLabelWidth">
            <el-input id="txtNoticeContent"
                type="textarea"
                :rows="6"
                :disabled="mode === 'view'" 
                v-model="noticeModel.Content" 
                autocomplete="off"></el-input>
        </el-form-item> -->
        <el-form-item label="Content" prop="Content" :label-width="formLabelWidth">
            <simpleRichEditor
                ref="editor"
                v-model="noticeModel.Content"
                :disabled="mode === 'view'"
            ></simpleRichEditor>
        </el-form-item>
    </el-form>
</baseDialog>
</template>

<script>
import baseDialog from "../common/baseDialog";
import simpleRichEditor from "../common/simpleRichEditor";

export default {
    name: "ViewNotice",
    components: {
        baseDialog,
        simpleRichEditor
    },
    props: {
        visible: {
            type: Boolean,
            default: false
        },
        model: {
            type: Object,
            default: null
        },
        mode: {
            type: String,
            default: "add"
        }
    },
    data() {
        const validateContent = (rule, value, callback) => {
            const length = value.length;
            if(length < 2 || length >4000){
                callback(new Error('Content length should between 2 to 4000.'));
                return;
            }

            const text = value.replace(/<[^>]+>/g,"").trim();
            if (text === '') {
                callback(new Error('Content is required.'));
            } else {
                callback();
            }
        };


        return {
            noticeModel: {},
            rules:{
                Title: [
                    { required: true, message: 'Title is required.', trigger: 'blur' },
                    { min: 2, max: 200, message: 'Title length should between 2 to 200.', trigger: 'blur' }
                ],
                Content: [
                    { required: true, message: 'Content is required.', trigger: 'blur' },
                    { validator: validateContent, trigger: 'blur' }
                ],
            },
            formLabelWidth: "100px"
        }
    },
    mounted() {

    },
    methods: {
        loadData() {
            if (this.mode === "edit" || this.mode === "view") {
                return window.axiosGet("/NoticeApi/Get", {
                    id: this.model.ID
                }).then(response => {
                    if (response.Status == 1) {
                        this.noticeModel = response.Data.Data;
                    } else {
                        this.noticeModel = {};
                    }

                    this.$nextTick(() => this.$refs.editor.reload());
                });
            } else {
                return Promise.resolve();
            }
        },
        handleSave() {
            this.$refs["noticeForm"].validate((valid) => {
                if(valid){
                    if (this.mode === "add") {
                        window.axiosPost("/NoticeApi/Add", this.noticeModel).then(response => {
                            if (response.Status == 1) {                       
                                this.handleClose(true);
                            }
                        });
                    } else if (this.mode === "edit") {
                        window.axiosPost("/NoticeApi/edit", this.noticeModel).then(response => {
                            if (response.Status == 1) {
                                this.handleClose(true);
                            }
                        });
                    }
                }
            })

        },
        handleOpen() {
            this.noticeModel = {
                Title: "",
                Content:""
            };

            this.$nextTick(() => this.$refs.editor.reload());
        },
        handleOpened() {
            let loading = this.$loading({
                lock: true,
                fullscreen: true
            });

            this.loadData().then(()=>{
                loading.close();
            })
        },
        handleClose(saved = false) {
            this.$emit("close", saved);
        }
    }
};
</script>

<style scoped>
</style>
