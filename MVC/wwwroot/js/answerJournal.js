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
    if (e.target == 'content') {
            e.items=[]
            e.items.push({
                text: "Edit Notes",
                onItemClick: function () {
                    var header = (e.row.data.userName??'') + ' email:' + (e.row.data.userEmail??'')
                    xPopup(`Notes for : ${header}`, '/AnswerNotes?id=' + e.row.key);

                }
            });
        e.items.push({
            text: "Copy cell",
            onItemClick: function () {
                var dataField = e.column.dataField;
                copyToClipboard(e.row.data[dataField])
            }
        });

        }
    

}

$(function () {


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



    $("#grid").dxDataGrid({
        remoteOperations: true, //{ paging: true, filtering: true, sorting: true, grouping: true, summary: true, groupPaging: true },

        onContextMenuPreparing:contextMenuPreparing,
        stateStoring: {
            storageKey: 'Answer8_grid',
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
        { caption: 'Note', dataField: 'note' },
        { caption: 'Date', dataField: 'cdate', dataType: 'date' },
        { caption: 'Time', dataField: 'formatedTime' },
        { caption: 'Session Id', dataField: 'sessionId' },
        { caption: 'OS', dataField: 'os' },
        { caption: 'Area', dataField: 'area' },
        { caption: 'Browser', dataField: 'browser' },
        { caption: 'Screen Size', dataField: 'screenSize', width: 50 }

        ]
        ,

       
    });

});