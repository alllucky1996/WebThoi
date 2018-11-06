//NamMV 10/09/2015
var DatatableNoneAjaxSource = function () {
    var tableOptions;
    var dataTable;
    var table;
    var tableContainer;
    var tableWrapper;
    var tableInitialized = false;
    var the;

    var countSelectedRecords = function () {
        var selected = $('tbody > tr > td:nth-child(1) input[type="checkbox"]:checked', table).size();
        var text = tableOptions.dataTable.language.adminversiononeGroupActions;
        if (selected > 0) {
            $('.table-group-actions > span', tableWrapper).text(text.replace("_TOTAL_", selected));
        } else {
            $('.table-group-actions > span', tableWrapper).text("");
        }
    };

    return {
        init: function (options) {

            if (!$().dataTable) {
                return;
            }

            the = this;

            options = $.extend(true, {
                src: "", 
                filterApplyAction: "filter",
                filterCancelAction: "filter_cancel",
                resetGroupActionInputOnSuccess: true,
                loadingMessage: 'Đang tải dữ liệu...',
                dataTable: {
                    //"dom": "<'row'<'col-md-8 col-sm-12'<'table-group-actions'>><'col-md-4 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-12 col-sm-12'pli>>",
                    "dom": "<'row'<'col-md-12'<'row table-group-actions-3'>>><'row'<'col-md-6 col-sm-12'<'table-group-actions'>><'col-md-6 col-sm-12 display-inline'<'display-inline pull-right'f><'table-group-actions-2 display-inline pull-right'>>r><'table-scrollable't><'row'<'col-md-12 col-sm-12'pli>>",
                    "pageLength": 10, // mặc định số bản ghi mỗi trang
                    "language": {
                        "adminversiononeGroupActions": "Đã chọn _TOTAL_ bản ghi  ",
                        "adminversiononeAjaxRequestGeneralError": "Không lấy được dữ liệu, vui lòng thử lại!",
                        "search": "Tìm kiếm: ",
                        "lengthMenu": "<span class='seperator'>|</span>Hiển thị _MENU_ bản ghi mỗi trang",
                        "info": "<span class='seperator'>|</span>Tổng số _TOTAL_ bản ghi",
                        "infoFiltered": "(tìm trong tổng số _MAX_ bản ghi)",
                        "infoEmpty": "<span class='seperator'>|</span>Không tìm thấy bản ghi nào",
                        "emptyTable": "Không có dữ liệu",
                        "zeroRecords": "Không tìm thấy dữ liệu",
                        "paginate": {
                            "previous": "Trang trước",
                            "next": "Trang sau",
                            "last": "Trang cuối",
                            "first": "Trang đầu",
                            "page": "Trang",
                            "pageOf": " trong tổng số"
                        }
                    },

                    "orderCellsTop": true,
                    "columnDefs": [{ 
                        'orderable': false,
                        'targets': [0,-1]
                    }],
                    "bDestroy": true,
                    "pagingType": "bootstrap_extended", // pagination type(bootstrap_full_number or bootstrap_extended)
                    "autoWidth": false,
                    "processing": true,
                    "serverSide": false,

                    "drawCallback": function (oSettings) {
                        if (tableInitialized === false) {
                            tableInitialized = true;
                            table.show();
                        }
                        Metronic.initUniform($('input[type="checkbox"]', table));
                        countSelectedRecords();
                    }
                }
            }, options);

            tableOptions = options;

            table = $(options.src);
            tableContainer = table.parents(".table-container");

            var tmp = $.fn.dataTableExt.oStdClasses;

            $.fn.dataTableExt.oStdClasses.sWrapper = $.fn.dataTableExt.oStdClasses.sWrapper + " dataTables_extended_wrapper";
            $.fn.dataTableExt.oStdClasses.sFilterInput = "form-control input-small input-sm input-inline";
            $.fn.dataTableExt.oStdClasses.sLengthSelect = "form-control input-xsmall input-sm input-inline";

            dataTable = table.DataTable(options.dataTable);

            $.fn.dataTableExt.oStdClasses.sWrapper = tmp.sWrapper;
            $.fn.dataTableExt.oStdClasses.sFilterInput = tmp.sFilterInput;
            $.fn.dataTableExt.oStdClasses.sLengthSelect = tmp.sLengthSelect;

            tableWrapper = table.parents('.dataTables_wrapper');
            tableWrapper.find(".dataTables_length select").select2({
                showSearchInput: false
            });
            if ($('.table-actions-wrapper', tableContainer).size() === 1) {
                $('.table-group-actions', tableWrapper).html($('.table-actions-wrapper', tableContainer).html());
                $('.table-actions-wrapper', tableContainer).remove();
            }
            $('.group-checkable', table).change(function () {
                var set = $('tbody > tr > td:nth-child(1) input[type="checkbox"]', table);
                var checked = $(this).is(":checked");
                $(set).each(function () {
                    $(this).attr("checked", checked);
                    if (checked) {
                        $(this).parents('tr').addClass("active");
                    } else {
                        $(this).parents('tr').removeClass("active");
                    }
                });
                $.uniform.update(set);
                countSelectedRecords();
            });
            table.on('change', 'tbody tr .checkboxes', function () {
                $(this).parents('tr').toggleClass("active");
            });
            table.on('change', 'tbody > tr > td:nth-child(1) input[type="checkbox"]', function () {
                countSelectedRecords();
            });
        },

        getSelectedRowsCount: function () {
            return $('tbody > tr > td:nth-child(1) input[type="checkbox"]:checked', table).size();
        },

        getSelectedRows: function () {
            var rows = [];
            $('tbody > tr > td:nth-child(1) input[type="checkbox"]:checked', table).each(function () {
                rows.push($(this).val());
            });

            return rows;
        },

        getTableWrapper: function () {
            return tableWrapper;
        },
    };
};