function showNoty(text, type, layout, timeout) {
    //if ($noty != null)
    //    $noty.close();
    $.noty.closeAll();
    if ($.trim(text) != '') {
        window.setTimeout(function () {
            var $noty = noty({
                text: text,
                type: type,
                dismissQueue: true,
                layout: layout,
                theme: 'defaultTheme',
                timeout: timeout, // delay for closing event. Set false for sticky notifications
                force: false, // adds notification to the beginning of queue when set to true
                modal: false,
                maxVisible: 5, // you can set max visible notification for dismissQueue true option,
                killer: true, // for close all notifications before show
                closeWith: ['hover'], // ['click', 'button', 'hover']
            });
        }, 100);
    }
}
//Hàm Lọc người dùng khi chọn chuyên gia để phản biện cho câu hỏi
function AccountFilter(p) {
    //alert(p);
    Metronic.blockUI({
        message: 'Vui lòng đợi!',
        overlayColor: 'none',
        cenrerY: true,
        boxed: true,
        zIndex: 99999
    });
    var key = $.trim($('.search-account').val());
    var idTacGia = $('.search-account').attr('data-idtacgia');
    var idCauHoi = $('.search-account').attr('data-idcauhoi');
    //var cungLinhVuc = $('.search-account').attr('data-cunglinhvuc');
    //$(this).is(':checked')
    var cungLinhVuc = 0;
    if ($('#cungLinhVuc').is(':checked')) {
        cungLinhVuc = 1;
    }
    var $url = '/quan-ly/cau-hoi/chon-chuyen-gia-phan-bien-2/' + idCauHoi + '/' + idTacGia + '/' + cungLinhVuc + '/' + p + '/' + key;

    $.ajax({
        type: 'GET',
        cache: false,
        async: true,
        url: $url,
        dataType: 'html',
        success: function (result) {
            $('.content-partial-3').html(result);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            showNoty(errorThrown, 'error', 'center', 5000);
        },
        complete: function (result) {
            Metronic.unblockUI();
        }
    });
}
//End
//Hàm tải lại danh sách chuyên gia phản biện câu hỏi
function DanhSachChuyenGiaPhanBien(idCauHoi, idTacGia) {
    Metronic.blockUI({
        message: 'Vui lòng đợi!',
        overlayColor: 'none',
        cenrerY: true,
        boxed: true,
        zIndex: 99999
    });
    var $url = '/quan-ly/cau-hoi/chon-chuyen-gia-phan-bien/' + idCauHoi + '/' + idTacGia;
    $.ajax({
        type: 'GET',
        cache: false,
        async: true,
        url: $url,
        dataType: 'html',
        success: function (result) {
            $('.content-partial-2').html(result);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //showNoty(errorThrown, 'error', 'center', 5000);
        },
        complete: function (result) {
            Metronic.unblockUI();
        }
    });
}
//End
//Hàm Lấy danh sách khối kiến thức theo môn học khi chọn khối kiến thức để nhập câu hỏi
function DanhSachKhoiKienThucTheoMonHoc(maMonHoc) {
    var URL = '/quan-ly/danh-sach-khoi-kien-thuc-theo-mon-hoc/' + maMonHoc;
    $.getJSON(URL, function (data) {
        var items = "<option value=''></option>";
        $.each(data, function (i, item) {
            items += "<option value='" + item.Value + "'>" + item.Text + "</option>";
        });
        $('#SelDanhSachKhoiKienThucPopup').html(items);
    });
};
//End
jQuery(document).ready(function () {
    // Lấy danh sách khối kiến thức theo môn học khi chọn khối kiến thức để nhập câu hỏi
    $(document).on('change', '#SelDanhSachMonHocPopup', function (e) {
        DanhSachKhoiKienThucTheoMonHoc($(this).val());
    });
    //End
    //Thực hiện chọn khối kiến thức và tác giả trước khi vào nhập câu hỏi (xem danh sách câu hỏi)
    $(document).on('click', '.btn-chon-khoi-kien-thuc-popup', function (e) {
        e.preventDefault();
        Metronic.blockUI({
            message: 'Vui lòng đợi!',
            overlayColor: 'none',
            cenrerY: true,
            boxed: true,
            zIndex: 99999
        });
        var $url = '/quan-ly/cau-hoi/chon-mon-hoc-va-tac-gia-post';
        $.ajax({
            type: 'POST',
            cache: false,
            async: true,
            url: $url,
            data: $('.form-chon-khoi-kien-thuc-popup').serialize(),
            dataType: 'json',
            success: function (result) {
                if (result.success) {
                    //showNoty(result.message, 'success', 'center', 5000);
                    //laChuyenGia = laChuyenGia, maKhoiKienThuc = kkt.Ma, accountId = accountId 
                    //var url2 = '/quan-ly/cau-hoi/nhap-cau-hoi/' + result.maKhoiKienThuc + '/' + result.accountId;
                    var returnUrl = result.returnUrl;
                    var url2 = '';
                    if (returnUrl == 'Index')
                        url2 = '/quan-ly/cau-hoi/danh-sach-cau-hoi-dang-soan-thao/' + result.maKhoiKienThuc + '/' + result.accountId;
                    else 
                        url2 = '/quan-ly/duyet-cau-hoi/danh-sach-cau-hoi-dang-duyet/' + result.maKhoiKienThuc + '/' + result.accountId;//if (returnUrl == 'CauHoiDangDuyet2')
                    window.location = url2;
                    $('.modal-header .close').click();
                }
                else {
                    Metronic.unblockUI();
                    showNoty(result.message, 'error', 'center', 5000);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                Metronic.unblockUI();
                showNoty(errorThrown, 'error', 'center', 5000);
            },
            complete: function (result) {

            }
        });
    });
    //End
    //Lọc người dùng khi chọn chuyên gia để phản biện cho câu hỏi
    $(document).on('keyup', '.search-account', function (e) {
        if (e.which == 13) {
            AccountFilter(1);
        }
        return;
    });
    $(document).on('click', '.btn-search-account', function () {
        AccountFilter(1);
        return;
    });
    $(document).on('click', '.next-article-page', function () {
        var page = $(this).attr("data-page");
        AccountFilter(page);
        return;
    });
    $(document).on('change', '#cungLinhVuc', function () {
        AccountFilter(1);
        return;
    });
    //Chọn 1 chuyên gia để phản biện
    $(document).on('click', '.chon-chuyen-gia-phan-bien', function () {
        var accountId = $(this).attr('data-accountid');
        var idCauHoi = $(this).attr('data-idcauhoi');
        var idTacGia = $(this).attr('data-idtacgia');
        var thuTu = $(this).attr('data-thutu');

        var pbnd = 0;
        if ($('#ChonLamPhanBienNoiDung_' + thuTu).is(':checked')) {
            pbnd = 1;
        }
        var pbkt = 0;
        if ($('#ChonLamPhanBienKyThuat_' + thuTu).is(':checked')) {
            pbkt = 1;
        }
        Metronic.blockUI({
            message: 'Vui lòng đợi!',
            overlayColor: 'none',
            cenrerY: true,
            boxed: true,
            zIndex: 99999
        });
        var $url = '/quan-ly/cau-hoi/chon-chuyen-gia-phan-bien-3/' + idCauHoi + '/' + accountId + '/' + pbnd + '/' + pbkt;
        $.ajax({
            type: 'POST',
            cache: false,
            async: true,
            url: $url,
            dataType: 'json',
            success: function (result) {
                if (result.success) {
                    showNoty(result.message, 'success', 'center', 5000);
                    $('.chon-phan-bien-modal-header .close').click();
                }
                else {
                    showNoty(result.message, 'error', 'center', 5000);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                showNoty(errorThrown, 'error', 'center', 5000);
            },
            complete: function (result) {
                Metronic.unblockUI();
                DanhSachChuyenGiaPhanBien(idCauHoi, idTacGia);
                return;
            }
        });
    });
    //End
    //Hủy chọn chuyên gia phản biện
    $(document).on('click', '.huy-chon-chuyen-gia-phan-bien', function () {
        var accountId = $(this).attr('data-accountid');
        var idCauHoi = $(this).attr('data-idcauhoi');
        var idTacGia = $(this).attr('data-idtacgia');
        Metronic.blockUI({
            message: 'Vui lòng đợi!',
            overlayColor: 'none',
            cenrerY: true,
            boxed: true,
            zIndex: 99999
        });
        var $url = '/quan-ly/cau-hoi/huy-chon-chuyen-gia-phan-bien/' + idCauHoi + '/' + accountId;
        $.ajax({
            type: 'POST',
            cache: false,
            async: true,
            url: $url,
            dataType: 'json',
            success: function (result) {
                if (result.success) {
                    showNoty(result.message, 'success', 'center', 5000);
                }
                else {
                    showNoty(result.message, 'error', 'center', 5000);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                showNoty(errorThrown, 'error', 'center', 5000);
            },
            complete: function (result) {
                Metronic.unblockUI();
                DanhSachChuyenGiaPhanBien(idCauHoi, idTacGia);
                return;
            }
        });
    });
    //End
    //Chốt/Hủy chốt danh sách phản biện
    $(document).on('click', '.chot-danh-sach-chuyen-gia-phan-bien', function () {
        var idCauHoi = $(this).attr('data-idcauhoi');
        var chot = $(this).attr('data-chot');
        Metronic.blockUI({
            message: 'Vui lòng đợi!',
            overlayColor: 'none',
            cenrerY: true,
            boxed: true,
            zIndex: 99999
        });
        var $url = '/quan-ly/cau-hoi/chot-danh-sach-phan-bien/' + idCauHoi + '/' + chot;
        $.ajax({
            type: 'POST',
            cache: false,
            async: true,
            url: $url,
            dataType: 'json',
            success: function (result) {
                if (result.success) {
                    showNoty(result.message, 'success', 'center', 5000);
                    $('.modal-header .close').click();
                }
                else {
                    showNoty(result.message, 'error', 'center', 5000);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                showNoty(errorThrown, 'error', 'center', 5000);
            },
            complete: function (result) {
                Metronic.unblockUI();
                return;
            }
        });
    });
    //End
    //Chọn/bỏ chọn là chuyên gia phản biện nội dung/kỹ thuật
    $(document).on('change', '.chon-loai-phan-bien', function () {
        var idPhanBienCauHoi = $(this).attr('data-phanbiencauhoi');
        var loaiPhanBien = $(this).attr('data-loaiphanbien');
        var chon = 0;
        if ($(this).is(':checked')) {
            chon = 1;
        }
        Metronic.blockUI({
            message: 'Vui lòng đợi!',
            overlayColor: 'none',
            cenrerY: true,
            boxed: true,
            zIndex: 99999
        });
        var $url = '/quan-ly/cau-hoi/thay-doi-loai-phan-bien/' + idPhanBienCauHoi + '/' + loaiPhanBien + '/' + chon;
        $.ajax({
            type: 'POST',
            cache: false,
            async: true,
            url: $url,
            dataType: 'json',
            success: function (result) {
                if (result.success) {
                    showNoty(result.message, 'success', 'center', 5000);
                }
                else {
                    showNoty(result.message, 'error', 'center', 5000);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                showNoty(errorThrown, 'error', 'center', 5000);
            },
            complete: function (result) {
                Metronic.unblockUI();
                return;
            }
        });
    });
    //End
});