using System.Web.Optimization;

namespace SRS.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScriptBundles(bundles);
            RegisterStyleBundles(bundles);
        }

        private static void RegisterScriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                        "~/Scripts/Login/login.js"));

            bundles.Add(new ScriptBundle("~/bundles/chosen").Include(
                        "~/Scripts/Chosen/chosen.jquery.js",
                        "~/Scripts/Chosen/DocSupport/prism.js"));

            bundles.Add(new ScriptBundle("~/bundles/departmentSelector").Include(
                        "~/Scripts/Common/departmentSelector.js"));

            bundles.Add(new ScriptBundle("~/bundles/customDatatable").Include(
                        "~/Scripts/Common/customDatatable.js"));
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/chosen").Include(
                      "~/Content/Chosen/DocSupport/style.css",
                      "~/Content/Chosen/DocSupport/prism.css",
                      "~/Content/Chosen/chosen.css",
                      "~/Content/Chosen/custom.css"));
        }
    }
}
