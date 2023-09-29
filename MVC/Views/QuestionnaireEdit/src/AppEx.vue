<script >
import { data} from './data.js'

// import HighCode from 'vue-highlight-code'
// import 'vue-highlight-code/dist/style.css'
import HighCode from './components/HighCode.vue'
// import { HighCode } from 'vue-highlight-code'
// import 'vue-highlight-code/dist/style.css'
export default {
  name: 'App',
  components: {

    HighCode,
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
    <div ref="d"></div>
    <HighCode
      ref="H"
  
      :codeValue="dataJson"
      :theme="dark"
      :fontSize="fontSize"
      :nameShow="false"
      :copy="true"
      :height="height"
      :width="width"
      :textEditor="true"
    ></HighCode>
    <HighCode
      ref="H"
      class="code"
      :codeValue="value2"
      :theme="dark"
      :fontSize="fontSize"
      :nameShow="false"
      :copy="false"
      :height="height"
      :width="width"
      :readOnly="boolfalse"
    ></HighCode>
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
