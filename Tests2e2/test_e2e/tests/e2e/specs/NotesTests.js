
describe('Тест Notes', () => {




it('Add record test', () => {

        cy.visit('https://localhost:7297/AnswerNotes?id=1')
        const text = Date()
        cy.get('#newNote').clear().type(text).blur();//input text
        cy.get('#addBtn').click();//click Add button
        cy.wait(500);
        GetLastItemIdAsinc().then(lastId => {

             cy.get('#note_'+lastId).should('have.value', text);
        });
 
    });

    it('Edit record test', () => {
        cy.visit('https://localhost:7297/AnswerNotes?id=1')
        cy.wait(500);
        GetLastItemIdAsinc().then(lastId => {

            const text = Date()
            cy.get('#note_'+lastId).clear().type(text).blur();
            cy.get('#saveBtn_'+lastId).click();
            cy.get('#note_'+lastId).should('have.value', text);
       });

    });

        it('delete record test', () => {

        cy.visit('https://localhost:7297/AnswerNotes?id=1')
        cy.wait(500);
        GetLastItemIdAsinc().then(lastId => {
            cy.log('-----------------------'+lastId)
            cy.get('#delBtn_'+lastId).click();//click delete button
            cy.wait(500);   
            cy.get('#note_'+lastId).should('not.exist');
        });

    });



})


function GetLastItemIdAsinc(){
    return new Promise((resolve, reject) => {
        let noteId;
        let texAreas = cy.get('#items').find('textarea');
        texAreas.each(($textarea, index, $textareas) => {
            const currentId = $textarea.attr('id');
            if (currentId !== 'newNote') {
                noteId = currentId.replace('note_','');;
            }
            if (index === $textareas.length - 1) {
                resolve(noteId);
            }
        });
    });
}

function GetTextById(id){
    let text;
    cy.get('#'+id).each(($textarea) => {
        text = $textarea.val();
        cy.log(`Textarea text: ${text}`); // Получаем id каждого элемента textarea и выводим в консоль
    });
    return text;
}
