export const limitedAge = {
  type: "integer",
  title: "Age must be between 0 and 120",
  minimum: 0,
  maximum: 120,
};
export const radioOptions = {
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
export const emptyOptions = {
  context: {},
};
export const radioShema = {
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
export const stringShema = {
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
export const dateShema = {
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
export const numShema = {
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

export const radioItem = {
  type: "number",
  title: "Select",
  "x-display": "radio",
  "x-fromData": "context.Items",
  "x-itemKey": "Id",
  "x-itemTitle": "Name",
  description: "",
};

export const stringItem = {
  type: "string",
  title: "Input string please",
  description: "Help",
};
export const dateItem = {
  type: "string",
  format: "date",
  title: "Input date please",
  description: "Help",
};
export const numItem = {
  type: "string",
  format: "number",
  title: "Input numper please",
  description: "Help",
};
export const data = {
  Item:{
    "Name": "Spanich",
    "Text": "Responda algunas preguntas y vea si usted o alguien a quien cuida puede ser elegible",
    "Main": true,
    "CssStyle": {
      "font-size": "large;",
      "font-kerning": "inherit;"
    },
    "Questions": [
      {
        "Name": "QUESTION 1",
        "Text": "?Tienes picaz?n en la piel?",
        "Order": 1,
        "ShowNexButton":true,
        "NextQuestionCondition":"if($Answer == 1) return 'Images'; else return 'END1';",
        "NextButtonText": null,
        "PrevButtonText": null,
        "CssStyle": null,
        "Schema": {
          "type": "object",
          "requred": [
            "Answer"
          ],
          "properties": {
            "Answer": {
              "type": "number",
              "title": "Elegir la respuesta correcta",
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
                "Name": "Si"
              },
              {
                "Id": 2,
                "Name": "No"
              }
            ]
          },
          "selectAll": true
        },
        "QuestionnaireId": 57,
        "Description": "",
        "Id": 416
      },
      {
        "Name": "Images",
        "Text": "¿De las siguientes imágenes, puede identificar alguna que se parezca a la condición actual de su piel?",
        "Order": 2,
        "NextQuestionCondition":"if($Answer == 1) return 'InputUserData'; else return 'END1';",
        "ShowNexButton":true,
        "NextButtonText": null,
        "PrevButtonText": null,
        "CssStyle": null,
        "Schema": {
          "type": "object",
          "requred": [
            "Answer"
          ],
          "properties": {
            "Answer": {
              "type": "number",
              "title": "Elegir la respuesta correcta",
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
                "Name": "Si"
              },
              {
                "Id": 2,
                "Name": "No"
              }
            ]
          },
          "selectAll": true
        },
        "Images":["/Content/Images/item1.PNG","/Content/Images/item2.PNG","/Content/Images/item3.PNG","/Content/Images/item4.PNG"],
        "QuestionnaireId": 57,
        "Description": "",
        "Id": 417
      },

      {
        "Name": "InputUserData",
        "Text": "Es posible que usted pueda ser elegido/a para ser parte de este estudio cl?nico y recibir una compensaci?n de hasta $400 d?lares. Por favor provea su nombre completo, n?mero telef?nico y correo electr?nico. Un representante de nuestro equipo le contactar? dentro de los pr?ximos 3 d?as h?biles.",
        "Order": 3,
        "NextButtonText": "Finish",
        "PrevButtonText": null,
        "CssStyle": null,
        "Schema": {
          "type": "object",
          "requred": [
            "phone"
          ],
          "properties": {
            "phone": {
              "type": "string",
              "title": "Input your phone please",
              "pattern": "^\\s?[\\s(]?\\s?\\d[\\s-]?\\d[\\s-]?\\d[\\s\\-)]?\\s?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s]?$|^[\\+]?\\d\\s?[\\s(]?\\s?\\d[\\s-]?\\d[\\s-]?\\d[\\s\\-)]?\\s?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s]?$",
              "x-options": {
                "messages": {
                  "pattern": "Valid number mast be contains 11 numbers +1 (000) 000 00 00 or 10 numbers (000) 000 00 00 "
                }
              }
            }
          }
        },
        "Options": {},
        "QuestionnaireId": 57,
        "Description": "",
        "Id": 419
      },
      {
        "Name": "END",
        "Text": "?Gracias por contactar!",
        "Order": 4,
        "ShowNexButton":false,
        "NextButtonText": null,
        "PrevButtonText": null,
        "CssStyle": null,
        "Schema": {},
        "Options": {},
        "QuestionnaireId": 57,
        "Description": "",
        "Id": 420
      },
      {
        "Name": "END1",
        "Text": "!Lo sentimos! Apreciamos el tiempo que se ha tomado para responder a nuestro llamado; sin embargo, parece que por el momento no cumple con los requisitos para participar en este estudio cl?nico en particular.",
        "Order": 5,
        "NextButtonText": null,
        "ShowNexButton":false,
        "PrevButtonText": null,
        "CssStyle": null,
        "Schema": {},
        "Options": {},
        "QuestionnaireId": 57,
        "Description": "",
        "Id": 418
      },
    ],
    "Id": 57
  }
};
