using System.Web;
using System.Web.Optimization;

namespace IMS_IMS_IMS
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //    "~/Scripts/jquery.unobtrusive-ajax.js", 
            //    "~/Scripts/jquery.unobtrusive-ajax.min.js"));


            bundles.Add(new ScriptBundle("~/admin-lte/js").Include(
             "~/admin-lte/js/app.js",
              "~/admin-lte/plugins/fastclick/fastclick.js",
              "~/admin-lte/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js"

             ));
            bundles.Add(new StyleBundle("~/admin-lte/css").Include(
                     "~/admin-lte/css/AdminLTE.css",
                     "~/admin-lte/css/AdminLTE.min.css",
                     "~/admin-lte/css/adminlte.css.map",
                     "~/admin-lte/css/adminlte.min.css.map",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/admin-lte/css/AdminLTE.css",
                      "~/admin-lte/css/skins/skin-blue.css",
                      "~/admin-lte/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css",
                      "~/Content/Styles/bootstrap-theme.min.css",
                      "~/Content/Styles/jquery-ui.css"


                      ));
            bundles.Add(new ScriptBundle("~/bundles/scripts").IncludeDirectory(
                "~/Scripts", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
              "~/Scripts/respond.js",
              "~/Scripts/moment.js",
              "~/Scripts/bootstrap.js",
              "~/Scripts/bootstrap-datepicker.js"));


            BundleTable.EnableOptimizations = true;
        }

    }
}
