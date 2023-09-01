<template>
  <v-app id="app" style="width: 100%; padding: 3px" 
  @keydown.ctrl.83.prevent.stop="Save"
  @keydown.ctrl.37.prevent.stop="SelectPrevQuestion" 
  @keydown.ctrl.39.prevent.stop="SelectNextQuestion">
    <div class="modal" v-if="loadingData">
      <img class="loader-icon" 
      :src="require('../../../wwwroot/Content/Images/loading.gif')" />
    </div>
    <vue-simple-context-menu
      :options="[]"
      :elementId="contentMenuId"
      :ref="'contextMenu'"
      @option-clicked="contextMenuClicked"
    ></vue-simple-context-menu>
    <v-main>
      <v-row>
        <v-col cols="2" style="height: 70vh; overflow: scroll"><!--v-if="mode == 'edit'"-->
          <v-row style="height: 50px" >
              <v-menu class="center" offset-y="40">
                <template v-slot:activator="{ on, attrs }">
                  <v-btn height="25" v-bind="attrs" v-on="on">
                    Add Question
                  </v-btn>
                </template>
                <v-list>
                  <v-list-item
                    v-for="(item, index) in questionTemplates"
                    :key="index"
                    link
                  >
                    <v-list-item-title @click="AddQuestion(item)">{{
                      item.Name
                    }}</v-list-item-title>
                  </v-list-item>
                </v-list>
              </v-menu>
            </v-row>
            <div
              style="
                display: inline-flex;
                flex-direction: row;
                justify-content: flex-start;
                width: 100%;
              "
              v-for="(q, i) in orderedQuestions"
              :key="q.Id"
              no-gutters
            >
              {{ i }})
              <button
                class="button"
                :style="SetSelectedStyle(q)"
                title="Ask Question"
                @click="SelectQuestion(q)"
                @contextmenu.prevent.stop="handleContextMenu($event, q)"
              >
                <div style="display: inline-flex">
                  <img
                    v-if="IsQueryValid(q)"
                    :src="require('../../../wwwroot/Content/Icons/checked.png')"
                    width="20"
                    height="20"
                  />
                  <img
                    v-else
                    :src="require('../../../wwwroot/Content/Icons/unChecked.png')"
                    width="20"
                    height="20"
                  />
                  <input
                    type="text"
                    :readonly="mode == 'work'"
                    v-model="q.Name"
                    style="width: 100%; overflow: hidden"
                  />
                </div>
              </button>
              <div></div>
            </div> 
        </v-col>
        <v-col col="10">
          <v-row  v-if="Questionnarie != null " class="panel" style="background-color: aquamarine; ; ">
          <h2>  {{ Questionnarie.Name }} </h2>
          </v-row> 
           <!-- <div>{{currentModel.answerModel}}  </div>        <div>{{ enableNext }}  </div>     -->
      <div style="min-height:600px ;display: flex;flex-direction: column; justify-content: space-between;" class="panel" >

       <div style="display:flex; justify-content: space-between;">
          
          <h3  v-if="currentQuestion != null">
            {{ currentQuestion.Name }}
          </h3>
           <div  style="width:60px ;min-width: 60px; height: 35px; background-color: rgb(8, 219, 8);border-radius: 25px; padding:5px;margin: 5px;" v-if="this.Questionnarie.Questions != null">
            {{currentQuestion.Order }} of {{ this.Questionnarie.Questions.length }} 
          </div>
        </div>
        <v-form ref="form" v-model="currentModel.valid" >
          <v-jsf v-if="currentQuestion != null" 
          v-model="currentModel.answerModel" 
          :schema="currentQuestion.Schema"
          :options="currentQuestion.Options" />
        </v-form>
        <div style="text-align: left" v-if="currentQuestion != null">
            {{ currentQuestion.Description }}
        </div> 
        <v-spacer></v-spacer>               
        <div style=" display: flex; width: 100% ;height:50px; padding: 3px;margin: 3px;">
          
          <v-btn  class="buttion" @click="SelectPrevQuestion()" v-if="currentQuestion.Order > 1">
            Prev</v-btn>
         <v-spacer></v-spacer> 
          <v-btn class="buttion" @click="SelectNextQuestion()" :disabled="!enableNext" v-if="currentQuestion.Order < Questionnarie.Questions.length
            ">
            Next
         </v-btn> <!-- <img :src="require('../../../wwwroot/Content/Icons/next.png')" width="20" height="20" /> -->
        </div>
      </div>
    </v-col>
    </v-row>
    <v-footer fixed class="d-flex justify-end">
        <v-btn
          height="25"
          @click="SaveAnsver()"
          :disabled="!(patient.id > 0)"
          v-if="mode == 'work'"
        >
          Save Ansvers
        </v-btn>
        <v-btn
          height="25"
          @click="SaveQuestionnaire()"
          style="min-width: 130px"
          v-else
          >Save Questionnaire</v-btn
        >
        <v-btn @click="CloseThis()" height="25" style="min-width: 130px"
          >Cancel</v-btn
        >
        <div style="width: 30px" />
      </v-footer>
  </v-main>
  </v-app>
