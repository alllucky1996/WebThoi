CKEDITOR.editorConfig = function (config) {
    config.language = 'vi';

    config.basicEntities = false;
    config.entities = false;
    config.entities_greek = false;
    config.entities_latin = false;
    config.htmlEncodeOutput = false;
    config.entities_processNumerical = false;

    //config.pasteFromWordPromptCleanup = true;
    config.pasteFromWordRemoveFontStyles = false;
    config.pasteFromWordNumberedHeadingToList = false;
    config.pasteFromWordRemoveStyles = false;

    config.extraPlugins = "base64image,pastefromword,mathjax,widget,lineutils";


    config.toolbar = [
        {
            name: 'editing', items: ['NewPage', 'Bold', 'Italic', 'Underline', 'Strike','JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', 'Image', 'Subscript', 'Superscript', 'Undo', 'Redo']
        },
    ];
    config.removePlugins = 'elementspath';
};
