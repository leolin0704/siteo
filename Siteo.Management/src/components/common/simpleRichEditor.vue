<template>
  <div class="editor">
    <div class="editor__content" v-if="disabled" v-html="decodeValue">
    </div>
    <editor-menu-bar v-if="!disabled" :editor="editor">
      <div class="editor__menubar" slot-scope="{ commands, isActive }">
        <button
          class="editor__menubar__button"
          :class="{ 'is-active': isActive.bold() }"
          @click="commands.bold"
        >
          <i class="icon iconfont icon-bold"></i>
        </button>
        <button
          class="editor__menubar__button"
          :class="{ 'is-active': isActive.italic() }"
          @click="commands.italic"
        >
          <i class="icon iconfont icon-italic"></i>
        </button>

        <button
          class="editor__menubar__button"
          :class="{ 'is-active': isActive.strike() }"
          @click="commands.strike"
        >
          <i class="icon iconfont icon-strike"></i>
        </button>
        <button
          class="editor__menubar__button"
          :class="{ 'is-active': isActive.underline() }"
          @click="commands.underline"
        >
          <i class="icon iconfont icon-underline"></i>
        </button>
        |
        <button
          class="editor__menubar__button"
          :class="{ 'is-active': isActive.bullet_list() }"
          @click="commands.bullet_list"
        >
          <i class="icon iconfont icon-unorderedlist"></i>
        </button>
        <button
          class="editor__menubar__button"
          :class="{ 'is-active': isActive.ordered_list() }"
          @click="commands.ordered_list"
        >
          <i class="icon iconfont icon-orderedlist"></i>
        </button>
        |
        <button
          class="editor__menubar__button"
          :class="{ 'is-active': isActive.heading({ level: 1 }) }"
          @click="commands.heading({ level: 1 })"
        >
          H1
        </button>
        <button
          class="editor__menubar__button"
          :class="{ 'is-active': isActive.heading({ level: 2 }) }"
          @click="commands.heading({ level: 2 })"
        >
          H2
        </button>
        <button
          class="editor__menubar__button"
          :class="{ 'is-active': isActive.heading({ level: 3 }) }"
          @click="commands.heading({ level: 3 })"
        >
          H3
        </button>
        |
        <button
          class="editor__menubar__button"
          :class="{ 'is-active': isActive.paragraph() }"
          @click="commands.paragraph"
        >
          <i class="icon iconfont icon-clearformat"></i>
        </button>
        <button
          class="editor__menubar__button"
          @click="commands.undo"
        >
          <i class="icon iconfont icon-undo"></i>
        </button>
        <button
          class="editor__menubar__button"
          @click="commands.redo"
        >
          <i class="icon iconfont icon-redo"></i>
        </button>
      </div>
    </editor-menu-bar>
    <editor-content v-if="!disabled" class="editor__content" :editor="editor" />
  </div>
</template>

<script>
import { Editor, EditorContent, EditorMenuBar } from 'tiptap';
import { HTMLDecode } from "../../util/commonUtil";

import {
  Blockquote,
  CodeBlock,
  HardBreak,
  Heading,
  OrderedList,
  BulletList,
  ListItem,
  TodoItem,
  TodoList,
  Bold,
  Code,
  Italic,
  Link,
  Strike,
  Underline,
  History,
} from 'tiptap-extensions'

export default {
  components: {
    EditorContent,
    EditorMenuBar,
  },
  props: {
    value: {
      type: String,
      default:""
    },
    disabled:{
      type:Boolean,
      default:false
    }
  },
  computed:{
    decodeValue(){
      return HTMLDecode(this.value);
    }
  },
  methods:{
      reload(){
        this.editor.setContent(HTMLDecode(this.value));
      }
  },
  data() {
    const me = this;
    return {
      editor: new Editor({
        onUpdate(e){
            me.$emit("input", e.getHTML());
        },
        extensions: [
          new Blockquote(),
          new BulletList(),
          new CodeBlock(),
          new HardBreak(),
          new Heading({ levels: [1, 2, 3] }),
          new ListItem(),
          new OrderedList(),
          new TodoItem(),
          new TodoList(),
          new Bold(),
          new Code(),
          new Italic(),
          new Link(),
          new Strike(),
          new Underline(),
          new History(),
        ],
        content: HTMLDecode(me.value),
      }),
    }
  },
  beforeDestroy() {
    this.editor.destroy()
  },
}
</script>
<style scoped lang="scss">
  .editor{
    border: 1px solid #dcdfe6;
    border-radius:4px;

    .editor__menubar{
      background-color: #dcdfe6;

      .editor__menubar__button{
        border:solid 1px #ccc;
        border-radius: 4px;
        margin:0 2px;
        padding:0 5px;
        min-width:22px;
        line-height:22px;
        background-color: #fff;

        .icon{
          font-size:14px;
        }
      }
    }

    .editor__content{
      padding:2px 8px;
      min-height:3em;
    }
  }
</style>