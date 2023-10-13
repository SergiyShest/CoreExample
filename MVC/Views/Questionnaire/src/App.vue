<template>
  <v-app
    id="app"
    style="width: 100%; padding: 3px"
    @keydown.ctrl.83.prevent.stop="Save"
    @keydown.ctrl.37.prevent.stop="SelectPrevQuestion"
    @keydown.ctrl.39.prevent.stop="SelectNextQuestion"
  >
    <v-main>
      <v-row
        v-if="Questionnarie != null"
        class="panel"
        style="background-color: aquamarine"
        :style="headerCssStyle"
      >
        <h2>{{ Questionnarie.Text }}</h2>
      </v-row>
      <div
        style="
          min-height: 600px;
          display: flex;
          flex-direction: column;
          justify-content: space-between;
        "
        :style="currentQuestionCssStyle"
        class="panel"
      >
        <div style="display: flex; justify-content: space-between">
          <h3 v-if="currentQuestion != null">
            {{ currentQuestion.Text }}
          </h3>
          <div class="questionInfo" v-if="this.Questionnarie.Questions != null">
            {{ currentQuestion.Order }} of
            {{ this.Questionnarie.Questions.length }}
          </div>
        </div>
        <v-form ref="form" v-model="currentModel.valid">
          <v-jsf
            v-if="currentQuestion != null"
            v-model="currentModel.answerModel"
            :schema="currentQuestion.Schema"
            :options="currentQuestion.Options"
          />
        </v-form>
        <div
          style="text-align: left"
          v-if="currentQuestion.Images && currentQuestion.Images.length > 0"
        >
          <img
            v-for="im in currentQuestion.Images"
            v-bind:key="im"
            :src="im"
            style="margin: 1px; width: 300px; height: 200px"
          />
        </div>
        <div style="text-align: left" v-if="currentQuestion != null">
          {{ currentQuestion.Description }}
        </div>
        <v-spacer></v-spacer>
        <div
          style="
            display: flex;
            width: 100%;
            padding: 3px;
            margin: 3px;
          "
        >
          <button
            class="buttion"
            @click="SelectPrevQuestion()"
            v-if="currentQuestion.ShowPrevButton && currentQuestion.Order > 1"
          >
            {{ PrevButtonText }}
          </button>
          <v-spacer></v-spacer>
          <button
            class="buttion"
            @click="SelectNextQuestion()"
            :disabled="!enableNext"
            v-if="
              currentQuestion.ShowNextButton &&
              currentQuestion.Order < Questionnarie.Questions.length
            "
          >
            {{ NextButtonText }}
          </button><v-spacer></v-spacer>
        </div>
      </div>
    </v-main>
  </v-app>
</template>

<script>
if (Id == undefined) {
 // var Id = null;
 // var SessionId = "baseMixin.GenerateGuid()";
}
// console.log(Id)
// if (SessionId == undefined) {
//   //
// }

import { data } from "./data.js";
import { baseMixin } from "./BaseMixin.js";
import VJsf from "@koumoul/vjsf/lib/VJsf.js";
import "@koumoul/vjsf/lib/VJsf.css";
import "@koumoul/vjsf/lib/deps/third-party.js";

export default {
  name: "App",
  mixins: [baseMixin],
  components: {
    VJsf,
  },
  data: () => ({
    userAnsverCounter: 0,
    currentModel: { answerModel: {}, valid: null },
    models: [],
    Questionnarie: { Questions: [] },

    currentQuestion: {
      Id: 0,
      Name: null,
      Options: {},
      Schema: {},
    },
    session: SessionId,
  }),
  computed: {
    enableNext() {
      return (
        this.currentModel.valid != false &&
        Object.keys(this.currentModel.answerModel).length > 0
      );
    },
    PrevButtonText() {
      return this.currentQuestion.PrevButtonText
        ? this.currentQuestion.PrevButtonText
        : "Prev";
    },
    NextButtonText() {
      return this.currentQuestion.NextButtonText
        ? this.currentQuestion.NextButtonText
        : "Next";
    },

    currentQuestionCssStyle() {
      if (this.currentQuestion.CssStyle) {
        return this.currentQuestion.CssStyle;
      }
    },
    headerCssStyle() {
      if (this.Questionnarie.CssStyle) {
        return this.Questionnarie.CssStyle;
      }
    },
  },
  watch: {
    enableNext(val) {
      
      if (val) {
        setTimeout(()=>{this.tryNext();}, 500);
      }
    },
  },
  methods: {
    tryNext() {
      if (this.enableNext) {
        console.log(this.currentQuestion.Name);
        if (this.currentQuestion.Name == "QUESTION 1")
        {this.SelectNextQuestion(); return;}
        if (this.currentQuestion.Name == "Images") {this.SelectNextQuestion();return;}
      }
    },
    validateForm() {
      this.$refs.form.validate();
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
      //this.PlayOk()
    },
    SelectNextQuestion() {
      let nextQuestion;
      if (this.currentQuestion.NextQuestionCondition) {
        var nextQuestionCondition =
          this.currentQuestion.NextQuestionCondition.replace(
            "$Answer",
            this.currentModel.answerModel.Answer
          );
        var F = new Function(nextQuestionCondition);
        var nextQuestionName = F();
        nextQuestion = this.Questionnarie.Questions.find(
          (x) => x.Name == nextQuestionName
        );
      } else {
        nextQuestion = this.Questionnarie.Questions.find(
          (x) => x.Order == this.currentQuestion.Order + 1
        );
      }

      if (nextQuestion) {
        this.SelectQuestion(nextQuestion);
      }
      this.SaveAnsver();
    },
    SelectPrevQuestion() {
      const prevQuestion = this.Questionnarie.Questions.find(
        (x) => x.Order == this.currentQuestion.Order - 1
      );
      if (prevQuestion) {
        this.SelectQuestion(prevQuestion);
      }
      console.log(prevQuestion);
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

    ok(item) {
      if (item.Errors && item.Errors.length > 0) {
        this.ShowErrors(item);
      }
    },
    GetQuestions() {
      this.fetch(this.SetQuestions, "Questionnaire/Get?id=" + Id);
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

    SaveAnsver() {
      if (this.models.length == 4) this.userAnsverCounter++;
      this.fetch(
        this.ok,
        "Questionnaire/SaveAnsvers?questionnaireId=" +
          Id +
          "&sessionId=" +
          this.session +
          "_" +
          this.userAnsverCounter,
        this.models
      );
    },
  },
  mounted: function () {
    console.log(Id);
    // if (Id) {
    // this.GetQuestions();
    this.SetQuestions(data);
    //}
  },
};
</script>
<style>
html,
body {
  height: 100%;
}

.questionInfo {
  width: 100px;
  min-width: 100px;
  height: 55px;
  background-color: green;
  border-radius: 25px;
  padding: 5px;
  margin: 5px;
}

.buttion {
  width: 130px;
  height: 80px;
  border-radius: 25px;
  border: outset;
  padding: 3px;
  margin: 3px;
  background-color: green !important;
  margin: 3px;
}
.v-label {
  font-size: 25px;
}
.v-input--selection-controls__input {
  height: 50px;
}
.panel {
  font-family: Arial, sans-serif;
  font-size: 30px;
  border: outset;
  border-radius: 15px;
  padding: 15px;
  margin: 5px;
  background-color: rgb(247, 247, 247);
}
</style>