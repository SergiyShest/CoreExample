
export const selectLang = {
  Item:{
    "Name": "SelectLang",
    "Text": "Please choose the language",
    "Main": true,
    "CssStyle": {
      "font-family": "Arial",
      "font-size": "25px",
      "background": "white"
    },
    "Questions": [
      {
          "Name": "QUESTION 0 ",
            "Text": "Please select your language",
          "Order": 1,
          "NextQuestionCondition": "let lg='es' ;if($Answer == 1){lg='en'}; window.location.href = document.location.origin + '/'+lg;",
          "NextButtonText": "NEXT",
          "PrevButtonText": "PREV",
          "ShowNextButton": false,
          "ShowPrevButton": false,
          "Images": [],
          "CssStyle": null,
          "ShowCounter":false,
          "headerCssStyle": 'display:none',         
          "Schema": {
            "type": "object",
            "requred": [
              "Answer"
            ],
            "properties": {
              "Answer": {
                "type": "number",
                "title": "Check the answer that applies to you",
                "x-display": "radio",
                "x-itemKey": "Id",
                "x-fromData": "context.Items",
                "x-itemTitle": "Name"
              }
            }
          },
          "Options": {
            "context": {
              "Items": [
                {
                  "Id": 1,
                  "Name": "English"
                },
                {
                  "Id": 2,
                  "Name": "Spanish"
                }
              ]
            },
            "selectAll": true
          },
          "QuestionnaireId": 0,
          "Description": "",
          "Id": -1
      }
    ],
    "Id": 10
  }
}
