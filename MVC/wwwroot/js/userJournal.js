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
        var dataGrid = $('#usersGrid').dxDataGrid('instance');
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



    $("#usersGrid").dxDataGrid({
      

        dataSource: DevExpress.data.AspNet.createStore({
            key: "id",
            loadUrl: "../UserJournal/Get",
            deleteUrl: "../UserJournal/Delete",
            updateUrl: "../UserJournal/Update",
            onBeforeSend: function (method, ajaxOptions) {
                ajaxOptions.xhrFields = { withCredentials: true };
            }
        }),
        editing: {
            mode: 'row',
            allowUpdating: true,
            allowDeleting: true,
        },
        stateStoring: { storageKey: 'user_Grid'},
        columns: [
            {
                dataField: "id",
                dataType: "number",
                allowEditing: false,
                width: "70px",
            },
            { caption: "Name ", dataField: "name" }, 
            { caption: "Email ", dataField: "email"  },    
            { caption: "Regictration date ", dataField: "cDate", dataType: "date" },    
        ]
    });




    $("#avaiableUsersGrid").dxDataGrid({

        dataSource: DevExpress.data.AspNet.createStore({
            key: "id",
            loadUrl: "../UserJournal/GetAvaiable",
            insertUrl: "../UserJournal/InsertAvaiable",
            updateUrl: "../UserJournal/UpdateAvaiable",
            deleteUrl: "../UserJournal/DeleteAvaiable",
            onBeforeSend: function (method, ajaxOptions) {
                ajaxOptions.xhrFields = { withCredentials: true };
            }
        }),

        stateStoring: { storageKey: 'user_Grid'},

        editing: {
            mode: 'row',
            allowUpdating: true,
            allowAdding: true,
            allowDeleting: true,
            popup: {
                title: 'Avaiable Users',
                showTitle: true,
                width: 700,
                height: 525,
            },
        },
        columns: [
            {
                dataField: "id",
                dataType: "number",
                allowEditing: false,
                width: "70px",
            },
            { caption: "Email ", dataField: "email" },

        ]
    });

});