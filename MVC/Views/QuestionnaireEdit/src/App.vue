<template>
  <v-app id="app" style="width: 100%; padding: 3px" @keydown.ctrl.83.prevent.stop="Save"
    @keydown.ctrl.37.prevent.stop="SelectPrevQuestion" @keydown.ctrl.39.prevent.stop="SelectNextQuestion">
    <div class="modal" v-if="loadingData">
      <img class="loader-icon" :src="require('../../../wwwroot/Content/Images/loading.gif')" />
    </div>
    <v-main>
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
    session:SessionId
  }),
  computed: {
     enableNext() {
      return Object.keys(this.currentModel.answerModel).length>0
    },
    
  },
  methods: {
    validateForm() {
      this.$refs.form.validate();
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
</style>