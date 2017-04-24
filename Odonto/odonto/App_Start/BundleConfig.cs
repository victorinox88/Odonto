using System.Web;
using System.Web.Optimization;

namespace odonto
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/moment.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-datetimepicker.js",
                      "~/Scripts/fullcalendar.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                "~/Scripts/jquery.dataTables.js",
                "~/Scripts/dataTables.bootstrap.js",
                "~/Scripts/select2.js",
                "~/Scripts/calendar.js",
                "~/Scripts/funtions.js",
                "~/Scripts/jquery.form.js",
                "~/Scripts/jquery.blockUI.js",
                "~/Scripts/jscolor.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      /*"~/Content/jquery.dataTables.css",*/
                      "~/Content/dataTables.bootstrap.css",
                      "~/Content/select2.css",
                      "~/Content/Calendar.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/fullcalendar.css"));
        }
    }
}
