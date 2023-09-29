<script >
import { data} from './data.js'

// import HighCode from 'vue-highlight-code'
// import 'vue-highlight-code/dist/style.css'
import { component as VueCodeHighlight } from 'vue-code-highlight';
import 'vue-code-highlight/themes/window.css'
import 'prism-es6/components/prism-markup-templating';
import 'prism-es6/components/prism-json';
export default {
  name: 'App',
  components: {

    VueCodeHighlight,
   // VueSimpleContextMenu,
  },
  data: () => ({ 
value2 : `#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
.wrapper {
  display: flex;
  flex-direction: column;
}
.code {
  /* margin: 20px; */
}
`,
 dataJson : JSON.stringify(data, null, "\t"),
//H : this.$refs(null)
}),
}
</script>
<template>
  <div class="wrapper">
   
    <vue-code-highlight language="json" >
    <code>
 {{dataJson}}
  </code>
    </vue-code-highlight>

  </div>
</template>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
.wrapper {
  display: flex;
  flex-direction: column;
}
.code {
  margin: 20px;
}
</style>
