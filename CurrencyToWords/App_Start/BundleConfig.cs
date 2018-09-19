using System.Web.Optimization;

namespace CurrencyToWordsWeb
{
    public static class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Assets/js/jquery-1.8.3.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Assets/js/modernizr-2.6.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/validation").Include(
                      "~/Assets/js/jquery.validate.js",
                      "~/Assets/js/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Assets/css/bootstrap.min.css",
                      "~/Assets/css/site.css"));
        }
    }
}