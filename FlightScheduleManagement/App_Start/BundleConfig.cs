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
    "~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new ScriptBundle("~/AdminPortal/js").Include(
    "~/AdminPortal/js/adminlte.min.js",
    "~/AdminPortal/plugins/summernote/summernote-bs4.min.js"
    ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
             "~/Content/bootstrap.css",
             "~/AdminPortal/css/adminlte.min.css",
             "~/AdminPortal/plugins/fontawesome-free/css/all.min.css",
             "~/AdminPortal/plugins/summernote/summernote-bs4.min.css",
             "~/Content/site.css"
             ));
        }
    }
}
