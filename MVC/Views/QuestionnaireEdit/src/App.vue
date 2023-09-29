<template>
  <v-app id="app" style="width: 100%; padding: 3px" @keydown.ctrl.83.prevent.stop="Save"
    @keydown.ctrl.37.prevent.stop="SelectPrevQuestion" @keydown.ctrl.39.prevent.stop="SelectNextQuestion">
    <div class="modal" v-if="loadingData">
      <img class="loader-icon" :src="require('../../../wwwroot/Content/Images/loading.gif')" />
    </div>
    <!-- <vue-simple-context-menu
      :options="[]"
      :elementId="contentMenuId"
      :ref="'contextMenu'"
      @option-clicked="contextMenuClicked"
    ></vue-simple-context-menu> -->
    <v-main>
      <v-container fluid>


        <v-row>
          <v-col cols="2" style="height: 70vh; overflow: scroll"><!--v-if="mode == 'edit'"-->
            <v-row style="height: 50px">
              <v-menu class="center" offset-y="40">
                <template v-slot:activator="{ on, attrs }">
                  <v-btn height="25" v-bind="attrs" v-on="on">
                    Add Question
                  </v-btn>
                </template>
                <v-list>
                  <v-list-item v-for="(item, index) in questionTemplates" :key="index" link>
                    <v-list-item-title @click="AddQuestion(item)">{{
                      item.Name
                    }}</v-list-item-title>
                  </v-list-item>
                </v-list>
              </v-menu>
            </v-row>
            <div style="
                display: inline-flex;
                flex-direction: row;
                justify-content: flex-start;
                width: 100%;
              " v-for="(q, i) in orderedQuestions" :key="q.Id" no-gutters>
              {{ i }})
              <button class="button" :style="SetSelectedStyle(q)" title="Ask Question" @click="SelectQuestion(q)"
                @contextmenu.prevent.stop="handleContextMenu($event, q)">
                <div style="display: inline-flex">
                  <img v-if="IsQueryValid(q)" :src="require('../../../wwwroot/Content/Icons/checked.png')" width="20"
                    height="20" />
                  <img v-else :src="require('../../../wwwroot/Content/Icons/unChecked.png')" width="20" height="20" />
                  <input type="text" :readonly="mode == 'work'" v-model="q.Name" style="width: 100%; overflow: hidden" />
                </div>
              </button>
              <div></div>
            </div>
          </v-col>
          <v-col col="10">
            <v-row v-if="Questionnarie != null" class="panel" style="background-color: aquamarine;"
              :style="headerCssStyle">
              <h2 style="width: 100%;">                   <textarea v-model="Questionnarie.Text"
              style="width: 100%;"></textarea> </h2>
            </v-row>
            <!-- <div>{{currentModel.answerModel}}  </div>        <div>{{ enableNext }}  </div>     -->
            <div style="min-height:600px ;display: flex;flex-direction: column; justify-content: space-between;" :style="currentQuestionCssStyle"  class="panel">
             

              <div style="display:flex; justify-content: space-between;">

                <h3 v-if="currentQuestion != null" style="width: 100%;" >
                  <textarea v-model="currentQuestion.Text"
              style="width: 100%;"></textarea>
                </h3>
                <div class='questionInfo' v-if="this.Questionnarie.Questions != null">
                  {{ currentQuestion.Order }} of {{ this.Questionnarie.Questions.length }}
                </div>
              </div>
              <v-form ref="form" v-model="currentModel.valid">
                <v-jsf v-if="currentQuestion != null" v-model="currentModel.answerModel" :schema="currentQuestion.Schema"
                  :options="currentQuestion.Options" />
              </v-form>
              <div style="text-align: left" v-if="currentQuestion != null">
                <textarea v-model="currentQuestion.Description"
              style="width: 100%;"></textarea>
              </div>
              <v-spacer></v-spacer>
              <div style=" display: flex; width: 100% ;height:50px; padding: 3px;margin: 3px;">

                <v-btn class="buttion" @click="SelectPrevQuestion()" v-if="currentQuestion.Order > 1">
                  {{ PrevButtonText }}</v-btn>
                <v-spacer></v-spacer>
                <v-btn class="buttion" @click="SelectNextQuestion()" :disabled="!enableNext"
                  v-if="currentQuestion.Order < Questionnarie.Questions.length">
                  {{ NextButtonText }}
                </v-btn>
              </div>
            </div>
          </v-col>
        </v-row>
        <!-- <v-row>
          <fieldset style="border: inset;width:100%;">
            <legend style="width: 100px; padding-top: 2px; padding-left: 2px">
              Description
              </legend>
            <textarea v-model="currentQuestion.Description"
              style="width: 100%;"></textarea>
            </fieldset>
        </v-row> -->
        <v-row>
          <v-col cols="6">
            <div style="border: inset;width:100%;display: inline-flex; ">
              <div style="width:100%;">
                Previous question button text:
              </div>
              <input style="width:100%;" v-model="currentQuestion.PrevButtonText"  />
            </div>
          </v-col>
          <v-col cols="6">
            <div style="border: inset;width:100%;display: inline-flex; ">
              <div style="width:100%;">
                Next question button text:
              </div>
              <input style="width:100%;" v-model="currentQuestion.NextButtonText"  />
            </div>
          </v-col>

        </v-row>
        <v-row>
          <v-col cols="6">
            <fieldset style="border: inset;width:100%; height: 300px;">
              <legend style="width: 100px; padding-top: 2px; padding-left: 2px">
                schema
              </legend>
              <prism-editor v-model="schemaStr" :highlight="highlighter" />

              <div class="errorV">
                {{ notValidSchemaError }}
              </div>
            </fieldset>
          </v-col>
          <v-col cols="6">
            <fieldset style="border: inset;width:100%; height: 300px;">
              <legend style="width: 100px; padding-top: 2px; padding-left: 2px">
                options
              </legend>

              <prism-editor v-model="optionsStr" :highlight="highlighter" />
              <div class="errorV">
                {{ notValidOptionsError }}
              </div>
            </fieldset>
          </v-col>

        </v-row>
        <v-row>
        
             <fieldset style="border: inset;width:100%; height: 300px;">
              <legend style="width: 100px; padding-top: 2px; padding-left: 2px">
                 currentQuestion.CssStyle
              </legend>
              <prism-editor v-model="styleStr" :highlight="highlighter" />

              <div class="errorV">
                {{ notValidCssError }}
              </div>
            </fieldset> 
         
        </v-row>        
      </v-container>
      <v-footer fixed class="d-flex justify-end">
        <v-btn height="25" @click="SaveQuestionnaire()" style="min-width: 130px">Save Questionnaire</v-btn>
        <v-btn @click="CloseThis()" height="25" style="min-width: 130px">Cancel</v-btn>
        <div style="width: 30px" />
      </v-footer>
    </v-main>
  </v-app>
