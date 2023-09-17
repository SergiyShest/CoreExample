(function(){"use strict";var e={2179:function(e,t,n){var r=n(538),o=n(998),i=n(4562),s=n(5125),u=n(3059),l=n(1713),a=n(3687),c=function(){var e=this,t=e._self._c;return t(o.Z,{staticStyle:{width:"100%",padding:"3px"},attrs:{id:"app"},on:{keydown:[function(t){return(t.type.indexOf("key")||83===t.keyCode)&&t.ctrlKey?(t.preventDefault(),t.stopPropagation(),e.Save.apply(null,arguments)):null},function(t){return(t.type.indexOf("key")||37===t.keyCode)&&t.ctrlKey?(t.preventDefault(),t.stopPropagation(),e.SelectPrevQuestion.apply(null,arguments)):null},function(t){return(t.type.indexOf("key")||39===t.keyCode)&&t.ctrlKey?(t.preventDefault(),t.stopPropagation(),e.SelectNextQuestion.apply(null,arguments)):null}]}},[t(u.Z,[null!=e.Questionnarie?t(l.Z,{staticClass:"panel",staticStyle:{"background-color":"aquamarine"},style:e.headerCssStyle},[t("h2",[e._v(" "+e._s(e.Questionnarie.Text)+" ")])]):e._e(),t("div",{staticClass:"panel",staticStyle:{"min-height":"600px",display:"flex","flex-direction":"column","justify-content":"space-between"},style:e.currentQuestionCssStyle},[t("div",{staticStyle:{display:"flex","justify-content":"space-between"}},[null!=e.currentQuestion?t("h3",[e._v(" "+e._s(e.currentQuestion.Text)+" ")]):e._e(),null!=this.Questionnarie.Questions?t("div",{staticClass:"questionInfo"},[e._v(" "+e._s(e.currentQuestion.Order)+" of "+e._s(this.Questionnarie.Questions.length)+" ")]):e._e()]),t(s.Z,{ref:"form",model:{value:e.currentModel.valid,callback:function(t){e.$set(e.currentModel,"valid",t)},expression:"currentModel.valid"}},[null!=e.currentQuestion?t("v-jsf",{attrs:{schema:e.currentQuestion.Schema,options:e.currentQuestion.Options},model:{value:e.currentModel.answerModel,callback:function(t){e.$set(e.currentModel,"answerModel",t)},expression:"currentModel.answerModel"}}):e._e()],1),null!=e.currentQuestion?t("div",{staticStyle:{"text-align":"left"}},[e._v(" "+e._s(e.currentQuestion.Description)+" ")]):e._e(),t(a.Z),t("div",{staticStyle:{display:"flex",width:"100%",height:"50px",padding:"3px",margin:"3px"}},[e.currentQuestion.Order>1?t(i.Z,{staticClass:"buttion",on:{click:function(t){return e.SelectPrevQuestion()}}},[e._v(" Prev")]):e._e(),t(a.Z),e.currentQuestion.Order<e.Questionnarie.Questions.length?t(i.Z,{staticClass:"buttion",attrs:{disabled:!e.enableNext},on:{click:function(t){return e.SelectNextQuestion()}}},[e._v(" "+e._s(e.NextButtonText)+" ")]):e._e()],1)],1)],1)],1)},d=[];n(7658);const h={data:()=>({loadingData:!1}),methods:{GenerateGuid(){var e=(new Date).getTime(),t="undefined"!==typeof performance&&performance.now&&1e3*performance.now()||0;return"xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g,(function(n){var r=16*Math.random();return e>0?(r=(e+r)%16|0,e=Math.floor(e/16)):(r=(t+r)%16|0,t=Math.floor(t/16)),("x"===n?r:3&r|8).toString(16)}))},ExtractItem(e){if(e.Errors&&e.Errors.length>0)this.ShowErrors(e);else if(e.Item)return e.Item},ShowErrors(e){var t;this.PlayError(),console.log(e.Errors),Array.isArray(e.Errors)?e.Errors.forEach((e=>{e.Name&&(t+=e.Name+":"),t+=e.Errors.join("\r\n")})):t=e.Errors,t&&this.ShowAlert(t)},PlayOk(){this.PlaySound("/Content/Sounds/code-found.mp3")},PlayError(){this.PlaySound("/Content/Sounds/bad-beep.mp3")},PlaySound(e){var t=document.location.origin+e,n=new Audio(t);n.autoplay=!0},CloseThis:function(){window.parent.postMessage("CloseUrlForm","*")},ShowAlert(e){console.log(e),alert(e)},fetch:function(e,t,n=null){this.loadingData=!0;var r=e,o=document.location.origin+"/"+t,i=JSON.stringify(n);fetch(o,{method:"POST",mode:"no-cors",body:i,referrer:"unsafe-url",headers:{"Content-Type":"application/json"}}).then((e=>{if(this.loadingData=!1,e)return e.json()})).then((e=>{e&&(console.log(e),r(e)),this.loadingData=!1})).catch((e=>{this.loadingData=!1,console.error("Error while getting server data: "+o+" :"+e),this.ShowAlert("Error while getting server data: "+o+" :"+e)}))},GetDate(e){return e&&e.length>10?e.substring(0,10):e},GetAge(e){var t=new Date(e);if(null==e||""==e)return null;var n=Date.now()-t.getTime(),r=new Date(n),o=r.getUTCFullYear(),i=Math.abs(o-1970);return i}}};var f=n(9310);n(6800);Id,console.log(Id),SessionId;var p={name:"App",mixins:[h],components:{VJsf:f.Z},data:()=>({currentModel:{answerModel:{},valid:null},models:[],Questionnarie:{Questions:[]},currentQuestion:{Id:0,ParentId:1,Name:null,Options:{},Schema:{}},session:SessionId}),computed:{enableNext(){return 0!=this.currentModel.valid&&Object.keys(this.currentModel.answerModel).length>0},NextButtonText(){return this.currentQuestion.NextButtonText?this.currentQuestion.NextButtonText:"NEXT"},currentQuestionCssStyle(){if(this.currentQuestion.CssStyle)return this.currentQuestion.CssStyle},headerCssStyle(){if(this.Questionnarie.CssStyle)return this.Questionnarie.CssStyle}},methods:{validateForm(){this.$refs.form.validate()},SelectQuestion(e){if(this.currentQuestion.Id==e.Id)return;if(0==e.Id)return;let t=this.models.find((e=>e.QuestionId==this.currentQuestion.Id));t&&(t=this.currentModel),this.currentQuestion=e,this.notValidOptions=null,this.notValidSchema=null,this.SetModel(),this.PlayOk()},SelectNextQuestion(){const e=this.Questionnarie.Questions.find((e=>e.Order==this.currentQuestion.Order+1));e&&this.SelectQuestion(e),this.SaveAnsver()},SelectPrevQuestion(){const e=this.Questionnarie.Questions.find((e=>e.Order==this.currentQuestion.Order-1));e&&this.SelectQuestion(e),console.log(e)},SetModel(){let e=this.models.find((e=>e.QuestionId==this.currentQuestion.Id));e||(e={QuestionId:this.currentQuestion.Id,answerModel:{},valid:null},this.models.push(e)),this.currentModel=e},ok(e){e.Errors&&e.Errors.length>0&&this.ShowErrors(e)},GetQuestions(){console.log("SetQuestions(val)"),this.fetch(this.SetQuestions,"Questionnaire/Get?id="+Id)},SetQuestions(e){console.log(e),e.Errors&&this.ShowErrors(e),e.Item&&(this.Questionnarie=e.Item,this.Questionnarie.Questions.length>0&&this.SelectQuestion(this.Questionnarie.Questions[0]))},SaveAnsver(){this.fetch(this.ok,"Questionnaire/SaveAnsvers?questionnaireId="+Id+"&sessionId="+this.session,this.models)}},mounted:function(){console.log(Id),Id&&this.GetQuestions()}},v=p,x=n(1001),y=(0,x.Z)(v,c,d,!1,null,null,null),Q=y.exports,g=n(1858);r.ZP.use(g.Z);var S=new g.Z({icons:{iconfont:"mdi"}});r.ZP.config.productionTip=!1,new r.ZP({vuetify:S,render:e=>e(Q)}).$mount("#app")}},t={};function n(r){var o=t[r];if(void 0!==o)return o.exports;var i=t[r]={exports:{}};return e[r].call(i.exports,i,i.exports,n),i.exports}n.m=e,function(){var e=[];n.O=function(t,r,o,i){if(!r){var s=1/0;for(c=0;c<e.length;c++){r=e[c][0],o=e[c][1],i=e[c][2];for(var u=!0,l=0;l<r.length;l++)(!1&i||s>=i)&&Object.keys(n.O).every((function(e){return n.O[e](r[l])}))?r.splice(l--,1):(u=!1,i<s&&(s=i));if(u){e.splice(c--,1);var a=o();void 0!==a&&(t=a)}}return t}i=i||0;for(var c=e.length;c>0&&e[c-1][2]>i;c--)e[c]=e[c-1];e[c]=[r,o,i]}}(),function(){n.n=function(e){var t=e&&e.__esModule?function(){return e["default"]}:function(){return e};return n.d(t,{a:t}),t}}(),function(){n.d=function(e,t){for(var r in t)n.o(t,r)&&!n.o(e,r)&&Object.defineProperty(e,r,{enumerable:!0,get:t[r]})}}(),function(){n.g=function(){if("object"===typeof globalThis)return globalThis;try{return this||new Function("return this")()}catch(e){if("object"===typeof window)return window}}()}(),function(){n.o=function(e,t){return Object.prototype.hasOwnProperty.call(e,t)}}(),function(){n.r=function(e){"undefined"!==typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})}}(),function(){var e={143:0};n.O.j=function(t){return 0===e[t]};var t=function(t,r){var o,i,s=r[0],u=r[1],l=r[2],a=0;if(s.some((function(t){return 0!==e[t]}))){for(o in u)n.o(u,o)&&(n.m[o]=u[o]);if(l)var c=l(n)}for(t&&t(r);a<s.length;a++)i=s[a],n.o(e,i)&&e[i]&&e[i][0](),e[i]=0;return n.O(c)},r=self["webpackChunkquestinnaire"]=self["webpackChunkquestinnaire"]||[];r.forEach(t.bind(null,0)),r.push=t.bind(null,r.push.bind(r))}();var r=n.O(void 0,[998],(function(){return n(2179)}));r=n.O(r)})();
//# sourceMappingURL=app.js.map