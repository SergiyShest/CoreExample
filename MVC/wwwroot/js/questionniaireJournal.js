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

const columns_ = [
    {
        dataField: "id",
        dataType: "number",
        formItem: {
            visible: true
        },
        headerCellTemplate: $('<i style="color: red">ID</i>')
    },
    { caption: "Name ", dataField: "name" },
    { caption: "Text ", dataField: "text" },


];



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
        remoteOperations: { paging: true, filtering: true, sorting: true, grouping: true, summary: true, groupPaging: true },

        dataSource: DevExpress.data.AspNet.createStore({
            key: "id",
            loadUrl: "../Home/Get",
        }),

        scrolling: {
            rowRenderingMode: 'virtual',
        },
        paging: {
            pageSize: 30,
        },
        pager: {
            visible: true,
            allowedPageSizes: [30, 50, 100],
            showPageSizeSelector: true,
            showInfo: true,
            showNavigationButtons: true,
        },
        grouping: {
            contextMenuEnabled: true
        },
        groupPanel: {
            visible: true   // or "auto"
        },
        onContextMenuPreparing: contextMenuPreparing,
        focusedRowEnabled: true,
        rowAlternationEnabled: true,
        focusedRowKey: 3,
        // filterValue: [["OrderID", ">", "400"], "and", ["OrderID", '<',"100000"]],
        onInitNewRow: function (e) {
            e.data = {
                OrderDate: new Date()
            };
        },
        columnAutoWidth: true,
        filterRow: {
            visible: true,
            applyFilter: "auto"
        },
        stateStoring: {
            enabled: true,
            type: 'localStorage',
            storageKey: 'QuesionniareGrid',
        },
        headerFilter: { visible: true },
        filterPanel: { visible: true },
        selection: {
            mode: "multiple",
            allowSelectAll: true
        },
        columns: columns_
    });


});