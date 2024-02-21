const contMenuId='[aria-rowindex="1"] > .dx-command-select'

it('Notes dialog open test', () => {
   
    cy.wait(500);
    cy.get(contMenuId).rightclick(); 
    cy.get(':nth-child(1) > .dx-item > .dx-item-content > .dx-menu-item-text').click(); // поднятие меню
    cy.get('.dx-toolbar-before > .dx-item > .dx-item-content > div').contains('Notes'); // Проверка наличия в заговоловке надписи Notes
     cy.get('.dx-closebutton > .dx-button-content > .dx-icon').click(); // Закрытие меню    

});

it('Проверка наличия заголовка', () => {
    cy.contains('H1', 'responses')

})

it('проверка фильра на наличие записей', () => {

    cy.get('#d1From').type('1023-12-20').blur();
    cy.get('#d1To').type('2024-12-31').blur();
    cy.get('#findButton').click();
    cy.wait(1000);

    cy.get('.dx-info').invoke('text').then((text) => {
        // регулярное выражение для извлечения числа из текста Page 1 of 1 (0 items)
        const matches = text.match(/\((\d+)/);

        // Проверяем, что удалось извлечь число и оно больше 0
        const extractedNumber = matches && parseInt(matches[1], 10);
        cy.wrap(extractedNumber).should('be.gt', 0);
    })

})

it('Проверка фильтра на отсутствие записей', () => {

    cy.get('#d1From').type('2000-12-31').blur();//unreal date tnere no records
    cy.get('#d1To').type('2000-12-31').blur();
    cy.get('#findButton').click();
    cy.wait(1000);
    cy.get('.dx-info').invoke('text').then((text) => {
        //регулярное выражение для извлечения числа из текста Page 1 of 1 (0 items)
        const matches = text.match(/\((\d+)/);

        // Проверяем, что удалось извлечь число и оно 0
        const extractedNumber = matches && parseInt(matches[1], 10);
        cy.wrap(extractedNumber).should('eq', 0);
    })
})