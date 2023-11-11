
export const dataEs = {
  Item:{
    "Name": "Spanich with images2",
    "Text": "Responda algunas preguntas y vea si usted o alguien a quien cuida puede ser elegible",
    "Main": true,
    "CssStyle": {
      "font-family": "Arial",
      "font-size": "25px",
      "background": "white"
    },
    "Questions": [
      {
        "Name": "QUESTION 1",
        "Text": "¿Tienes picazón en la piel?",
        "Order": 1,
        "NextQuestionCondition": "if($Answer == 1) return 'Images'; else return 'END1';",
        "NextButtonText": "SIG",
        "PrevButtonText": "ANT",
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
              "title": "Marque la repuesta que le corresponde",
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
        "QuestionnaireId": 10,
        "Description": "",
        "Id": 133
      },
      {
        "Name": "Images",
        "Text": "¿De las siguientes imágenes, puede identificar alguna que se parezca a la condición actual de su piel?",
        "Order": 2,
        "NextQuestionCondition": "if($Answer == 1) return 'InputUserData'; else return 'END1';",
        "NextButtonText": "SIG",
        "PrevButtonText": "ANT",
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
              "title": "Marque la repuesta que le corresponde",
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
        "QuestionnaireId": 10,
        "Description": "",
        "Id": 134
      },
      {
        "Name": "InputUserData",
        "Text": "Es posible que usted pueda ser elegido/a para ser parte de este estudio clínico y recibir una compensación de hasta $400 dólares. Por favor provea su nombre completo, número telefónico y correo electrónico. Un representante de nuestro equipo le contactará dentro de los próximos 3 días hábiles.",
        "Order": 3,
        "NextQuestionCondition": null,
        "NextButtonText": "ENVIAR",
        "PrevButtonText": "ANT",
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
              "title": "Nombre completo:"
            },
            "email": {
              "type": "string",
              "title": "Correo electrónico (opcional)",
              "pattern": "[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,5}",
              "x-options": {
                "messages": {
                  "pattern": "Debe tener una direccioon de correo electrónico valida"
                }
              }
            },
            "phone": {
              "type": "string",
              "title": "Número telefónico:",
              "pattern": "^\\s?[\\s(]?\\s?\\d[\\s-]?\\d[\\s-]?\\d[\\s\\-)]?\\s?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s]?$|^[\\+]?\\d\\s?[\\s(]?\\s?\\d[\\s-]?\\s?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?\\d[\\s-]?",
              "x-options": {
                "messages": {
                  "pattern": "El numero valido debe contener 10 numeros +34 (000) 000 00  or 8 numeros (000) 000 00 "
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
        "Text": "¡Gracias por contactar!",
        "Order": 4,
        "NextQuestionCondition": null,
        "NextButtonText": "SIG",
        "PrevButtonText": "ANT",
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
        "Text": "¡Lo sentimos! Apreciamos el tiempo que se ha tomado para responder a nuestro llamado; sin embargo, parece que por el momento no cumple con los requisitos para participar en este estudio clinico en particular.",
        "Order": 5,
        "NextQuestionCondition": null,
        "NextButtonText": "SIG",
        "PrevButtonText": "ANT",
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
