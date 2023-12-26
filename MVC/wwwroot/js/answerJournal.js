function copyToClipboard (str ) {
    const el = document.createElement('textarea');
    el.value = str;
    document.body.appendChild(el);
    el.select();
    document.execCommand('copy');
    document.body.removeChild(el);
};

//добавление пункта меню копировать
function contextMenuPreparing(e) {

    if (e.target == 'header') {
        var dataGrid = $('#grid').dxDataGrid('instance');
        var selectedRows = dataGrid.getSelectedRowsData();
        if (selectedRows.length > 0) {
            var ind = e.column.dataField;
            e.items.push({
                text: "Copy",
                onItemClick: function () {
                    var res = '';
                    for (i = 0; i < selectedRows.length; i++) { res += selectedRows[i][ind] + ',' }
                    copyToClipboard(res.trim(','));
                }
            });
        }
    }
}

$(function () {

//получение строки фильтров
function processFilter(dataGridInstance, filter) {
    if ($.isArray(filter)) {
        if ($.isFunction(filter[0])) {
            filter[0] = getColumnFieldName(dataGridInstance, filter[0]);
        }
        else {
            for (var i = 0; i < filter.length; i++) {
                processFilter(dataGridInstance, filter[i]);
            }
        }
    }
}

//$("#buttonContainer").dxButton({
//    text: "проверка фильтра",
//    onClick: function (e) {
//        var grid = $("#grid").dxDataGrid("instance"),
//        filter = grid.getCombinedFilter();
//        processFilter(grid, filter);
//        alert(filter);
//    }
    //}).dxButton("instance");

//поиск имени конолки по фильтру
function getColumnFieldName(dataGridInstance, getter) {
    var column,
        i;

    if ($.isFunction(getter)) {
        for (i = 0; i < dataGridInstance.columnCount(); i++) {
            column = dataGridInstance.columnOption(i);
            if (column.calculateCellValue.guid === getter.guid) {
                return column.dataField;
            }
        }
    }
    else {
        return getter;
    }
}


    //установка источника данных
    





    $("#grid").dxDataGrid({
        remoteOperations: { paging: true, filtering: true, sorting: true, grouping: true, summary: true, groupPaging: true },
        //dataSource: DevExpress.data.AspNet.createStore({
        //    key: "id",
        //    loadUrl: "../AnswerJournal/Get",
        //}),

        stateStoring: {
            storageKey: 'answer2_Grid',
        },

        columns: [
            {
                dataField: "id",
                dataType: "number",
                formItem: {
                    visible: true
                },
                headerCellTemplate: $('<i style="color: black">ID</i>')
            },

        { caption: 'User Name', dataField: 'userName' },
        { caption: 'Email', dataField: 'userEmail' },
        { caption: 'Phone', dataField: 'userPhone' },
        { caption: 'Date', dataField: 'cdate', dataType: 'date' },
        { caption: 'Time', dataField: 'time' },
        { caption: 'Session Id', dataField: 'sessionId' },
        { caption: 'OS', dataField: 'os' },
        { caption: 'Area', dataField: 'area' },
        { caption: 'Browser', dataField: 'browser' },
        { caption: 'Screen Size', dataField: 'screenSize', width: 50 }

        ]
        ,

       
    });

});