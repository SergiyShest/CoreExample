const contMenuId='[aria-rowindex="1"] > .dx-command-select'

it('Edit record test', () => {
   
    cy.wait(500);
    cy.get(contMenuId).rightclick(); 
    cy.get(':nth-child(1) > .dx-item > .dx-item-content > .dx-menu-item-text').click(); // поднятие меню
    cy.get('.dx-toolbar-before > .dx-item > .dx-item-content > div').contains('Notes'); // Проверка наличия в заговоловке надписи Notes
     cy.get('.dx-closebutton > .dx-button-content > .dx-icon').click(); // Закрытие меню    

});