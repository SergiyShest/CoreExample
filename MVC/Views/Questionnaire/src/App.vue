<template>
  <v-app id="app" style="width: 100%; padding: 0%" @keydown.ctrl.83.prevent.stop="Save"
    @keydown.ctrl.37.prevent.stop="SelectPrevQuestion" @keydown.ctrl.39.prevent.stop="SelectNextQuestion">
    <div class="modal" v-if="loadingData">
      <img class="loader-icon" :src="require('../../../wwwroot/Content/Images/loading.gif')" />
    </div>
    <v-main>


      <v-row style="border: outset; padding: 5px; background-color: lightgray">
        <v-form ref="form" v-model="currentModel.valid" style="width: 100%; padding: 5px">
          <div style="text-align: center" v-if="currentQuestion != null">
            {{ currentQuestion.Name }} ({{ currentQuestion.Id }})
          </div>
           <div style="text-align: center" v-if="this.Questionnarie.Questions != null">
            {{ this.Questionnarie.Questions.length }} 
          </div>         
          <v-jsf v-if="currentQuestion != null" v-model="currentModel.answerModel" :schema="currentQuestion.Schema"
            :options="currentQuestion.Options" />
        </v-form>
        <v-row style="width: 100%; padding: 5px">
          <v-spacer></v-spacer>
          <v-btn style="width: 60px; margin: 3px" @click="SelectPrevQuestion()" :disabled="currentQuestion.Order == 1">
            <img :src="require('../../../wwwroot/Content/Icons/prev.png')" width="20" height="20" />Prev</v-btn>
          <v-btn style="width: 60px; margin: 3px" @click="SelectNextQuestion()" :disabled="currentQuestion.Order >= Questionnarie.Questions.length
            ">
            Next<img :src="require('../../../wwwroot/Content/Icons/next.png')" width="20" height="20" />
          </v-btn>
        </v-row>
      </v-row>
    </v-main>
  </v-app>
</template>

<script>
if (Id == undefined) {
  var Id = 1
}
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
  components: {
    VJsf
  },
  data: () => ({
    valid: false,
    currentModel: { answerModel: {}, valid: false },
    models: [],
    Questionnarie: { Questions: [] },

    currentQuestion: {
      Id: 0,
      ParentId: 1,
      Name: null,
      Options: radioOptions,
      Schema: radioShema,
    },

  }),
  methods: {
    validateForm() {
      this.$refs.form.validate();
    },


    IsQueryValid(question) {

      if (this.patient && this.patient.id > 0) {
        let model = this.models.find((x) => x.QuestionId == question.Id);

        return model && model.valid;

      }

      return false;
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
        (x) => x.Order == this.currentQuestion.Order + 1
      );
      if (nextQuestion) {
        this.SelectQuestion(nextQuestion);
      }
      console.log(nextQuestion) 
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

  },
  mounted: function () {
    if (Id) {
      this.GetQuestions();

    }
  },
};
</script>