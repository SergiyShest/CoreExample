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

export const Languages=[
  {
    name: "English",
    flag: "/Content/Images/englishFlag.png"
  },
  {
    name: "Spanish",
    flag: "/Content/Images/spanishFlag.png"
  },
]



export const dataEn = {
  Item:{
    "Name": "English with images2",
    "Text": "Answer a few questions and see if you or someone you care for may be eligible",
    "Main": true,
    "CssStyle": {
      "font-family": "Arial",
      "font-size": "25px",
      "background": "white"
    },
    "Questions": [
      {
        "Name": "QUESTION 1",
        "Text": "Do you have itching on your skin?",
        "Order": 1,
        "NextQuestionCondition": "if($Answer == 1) return 'Images'; else return 'END1';",
        "NextButtonText": "NEXT",
        "PrevButtonText": "PREV",
        "ShowNextButton": false,
        "ShowPrevButton": false,
        "Images": [],
        "CssStyle": null,
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
                "Name": "Yes"
              },
              {
                "Id": 2,
                "Name": "No"
              }
            ]
          },
          "selectAll": true
        },
        "QuestionnaireId": 10,
        "Description": "",
        "Id": 133
      },
      {
        "Name": "Images",
        "Text": "From the following images, can you identify any that resemble the current condition of your skin?",
        "Order": 2,
        "NextQuestionCondition": "if($Answer == 1) return 'InputUserData'; else return 'END1';",
        "NextButtonText": "NEXT",
        "PrevButtonText": "PREV",
        "ShowNextButton": false,
        "ShowPrevButton": true,
        "Images": [
          "/Content/Images/item1.PNG",
          "/Content/Images/item2.PNG",
          "/Content/Images/item4.PNG",
          "/Content/Images/item3.PNG",
          "/Content/Images/item5.PNG",
          "/Content/Images/item6.PNG",
        ],
        "CssStyle": null,
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
                "Name": "Yes"
              },
              {
                "Id": 2,
                "Name": "No"
              }
            ]
          },
          "selectAll": true
        },
        "QuestionnaireId": 10,
        "Description": "",
        "Id": 134
      },
      {
        "Name": "InputUserData",
        "Text": "It is possible that you may be chosen to be part of this clinical study and receive compensation of up to $400 dollars. Please provide your full name, phone number, and email. A representative from our team will contact you within the next 3 business days.",
        "Order": 3,
        "NextQuestionCondition": null,
        "NextButtonText": "SEND",
        "PrevButtonText": "PREV",
        "ShowNextButton": true,
        "ShowPrevButton": false,
        "Images": [],
        "CssStyle": null,
        "Schema": {
          "type": "object",
          "requred": [
            "phone",
            "name"
          ],
          "properties": {
            "name": {
              "type": "string",
              "title": "Full name:"
            },
            "email": {
              "type": "string",
              "title": "Email (optional):",
              "pattern": "[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,5}",
              "x-options": {
                "messages": {
                  "pattern": "You must have a valid email address"
                }
              }
            },
            "phone": {
              "type": "string",
              "title": "Phone number:",
              "pattern": "^\\s?[\\s(]?\\s?\\d[\\s-]?\\d[\\s-]?\\d[\\s\\-)]?\\s?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s]?$|^[\\+]?\\d\\s?[\\s(]?\\s?\\d[\\s-]?\\s?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?",
              "x-options": {
                "messages": {
                  "pattern": "The valid number must contain 10 numbers +34 (000) 000 00 or 8 numbers (000) 000 00 "
                }
              }
            }
          }
        },
        "Options": {},
        "QuestionnaireId": 10,
        "Description": "",
        "Id": 135
      },
      {
        "Name": "END",
        "Text": "Thank you for contacting!",
        "Order": 4,
        "NextQuestionCondition": null,
        "NextButtonText": "NEXT",
        "PrevButtonText": "PREV",
        "ShowNextButton": false,
        "ShowPrevButton": false,
        "Images": [],
        "CssStyle": null,
        "Schema": {},
        "Options": {},
        "QuestionnaireId": 10,
        "Description": "",
        "Id": 136
      },
      {
        "Name": "END1",
        "Text": "We apologize! We appreciate the time you took to answer our questions. However, it appears that you are not eligible for this particular clinical trial at this time.",
        "Order": 5,
        "NextQuestionCondition": null,
        "NextButtonText": "NEXT",
        "PrevButtonText": "PREV",
        "ShowNextButton": false,
        "ShowPrevButton": false,
        "Images": [],
        "CssStyle": null,
        "Schema": {},
        "Options": {},
        "QuestionnaireId": 10,
        "Description": "",
        "Id": 137
      }
    ],
    "Id": 10
  }
}
