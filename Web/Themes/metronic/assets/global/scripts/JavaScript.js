jQuery(document).ready(function () {
    
    CKFinder.setupCKEditor(null, '/Editor/ckfinder');
    window.ymPTH = function () {
        ymPTH.ComboboxRender($("select.combobox"));
        ymPTH.ComboboxStRender($("select.status"));
        try {
            disRender
            ymPTH.ComboboxDisRender($("select.combobox"));
        } catch (e) {
            ymPTH.CkfinderRender($("textarea.richtext"));
        }

        $('.date-picker').datepicker({
            autoclose: true
        });
    };
    ymPTH.formatSel2 = function (state, data) {
        var path = state.text.split(",,,");

        if (!state.id) return state.text; // optgroup

        return "<span class='label lable-sm' style='background: " + (path[1] != (undefined || "") ? path[1] : "black") + "'>" + path[0] + "</span>";
    };

    ymPTH.CkfinderRender = function (ids) {
        
        $.each(ids, function (i, item) {
            try {
                CKEDITOR.replace(item, {
                    toolbar: [
                        ['Bold', 'Italic', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'Link', 'Unlink', 'Image'], ['Table'], ['FontSize', 'TextColor', 'BGColor']
                    ]
                });
            }
            catch(e) {

            }
        });
    }
    ymPTH.ComboboxRender = function (ids) {
        $.each(ids, function (i, item) {
            $(item).select2({
                placeholder: "---Tất cả---",
                allowClear: true
            });
        });
    }

    ymPTH.ComboboxStRender = function (ids) {
        $.each(ids, function (i, item) {
            $(item).select2({
                formatSelection: ymPTH.formatSel2,
                formatResult: ymPTH.formatSel2
            }).select2('disable');
        });
    }

    ymPTH.ComboboxDisRender = function (ids) {
        $.each(ids, function (i, item) {
            $(item).select2('disable');
        });
    }

    $('form').on('submit', function (e) {
        $.each($(".form [role^='application']"), function (i, item) {
            var idOrg = $(this).attr('id').replace('cke_', '');
            $('#' + idOrg).val(CKEDITOR.instances[idOrg].getData());
        });
    });

    ymPTH();

})

