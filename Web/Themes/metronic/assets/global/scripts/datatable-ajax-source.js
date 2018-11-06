//NamMV 10/09/2015
var DatatableAjaxSource = function () {
    var tableOptions; // main options
    var dataTable; // datatable object
    var table; // actual table jquery object
    var tableContainer; // actual table container object
    var tableWrapper; // actual table wrapper jquery object
    var tableInitialized = false;
    var ajaxParams = {}; // set filter mode
    var the;

    var countSelectedRecords = function () {
        var selected = $('tbody > tr > td:nth-child(1) input[type="checkbox"]:checked', table).size();
        var text = tableOptions.dataTable.language.metronicGroupActions;
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
                src: "", // actual table  
                filterApplyAction: "filter",
                filterCancelAction: "filter_cancel",
                resetGroupActionInputOnSuccess: true,
                loadingMessage: 'Đang tải dữ liệu...',
                dataTable: {
                    //"dom": "<'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>", 
                    //"dom": "<'row'<'col-md-8 col-sm-12'<'table-group-actions'>><'col-md-4 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-12 col-sm-12'pli>>",
                    //"dom": "<'row'<'col-md-12'<'table-group-actions'>>r><'table-scrollable't><'row'<'col-md-8 col-sm-12'pli><'col-md-4 col-sm-12'>>",                     
                    //"dom": "<'row'<'col-md-6 col-sm-12'<'table-group-actions'>><'col-md-6 col-sm-12 display-inline'<'display-inline pull-right'f><'table-group-actions-2 display-inline pull-right'>>r><'table-scrollable't><'row'<'col-md-12 col-sm-12'pli>>",
                    "dom": "<'row'<'col-md-12'<'row table-group-actions-3'>>><'row'<'col-md-6 col-sm-12'<'table-group-actions'>><'col-md-6 col-sm-12 display-inline'<'display-inline pull-right'f><'table-group-actions-2 display-inline pull-right'>>r><'table-scrollable't><'row'<'col-md-12 col-sm-12'pli>>",
                    "lengthMenu": [
                            [10, 20, 50, 100, 150, -1],
                            [10, 20, 50, 100, 150, "Tất cả"]//Ô chọn hiển thị số bản ghi mỗi trang
                    ],
                    "pageLength": 10,
                    "language": { 
                        "metronicGroupActions": "_TOTAL_ bản ghi đã chọn",
                        "metronicAjaxRequestGeneralError": "Không lấy được dữ liệu, vui lòng thử lại!",
                        "search": "Tìm kiếm: ",
                        "lengthMenu": "<span class='seperator'>|</span>Hiển thị _MENU_ bản ghi mỗi trang",
                        "info": "<span class='seperator'>|</span>Tổng số _TOTAL_ bản ghi",
                        "infoFiltered": "(tìm trong tổng số _MAX_ bản ghi)",
                        "infoEmpty": " Không tìm thấy bản ghi nào",
                        "emptyTable": " Không có dữ liệu",
                        "zeroRecords": " Không tìm thấy dữ liệu",
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
                        'targets': [0, -1]
                    }],

                    "pagingType": "bootstrap_extended", 
                    "autoWidth": false,
                    "processing": false,
                    "serverSide": true, 
                    "ajax": { 
                        "url": "",
                        "type": "GET",
                        "timeout": 20000,
                        "data": function (data) { 
                            $.each(ajaxParams, function (key, value) {
                                data[key] = value;
                            });
                            Metronic.blockUI({
                                message: tableOptions.loadingMessage,
                                target: tableContainer,
                                overlayColor: 'none',
                                cenrerY: true,
                                boxed: true
                            });
                        },
                        "dataSrc": function (res) { // Manipulate the data returned from the server
                            if (res.customActionMessage) {
                                Metronic.alert({
                                    type: (res.customActionStatus == 'OK' ? 'success' : 'danger'),
                                    icon: (res.customActionStatus == 'OK' ? 'check' : 'warning'),
                                    message: res.customActionMessage,
                                    container: tableWrapper,
                                    place: 'prepend'
                                });
                            }

                            if (res.customActionStatus) {
                                if (tableOptions.resetGroupActionInputOnSuccess) {
                                    $('.table-group-action-input', tableWrapper).val("");
                                }
                            }

                            if ($('.group-checkable', table).size() === 1) {
                                $('.group-checkable', table).attr("checked", false);
                                $.uniform.update($('.group-checkable', table));
                            }

                            if (tableOptions.onSuccess) {
                                tableOptions.onSuccess.call(undefined, the, res);
                            }

                            Metronic.unblockUI(tableContainer);

                            return res.data;
                        },
                        "error": function () { // handle general connection errors
                            if (tableOptions.onError) {
                                tableOptions.onError.call(undefined, the);
                            }

                            Metronic.alert({
                                type: 'danger',
                                icon: 'warning',
                                message: tableOptions.dataTable.language.metronicAjaxRequestGeneralError,
                                container: tableWrapper,
                                place: 'prepend'
                            });

                            Metronic.unblockUI(tableContainer);
                        }
                    },

                    "drawCallback": function (oSettings) { // run some code on table redraw
                        if (tableInitialized === false) { // check if table has been initialized
                            tableInitialized = true; // set table initialized
                            table.show(); // display table
                        }
                        Metronic.initUniform($('input[type="checkbox"]:not(.toggle, .md-check, .md-radiobtn, .make-switch, .icheck), input[type=radio]:not(.toggle, .md-check, .md-radiobtn, .star, .make-switch, .icheck, .not-radio)', table)); // reinitialize uniform checkboxes on each table reload
                        //Metronic.initUniform($('input[type="radio"]:not(.not-radio)', table)); // reinitialize uniform checkboxes on each table reload
                        countSelectedRecords(); // reset selected records indicator
                        $(".make-switch").bootstrapSwitch();

                        if (!(window.MathJax && MathJax.Hub)) {
                        }
                        else{
                            //
                            //MathJax.Hub.Config({
                            //    tex2jax: { inlineMath: [["$", "$"], ["\\(", "\\)"]] },
                            //    MMLorHTML: { prefer: { Firefox: "MML" } },
                            //});
                            //Phải gọi cái này để hiển thị công thức toán học đối với nội dung được trả về bằng ajax
                            MathJax.Hub.Typeset();
                            //
                        }
                        // callback for ajax data load
                        if (tableOptions.onDataLoad) {
                            tableOptions.onDataLoad.call(undefined, the);
                        }
                    }
                }
            }, options);

            tableOptions = options;

            // create table's jquery object
            table = $(options.src);
            tableContainer = table.parents(".table-container");

            // apply the special class that used to restyle the default datatable
            var tmp = $.fn.dataTableExt.oStdClasses;

            $.fn.dataTableExt.oStdClasses.sWrapper = $.fn.dataTableExt.oStdClasses.sWrapper + " dataTables_extended_wrapper";
            $.fn.dataTableExt.oStdClasses.sFilterInput = "form-control input-small input-sm input-inline";
            $.fn.dataTableExt.oStdClasses.sLengthSelect = "form-control input-xsmall input-sm input-inline";

            // initialize a datatable
            dataTable = table.DataTable(options.dataTable);

            // revert back to default
            $.fn.dataTableExt.oStdClasses.sWrapper = tmp.sWrapper;
            $.fn.dataTableExt.oStdClasses.sFilterInput = tmp.sFilterInput;
            $.fn.dataTableExt.oStdClasses.sLengthSelect = tmp.sLengthSelect;

            // get table wrapper
            tableWrapper = table.parents('.dataTables_wrapper');
            tableWrapper.find(".dataTables_length select").select2({
                showSearchInput: false
            });
            // build table group actions panel
            if ($('.table-actions-wrapper', tableContainer).size() === 1) {
                $('.table-group-actions', tableWrapper).html($('.table-actions-wrapper', tableContainer).html()); // place the panel inside the wrapper
                $('.table-actions-wrapper', tableContainer).remove(); // remove the template container
            }
            if ($('.table-actions-2-wrapper', tableContainer).size() === 1) {
                $('.table-group-actions-2', tableWrapper).html($('.table-actions-2-wrapper', tableContainer).html()); // place the panel inside the wrapper
                $('.table-actions-2-wrapper', tableContainer).remove(); // remove the template container
            }
            if ($('.table-actions-3-wrapper', tableContainer).size() === 1) {
                $('.table-group-actions-3', tableWrapper).html($('.table-actions-3-wrapper', tableContainer).html()); // place the panel inside the wrapper
                $('.table-actions-3-wrapper', tableContainer).remove(); // remove the template container
            }
            // handle group checkboxes check/uncheck
            $('.group-checkable', table).change(function () {
                var set = $('tbody > tr > td:nth-child(1) input[type="checkbox"]', table);
                var checked = $(this).is(":checked");
                $(set).each(function () {
                    $(this).attr("checked", checked);
                });
                $.uniform.update(set);
                countSelectedRecords();
            });

            // handle row's checkbox click
            table.on('change', 'tbody > tr > td:nth-child(1) input[type="checkbox"]', function () {
                countSelectedRecords();
            });

            // handle filter submit button click
            table.on('click', '.filter-submit', function (e) {
                e.preventDefault();
                the.submitFilter();
            });

            // handle filter cancel button click
            table.on('click', '.filter-cancel', function (e) {
                e.preventDefault();
                the.resetFilter();
            });

            table.on('search.dt', function () {
                if ($('select.object-filter'))
                    the.setAjaxParam('object-filter', $('select.object-filter').val());
                if ($('select.object-filter-2'))
                    the.setAjaxParam('object-filter-2', $('select.object-filter-2').val());
                if ($('select.object-filter-3'))
                    the.setAjaxParam('object-filter-3', $('select.object-filter-3').val());
            });
            table.on('order.dt', function () {
                if ($('select.object-filter'))
                    the.setAjaxParam('object-filter', $('select.object-filter').val());
                if ($('select.object-filter-2'))
                    the.setAjaxParam('object-filter-2', $('select.object-filter-2').val());
                if ($('select.object-filter-3'))
                    the.setAjaxParam('object-filter-3', $('select.object-filter-3').val());
            });
            table.on('page.dt', function () {
                if ($('select.object-filter'))
                    the.setAjaxParam('object-filter', $('select.object-filter').val());
                if ($('select.object-filter-2'))
                    the.setAjaxParam('object-filter-2', $('select.object-filter-2').val());
                if ($('select.object-filter-3'))
                    the.setAjaxParam('object-filter-3', $('select.object-filter-3').val());
            });
        },

        submitFilter: function () {
            the.setAjaxParam("action", tableOptions.filterApplyAction);

            // get all typeable inputs
            $('textarea.form-filter, select.form-filter, input.form-filter:not([type="radio"],[type="checkbox"])', table).each(function () {
                the.setAjaxParam($(this).attr("name"), $(this).val());
            });

            // get all checkboxes
            $('input.form-filter[type="checkbox"]:checked', table).each(function () {
                the.addAjaxParam($(this).attr("name"), $(this).val());
            });

            // get all radio buttons
            $('input.form-filter[type="radio"]:checked', table).each(function () {
                the.setAjaxParam($(this).attr("name"), $(this).val());
            });

            dataTable.ajax.reload();
        },

        resetFilter: function () {
            $('textarea.form-filter, select.form-filter, input.form-filter', table).each(function () {
                $(this).val("");
            });
            $('input.form-filter[type="checkbox"]', table).each(function () {
                $(this).attr("checked", false);
            });
            the.clearAjaxParams();
            the.addAjaxParam("action", tableOptions.filterCancelAction);
            dataTable.ajax.reload();
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

        setAjaxParam: function (name, value) {
            ajaxParams[name] = value;
        },

        addAjaxParam: function (name, value) {
            if (!ajaxParams[name]) {
                ajaxParams[name] = [];
            }

            skip = false;
            for (var i = 0; i < (ajaxParams[name]).length; i++) { // check for duplicates
                if (ajaxParams[name][i] === value) {
                    skip = true;
                }
            }

            if (skip === false) {
                ajaxParams[name].push(value);
            }
        },

        clearAjaxParams: function (name, value) {
            ajaxParams = {};
        },

        getDataTable: function () {
            return dataTable;
        },

        getTableWrapper: function () {
            return tableWrapper;
        },

        gettableContainer: function () {
            return tableContainer;
        },

        getTable: function () {
            return table;
        }

    };

};