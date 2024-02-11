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

$("#buttonContainer").dxButton({
    text: "проверка фильтра",
    onClick: function (e) {
        var grid = $("#grid").dxDataGrid("instance"),
        filter = grid.getCombinedFilter();
        processFilter(grid, filter);
        alert(filter);
    }
}).dxButton("instance");
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
        remoteOperations: true,
        dataSource: DevExpress.data.AspNet.createStore({
            key: "id",
            loadUrl: "../HitCounter/Get",
        }),
        groupPanel: {
            visible: true,
        },
        stateStoring: {
            storageKey: 'hitCounter_Grid',
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
            { caption: "SessionId ", dataField: "sessionId"  },    
            { caption: "ScreenSize ", dataField: "screenSize" },           
            { caption: "Browser", dataField: "browser" },
            { caption: "Mobile", dataField: "mobile" },
            { caption: "Os", dataField: "os" },
            { caption: "Time zone", dataField: "area" },
            { caption: "Date", dataField: "cdate", dataType: 'date' },
            { caption: "Time", dataField: "cdate", dataType:'datetime', format:'shortTime' },
 
   
        ]
        ,

       
    });

});