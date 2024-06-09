using System.Web;
using System.Web.Optimization;

namespace StudentWebApp
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

            //-- Data Table Script--
            bundles.Add(new ScriptBundle("~/bundles/multiselect").Include(
                "~/App_Themes/assets/plugins/multiselect/js/jquery.multi-select.js"));
            bundles.Add(new ScriptBundle("~/bundles/customselect").Include(
                "~/App_Themes/assets/plugins/custom-select/js/select2.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
               "~/App_Themes/assets/plugins/datatables/DataTables-1.10.18/js/jquery.dataTables.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/datatablebootstrp").Include(
              "~/App_Themes/assets/plugins/datatables/DataTables-1.10.18/js/dataTables.bootstrap.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/datatablebutton").Include(
             "~/App_Themes/assets/plugins/datatables/Buttons-1.5.4/js/dataTables.buttons.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/buttonbootstrap").Include(
             "~/App_Themes/assets/plugins/datatables/Buttons-1.5.4/js/buttons.bootstrap.min.js"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // --- Data Table Css
            bundles.Add(new StyleBundle("~/DataTable/cssdatatable").Include(
                "~/App_Themes/assets/plugins/datatables/DataTables-1.10.18/css/jquery.dataTables.min.css"));
            bundles.Add(new StyleBundle("~/DataTable/buttonbootstrap").Include
                ("~/App_Themes/assets/plugins/datatables/Buttons-1.5.4/css/buttons.bootstrap.min.css"));
            bundles.Add(new StyleBundle("~/DataTable/responsivebootstrap").Include(
                "~/App_Themes/assets/plugins/datatables/Responsive-2.2.2/css/responsive.bootstrap.min.css"));
            bundles.Add(new StyleBundle("~/DataTable/datatablebootstrap").Include(
                "~/App_Themes/assets/plugins/datatables/DataTables-1.10.18/css/dataTables.bootstrap.min.css"));
            bundles.Add(new StyleBundle("~/DataTable/multiselect").Include(
                "~/App_Themes/assets/plugins/multiselect/css/multi-select.css"));
        }
    }
}
