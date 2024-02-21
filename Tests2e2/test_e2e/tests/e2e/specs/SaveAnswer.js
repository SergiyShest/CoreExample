



it('test form', () => {
    cy.visit('https://localhost:7297')
    var text=Date()
    cy.get('#C1BugNEwfxfySWID').type(text).blur();
    cy.get('#BQKFvD9nsMMqM0X1').type('000 000 00 00').blur();
    cy.get('#pAv7roTMaPY3tccR_').type('test@test.com').blur();
    cy.get('#EyJo5wLcyyavAVVV').click();
    cy.url().should('eq', 'https://localhost:7297/Questionnaire2/End'); 
    cy.visit('https://localhost:7297/home')
    cy.get('[aria-colindex="3"] > .dx-editor-with-menu > .dx-editor-container > .dx-textbox > .dx-texteditor-container > .dx-texteditor-input').type(text).blur();
    cy.wait(1000);

    cy.get('.dx-info').invoke('text').then((text) => {
        //регулярное выражение для извлечения числа из текста Page 1 of 1 (1 items)
        const matches = text.match(/\((\d+)/);

        // Проверяем, что удалось извлечь число и оно 1
        const extractedNumber = matches && parseInt(matches[1], 10);
        cy.wrap(extractedNumber).should('eq', 1);
    })
})