</template>

<script>
// if (Id == undefined) {
//   var Id = 48
//   var SessionId = 'xxx'
// }


import { data, radioOptions, radioShema, emptyOptions, stringShema, dateShema, numShema, radioItem, stringItem, dateItem, numItem } from "./data.js";
import { baseMixin } from "./BaseMixin.js";
//import VueSimpleContextMenu from "vue-simple-context-menu";
import VJsf from '@koumoul/vjsf/lib/VJsf.js'
import '@koumoul/vjsf/lib/VJsf.css'
import '@koumoul/vjsf/lib/deps/third-party.js'
import { PrismEditor } from 'vue-prism-editor';
import 'vue-prism-editor/dist/prismeditor.min.css'; // import the styles somewhere

// import highlighting library (you can use any library you want just return html string)
import { highlight, languages } from 'prismjs/components/prism-core';
import 'prismjs/components/prism-clike';
import 'prismjs/components/prism-javascript';
import 'prismjs/themes/prism-tomorrow.css'; //

export default {
  name: 'App',
  mixins: [baseMixin],
  components: {
    VJsf,
    PrismEditor,
    // VueSimpleContextMenu,
  },
  data: () => ({
    H: null,
    valid: false,
    contentMenuId: 1,
    currentModel: { answerModel: {}, valid: false },
    mode: "edit",
    models: [],
    Questionnarie: { Questions: [] },
    notValidCss: "",
    notValidCssError: "",    
    notValidSchemaError: "",
    notValidSchema: "",
    notValidSchemaError: "",
    notValidOptions: "",
    notValidOptionsError: "",
    currentQuestion: {
      Id: 0,
      Name: null,
      Options: radioOptions,
      Schema: radioShema,
      CssStyle:{'background-color': '#EE0'}
    },
    session: SessionId,
    questionTemplates: [
      { Id: 0, Name: "radio", Options: radioOptions, Schema: radioShema },
      { Id: 0, Name: "string", Options: emptyOptions, Schema: stringShema },
      { Id: 0, Name: "date", Options: emptyOptions, Schema: dateShema },
      { Id: 0, Name: "number", Options: emptyOptions, Schema: numShema },
    ],
    questionItemTemplates: [
      { Name: "radio", Item: radioItem },
      { Name: "string", Item: stringItem },
      { Name: "date", Item: dateItem },
      { Name: "number", Item: numItem },
    ],
    newItemName: "",
    newItemText: "",
  }),
  computed: {
    orderedQuestions() {
      if(this.Questionnarie)
      return this.Questionnarie.Questions.sort((a, b) => a.Order - b.Order);
    },
    optionsStr: {
      get() {
        if (this.notValidOptions) {
          return this.notValidOptions;
        }
        return JSON.stringify(this.currentQuestion.Options, null, "\t");
      },
      set(value) {
        try {
          console.log(value);
          this.currentQuestion.Options = JSON.parse(value);
          this.notValidOptions = null;
          this.notValidOptionsError = null;
        } catch (ex) {
          this.notValidOptions = value;
          this.notValidOptionsError = ex;
        }
      },
    },
    schemaStr: {
      get() {
        //    if (this.notValidSchema) return this.notValidSchema;
        return JSON.stringify(this.currentQuestion.Schema, null, "\t");
      },
      set(value) {
        try {
          this.currentQuestion.Schema = JSON.parse(value);
          this.notValidSchema = null;
          this.notValidSchemaError = null;
        } catch (ex) {
          this.notValidSchema = value;
          this.notValidSchemaError = ex;
        }
      },
    },
    styleStr: {
      get() {
           if (this.notValidCss) return this.notValidCss;
        return JSON.stringify(this.currentQuestion.CssStyle, null, "\t");
      },
      set(value) {
        try {
          console.log(value)
          this.currentQuestion.CssStyle = JSON.parse(value);
          this.notValidCss = null;
          this.notValidCssError = null;
        } catch (ex) {
          this.notValidCss = value;
          this.notValidCssError = ex;
        }
      },
    },

    enableNext() {

      return this.currentModel.valid != false && Object.keys(this.currentModel.answerModel).length > 0
    },
    PrevButtonText() {
      return this.currentQuestion.PrevButtonText ? this.currentQuestion.PrevButtonText : "Prev"
    },
    NextButtonText() {
      return this.currentQuestion.NextButtonText ? this.currentQuestion.NextButtonText : "NEXT"
    },

    currentQuestionCssStyle() {
      if (this.currentQuestion.CssStyle) {
        console.log(this.currentQuestion.CssStyle)
        return this.currentQuestion.CssStyle;
      }
    },
    headerCssStyle() {

      if (this.Questionnarie.CssStyle) {
        return this.Questionnarie.CssStyle;
      }
    }

  },

  methods: {
    validateForm() {
      this.$refs.form.validate();
    },
    handleContextMenu(event, item) {
      console.log("handleContextMenu");
      this.$refs.contextMenu.options = this.GetContextMenuItems(item);
      this.$refs.contextMenu.showMenu(event, item);
    },
    // getting a menu depending on the item that is selected clear,showSamples,addSamples,addChaild,rename ,delete
    GetContextMenuItems(item) {
      var items = [];

      if (this.mode == "edit") {
        items.push({ name: "Save question", slug: "save" });
        items.push({ name: "Move up", slug: "up" });
        items.push({ name: "Move down", slug: "down" });
        items.push({ name: "Delete question", slug: "delete" });
        items.push({ name: 'Switch OFF "Edit questions mode"', slug: "work" });
      } else {
        items.push({ name: 'Switch to "Edit questions mode"', slug: "edit" });
      }
      return items;
    },
    contextMenuClicked(event) {
      switch (event.option.slug) {
        case "up":
          this.move("up", event.item);
          break;
        case "down":
          this.move("down", event.item);
          break;
        case "edit":
          this.mode = "edit";
          break;
        case "save":
          this.SaveQuestion(event.item);
          break;
        case "work":
          this.mode = "work";
          break;
        case "delete":
          const DeleteQuestion = this.DeleteQuestion;
          dxConfirm('Are you sure to delete "' + event.item.Name + '"').success(
            function () {
              DeleteQuestion(event.item);
            }
          );
          break;
      }
    },
    move(dir, node) {
      let findNum;
      if (dir == "up") {
        findNum = node.Num - 1;
      } else {
        findNum = node.Num + 1;
      }
      let pairNode = this.orderedQuestions.find((x) => x.Num == findNum);
      if (pairNode) {
        pairNode.Num = node.Num;
        this.SaveQuestion(pairNode);
        node.Num = findNum;
        this.SaveQuestion(node);
      }
    },
    IsQueryValid(question) {

      if (this.patient && this.patient.id > 0) {
        let model = this.models.find((x) => x.QuestionId == question.Id);

        return model && model.valid;

      }

      return false;
    },
    AddQuestion(question) {
      this.addedCouner--;
      question.Id = this.addedCouner; //Сѓ РєР°Р¶РґРѕРіРѕ РґРѕР±Р°РІР»РµРЅРЅРѕРіРѕ РІРѕРїСЂРѕСЃР° РїРѕРєР° РѕРЅ РЅРµ СЃРѕС…СЂР°РЅРµРЅ id РѕС‚СЂРёС†Р°С‚РµР»СЊРЅС‹Р№
      if (this.currentQuestion.Num) {
        //
        question.Num = this.currentQuestion.Num + 1;

        var filtred = this.Questionnarie.Questions.filter(
          (x) => x.Num > this.currentQuestion.Num
        );
        for (const key in filtred) {
          const question = filtred[key];
          question.Num++;
        }
      }

      this.Questionnarie.Questions.push(question);
      console.log(question);
      this.SelectQuestion(question);
    },
    AddItemInQuestion(item) {
      item.Item.title = this.newItemText;

      this.currentQuestion.Schema.properties._new_Name_ = item.Item;

      let schStr = JSON.stringify(this.currentQuestion.Schema);
      schStr = schStr.replace("_new_Name_", this.newItemName);

      this.schemaStr = schStr;
    },






    SetSelectedStyle(question) {
      if (this.currentQuestion.Id == question.Id) {
        return { background: "#ccc" };
      }
    },
    SelectQuestion(question) {
    try{
      if (this.currentQuestion && this.currentQuestion.Id == question.Id) return; //если это повторный вызов
      if (question.Id == 0) return; //если это фейк  currentQuestion нужный для инициализации Vue

      let prmodel = this.models.find(
        (x) => x.QuestionId == this.currentQuestion.Id
      );
      if (prmodel) {
        // prmodel.answerModel=this.currentModel;
        prmodel = this.currentModel;
      }

      this.currentQuestion = question;
      this.notValidOptions = null;
      this.notValidSchema = null;
      this.SetModel();
      this.PlayOk()
    }
    catch(ex){
     console.error(ex)
    }
    },
    SelectNextQuestion() {
      const nextQuestion = this.Questionnarie.Questions.find(
        (x) => x.Order == this.currentQuestion.Order + 1
      );
      if (nextQuestion) {
        this.SelectQuestion(nextQuestion);
      }
      console.log(nextQuestion)
      this.SaveAnsver()
    },
    SelectPrevQuestion() {

      const prevQuestion = this.Questionnarie.Questions.find(
        (x) => x.Order == this.currentQuestion.Order - 1
      );
      if (prevQuestion) {
        this.SelectQuestion(prevQuestion);
      }
      console.log(prevQuestion)
    },
    SetModel() {
      let model = this.models.find(
        (x) => x.QuestionId == this.currentQuestion.Id
      );
      if (!model) {
        model = {
          QuestionId: this.currentQuestion.Id,
          answerModel: {},
          valid: null,
        };
        this.models.push(model);
      }
      this.currentModel = model;
    },
    Save() {
      this.SaveQuestionnaire()

    },
    SaveQuestion() {
      this.fetch(
        this.ok,
        "QuestionnaireEdit/SaveQuestion?questionnaireId=" + Id,
        this.currentQuestion
      );
    },
    SaveQuestionnaire() {
      console.log('save')
      this.fetch(
        this.CloseThis,
        "QuestionnaireEdit/SaveQuestionnaire?questionnaireId=" + Id,
        this.Questionnarie
      );
    },
    DeleteQuestion(question) {
      if (question.Id <= 0) {
        for (var i = 0; i < this.Questionnarie.Questions.length; i++) {
          if (arr[i].Id === question.Id) {
            this.Questionnarie.Questions.splice(i, 1);
            break;
          }
        }
      } else {
        this.fetch(
          this.GetQuestions,
          "/Questions/Questionnaire/DeleteQuestion?questionnaireId=" +
          Id +
          "&questionId=" +
          question.Id
        );
      }
    },



    ok(item) {
      if (item.Errors && item.Errors.length > 0) {
        this.ShowErrors(item);
      }
    },
    GetQuestions() {
      console.log('SetQuestions(val)')
      this.fetch(
        this.SetQuestions,
        "Questionnaire/Get?id=" + Id
      );
    },
    SetQuestions(val) {
      console.log(val)
      if (val.Errors) {
        this.ShowErrors(val);
      }
      if (val.Item) {
        this.Questionnarie = val.Item; // new QuestionnarieBo (val.Item);
        if (this.Questionnarie.Questions.length > 0) {
          this.SelectQuestion(this.Questionnarie.Questions[0]);
        }
      }
    },

    SaveAnsver() {
      return ;
      this.fetch(
        this.ok,
        `Questionnaire/SaveAnsvers?questionnaireId={Id} +'&sessionId= + {this.session}`,
        this.models
      );
    },
    highlighter(code) {
      return highlight(code, languages.js); // languages.<insert language> to return html with markup
    },
  },
  mounted: function () {
    console.log(Id);
     if (Id) {
      this.GetQuestions();
    // this.SetQuestions(data);
     }
  },
};
</script>
<style>
html,
body {
  height: 100%;

}

