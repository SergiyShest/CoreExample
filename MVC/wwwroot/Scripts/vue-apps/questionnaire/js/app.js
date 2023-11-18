(function(){"use strict";var e={6793:function(e,t,n){var o=n(538),s=n(998),r=n(5125),i=n(3059),a=n(3687),u=function(){var e=this,t=e._self._c;return t(s.Z,{staticStyle:{width:"100%",padding:"3px"},attrs:{id:"app"},on:{keydown:[function(t){return(t.type.indexOf("key")||83===t.keyCode)&&t.ctrlKey?(t.preventDefault(),t.stopPropagation(),e.Save.apply(null,arguments)):null},function(t){return(t.type.indexOf("key")||37===t.keyCode)&&t.ctrlKey?(t.preventDefault(),t.stopPropagation(),e.SelectPrevQuestion.apply(null,arguments)):null},function(t){return(t.type.indexOf("key")||39===t.keyCode)&&t.ctrlKey?(t.preventDefault(),t.stopPropagation(),e.SelectNextQuestion.apply(null,arguments)):null}]}},[t(i.Z,[null!=e.Questionnarie?t("div",{staticClass:"panel",staticStyle:{"background-color":"aquamarine",display:"flex"},style:e.headerCssStyle},[t("h2",{staticStyle:{width:"100%"}},[e._v(e._s(e.Questionnarie.Text))])]):e._e(),t("div",{staticClass:"panel",staticStyle:{"min-height":"600px",display:"flex","flex-direction":"column","justify-content":"space-between"},style:e.currentQuestionCssStyle},[t("div",{staticStyle:{display:"flex","justify-content":"space-between"}},[null!=e.currentQuestion?t("h3",[e._v(" "+e._s(e.currentQuestion.Text)+" ")]):e._e(),null!=this.Questionnarie.Questions&&e.currentQuestion.ShowCounter?t("div",{staticClass:"questionInfo"},[e._v(" "+e._s(e.currentQuestion.Order)+" of "+e._s(this.Questionnarie.Questions.length)+" ")]):e._e()]),t(r.Z,{ref:"form",model:{value:e.currentModel.valid,callback:function(t){e.$set(e.currentModel,"valid",t)},expression:"currentModel.valid"}},[null!=e.currentQuestion?t("v-jsf",{attrs:{schema:e.currentQuestion.Schema,options:e.currentQuestion.Options},model:{value:e.currentModel.answerModel,callback:function(t){e.$set(e.currentModel,"answerModel",t)},expression:"currentModel.answerModel"}}):e._e()],1),e.currentQuestion.Images&&e.currentQuestion.Images.length>0?t("div",{staticStyle:{"text-align":"left"}},e._l(e.currentQuestion.Images,(function(e){return t("img",{key:e,staticStyle:{margin:"1px",width:"300px",height:"200px"},attrs:{src:e}})})),0):e._e(),null!=e.currentQuestion?t("div",{staticStyle:{"text-align":"left"}},[e._v(" "+e._s(e.currentQuestion.Description)+" ")]):e._e(),t(a.Z),t("div",{staticStyle:{display:"flex",width:"100%",padding:"3px",margin:"3px"}},[e.currentQuestion.ShowPrevButton&&e.currentQuestion.Order>1?t("button",{staticClass:"buttion",on:{click:function(t){return e.SelectPrevQuestion()}}},[e._v(" "+e._s(e.PrevButtonText)+" ")]):e._e(),t(a.Z),e.currentQuestion.ShowNextButton&&e.currentQuestion.Order<e.Questionnarie.Questions.length?t("button",{staticClass:"buttion",attrs:{disabled:!e.enableNext},on:{click:function(t){return e.SelectNextQuestion()}}},[e._v(" "+e._s(e.NextButtonText))]):e._e(),t(a.Z)],1)],1)])],1)},l=[];n(7658);const d={Item:{Name:"SelectLang",Text:"Please choose the language",Main:!0,CssStyle:{"font-family":"Arial","font-size":"25px",background:"white"},Questions:[{Name:"QUESTION 0 ",Text:"Please choose the language",Order:1,NextQuestionCondition:"let lg='es' ;if($Answer == 1){lg='en'}; window.location.href = document.location.origin + '/'+lg;",NextButtonText:"NEXT",PrevButtonText:"PREV",ShowNextButton:!1,ShowPrevButton:!1,Images:[],CssStyle:null,ShowCounter:!1,headerCssStyle:"display:none",Schema:{type:"object",requred:["Answer"],properties:{Answer:{type:"number",title:"Check the answer that applies to you","x-display":"radio","x-itemKey":"Id","x-fromData":"context.Items","x-itemTitle":"Name"}}},Options:{context:{Items:[{Id:1,Name:"English"},{Id:2,Name:"Spanish"}]},selectAll:!0},QuestionnaireId:0,Description:"",Id:-1}],Id:10}},c=[{name:"Spanish",flag:"/Content/Images/spanishFlag.png"},{name:"English",flag:"/Content/Images/englishFlag.png"}],h={Item:{Name:"English with images2",Text:"Answer a few questions and see if you or someone you care for may be eligible",Main:!0,CssStyle:{"font-family":"Arial","font-size":"25px",background:"white"},Questions:[{Name:"QUESTION 1 en",Text:"Do you have itchy skin?",Order:1,NextQuestionCondition:"if($Answer == 1) return 'Images'; else return 'END1';",NextButtonText:"NEXT",PrevButtonText:"PREV",ShowNextButton:!1,ShowPrevButton:!1,Images:[],CssStyle:null,ShowCounter:!0,Schema:{type:"object",requred:[],properties:{Answer:{type:"number",title:"Check the answer that applies to you","x-display":"radio","x-itemKey":"Id","x-fromData":"context.Items","x-itemTitle":"Name"}}},Options:{context:{Items:[{Id:1,Name:"Yes"},{Id:2,Name:"No"}]},selectAll:!0},QuestionnaireId:10,Description:"",Id:133},{Name:"Images",Text:"Do any of these photos resemble the current condition of your skin?",Order:2,NextQuestionCondition:"if($Answer == 1) return 'InputUserData'; else return 'END1';",NextButtonText:"NEXT",PrevButtonText:"PREV",ShowNextButton:!1,ShowPrevButton:!0,Images:["/Content/Images/item1.PNG","/Content/Images/item2.PNG","/Content/Images/item4.PNG","/Content/Images/item3.PNG","/Content/Images/item5.PNG","/Content/Images/item6.PNG"],CssStyle:null,ShowCounter:!0,Schema:{type:"object",requred:["Answer"],properties:{Answer:{type:"number",title:"Check the answer that applies to you","x-display":"radio","x-itemKey":"Id","x-fromData":"context.Items","x-itemTitle":"Name"}}},Options:{context:{Items:[{Id:1,Name:"Yes"},{Id:2,Name:"No"}]},selectAll:!0},QuestionnaireId:10,Description:"",Id:134},{Name:"InputUserData",Text:"It is possible that you may be chosen to be part of this clinical study and receive compensation of up to $400 dollars. Please provide your full name, phone number, and email. A representative from our team will contact you within the next 3 business days.",Order:3,NextQuestionCondition:null,NextButtonText:"SEND",PrevButtonText:"PREV",ShowNextButton:!0,ShowPrevButton:!1,Images:[],CssStyle:null,ShowCounter:!0,Schema:{type:"object",requred:["phone","name"],properties:{name:{type:"string",title:"Full name:"},email:{type:"string",title:"Email (optional):",pattern:"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,5}","x-options":{messages:{pattern:"You must have a valid email address"}}},phone:{type:"string",title:"Phone number:",pattern:"^\\s?[\\s(]?\\s?\\d[\\s-]?\\d[\\s-]?\\d[\\s\\-)]?\\s?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s]?$|^[\\+]?\\d\\s?[\\s(]?\\s?\\d[\\s-]?\\s?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?","x-options":{messages:{pattern:"The valid number must contain 10 numbers +34 (000) 000 00 or 8 numbers (000) 000 00 "}}}}},Options:{},QuestionnaireId:10,Description:"",Id:135},{Name:"END",Text:"Thank you for contacting!",Order:4,NextQuestionCondition:null,NextButtonText:"NEXT",PrevButtonText:"PREV",ShowNextButton:!1,ShowPrevButton:!1,Images:[],CssStyle:null,ShowCounter:!0,Schema:{},Options:{},QuestionnaireId:10,Description:"",Id:136},{Name:"END1",Text:"We apologize! We appreciate the time you took to answer our questions. However, it appears that you are not eligible for this particular clinical trial at this time.",Order:5,NextQuestionCondition:null,NextButtonText:"NEXT",PrevButtonText:"PREV",ShowNextButton:!1,ShowPrevButton:!1,Images:[],CssStyle:null,ShowCounter:!0,Schema:{},Options:{},QuestionnaireId:10,Description:"",Id:137}],Id:10}},m={Item:{Name:"Spanich with images2",Text:"Responda algunas preguntas y vea si usted o alguien a quien cuida puede ser elegible",Main:!0,CssStyle:{"font-family":"Arial","font-size":"25px",background:"white"},Questions:[{Name:"QUESTION 1 es",Text:"¿Tienes picazón en la piel?",Order:1,NextQuestionCondition:"if($Answer == 1) return 'Images'; else return 'END1';",NextButtonText:"SIG",PrevButtonText:"ANT",ShowNextButton:!1,ShowPrevButton:!1,Images:[],CssStyle:null,ShowCounter:!0,Schema:{type:"object",requred:["Answer"],properties:{Answer:{type:"number",title:"Marque la repuesta que le corresponde","x-display":"radio","x-itemKey":"Id","x-fromData":"context.Items","x-itemTitle":"Name"}}},Options:{context:{Items:[{Id:1,Name:"Si"},{Id:2,Name:"No"}]},selectAll:!0},QuestionnaireId:10,Description:"",Id:133},{Name:"Images",Text:"¿De las siguientes imágenes, puede identificar alguna que se parezca a la condición actual de su piel?",Order:2,NextQuestionCondition:"if($Answer == 1) return 'InputUserData'; else return 'END1';",NextButtonText:"SIG",PrevButtonText:"ANT",ShowNextButton:!1,ShowPrevButton:!0,Images:["/Content/Images/item1.PNG","/Content/Images/item2.PNG","/Content/Images/item4.PNG","/Content/Images/item3.PNG","/Content/Images/item5.PNG","/Content/Images/item6.PNG"],CssStyle:null,ShowCounter:!0,Schema:{type:"object",requred:["Answer"],properties:{Answer:{type:"number",title:"Marque la repuesta que le corresponde","x-display":"radio","x-itemKey":"Id","x-fromData":"context.Items","x-itemTitle":"Name"}}},Options:{context:{Items:[{Id:1,Name:"Si"},{Id:2,Name:"No"}]},selectAll:!0},QuestionnaireId:10,Description:"",Id:134},{Name:"InputUserData",Text:"Es posible que usted pueda ser elegido/a para ser parte de este estudio clínico y recibir una compensación de hasta $400 dólares. Por favor provea su nombre completo, número telefónico y correo electrónico. Un representante de nuestro equipo le contactará dentro de los próximos 3 días hábiles.",Order:3,NextQuestionCondition:null,NextButtonText:"ENVIAR",PrevButtonText:"ANT",ShowNextButton:!0,ShowPrevButton:!1,Images:[],CssStyle:null,ShowCounter:!0,Schema:{type:"object",requred:["phone","name"],properties:{name:{type:"string",title:"Nombre completo:"},email:{type:"string",title:"Correo electrónico (opcional)",pattern:"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,5}","x-options":{messages:{pattern:"Debe tener una direccioon de correo electrónico valida"}}},phone:{type:"string",title:"Número telefónico:",pattern:"^\\s?[\\s(]?\\s?\\d[\\s-]?\\d[\\s-]?\\d[\\s\\-)]?\\s?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s]?$|^[\\+]?\\d\\s?[\\s(]?\\s?\\d[\\s-]?\\s?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?","x-options":{messages:{pattern:"El numero valido debe contener 10 numeros +34 (000) 000 00  or 8 numeros (000) 000 00 "}}}}},Options:{},QuestionnaireId:10,Description:"",Id:135},{Name:"END",Text:"¡Gracias por contactar!",Order:4,NextQuestionCondition:null,NextButtonText:"SIG",PrevButtonText:"ANT",ShowNextButton:!1,ShowPrevButton:!1,Images:[],CssStyle:null,ShowCounter:!0,Schema:{},Options:{},QuestionnaireId:10,Description:"",Id:136},{Name:"END1",Text:"¡Lo sentimos! Apreciamos el tiempo que se ha tomado para responder a nuestro llamado; sin embargo, parece que por el momento no cumple con los requisitos para participar en este estudio clinico en particular.",Order:5,NextQuestionCondition:null,NextButtonText:"SIG",PrevButtonText:"ANT",ShowNextButton:!1,ShowPrevButton:!1,Images:[],CssStyle:null,ShowCounter:!0,Schema:{},Options:{},QuestionnaireId:10,Description:"",Id:137}],Id:10}},p={data:()=>({loadingData:!1}),methods:{GenerateGuid(){var e=(new Date).getTime(),t="undefined"!==typeof performance&&performance.now&&1e3*performance.now()||0;return"xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g,(function(n){var o=16*Math.random();return e>0?(o=(e+o)%16|0,e=Math.floor(e/16)):(o=(t+o)%16|0,t=Math.floor(t/16)),("x"===n?o:3&o|8).toString(16)}))},ExtractItem(e){if(e.Errors&&e.Errors.length>0)this.ShowErrors(e);else if(e.Item)return e.Item},ShowErrors(e){var t;this.PlayError(),console.log(e.Errors),Array.isArray(e.Errors)?e.Errors.forEach((e=>{e.Name&&(t+=e.Name+":"),t+=e.Errors.join("\r\n")})):t=e.Errors,t&&this.ShowAlert(t)},PlayOk(){this.PlaySound("/Content/Sounds/code-found.mp3")},PlayError(){this.PlaySound("/Content/Sounds/bad-beep.mp3")},PlaySound(e){var t=document.location.origin+e,n=new Audio(t);n.autoplay=!0},CloseThis:function(){window.parent.postMessage("CloseUrlForm","*")},ShowAlert(e){console.log(e),alert(e)},fetch:function(e,t,n=null){this.loadingData=!0;const o=e,s=document.location.origin+"/"+t,r=JSON.stringify(n);fetch(s,{method:"POST",mode:"no-cors",body:r,referrer:"unsafe-url",headers:{"Content-Type":"application/json"}}).then((e=>{if(this.loadingData=!1,e)return e.json()})).then((e=>{e&&(console.log(e),o(e)),this.loadingData=!1})).catch((e=>{this.loadingData=!1,console.error("Error while getting server data: "+s+" :"+e)}))},GetDate(e){return e&&e.length>10?e.substring(0,10):e},GetAge(e){var t=new Date(e);if(null==e||""==e)return null;var n=Date.now()-t.getTime(),o=new Date(n),s=o.getUTCFullYear(),r=Math.abs(s-1970);return r}}};var x=n(3599),f=(n(6800),{name:"App",mixins:[p],components:{VJsf:x.Z},data:()=>({Languages:c,Lang:c[0],userAnsverCounter:0,currentModel:{answerModel:{},valid:null},models:[],Questionnarie:{Questions:[]},currentQuestion:{Id:0,Name:null,Options:{},Schema:{}},session:SessionId}),computed:{enableNext(){return 0!=this.currentModel.valid&&Object.keys(this.currentModel.answerModel).length>0},PrevButtonText(){return this.currentQuestion.PrevButtonText?this.currentQuestion.PrevButtonText:"Prev"},NextButtonText(){return this.currentQuestion.NextButtonText?this.currentQuestion.NextButtonText:"Next"},currentQuestionCssStyle(){if(this.currentQuestion.CssStyle)return this.currentQuestion.CssStyle},headerCssStyle(){return this.currentQuestion.headerCssStyle?this.currentQuestion.headerCssStyle:this.Questionnarie.CssStyle?this.Questionnarie.CssStyle:void 0}},watch:{enableNext(e){e&&setTimeout((()=>{this.tryNext()}),500)},Lang(e){const t=this.currentQuestion.Id;"English"==e.name?this.SetQuestions(h):this.SetQuestions(m);let n=this.Questionnarie.Questions.find((e=>e.Id==t));this.SelectQuestion(n)}},methods:{tryNext(){if(this.enableNext){const e=new RegExp("QUESTION 1*");if(e.test(this.currentQuestion.Name))return void this.SelectNextQuestion();if("Images"==this.currentQuestion.Name)return void this.SelectNextQuestion()}},validateForm(){this.$refs.form.validate()},SelectQuestion(e){if(0==e.Id)return;let t=this.models.find((e=>e.QuestionId==this.currentQuestion.Id));t&&(t=this.currentModel),this.currentQuestion=e,this.notValidOptions=null,this.notValidSchema=null,this.SetModel()},SelectNextQuestion(){let e;if(this.currentQuestion.NextQuestionCondition){var t=this.currentQuestion.NextQuestionCondition.replace("$Answer",this.currentModel.answerModel.Answer),n=new Function(t),o=n();e=this.Questionnarie.Questions.find((e=>e.Name==o))}else e=this.Questionnarie.Questions.find((e=>e.Order==this.currentQuestion.Order+1));e&&this.SelectQuestion(e),this.SaveAnsver()},SelectPrevQuestion(){const e=this.Questionnarie.Questions.find((e=>e.Order==this.currentQuestion.Order-1));e&&this.SelectQuestion(e),console.log(e)},SetModel(){let e=this.models.find((e=>e.QuestionId==this.currentQuestion.Id));e||(e={QuestionId:this.currentQuestion.Id,answerModel:{},valid:null},this.models.push(e)),this.currentModel=e},ok(e){e.Errors&&e.Errors.length>0&&this.ShowErrors(e)},GetQuestions(){this.fetch(this.SetQuestions,"Questionnaire/Get?id="+Id)},SetQuestions(e){e.Errors&&this.ShowErrors(e),e.Item&&(this.Questionnarie=e.Item,this.Questionnarie.Questions.length>0&&(console.log(this.Questionnarie.Questions[0]),this.SelectQuestion(this.Questionnarie.Questions[0])))},SaveAnsver(){4==this.models.length&&this.userAnsverCounter++,this.fetch(this.ok,"Questionnaire/SaveAnsvers?questionnaireId="+Id+"&sessionId="+this.session+"_"+this.userAnsverCounter,this.models)}},mounted:function(){Lang?"en"==Lang?this.SetQuestions(h):this.SetQuestions(m):this.SetQuestions(d)}}),g=f,y=n(1001),S=(0,y.Z)(g,u,l,!1,null,null,null),N=S.exports,Q=n(1858);o.ZP.use(Q.Z);var I=new Q.Z({icons:{iconfont:"mdi"}});o.ZP.config.productionTip=!1,new o.ZP({vuetify:I,render:e=>e(N)}).$mount("#app")}},t={};function n(o){var s=t[o];if(void 0!==s)return s.exports;var r=t[o]={exports:{}};return e[o].call(r.exports,r,r.exports,n),r.exports}n.m=e,function(){var e=[];n.O=function(t,o,s,r){if(!o){var i=1/0;for(d=0;d<e.length;d++){o=e[d][0],s=e[d][1],r=e[d][2];for(var a=!0,u=0;u<o.length;u++)(!1&r||i>=r)&&Object.keys(n.O).every((function(e){return n.O[e](o[u])}))?o.splice(u--,1):(a=!1,r<i&&(i=r));if(a){e.splice(d--,1);var l=s();void 0!==l&&(t=l)}}return t}r=r||0;for(var d=e.length;d>0&&e[d-1][2]>r;d--)e[d]=e[d-1];e[d]=[o,s,r]}}(),function(){n.n=function(e){var t=e&&e.__esModule?function(){return e["default"]}:function(){return e};return n.d(t,{a:t}),t}}(),function(){n.d=function(e,t){for(var o in t)n.o(t,o)&&!n.o(e,o)&&Object.defineProperty(e,o,{enumerable:!0,get:t[o]})}}(),function(){n.g=function(){if("object"===typeof globalThis)return globalThis;try{return this||new Function("return this")()}catch(e){if("object"===typeof window)return window}}()}(),function(){n.o=function(e,t){return Object.prototype.hasOwnProperty.call(e,t)}}(),function(){n.r=function(e){"undefined"!==typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})}}(),function(){var e={143:0};n.O.j=function(t){return 0===e[t]};var t=function(t,o){var s,r,i=o[0],a=o[1],u=o[2],l=0;if(i.some((function(t){return 0!==e[t]}))){for(s in a)n.o(a,s)&&(n.m[s]=a[s]);if(u)var d=u(n)}for(t&&t(o);l<i.length;l++)r=i[l],n.o(e,r)&&e[r]&&e[r][0](),e[r]=0;return n.O(d)},o=self["webpackChunkquestinnaire"]=self["webpackChunkquestinnaire"]||[];o.forEach(t.bind(null,0)),o.push=t.bind(null,o.push.bind(o))}();var o=n.O(void 0,[998],(function(){return n(6793)}));o=n.O(o)})();
//# sourceMappingURL=app.js.map