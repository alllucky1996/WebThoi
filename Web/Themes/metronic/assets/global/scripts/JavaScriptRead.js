jQuery(document).ready(function () {

    var dataTableWillReLoad = [];
    var dataTableWillReLoads = "";


    window.uploadType = {};
    uploadType.img = ['gif', 'png', 'jpg', 'jpeg', 'bmp', ];
    uploadType.doc = ['doc', 'docx', 'pdf', 'svg', 'gif', 'png', 'jpg', 'jpeg', 'bmp', 'excel'];

    $(document).on('click', '.select-file', function () {
        $("." + $(this).attr("data-pre") + "file").click();

        if ($(this).attr("binded") == undefined) {

            $("." + $(this).attr("data-pre") + "file").change(function (e) {


                $fType = $(this).attr("data-type");
                var files = e.originalEvent.target.files;
                for (var i = 0, len = files.length; i < len; i++) {
                    var n = files[i].name,
                        s = files[i].size,
                        t = files[i].type;
                    var ext = n.split('.').pop().toLowerCase();
                    if ($.inArray(ext, uploadType[$fType]) == -1) {
                        var msg1 = 'Tệp tin đính kèm không hợp lệ! \nChỉ chấp nhận các loại tệp có định dạng: ' + (uploadType[$fType]).toString() + ' \nVui lòng chọn tài liệu khác!';
                        $(this).val('');
                        alert(msg1);
                    }
                    else {
                        var prv = $(this).attr("class").replace("file", "prv");

                        if ($fType == "doc") {
                            $("." + prv).html('');
                            $("." + prv).append("<div style=\"line-height: 2em;\">" + files[i].name + "</div>");
                            $("." + prv).show();
                        }
                        if ($fType == "img") {
                            var reader = new FileReader();
                            reader.onload = function (e) {
                                $("." + prv + ' .Image_Preview').attr('src', e.target.result);
                            }
                            reader.readAsDataURL(files[i]);
                        }

                    }
                }
            });

            $(this).attr("binded", 1);
        }
    });



    //Cập nhật thông tin đề xuất
    $(document).on('click', '.btn-update', function () {



        Metronic.blockUI({
            message: 'Vui lòng đợi!',
            target: $('#account_detail'),
            overlayColor: 'none',
            cenrerY: true,
            boxed: true
        });

        $.each($(".form-update-account [role^='application']"), function (i, item) {
            var idOrg = $(this).attr('id').replace('cke_', '');
            $('#' + idOrg).val(CKEDITOR.instances[idOrg].getData());
        });

        var $url = $urlDetail;
        var formData = new FormData($('.form-update-account')[0]);


        $.ajax({
            type: 'POST',
            cache: false,
            async: true,
            url: $url,
            data: { id: '@Model.Ma' },
            //data: $('.form-update-account').serialize(),
            data: formData,
            dataType: 'json',
            enctype: 'multipart/form-data',
            contentType: false,
            processData: false,

            success: function (result) {
                if (result.success) {
                    showNoty(result.message, 'success', 'center', 5000);
                    setTimeout("location.reload(true);", 5000);
                }
                else
                    showNoty(result.message, 'error', 'center', 5000);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                showNoty(errorThrown, 'error', 'center', 5000);
            },
            complete: function (result) {
                Metronic.unblockUI($('#account_detail'));
            }
        });
        return;
    });


    $(document).on('click', '.btn-add-base', function () {



        Metronic.blockUI({
            message: 'Vui lòng đợi!',
            target: $('#account_detail'),
            overlayColor: 'none',
            cenrerY: true,
            boxed: true
        });

        var thisForm = $(this).closest(".form").find("form")[0];

        dataTableWillReLoad = $(thisForm).attr('dataTB').split('-');
        dataTableWillReLoads = $(thisForm).attr('dataTB');

        var $url = $(thisForm).attr('dataUrl');



        $.each($(".form-add-base input[type='checkbox']"), function (i, item) {
            if (item.checked) {
                item.value = true;
            } else {
                item.value = false;
            }
        });

        $.each($(".form-add-base [role^='application']"), function (i, item) {
            var idOrg = $(this).attr('id').replace('cke_', '');
            $('#' + idOrg).val(CKEDITOR.instances[idOrg].getData());
        });


        var formData = new FormData(thisForm);




        $.ajax({
            type: 'POST',
            cache: false,
            async: true,
            url: $url,
            //data: $('.form-update-account').serialize(),
            data: formData,
            dataType: 'json',
            enctype: 'multipart/form-data',
            contentType: false,
            processData: false,

            success: function (result) {
                if (result.success) {
                    showNoty(result.message, 'success', 'center', 5000);
                    $('#' + dataTableWillReLoads + '_main .close').click();
                    $.each(dataTableWillReLoad, function (i, item) {
                        $('#datatable_ajax_source_' + item).DataTable().ajax.reload()
                    });
                    dataTableWillReLoad = [];
                }
                else
                    showNoty(result.message, 'error', 'center', 5000);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                showNoty(errorThrown, 'error', 'center', 5000);
            },
            complete: function (result) {
                Metronic.unblockUI($('#account_detail'));
            }
        });
        return;
    });

    $(".modal").on("loaded.bs.modal", function (e) {
        ymPTH();
    });

    
    $(".modal").on("hidden.bs.modal", function () {

        jQuery(".modal-content", this).empty();
        $.each(dataTableWillReLoad, function (i, item) {
            $('#datatable_ajax_source_' + item).DataTable().ajax.reload()
        });
        dataTableWillReLoad = [];

    });

})

