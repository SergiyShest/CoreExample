<template>
 <v-app id="app" style="width: 100%; padding: 0%" 
  @keydown.ctrl.83.prevent.stop="Save" 
  @keydown.ctrl.37.prevent.stop="SelectPrevQuestion" 
  @keydown.ctrl.39.prevent.stop="SelectNextQuestion" 
  >
    <div class="modal" v-if="loadingData">
      <img
        class="loader-icon"
        :src="require('../../../wwwroot/Content/Images/loading.gif')"
      />
    </div>
    <v-main>
 <v-form
                ref="form"
                v-model="currentModel.valid"
                style="width: 100%; padding: 5px"
              >
                <div style="text-align: center" v-if="currentQuestion != null">
                  {{ currentQuestion.Name }} ({{ currentQuestion.Id }})
                </div>
                <v-jsf
                  v-if="currentQuestion != null"
                  v-model="currentModel.answerModel"
                  :schema="currentQuestion.Schema"
                  :options="currentQuestion.Options"
                />
              </v-form>
      <p>valid=</p>
      <pre></pre>
    </v-main>
  </v-app>
</template>

<script>
import { baseMixin } from "./BaseMixin.js";
import VJsf from '@koumoul/vjsf/lib/VJsf.js'
import '@koumoul/vjsf/lib/VJsf.css'
import '@koumoul/vjsf/lib/deps/third-party.js'
const radioOptions = {
  context: {
    Items: [
      { Id: 1, Name: "item 1" },
      { Id: 2, Name: "item 2" },
      { Id: 3, Name: "item 3" },
      { Id: 4, Name: "item 4" },
    ],
  },
  selectAll: true,
};
const radioShema = {
  type: "object",
  requred: ["ListProp"],
  properties: {
    ListProp: {
      type: "number",
      title: "Select",
      "x-display": "radio",
      "x-fromData": "context.Items",
      "x-itemKey": "Id",
      "x-itemTitle": "Name",
      description: "",
    },
  },
};

export default {
  name: 'App',
  mixins: [baseMixin],
  components: { VJsf 
},
  data: () => ({
    valid: false,
    currentModel: { answerModel: {}, valid: false },
    schema: {
      type: 'object',
      properties: {
        stringProp: { type: 'string' },
        colorProp: { type: 'string', 'x-display': 'color-picker' },
      }
    },
    currentQuestion: {
      Id: 0,
      ParentId: 1,
      Name: null,
      Options: radioOptions,
      Schema: radioShema,
    },

  }),
};
</script>