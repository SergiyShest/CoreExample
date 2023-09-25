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
    "Name": "Questionniare right new",
    "Text": "Answer a few questions and see if you or someone you are looking after may be eligible",
    "Main": true,
    "CssStyle": {
      "font-size": "large;",
      "font-kerning": "inherit;"
    },
    "Questions": [
      {
        "Name": "QUESTION 1",
        "Text": "Do you have a current diagnosis of Atopic Dermatitis (Atopic Eczema)?",
        "Order": 1,
        "NextButtonText": null,
        "PrevButtonText": null,
        "CssStyle":{'background-color': 'red'},
        "Schema": {
          "type": "object",
          "requred": [
            "ListProp"
          ],
          "properties": {
            "ListProp": {
              "type": "number",
              "title": "Choose the right answer",
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
                "Name": "No, but currently undergoing medical examinations"
              },
              {
                "Id": 3,
                "Name": "No"
              }
            ]
          },
          "selectAll": true
        },
        "QuestionnaireId": 22,
        "Description": "Atopic Dermatitis, also known as Atopic Eczema, is a type of inflammation of the skin, resulting in itchy, red, swollen, and cracked skin.",
        "Id": 195,

      },
      {
        "Text": "How old are you",
        "Name": "QUESTION",
        "Order": 2,
        "NextButtonText": null,
        "PrevButtonText": null,
        "CssStyle": null,
        "Schema": {
          "type": "object",
          "requred": [
            "age"
          ],
          "properties": {
            "age": {
              "type": "number",
              "title": "Input your age please",
              "format": "number",
              "maximum": 120,
              "minimum": 0,
              "description": "Age must be between 0 and 120"
            }
          }
        },
        "Options": {},
        "QuestionnaireId": 22,
        "Description": "",
        "Id": 196
      },
      {
        "Text": "When where you diagnosed Atopic Dermatitis (Atopic Eczema)?",
        "Name": "QUESTION",
        "Order": 3,
        "NextButtonText": null,
        "PrevButtonText": null,
        "CssStyle": null,
        "Schema": {
          "type": "object",
          "properties": {
            "ListProp": {
              "type": "number",
              "title": "Choose the right answer",
              "x-display": "radio",
              "x-itemKey": "Id",
              "x-fromData": "context.Items",
              "description": "",
              "x-itemTitle": "Name"
            }
          }
        },
        "Options": {
          "context": {
            "Items": [
              {
                "Id": 1,
                "Name": "Less then 6 Months ago"
              },
              {
                "Id": 2,
                "Name": "Over 6 Months ago"
              }
            ]
          },
          "selectAll": true
        },
        "QuestionnaireId": 22,
        "Description": "",
        "Id": 197
      },
      {
        "Text": "How would you describe your current Atopic Dermatitis (Atopic Eczema)?",
        "Name": "QUESTION",
        "Order": 4,
        "NextButtonText": null,
        "PrevButtonText": null,
        "CssStyle": null,
        "Schema": {
          "type": "object",
          "properties": {
            "disease": {
              "type": "number",
              "title": "Choose the right answer",
              "x-display": "radio",
              "x-itemKey": "Id",
              "x-fromData": "context.Items",
              "description": "",
              "x-itemTitle": "Name"
            }
          }
        },
        "Options": {
          "context": {
            "Items": [
              {
                "Id": 1,
                "Name": "No active disease: I currently have no symptoms"
              },
              {
                "Id": 2,
                "Name": "Mild disease: Areas of dry, rough and red skin. The skin itches occasionally. There may be some cracking, flaking of the skin, weeping or oozing of fluid, or bleeding. It has little  impact on everyday activities, such as sleeping, getting dressed, showering and  exercising."
              },
              {
                "Id": 3,
                "Name": "Moderate disease: More extensive areas of dry, rough   and red skin. There is also cracking, flaking of the skin, weeping or oozing of fluid, or bleeding. The skin itches atextensive areas of dry, rough and red skin. There is also cracking, flaking of the skin, weeping or oozing of fluid, or bleeding. The skin itches at least 3-4 days a week. It has moderate impact on everyday activities, such as sleeping, getting dressed, showering and exercising."
              },
              {
                "Id": 4,
                "Name": "Severe disease: Widespread areas of dry, rough and red skin. There is a lot of cracking, flaking of the skin, weeping or oozing of fluid, or bleeding. The skin itches all the time. It has severe impact on everyday activities, such as sleeping, getting dressed, showering and exercising."
              }
            ]
          },
          "selectAll": true
        },
        "QuestionnaireId": 22,
        "Description": "",
        "Id": 198
      },
      {
        "Text": "Have you applied corticosteroids (also called 'steroids') for your Atopic Dermatitis (Atopic Eczema) within the last 6 months?",
        "Name": "QUESTION",
        "Order": 5,
        "NextButtonText": null,
        "PrevButtonText": null,
        "CssStyle": null,
        "Schema": {
          "type": "object",
          "properties": {
            "using": {
              "type": "number",
              "title": "Choose the right answer",
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
                "Name": "Yes, currently using these"
              },
              {
                "Id": 2,
                "Name": "Yes, but stopped in the last 6 months"
              },
              {
                "Id": 3,
                "Name": "Yes, but stopped more than 6 months ago"
              },
              {
                "Id": 4,
                "Name": "No, never"
              },
              {
                "Id": 5,
                "Name": "Unsure"
              }
            ]
          },
          "selectAll": true
        },
        "QuestionnaireId": 22,
        "Description": "There are many different types of these medications available that differ in strength. Some require a prescription from a doctor, but others do not. They can be either ointments, creams, lotions, or sprays. Common types of topical corticosteroids (steroids) are for example: Alclometasone (brand name example: Aclovate), Betamethasone (brand name examples: Diprolene, Beta-Val), Clobetasol (brand name example: Temovate)",
        "Id": 199
      },
      {
        "Text": "Do or did these topical corticosteroids (also called 'steroids') help with your Atopic Dermatitis (Atopic Eczema) symptoms (like dry skin, itching, redness, or skin thickening/cracking?",
        "NameText": "QUESTION",
        "Order": 6,
        "NextButtonText": null,
        "PrevButtonText": null,
        "CssStyle": null,
        "Schema": {
          "type": "object",
          "properties": {
            "Helping": {
              "type": "number",
              "title": "Choose the right answer",
              "x-display": "radio",
              "x-itemKey": "Id",
              "x-fromData": "context.Items",
              "description": "",
              "x-itemTitle": "Name"
            }
          }
        },
        "Options": {
          "context": {
            "Items": [
              {
                "Id": 1,
                "Name": "They are/were helping very well"
              },
              {
                "Id": 2,
                "Name": "They are/were helping, but there are still symptoms"
              },
              {
                "Id": 3,
                "Name": "They are/were helping, but there are side effects"
              },
              {
                "Id": 4,
                "Name": "They are/were helping, but there are still symptoms and side effects"
              },
              {
                "Id": 5,
                "Name": "They are hardly helping or not helping at all"
              }
            ]
          },
          "selectAll": true
        },
        "QuestionnaireId": 22,
        "Description": "",
        "Id": 200
      },
      {
        "Text": "Are you currently pregnant, breastfeeding or planning to become pregnant in the next 6 months?",
        "Name": "QUESTION",
        "Order": 7,
        "NextButtonText": "NEXT",
        "PrevButtonText": null,
        "CssStyle": null,
        "Schema": {
          "type": "object",
          "properties": {
            "ListProp": {
              "type": "number",
              "title": "Choose the right answer",
              "x-display": "radio",
              "x-itemKey": "Id",
              "x-fromData": "context.Items",
              "description": "",
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
              },
              {
                "Id": 3,
                "Name": "Unsure"
              }
            ]
          },
          "selectAll": true
        },
        "QuestionnaireId": 22,
        "Description": "",
        "Id": 201
      },
      {
        "Text": "If you need to consult a doctor, please leave your phone number and we will call you back.",
        "Name": "QUESTION",
        "Order": 8,
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
        "QuestionnaireId": 22,
        "Description": "",
        "Id": 202
      },
      {
        "Text": "Thanks for contacting.",
        "Name": "QUESTION 9",
        "Order": 9,
        "NextButtonText": null,
        "PrevButtonText": null,
        "CssStyle": null,
        "Schema": {},
        "Options": {},
        "QuestionnaireId": 22,
        "Description": "",
        "Id": 203
      }
    ],
    "Id": 48
  }
};
