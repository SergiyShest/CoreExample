function copyToClipboard(str) {
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

function OnRowDblClick(e) {
    console.log('OnRowDblClick')
    console.log(e)
}

const columns_ = [
    {
        dataField: "id",
        dataType: "number",
        allowEditing: false,
        formItem: {
            visible: true
        },
        headerCellTemplate: $('<i style="color: red">ID</i>')
    },
    { caption: "Name ", dataField: "name" },
    { caption: "Text ", dataField: "text" },
    { caption: "Main ", dataField: "main", dataType: "boolean" },

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

    function OnFocusedRowChanged(e) {
        const focusedRowKey = e.component.option('focusedRowKey');
        $('#selectedId').text(focusedRowKey);
        let row = e.component.getRowElement(e.rowIndex);
        // if (row)
        //row[0].style.backgroundColor = 'red';
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


    $("#grid").dxDataGrid({

        dataSource: DevExpress.data.AspNet.createStore({
            key: "id",
            loadUrl: "../Home/Get",
            insertUrl: "../Home/Insert",
            updateUrl: "../Home/Update",
            deleteUrl: "../Home/Delete",
            onBeforeSend: function (method, ajaxOptions) {
                ajaxOptions.xhrFields = { withCredentials: true };
            }

        }),
        onContextMenuPreparing: contextMenuPreparing,
        stateStoring: { storageKey: 'QuesionniareGrid' },

        onFocusedRowChanged: OnFocusedRowChanged,
        editing: {
            mode: 'row',
            allowUpdating: true,
            allowAdding: true,
            allowDeleting: true,
            popup: {
                title: 'Quesionniare',
                showTitle: true,
                width: 700,
                height: 525,
            },
        }
    });


});