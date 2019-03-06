﻿using System.Web;
using System.Web.Optimization;

namespace Linkedin
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryabs").Include(
                      "~/Scripts/jquery.unobtrusive-ajax.js",
                       "~/Scripts/bootjs.js"));

            bundles.Add(new ScriptBundle("~/bundles/feedscript").Include(
                       "~/Scripts/feedscript.js"));

            bundles.Add(new ScriptBundle("~/bundles/AsynchFeed").Include(
                       "~/Scripts/Asynchronous.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      //"~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"
                      ));


            bundles.Add(new ScriptBundle("~/bundles/profileFnc").Include(
                     "~/Scripts/script.js"
                     ));


            bundles.Add(new StyleBundle("~/bundles/css").Include(
                          "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/stylesheet.css",
                      "~/Content/style.css",
                      "~/Content/boot4.css",
                      "~/Content/stylesheetbody.css"
                      ));


            bundles.Add(new ScriptBundle("~/bundles/signalR").Include(
                    "~/Scripts/jquery.signalR-2.2.2.min.js"
             ));

            bundles.Add(new ScriptBundle("~/bundles/signalRUser").Include(
                   "~/signalr/js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/signalRClient").Include(
                  "~/Scripts/SignalRAsync.js"
           ));


            //< script src = "https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js" ></ script > 
            //< script src = "https://code.jquery.com/jquery-1.11.1.min.js" ></ script > *@





        }
    }
}
