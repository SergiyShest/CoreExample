

//
function copyToClipboard(str) {
    const el = document.createElement('textarea');
    el.value = str;
    document.body.appendChild(el);
    el.select();
    document.execCommand('copy');
    document.body.removeChild(el);
};

//
function CreateFilter(str) {
    const el = document.createElement('textarea');
    el.value = str;
    document.body.appendChild(el);
    el.select();
    document.execCommand('copy');
    document.body.removeChild(el);
};

function contentReady() {
    console.timeEnd("x");
}

//добавление пункта меню копировать
function contextMenuPreparing(e) {
    console.log(e)
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

const defaultDxOptions = {
    scrolling: {
        rowRenderingMode: 'virtual',
    },
    paging: {
        pageSize: 30,
    },
    export: {
        enabled: true
    },
    pager: {
        visible: true,
        allowedPageSizes: [30, 50, 100],
        showPageSizeSelector: true,
        showInfo: true,
        showNavigationButtons: true,
    },
    allowColumnResizing: true,
    columnResizingMode: 'widget',
    allowColumnReordering: true,
    focusedRowEnabled: true,
    rowAlternationEnabled: true,

    columnAutoWidth: true,
    filterRow: {
        visible: true,
        applyFilter: "auto"
    },
    stateStoring: {
        enabled: true,
        type: 'localStorage',
    },
    headerFilter: { visible: true },
    filterPanel: { visible: true },
    selection: {
        mode: "multiple",
        allowSelectAll: true
    },
}

DevExpress.ui.dxDataGrid.defaultOptions({
    device: { deviceType: "desktop" },
    options: defaultDxOptions
});
