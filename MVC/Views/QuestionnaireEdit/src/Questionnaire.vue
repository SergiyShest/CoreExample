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
    <!-- <div class="modal" v-if="showLoadFromFileDialog">
       <div style="background:beige;width: 300px;height: 100px;  top: 50%;left: 50%;">

       </div>
    </div> -->
    <v-dialog v-model="showLoadFromFileDialog" width="500px">
      <v-card height="200px">
        <v-card-title class="text-h5"></v-card-title>
        <v-card-actions>
          <v-col>
            <v-row>
              <v-file-input
                v-model="files"
                accept=".xls,.xlsx"
                show-size
                label="Select Excel file contains  questions"
              />
            </v-row>
            <v-row></v-row>
            <v-spacer />
          </v-col>
        </v-card-actions>
        <v-footer>
          <v-row>
            <v-spacer />

            <v-btn
              class="ma-2"
              :disabled="files.length == 0"
              style="min-width: 130px"
              @click="LoadFromFile"
              >OK</v-btn
            >
            <v-btn
              class="ma-2"
              style="min-width: 130px"
              @click="showLoadFromFileDialog = false"
              >Cancel</v-btn
            >
          </v-row>
        </v-footer>
      </v-card>
    </v-dialog>
    <!-- Modal loader -->
    <vue-simple-context-menu
      :options="[]"
      :elementId="contentMenuId"
      :ref="'contextMenu'"
      @option-clicked="contextMenuClicked"
    ></vue-simple-context-menu>
    <!--  -->
    <v-main>
      <v-container fluid>
        <v-row no-gutters>
          <h3 style="text-align: center" v-if="mode == 'work'">
            {{ Questionnarie.Name }} ({{ Questionnarie.Id }})
          </h3>
          <h3 style="text-align: center" v-else>
            <input type="text" v-model="Questionnarie.Name" /> ({{
              Questionnarie.Id
            }})
          </h3>
          <v-spacer />
          <v-btn
            v-if="mode == 'edit' && Questionnarie.Id"
            @click="showLoadFromFileDialog = true"
            >load from File</v-btn
          >
          <v-btn v-if="mode == 'edit'" @click="mode = 'work'"
            >to work mode</v-btn
          >
          <v-btn v-else @click="mode = 'edit'">to edit</v-btn>
        </v-row>
        <v-row
          no-gutters
          style="border: outset; padding: 5px"
          v-if="mode == 'work'"
        >
          <span style="margin: 4px">Patient name:</span>
          <span style="margin: 4px; font-weight: bold">
            {{ patient.firstName }} {{ patient.lastName }}
          </span>
          <span style="margin: 4px">Email:</span>
          <span style="margin: 4px; font-weight: bold">{{
            patient.email
          }}</span>
          <span style="margin: 4px">Age:</span>
          <input
            style="margin: 4px; font-weight: bold"
            type="string"
            :value="GetAge(patient.dob)"
            readonly
          />
          <span style="margin: 4px">Dob:</span>
          <input
            readonly
            style="margin: 4px; font-weight: bold"
            type="string"
            :value="GetDate(patient.dob)"
          />
          <v-spacer />
          <span v-if="!(patient.id > 0)" class="errorV blink"
            >Select patient please ==></span
          >
          <button
            class="button"
            @click="SelectPatient()"
            title="Select patient"
          >
            ...
          </button> </v-row
        ><!--patient-->
        <v-row>
          <v-col cols="2" style="height: 70vh; overflow: scroll">
            <v-row style="height: 50px" v-if="mode == 'edit'">
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
            </div> </v-col
          ><!-- menu row-->

          <v-col cols="8" v-if="Questionnarie.Questions.length > 0">
            <v-row
              style="border: outset; padding: 5px; background-color: lightgray"
            >
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
              <v-row style="width: 100%; padding: 5px">
                <v-spacer></v-spacer>
                <v-btn
                  style="width: 60px; margin: 3px"
                  @click="SelectPrevQuestion()"
                  :disabled="currentQuestion.Num == 1"
                >
                  <img
                    :src="require('../../../wwwroot/Content/Icons/prev.png')"
                    width="20"
                    height="20"
                  />Prev</v-btn
                >
                <v-btn
                  style="width: 60px; margin: 3px"
                  @click="SelectNextQuestion()"
                  :disabled="
                    currentQuestion.Num >= Questionnarie.Questions.length
                  "
                >
                  Next<img
                    :src="require('../../../wwwroot/Content/Icons/next.png')"
                    width="20"
                    height="20"
                  />
                </v-btn>
              </v-row>
            </v-row>
            <v-row style="padding: 10px" v-if="mode == 'edit'">
              <v-col>
                <v-row>
                  New item name: <v-spacer /><input
                    type="text"
                    class="inputStyle"
                    v-model="newItemName"
                /></v-row>
                <v-row>
                  New item text: <v-spacer /><input
                    type="text"
                    class="inputStyle"
                    v-model="newItemText"
                /></v-row>
                <v-row>
                  <v-spacer />
                  <v-menu class="center">
                    <template v-slot:activator="{ on, attrs }">
                      <v-btn height="25" v-bind="attrs" v-on="on">
                        Add Item to Question
                      </v-btn>
                    </template>
                    <v-list>
                      <v-list-item
                        v-for="(item, index) in questionItemTemplates"
                        :key="index"
                        link
                      >
                        <v-list-item-title @click="AddItemInQuestion(item)">{{
                          item.Name
                        }}</v-list-item-title>
                      </v-list-item>
                    </v-list>
                  </v-menu>
                </v-row>
              </v-col>
            </v-row>
            <v-row v-if="mode == 'edit'">
              <v-col cols="6">
                <fieldset style="border: inset">
                  <legend
                    style="width: 100px; padding-top: 2px; padding-left: 2px"
                  >
                    schema
                  </legend>
                  <prism-editor v-model="schemaStr" :highlight="highlighter" />
                  <div class="errorV">
                    {{ notValidSchemaError }}
                  </div>
                </fieldset>
              </v-col>
              <v-col cols="6">
                <fieldset style="border: inset">
                  <legend
                    style="width: 100px; padding-top: 2px; padding-left: 2px"
                  >
                    options
                  </legend>
                  <prism-editor v-model="optionsStr" :highlight="highlighter" />
                  <div class="errorV">
                    {{ notValidOptionsError }}
                  </div>
                </fieldset>
              </v-col>
            </v-row> </v-col
          ><!-- current Question-->
        </v-row>
      </v-container>
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
import { baseMixin } from "./BaseMixin.js";
import VueSimpleContextMenu from "vue-simple-context-menu";

