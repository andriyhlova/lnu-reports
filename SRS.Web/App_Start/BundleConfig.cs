using System.Web.Optimization;

namespace SRS.Web
{
    public static class BundleConfig
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
                        "~/Scripts/Chosen/DocSupport/prism.js",
                        "~/Scripts/Chosen/custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/steps").Include(
                        "~/Scripts/Steps/jquery.steps.js"));

            bundles.Add(new ScriptBundle("~/bundles/departmentSelector").Include(
                        "~/Scripts/Common/departmentSelector.js"));

            bundles.Add(new ScriptBundle("~/bundles/customDatatable").Include(
                        "~/Scripts/Common/customDatatable.js"));

            bundles.Add(new ScriptBundle("~/bundles/usersManagement/edit").Include(
                        "~/Scripts/UsersManagement/edit.js"));

            bundles.Add(new ScriptBundle("~/bundles/collapsible").Include(
                        "~/Scripts/Common/collapsible.js"));

            bundles.Add(new ScriptBundle("~/bundles/searchComponent").Include(
                        "~/Scripts/Common/searchComponent.js"));

            bundles.Add(new ScriptBundle("~/bundles/stepper").Include(
                        "~/Scripts/Common/stepper.js"));

            bundles.Add(new ScriptBundle("~/bundles/publications/edit").Include(
                        "~/Scripts/Publications/edit.js"));

            bundles.Add(new ScriptBundle("~/bundles/reports/edit").Include(
                        "~/Scripts/Reports/edit.js"));

            bundles.Add(new ScriptBundle("~/bundles/cathedrareports/edit").Include(
                        "~/Scripts/CathedraReports/edit.js"));

            bundles.Add(new ScriptBundle("~/bundles/themeofscientificworks/edit").Include(
                        "~/Scripts/ThemeOfScientificWorks/edit.js"));
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/chosen/bundle").Include(
                      "~/Content/Chosen/DocSupport/style.css",
                      "~/Content/Chosen/DocSupport/prism.css",
                      "~/Content/Chosen/chosen.css",
                      "~/Content/Chosen/custom.css"));

            bundles.Add(new StyleBundle("~/Content/steps/bundle").Include(
                      "~/Content/Steps/steps.css"));

            bundles.Add(new StyleBundle("~/Content/usersManagement/bundle").Include(
                      "~/Content/UsersManagement/edit.css"));

            bundles.Add(new StyleBundle("~/Content/search/bundle").Include(
                      "~/Content/Search/search.css"));

            bundles.Add(new StyleBundle("~/Content/reports/bundle").Include(
                      "~/Content/Reports/edit.css"));

            bundles.Add(new StyleBundle("~/Content/themeOfScientificWorks/bundle").Include(
                      "~/Content/ThemeOfScientificWorks/edit.css"));
        }
    }
}
