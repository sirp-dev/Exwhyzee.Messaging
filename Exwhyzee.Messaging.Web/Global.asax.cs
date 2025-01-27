﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Exwhyzee.Messaging.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(App_Start.WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            HttpConfiguration config = GlobalConfiguration.Configuration;

            config.Formatters.JsonFormatter
                        .SerializerSettings
                        .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

            // Get the exception object.
            Exception exc = Server.GetLastError();
            String strUrl = HttpContext.Current.Request.Url.Authority;

            // Handle HTTP errors
            if (exc.GetType() == typeof(HttpException))
            {
                // The Complete Error Handling Example generates
                // some errors using URLs with "NoCatch" in them;
                // ignore these here to simulate what would happen
                // if a global.asax handler were not implemented.
                if (exc.Message.Contains("NoCatch") || exc.Message.Contains("maxUrlLength"))
                    return;

                //Redirect HTTP errors to HttpError page
                //   Server.Transfer("error404.html");
            }

            // For other kinds of errors give the user some information
            // but stay on the default page
            Response.Write("<br/>");
            Response.Write("<br/>");




            Response.Write("<span style=\"height:100;width:100%;;color: #ff0000;\">");
            Response.Write("<h1 style=\"font-size:120px;color: #ff0000;text-align:center;padding-bottom:0px;margin-bottom:10px;\"> 404 </h2>");

            Response.Write("<h1 style=\"font-size:80px;color: #ff0000;text-align:center;margin-top:10px;\"> Oops! Not found.</h3>");

            Response.Write("<p style=\"text-align:center;\">");

            Response.Write("Try Again Later.");
            Response.Write("</p>");
            Response.Write("<p style=\"text-align:center;\">");
            Response.Write("<br/>");

            Response.Write("Mean while, you may Return to the <a style=\"background-color:#ff0000;color:#ffffff;padding:10px;\" href='javascript: history.back()'>previous Page</a>");
            Response.Write("</p>");


            Response.Write("</span>");



            // Log the exception and notify system operators
            ExceptionUtility.LogException(exc, "DefaultPage");
            ExceptionUtility.NotifySystemOps(exc);
            string sa = HttpContext.Current.Request.Url.AbsoluteUri;
            try
            {

                MailMessage mail = new MailMessage();

                //set the addresses 
                mail.From = new MailAddress("espErrorMail@exwhyzee.ng"); //IMPORTANT: This must be same as your smtp authentication address.
                mail.To.Add("espErrorMail@exwhyzee.ng");

                //set the content 

                mail.Subject = sa + " SMS WEBSITE ";


                mail.Body = exc.ToString();
                //send the message 
                SmtpClient smtp = new SmtpClient("mail.exwhyzee.ng");

                //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                NetworkCredential Credentials = new NetworkCredential("espErrorMail@exwhyzee.ng", "Exwhyzee@123");
                smtp.Credentials = Credentials;
                smtp.Send(mail);

            }
            catch (Exception ex)
            {


            }

            // Clear the error from the server
            Server.ClearError();
        }

        //void Application_Start(object sender, EventArgs e)
        //{
        //    // Code that runs on application startup  
        //    Application["TotalOnlineUsers"] = 0;
        //}

        //void Application_End(object sender, EventArgs e)
        //{
        //    //  Code that runs on application shutdown  

        //}

       

        //void Session_Start(object sender, EventArgs e)
        //{
        //    // Code that runs when a new session is started  
        //    Application.Lock();
        //    Application["TotalOnlineUsers"] = (int)Application["TotalOnlineUsers"] + 1;
        //    Application.UnLock();
        //}

        //void Session_End(object sender, EventArgs e)
        //{
        //    // Code that runs when a session ends.   
        //    // Note: The Session_End event is raised only when the sessionstate mode  
        //    // is set to InProc in the Web.config file. If session mode is set to StateServer   
        //    // or SQLServer, the event is not raised.  
        //    Application.Lock();
        //    Application["TotalOnlineUsers"] = (int)Application["TotalOnlineUsers"] - 1;
        //    Application.UnLock();
        //}

        
    }
}