import VJsf from "@koumoul/vjsf/lib/VJsf.js";
import "@koumoul/vjsf/lib/VJsf.css";
import "@koumoul/vjsf/lib/deps/third-party.js";

import { PrismEditor } from "vue-prism-editor";
import "vue-prism-editor/dist/prismeditor.min.css"; // import the styles somewhere

import { highlight, languages } from "prismjs/components/prism-core";
import "prismjs/components/prism-clike";
import "prismjs/components/prism-javascript";
import "prismjs/themes/prism.css"; // import syntax highlighting styles

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
  name: "App",
  mixins: [baseMixin],
  components: {
    VJsf,
    PrismEditor,
    VueSimpleContextMenu,
  },
  data: () => ({
    addedCouner: 0,
    contentMenuId: 1,
    currentModel: { answerModel: {}, valid: false },
    mode: Mode,
    models: [],
    Questionnarie: { Questions: [] },
    patient: {},

    notValidSchema: "",
    notValidSchemaError: "",
    notValidOptions: "",
    notValidOptionsError: "",

    showLoadFromFileDialog: false,
    files: [],
    currentQuestion: {
      Id: 0,
      ParentId: Id,
      Name: null,
      Options: radioOptions,
      Schema: radioShema,
    },
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
      return this.Questionnarie.Questions.sort((a, b) => a.Num - b.Num);
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
        if (this.notValidSchema) return this.notValidSchema;
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
      if (this.currentQuestion.Id == question.Id) return; //если это повторный вызов
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
    },
    SelectNextQuestion() {
      const nextQuestion = this.Questionnarie.Questions.find(
        (x) => x.Num == this.currentQuestion.Num + 1
      );
      if (nextQuestion) {
        this.SelectQuestion(nextQuestion);
      }
    },
    SelectPrevQuestion() {
      const prevQuestion = this.Questionnarie.Questions.find(
        (x) => x.Num == this.currentQuestion.Num - 1
      );
      if (prevQuestion) {
        this.SelectQuestion(prevQuestion);
      }
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
    LoadFromFile() {
      this.showLoadFromFileDialog = false;
      var collBack = this.GetQuestions;

      var req = new XMLHttpRequest();
      var formData = new FormData();
      console.log(this.files);
      formData.append("files", this.files);
      formData.append("questionnaireName", this.Questionnarie.Name);
      formData.append("questionnaireId", Id);
      req.open("POST", "/Questions/Questionnaire/UploadFile");
      req.send(formData);

      req.onload = function () {
        collBack();
      };

      req.onerror = function (error) {
        this.ShowErrors(error);
      };
    },
    ok(item) {
      if (item.Errors && item.Errors.length > 0) {
        this.ShowErrors(item);
      }
    },
    GetQuestions() {
      this.fetch(
        this.SetQuestions,
        "/Questions/Questionnaire/GetQuestions?id=" + Id
      );
    },
    SetQuestions(val) {
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
    GetAnswers() {
      this.fetch(
        this.SetAnswers,
        "/Questions/Questionnaire/GetAnsvers?questionnaireId=" +
          Id +
          "&patientId=" +
          this.patient.id
      );
    },
    SetAnswers(val) {
      console.log(val);
      if (val.Errors) {
        this.ShowErrors(val);
      }
      if (val.Item) {
        // this.$nextTick(() => {
        this.models = val.Item;
        this.SetModel();
        // });
      } else {
        this.models = [];
      }
    },
    SaveAnsver() {
      this.fetch(
        this.ok,
        "/Questions/Questionnaire/SaveAnsvers?questionnaireId=" +
          Id +
          "&patientId=" +
          this.patient.id,
        this.models
      );
    },
    GetPatient: function (patientId) {
      this.fetch(
        this.SetPatient,
        "/SamplesEdit/GroupEdit/GetPatient?patientId=" + patientId
      );
    },
    SetPatient: function (val) {
      if (val.Errors && val.Errors.length > 0) {
        this.ShowErrors(val);
      } else {
        this.patient = val.Patient;
        this.GetAnswers();
      }
    },
    SelectPatient() {
      //function placed in file \Areas\SamplesEdit\Views\GroupEdit\Index.cshtml
      FindPatient();
    },
    highlighter(code) {
      return highlight(code, languages.js, "json");
    },
  },
  mounted: function () {
    if (Id) {
      this.GetQuestions();
      if (PatientId) {
        this.GetPatient(PatientId);
      }
      window.globalEvent.$on("change-patient", (patientId) => {
        this.GetPatient(patientId);
      });
    }
  },
};
</script>

<style>
html,
body {
  height: 100%;
}
.button {
  background-color: white;
  border-color: grey;
  border: outset;
  border-width: 1px;
  min-width: 40px;
  margin: 3px;
}

.button:hover {
  border: inset;
  border-width: 1px;
  transition: 0.7s;
}
.my-editor {
  /* we dont use `language-` classes anymore so thats why we need to add background and text color manually 
		/background: #AAA;*/
  border: solid 1px #808080;
  border-radius: 5px;
  /* you must provide font-family font-size line-height. Example: */
  font-family: Fira code, Fira Mono, Consolas, Menlo, Courier, monospace;
  font-size: 14px;
  line-height: 1.5;
  padding: 5px;
  height: 200px;
}
.errorV {
  color: red;
}

/* optional class for removing the outline */
.prism-editor__textarea:focus {
  outline: none;
}
/* The Modal (background) */
.modal {
  display: block; /* Hidden by default */
  position: fixed; /* Stay in place */
  z-index: 1; /* Sit on top */
  /*padding-top: 50%; Location of the box */
  left: 0;
  top: 0;
  width: 100%; /* Full width */
  height: 100%; /* Full height */
  overflow: auto; /* Enable scroll if needed */
  background-color: rgb(0, 0, 0); /* Fallback color */
  background-color: rgba(0, 0, 0, 0.4); /* Black w/ opacity */
}

/* Modal Content */
.modal-content {
  background-color: #fefefe;
  margin: auto;
  padding: 20px;
  border: 1px solid #888;
  width: 80%;
}
.loader {
  top: 50%;
  border: 16px solid #f3f3f3; /* Light grey */
  border-top: 16px solid #3498db; /* Blue */
  border-radius: 50%;
  width: 50px;
  height: 50px;
  animation: spin 2s linear infinite;
}

@keyframes spin {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}

/* .blink {
  animation: blink-animation 1s steps(5, start) infinite;
  -webkit-animation: blink-animation 1s steps(5, start) infinite;
}
@keyframes blink-animation {
  to {
    visibility: hidden;
  }
}
@-webkit-keyframes blink-animation {
  to {
    visibility: hidden;
  }
} */

.blink {
  -webkit-animation: blink5 5s linear infinite;
  animation: blink5 1s linear infinite;
}
@-webkit-keyframes blink5 {
  0% {
    color: rgb(255, 255, 255);
  }
  30% {
    color: rgb(255, 0, 0);
  }
  70% {
    color: rgb(255, 0, 0);
  }
  100% {
    color: rgb(255, 255, 255);
  }
}
@keyframes blink5 {
  0% {
    color: rgb(255, 255, 255);
  }
  30% {
    color: rgb(255, 0, 0);
  }
  70% {
    color: rgb(255, 0, 0);
  }
  100% {
    color: rgb(255, 255, 255);
  }
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