.questionInfo {
  width: 60px;
  min-width: 60px;
  height: 35px;
  background-color: green;
  border-radius: 25px;
  padding: 5px;
  margin: 5px;
}

.buttion {
  width: 60px;
  border-radius: 25px;
  background-color: green !important;
  margin: 3px
}

.panel {
  font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
  border: outset;
  border-radius: 15px;
  padding: 15px;
  margin: 5px;
  background-color: rgb(247, 247, 247)
}

.vue-simple-context-menu {
  top: 0;
  left: 0;
  margin: 0, 0;
  padding: 0, 0;
  display: none;
  list-style: none;
  position: absolute;
  z-index: 1000000;
  background-color: rgb(202, 210, 21);
  border-bottom-width: 0px;
  font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", "Roboto", "Oxygen",
    "Ubuntu", "Cantarell", "Fira Sans", "Droid Sans", "Helvetica Neue",
    sans-serif;
  box-shadow: 10px 3px 6px 0 rgba(0, 0, 0, 0.4);
  border-radius: 2px;
}

.vue-simple-context-menu--active {
  display: block;
}

.vue-simple-context-menu__item {
  display: flex;
  color: black;
  height: 35px;
  cursor: pointer;
  padding: 3px, 6px;
  margin-left: 5px;
  margin-right: 5px;
  align-items: center;
}

.vue-simple-context-menu__item:hover {
  background-color: #ecf0f1;
}

/*	color: white;*/

.vue-simple-context-menu__divider {
  box-sizing: content-box;
  height: 2px;
  background-color: gray;
  padding: 4px 0;
  background-clip: content-box;
  pointer-events: none;
}

input {
  border-color: gray;
  border-style: solid;
  border-width: 1px;
}

.inputStyle {
  height: 20px;
  border-color: gray;
  border-style: solid;
  border-width: 1px;
  width: 80%;
}

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
