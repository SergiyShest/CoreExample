function xPopup(header, path, onCloseCallback) {

    var content = `<H1> no content </H1>`;
    var popupContentTemplate = function () {
        return content;
    };

    var popup = $('#dxPopup')
        .dxPopup({
            width: '50vw',
            height: '95vh',
            visible: false,
            title: header,
            resizeEnabled:true,
            hideOnOutsideClick: true,
            showCloseButton: true,
            contentTemplate: popupContentTemplate,
            onHidden: function () {
                // Вызов функции обратного вызова при закрытии окна
                if (typeof onCloseCallback === 'function') {
                    onCloseCallback();
                }
            }
        }).dxPopup('instance');

    path = document.location.origin + path;
    content = '<iframe src="' + path + '" style= "width:100%; height:100%">';

    popup.show();
}