</template>

<script>
if (Id == undefined) {
  var Id = 1
  var SessionId= 'xxx'
}


import { data } from "./data.js";
import { baseMixin } from "./BaseMixin.js";
import VueSimpleContextMenu from "vue-simple-context-menu";
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
const emptyOptions = {
  context: {},
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
const stringShema = {
  type: "object",
  requred: ["StrProp"],
  properties: {
    StrProp: {
      type: "string",
      title: "Input string please",
      description: "Help",
    },
  },
};
const dateShema = {
  type: "object",
  requred: ["DateProp"],
  properties: {
    DateProp: {
      type: "string",
      format: "date",
      title: "Input date please",
      description: "Help",
    },
  },
};
const numShema = {
  type: "object",
  requred: ["NumProp"],
  properties: {
    NumProp: {
      type: "string",
      format: "number",
      title: "Input numper please",
      description: "Help",
    },
  },
};

const radioItem = {
  type: "number",
  title: "Select",
  "x-display": "radio",
  "x-fromData": "context.Items",
  "x-itemKey": "Id",
  "x-itemTitle": "Name",
  description: "",
};

const stringItem = {
  type: "string",
  title: "Input string please",
  description: "Help",
};
const dateItem = {
  type: "string",
  format: "date",
  title: "Input date please",
  description: "Help",
};
const numItem = {
  type: "string",
  format: "number",
  title: "Input numper please",
  description: "Help",
};

export default {
  name: 'App',
  mixins: [baseMixin],
  components: {
    VJsf
  },
  data: () => ({
    valid: false,
    contentMenuId: 1,
    currentModel: { answerModel: {}, valid: false },
    mode :"edit",
    models: [],
    Questionnarie: { Questions: [] },

    currentQuestion: {
      Id: 0,
      ParentId: 1,
      Name: null,
      Options: radioOptions,
      Schema: radioShema,
    },
    session:SessionId,
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
  }),
  computed: {
    orderedQuestions() {
      return this.Questionnarie.Questions.sort((a, b) => a.Order - b.Order);
    },
     enableNext() {
      return Object.keys(this.currentModel.answerModel).length>0
    },
    
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
      question.Id = this.addedCouner; //у каждого добавленного вопроса пока он не сохранен id отрицательный
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
      if (this.currentQuestion.Id == question.Id) return; //���� ��� ��������� �����
      if (question.Id == 0) return; //���� ��� ����  currentQuestion ������ ��� ������������� Vue

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
          valud: false,
        };
        this.models.push(model);
      }
      this.currentModel = model;
    },
    Save()
    {
      if(mode=='edit'){
        this.SaveQuestionnaire()
      }else{
        this.SaveAnsver()
      }
    },
    SaveQuestion() {
      this.fetch(
        this.ok,
        "/Questions/Questionnaire/SaveQuestion?questionnaireId=" + Id,
        this.currentQuestion
      );
    },
    SaveQuestionnaire() {
      this.fetch(
        this.CloseThis,
        "/Questions/Questionnaire/SaveQuestionnaire?questionnaireId=" + Id,
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
       "Questionnaire/GetQuestions?id=" + Id
        );
    //  const callback=this.SetQuestions
    //   let httpReq = new XMLHttpRequest();

    //   httpReq.open('GET', 'https://localhost:7297/' + "Questionnaire/GetQuestions?id=" + Id);
    //   httpReq.setRequestHeader('Access-Control-Allow-Origin', 'https://localhost:7297');
    //   httpReq.setRequestHeader("Content-Type", "application/json"); 
    //   httpReq.onreadystatechange = function () { 
    //     if (this.readyState === 4 && this.status == 200 ) 
    //     {  
    //       console.log('ok')
    //       callback(httpReq); 
    //     }
    //   }
    //   httpReq.send()
      
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
      this.fetch(
        this.ok,
        `Questionnaire/SaveAnsvers?questionnaireId={Id} +'&sessionId= + {this.session}`,
        this.models
      );
    },

  },
  mounted: function () {
   // if (Id) {
   //   this.GetQuestions();
   this.SetQuestions(data);
   // }
  },
};
</script>
<style>
html,
body {
  height: 100%;
 
}

.buttion{
  width: 60px;
  border-radius: 25px;
  background-color: green !important; 
  margin: 3px
}
.panel{
 border: outset; 
 border-radius: 15px;
 padding:15px ;
 margin: 5px;
 background-color: rgb(201, 247, 248)
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
  background-color: rgb(202, 210, 212);
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
</style>
