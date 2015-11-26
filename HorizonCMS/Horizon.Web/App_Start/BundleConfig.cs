using System.Web;
using System.Web.Optimization;

namespace Horizon.Web
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, consulte http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/angular-js").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-route.js"));


            bundles.Add(new ScriptBundle("~/bundles/intelliTable-js").Include(
                "~/Scripts/intelliTable/intelliTable.js",
                 "~/Scripts/intelliTable/serializer.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/chart-js").Include(
                "~/Scripts/chart.js"));

            bundles.Add(new ScriptBundle("~/bundles/TinyMCE").Include("~/Scripts/tinymce/tinymce.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                       "~/Content/simple-sidebar.css"));

            bundles.Add(new StyleBundle("~/Content/admin").Include(
                "~/Content/Admin.css"
                ));

        }


    }
}
