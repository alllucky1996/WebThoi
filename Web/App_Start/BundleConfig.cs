using System.Web.Optimization;
namespace Web
{
    public class BundleConfig
    {
        #region General
        public static void RegisterGeneralBundles(BundleCollection bundles)
        {
            bundles.FileSetOrderList.Clear();
            bundles.Add(new ScriptBundle("~/js/noty").Include("~/Scripts/noty/packaged/jquery.noty.packaged.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/validate/jquery.unobtrusive*",
                        "~/Scripts/validate/jquery.validate*"));
        }
        #endregion
        #region Metronic
        public static void RegisterMetronicBundles(BundleCollection bundles)
        {
            
            bundles.Add(new ScriptBundle("~/js/metronic/ie").Include(
                "~/Themes/metronic/assets/global/plugins/respond.min.js",
                "~/Themes/metronic/assets/global/plugins/excanvas.min.js"
                ));

            bundles.Add(new ScriptBundle("~/form").Include(
                "~/Editor/ckeditor/ckeditor.js",
                "~/Editor/ckfinder/ckfinder.js",
                "~/Themes/metronic/assets/global/plugins/select2/select2.min.js",
                "~/Themes/metronic/assets/global/scripts/JavaScript.js"
                ));
            bundles.Add(new ScriptBundle("~/dvft").Include(
                "~/Themes/metronic/assets/global/scripts/JavaScriptDVFT.js"
                ));
            bundles.Add(new ScriptBundle("~/read").Include(
                "~/Editor/ckeditor/ckeditor.js",
                "~/Editor/ckfinder/ckfinder.js",
                "~/Themes/metronic/assets/global/plugins/select2/select2.min.js",
                "~/Themes/metronic/assets/global/scripts/JavaScript.js",
                "~/Themes/metronic/assets/global/scripts/JavaScriptRead.js"
                ));

            bundles.Add(new ScriptBundle("~/js/metronic/jquery").Include(
                "~/Themes/metronic/assets/global/plugins/jquery.min.js",
                "~/Themes/metronic/assets/global/plugins/jquery-migrate.min.js",
                "~/Themes/metronic/assets/global/plugins/jquery-ui/jquery-ui.min.js"
                ));

            bundles.Add(new ScriptBundle("~/js/metronic/bootstrap").Include(
                "~/Themes/metronic/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                "~/Themes/metronic/assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js"
                ));


            bundles.Add(new ScriptBundle("~/js/metronic/bootbox").Include("~/Themes/metronic/assets/global/plugins/bootbox/bootbox.min.js"));
            bundles.Add(new ScriptBundle("~/js/metronic/slimscroll").Include("~/Themes/metronic/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js"));
            bundles.Add(new ScriptBundle("~/js/metronic/blockui").Include("~/Themes/metronic/assets/global/plugins/jquery.blockui.min.js"));
            bundles.Add(new ScriptBundle("~/js/metronic/cokie").Include("~/Themes/metronic/assets/global/plugins/jquery.cokie.min.js"));
            bundles.Add(new ScriptBundle("~/js/metronic/uniform").Include("~/Themes/metronic/assets/global/plugins/uniform/jquery.uniform.min.js"));
            bundles.Add(new ScriptBundle("~/js/metronic/bootstrap-switch").Include("~/Themes/metronic/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.js"));

            bundles.Add(new ScriptBundle("~/js/metronic/init").Include(
                "~/Themes/metronic/assets/global/scripts/metronic.js",
                "~/Themes/metronic/assets/admin/layout4/scripts/layout.js"
                ));
            bundles.Add(new ScriptBundle("~/js/metronic/demo").Include("~/Themes/metronic/assets/admin/layout4/scripts/demo.js"));
            bundles.Add(new ScriptBundle("~/js/metronic/flot").Include(
                "~/Themes/metronic/assets/global/plugins/flot/jquery.flot.min.js",
                "~/Themes/metronic/assets/global/plugins/flot/jquery.flot.resize.min.js",
                "~/Themes/metronic/assets/global/plugins/flot/jquery.flot.pie.min.js",
                 "~/Themes/metronic/assets/global/plugins/flot/jquery.flot.stack.min.js",
                  "~/Themes/metronic/assets/global/plugins/flot/jquery.flot.crosshair.min.js",
                   "~/Themes/metronic/assets/global/plugins/flot/jquery.flot.categories.min.js"
                ));



            bundles.Add(new ScriptBundle("~/js/metronic/select2").Include("~/Themes/metronic/assets/global/plugins/select2/select2.min.js"));
            bundles.Add(new ScriptBundle("~/js/metronic/datatable").Include(
                "~/Themes/metronic/assets/global/plugins/datatables/media/js/jquery.dataTables.min.js",
                "~/Themes/metronic/assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"
                ));
            //bundles.Add(new ScriptBundle("~/js/metronic/datatable-customize").Include("~/Themes/metronic/datatable.js"));

            bundles.Add(new ScriptBundle("~/js/metronic/datatable-ajax-source").Include("~/Themes/metronic/assets/global/scripts/datatable-ajax-source.js"));
            bundles.Add(new ScriptBundle("~/js/metronic/datatable-none-ajax-source").Include("~/Themes/metronic/assets/global/scripts/datatable-none-ajax-source.js"));

            bundles.Add(new ScriptBundle("~/js/metronic/moment").Include("~/Themes/metronic/assets/global/plugins/moment.min.js"));
            bundles.Add(new ScriptBundle("~/js/metronic/datepicker").Include("~/Themes/metronic/assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"));

            bundles.Add(new ScriptBundle("~/js/metronic/datetimepicker").Include("~/Themes/metronic/assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js"));
            //Đoạn tùy chỉnh
            //bundles.Add(new ScriptBundle("~/js/metronic/ecommerce-index").Include("~/Themes/metronic/ecommerce-index.js"));
            //bundles.Add(new ScriptBundle("~/js/metronic/ecommerce-orders").Include("~/Themes/metronic/ecommerce-orders.js"));
            //bundles.Add(new ScriptBundle("~/js/metronic/ecommerce-orders-view").Include("~/Themes/metronic/ecommerce-orders-view.js"));
            //bundles.Add(new ScriptBundle("~/js/metronic/ecommerce-products").Include("~/Themes/metronic/ecommerce-products.js"));
            //bundles.Add(new ScriptBundle("~/js/metronic/ecommerce-products-edit").Include("~/Themes/metronic/ecommerce-products-edit.js"));
            //bundles.Add(new ScriptBundle("~/js/metronic/ui-general").Include("~/Themes/metronic/ui-general.js"));
            //bundles.Add(new ScriptBundle("~/js/metronic/ui-tree").Include("~/Themes/metronic/ui-tree.js"));
            //bundles.Add(new ScriptBundle("~/js/metronic/ui-blockui").Include("~/Themes/metronic/ui-blockui.js"));

            bundles.Add(new ScriptBundle("~/js/metronic/login").Include("~/Themes/metronic/assets/admin/pages/scripts/login.js"));
            //End đoạn tùy chỉnh

            bundles.Add(new ScriptBundle("~/js/metronic/maxlength").Include("~/Themes/metronic/assets/global/plugins/bootstrap-maxlength/bootstrap-maxlength.min.js"));
            bundles.Add(new ScriptBundle("~/js/metronic/touchspin").Include("~/Themes/metronic/assets/global/plugins/bootstrap-touchspin/bootstrap.touchspin.min.js"));
            bundles.Add(new ScriptBundle("~/js/metronic/inputmask").Include("~/Themes/metronic/assets/global/plugins/jquery-inputmask/jquery.inputmask.bundle.min.js"));
            bundles.Add(new ScriptBundle("~/js/metronic/fancybox").Include("~/Themes/metronic/assets/global/plugins/fancybox/source/jquery.fancybox.pack.js"));
            //bundles.Add(new ScriptBundle("~/js/metronic/plupload").Include("~/Themes/metronic/plupload/js/plupload.full.min.js"));

            //bundles.Add(new ScriptBundle("~/js/metronic/pulsate").Include("~/Themes/metronic/jquery.pulsate.min.js"));
            //bundles.Add(new ScriptBundle("~/js/metronic/bootpag").Include("~/Themes/metronic/jquery.bootpag.min.js"));
            //bundles.Add(new ScriptBundle("~/js/metronic/holder").Include("~/Themes/metronic/holder.js"));

            bundles.Add(new ScriptBundle("~/js/metronic/jstree").Include("~/Themes/metronic/assets/global/plugins/jstree/dist/jstree.min.js"));

            bundles.Add(new ScriptBundle("~/js/metronic/validate").Include("~/Themes/metronic/assets/global/plugins/jquery-validation/js/jquery.validate.min.js"));

            bundles.Add(new ScriptBundle("~/js/metronic/jqueryval").Include(
                        "~/Themes/metronic/assets/global/jqueryval/jquery.unobtrusive*",
                        "~/Themes/metronic/assets/global/jqueryval/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/js/metronic/bootstrap-fileinput").Include("~/Themes/metronic/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.js"));

            bundles.Add(new ScriptBundle("~/js/metronic/customize").Include("~/Themes/metronic/assets/global/scripts/customize.js"));
            bundles.Add(new ScriptBundle("~/js/metronic/colorpicker").Include("~/Themes/metronic/assets/global/plugins/bootstrap-colorpicker/js/bootstrap-colorpicker.js"));

            bundles.Add(new ScriptBundle("~/js/metronic/bootstrap-select").Include("~/Themes/metronic/assets/global/plugins/bootstrap-select/bootstrap-select.min.js"));

            //End Script
            //Start CSS

            bundles.Add(new StyleBundle("~/css/metronic/bootstrap-select").Include("~/Themes/metronic/assets/global/plugins/bootstrap-select/bootstrap-select.min.css"));

            bundles.Add(new StyleBundle("~/css/metronic/font-awesome").Include("~/Themes/metronic/assets/global/plugins/font-awesome/css/font-awesome.min.css"));
            bundles.Add(new StyleBundle("~/css/metronic/simple-line-icons").Include("~/Themes/metronic/assets/global/plugins/simple-line-icons/simple-line-icons.min.css"));
            bundles.Add(new StyleBundle("~/css/metronic/bootstrap").Include("~/Themes/metronic/assets/global/plugins/bootstrap/css/bootstrap.min.css"));
            bundles.Add(new StyleBundle("~/css/metronic/uniform").Include("~/Themes/metronic/assets/global/plugins/uniform/css/uniform.default.css"));
            bundles.Add(new StyleBundle("~/css/metronic/bootstrap-switch").Include("~/Themes/metronic/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css"));

            bundles.Add(new StyleBundle("~/css/metronic/components").Include("~/Themes/metronic/assets/global/css/components-md.css"));
            bundles.Add(new StyleBundle("~/css/metronic/plugins").Include("~/Themes/metronic/assets/global/css/plugins-md.css"));
            bundles.Add(new StyleBundle("~/css/metronic/layout").Include("~/Themes/metronic/assets/admin/layout4/css/layout.css"));
            bundles.Add(new StyleBundle("~/css/metronic/default").Include("~/Themes/metronic/assets/admin/layout4/css/themes/light.css"));
            bundles.Add(new StyleBundle("~/css/metronic/custom").Include("~/Themes/metronic/assets/admin/layout4/css/custom.css"));

            bundles.Add(new StyleBundle("~/css/metronic/select2").Include("~/Themes/metronic/assets/global/plugins/select2/select2.css"));
            bundles.Add(new StyleBundle("~/css/metronic/datatable").Include("~/Themes/metronic/assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css"));
            bundles.Add(new StyleBundle("~/css/metronic/datepicker").Include("~/Themes/metronic/assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css"));

            bundles.Add(new StyleBundle("~/css/metronic/datetimepicker").Include("~/Themes/metronic/assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.cs"));

            bundles.Add(new StyleBundle("~/css/metronic/fancybox").Include("~/Themes/metronic/assets/global/plugins/fancybox/source/jquery.fancybox.css"));

            //bundles.Add(new StyleBundle("~/css/metronic/style").Include("~/Themes/metronic/style.min.css"));

            bundles.Add(new StyleBundle("~/css/metronic/login3").Include("~/Themes/metronic/assets/admin/pages/css/login3.css"));

            bundles.Add(new StyleBundle("~/css/metronic/bootstrap-fileinput").Include("~/Themes/metronic/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css"));

            bundles.Add(new StyleBundle("~/css/metronic/profile").Include("~/Themes/metronic/assets/admin/pages/css/profile.css"));

            bundles.Add(new StyleBundle("~/css/metronic/error").Include("~/Themes/metronic/assets/admin/pages/css/error.css"));

            bundles.Add(new StyleBundle("~/css/metronic/pricing-table").Include("~/Themes/metronic/assets/admin/pages/css/pricing-table.css"));
            bundles.Add(new StyleBundle("~/css/metronic/colorpicker").Include("~/Themes/metronic/assets/global/plugins/bootstrap-colorpicker/css/colorpicker.css"));
            bundles.Add(new StyleBundle("~/css/metronic/timeline-old").Include("~/Themes/metronic/assets/admin/pages/css/timeline-old.css"));
            bundles.Add(new StyleBundle("~/css/metronic/jstree").Include("~/Themes/metronic/assets/global/plugins/jstree/dist/themes/default/style.min.css"));

            BundleTable.EnableOptimizations = false;
        }
        #endregion
    }
}
