
describe('Тест Notes', () => {

    it('logon', () => {
        cy.visit('https://localhost:7297/AnswerNotes?id=1')
       
        cy.get('#Email_id').type("Shest_akow@rambler.ru").blur();
        cy.get('#Password_id').type("Adm").blur();
        cy.get('#LogonBtn_id').click();

    })

    it('проверка сохранения записи', () => {
        cy.on('uncaught:exception', (err, runnable) => {
            const errorText = err.message;            // Получите текст ошибки
            cy.log('JavaScript error:', errorText);// Выведите текст ошибки в консоль Cypress
            throw new Error('Обнаружена JavaScript ошибка: ' + errorText);// Вызовите ошибку, чтобы тест был отмечен как проваленный
        });

        cy.visit('https://localhost:7297/AnswerNotes?id=1')
        cy.get('#Email_id').type("Shest_akow@rambler.ru").blur();
        cy.get('#Password_id').type("Adm").blur();
        cy.get('#LogonBtn_id').click();
        cy.wait(2000);
        cy.visit('https://localhost:7297/AnswerNotes?id=1')
        const text = Date()
        cy.get('#note_40').clear().type(text).blur();
        cy.get('#saveBtn_40').click();
        cy.wait(2000);
        cy.get('#note_40').should('have.value', text);
    })
})




