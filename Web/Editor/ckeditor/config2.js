/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';
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
    config.pasteFromWordRemoveStyles = true;

    config.extraPlugins = "base64image,pastefromword,mathjax,widget,lineutils";
    //config.filebrowserUploadUrl = "base64";
    //config.extraPlugins = 'imagepaste';
    //
    //http://ckeditor.com/addon/uploadimage

    //config.allowedContent = true;


    config.toolbar = [
    { name: 'clipboard', items: ['NewPage', 'Preview', '-', 'Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', 'Mathjax', '-', 'Undo', 'Redo'] },
    { name: 'editing', items: ['Styles', 'Format', 'Font', 'FontSize', 'TextColor', 'BGColor', '-', 'Find', 'Replace', '-', 'SelectAll'] },
    '/',
    { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', 'SpecialChar', '-', 'RemoveFormat'] },
    { name: 'paragraph', items: ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', 'NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv', '-'] },
    { name: 'insert', items: ['Link', 'Unlink', '-', 'Image', 'Table', 'HorizontalRule', 'PageBreak'] },
    { name: 'tools', items: ['Maximize', 'ShowBlocks', 'Source'] }
    ];
    config.toolbar = [
        {
            name: 'editing', items: ['NewPage', 'Preview', 'Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', 'Mathjax', 'SpecialChar', 'Image', 'Subscript', 'Superscript', 'Table', 'Undo', 'Redo']
        },
    ];
    config.removePlugins = 'elementspath';
};
