using System.Web;
using System.Web.Optimization;

namespace SI
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
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/cssThemeLight").Include(
                "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/cssThemeDark").Include(
                "~/Content/bootstrapDark.css"));

            bundles.Add(new StyleBundle("~/Content/cssDark").Include(                      
                       "~/Content/jquery.switchButton.css"));

            bundles.Add(new ScriptBundle("~/Scripts/scriptsDark").Include(
                        "~/Scripts/cookies.js",
                        "~/Scripts/darkSkin.js"));

            bundles.Add(new ScriptBundle("~/Scripts/switchButton").Include(
                        "~/Scripts/jquery.switchButton.js",
                        "~/Scripts/switchButtonProperties.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui.js"));

            bundles.Add(new StyleBundle("~/bundles/jqueryuicss").Include(
                        "~/Content/jquery-ui.css",
                        "~/Content/jquery-ui.structure.css",
                        "~/Content/jquery-ui.theme.css"));
        }
    }
}
