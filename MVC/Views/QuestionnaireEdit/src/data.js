export const limitedAge= {
    type: 'integer',
    title: 'Age must be between 0 and 120',
    minimum: 0,
    maximum: 120
}



export const  data =  {
    "Item": {
        "Id": 1,
        "Name": "Answer a few questions and see if you or someone you are looking after may be eligible",
        "Code": "QUESTIONNAIRE",

        "Questions": [
            {   "Id": 1,
                "Order":1,
                "Name": 'Do you have a current diagnosis of Atopic Dermatitis (Atopic Eczema)?',
                "Code": "QUESTION",
                "Description": "Atopic Dermatitis, also known as Atopic Eczema, is a type of inflammation of the skin, resulting in itchy, red, swollen, and cracked skin.",
                "Schema": {
                    "type": "object",
                     requred: ["ListProp"],
                    "properties": {
                        "ListProp": {
                            "type": "number",
                            "title": "Choose the right Ansver",
                            "x-display": "radio",
                            "x-fromData": "context.Items",
                            "x-itemKey": "Id",
                            "x-itemTitle": "Name",
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
                "QuestionnaireId": 1,

            },
            {    "Id": 2,"Order":2,
                "Name": "How old are yow",
                "Code": "QUESTION",
                "Description": "",

                "Schema": {
                        type: "object",
                        requred: ["age"],
                        properties: {
                          age: {
                            type: "string",
                            format: "number",
                            title: "Input your age please",
                            description: "Age must be between 0 and 120",
                            minimum: 0,
                            maximum: 120
                          },
                        },

                },
                "Options": { },
                "QuestionnaireId": 1,


            }
            ,
            {"Id": 3,
                "Order":3,
                "Name": 'When where yoy diagnosed Atopic Dermatitis (Atopic Eczema)?',
                "Code": "QUESTION",
                "Description": "",
                "Schema": {
                    "type": "object",
                    "properties": {
                        "ListProp": {
                            "type": "number",
                            "title": "Choose the right Ansver",
                            "x-display": "radio",
                            "x-fromData": "context.Items",
                            "x-itemKey": "Id",
                            "x-itemTitle": "Name",
                            "description": ""
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
                "QuestionnaireId": 1
            },
            {"Id": 4,
            "Order":4,
            "Name": "How would you describe your current Atopic Dermatitis (Atopic Eczema)?",
            "Code": "QUESTION",
            "Description": "",
            "Schema": {
                "type": "object",
                "properties": {
                    "ListProp": {
                        "type": "number",
                        "title": "Choose the right Ansver",
                        "x-display": "radio",
                        "x-fromData": "context.Items",
                        "x-itemKey": "Id",
                        "x-itemTitle": "Name",
                        "description": ""
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
                          {   "Id": 4,
                            "Name": "Severe disease: Widespread areas of dry, rough and red skin. There is a lot of cracking, flaking of the skin, weeping or oozing of fluid, or bleeding. The skin itches all the time. It has severe"
                        }
                    ]
                },
                "selectAll": true
            },
            "QuestionnaireId": 1,

        },           
        {"Id": 5,
        "Order":5,
        "Name": "Have you applied corticosteroids (also called 'steroids') for your Atopic Dermatitis (Atopic Eczema) within the last 6 months?",
        "Code": "QUESTION",
        "Description": "There are many different types of these medications available that differ in strength. Some require a prescription from a doctor, but others do not. They can be either ointments, creams, lotions, or sprays. Common types of topical corticosteroids (steroids) are for example: Alclometasone (brand name example: Aclovate), Betamethasone (brand name examples: Diprolene, Beta-Val), Clobetasol (brand name example: Temovate)"
        ,
        "Schema": {
            "type": "object",
            "properties": {
                "ListProp": {
                    "type": "number",
                    "title": "Choose the right Ansver",
                    "x-display": "radio",
                    "x-fromData": "context.Items",
                    "x-itemKey": "Id",
                    "x-itemTitle": "Name",
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
                    {   "Id": 4,
                        "Name": "No, never"
                    },
                    {   "Id": 5,
                        "Name": "Unsure"
                    }
                ]
            },
            "selectAll": true
        },
        "QuestionnaireId": 1,

    }             ,
            {   "Id": 6,
                "Order":6,
                "Name": "Do or did these topical corticosteroids (also called 'steroids') help with your Atopic Dermatitis (Atopic Eczema) symptoms (like dry skin, itching, redness, or skin thickening/cracking?",
                "Code": "QUESTION",
                "Description": "",
                "Schema": {
                    "type": "object",
                    "properties": {
                        "ListProp": {
                            "type": "number",
                            "title": "Choose the right Ansver",
                            "x-display": "radio",
                            "x-fromData": "context.Items",
                            "x-itemKey": "Id",
                            "x-itemTitle": "Name",
                            "description": ""
                        }
                    }
                },
                "Options": {
                    "context": {
                        "Items": [
                            {
                                "Id": 1,
                                "Name": "They are/were helping very well."
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
                "QuestionnaireId": 1,

            },
            {   "Id": 7,
                "Order":7,
                "Name": "Are you currently pregnant, breastfeeding or planning to become pregnant in the next 6 months?",
                "Code": "QUESTION",
                "Description": "",
                "Schema": {
                    "type": "object",
                    "properties": {
                        "ListProp": {
                            "type": "number",
                            "title": "Choose the right Ansver",
                            "x-display": "radio",
                            "x-fromData": "context.Items",
                            "x-itemKey": "Id",
                            "x-itemTitle": "Name",
                            "description": ""
                        }
                    }
                },
                "Options": {
                    "context": {
                        "Items": [
                            {
                                "Id": 1,
                                "Name": "Yes."
                            },
                            {
                                "Id": 2,
                                "Name": "No"
                            },
                            {
                                "Id": 2,
                                "Name": "Unsure"
                            }
                        ]
                    },
                    "selectAll": true
                },
                "QuestionnaireId": 1,

            }
        ]
 
    }
}