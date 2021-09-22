using System.Web;
using System.Web.Optimization;

namespace FlightScheduleManagement
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
    "~/Scripts/bootstrap.bundle.min.js",
    "~/Scripts/bootstrap-datepicker.min.js"
    ));

            bundles.Add(new Bundle("~/bundles/datatable").Include(
    "~/AdminPortal/plugins/datatables/jquery.dataTables.min.js",
    "~/AdminPortal/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js",
    "~/AdminPortal/plugins/datatables-responsive/js/dataTables.responsive.min.js",
    "~/AdminPortal/plugins/datatables-responsive/js/responsive.bootstrap4.min.js",
    "~/AdminPortal/plugins/datatables-buttons/js/dataTables.buttons.min.js",
    "~/AdminPortal/plugins/datatables-buttons/js/buttons.bootstrap4.min.js",
    "~/AdminPortal/plugins/jszip/jszip.min.js",
    "~/AdminPortal/plugins/pdfmake/pdfmake.min.js",
    "~/AdminPortal/plugins/pdfmake/vfs_fonts.js",
    "~/AdminPortal/plugins/datatables-buttons/js/buttons.html5.min.js",
    "~/AdminPortal/plugins/datatables-buttons/js/buttons.print.min.js",
    "~/AdminPortal/plugins/datatables-buttons/js/buttons.colVis.min.js",
    "~/AdminPortal/plugins/sweetalert2/sweetalert2.min.js",
    "~/AdminPortal/plugins/toastr/toastr.min.js",
    "~/AdminPortal/plugins/bootstrap-switch/js/bootstrap-switch.min.js"));

            bundles.Add(new ScriptBundle("~/AdminPortal/js").Include(
    "~/AdminPortal/js/adminlte.min.js",
    "~/AdminPortal/plugins/summernote/summernote-bs4.min.js"
    ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
             "~/Content/bootstrap.css",
             "~/AdminPortal/css/adminlte.min.css",
             "~/AdminPortal/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css",
             "~/AdminPortal/plugins/datatables-responsive/css/responsive.bootstrap4.min.css",
             "~/AdminPortal/plugins/datatables-buttons/css/buttons.bootstrap4.min.css",
             "~/AdminPortal/plugins/fontawesome-free/css/all.min.css",
             "~/AdminPortal/plugins/summernote/summernote-bs4.min.css",
              "~/AdminPortal/plugins/icheck-bootstrap/icheck-bootstrap.min.css",
              "~/AdminPortal/plugins/toastr/toastr.css",
             "~/Content/site.css",
              "~/Content/bootstrap-datepicker.min.css"
             ));
        }
    }
}
