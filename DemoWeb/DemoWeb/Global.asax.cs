using Demo.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DemoWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //不知道這個是幹嘛用的@@
        private static readonly object LockObj = new object();

        protected void Application_Start()
        {
            #region 原生Code
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            #endregion
        }
        #region Note About Global.asax.cs
        //reference: http://sharebody.com/list.asp?id=50684
        //這裡有很多事件可以註冊使用
        //·Application_Init：在應用程序被實例化或第一次被調用時，該事件被觸發。對于所有的HttpApplication 對象實例，它都會被調用。 
        //·Application_Disposed：在應用程序被銷毀之前觸發。這是清除以前所用資源的理想位置。 
        //·Application_Error：當應用程序中遇到一個未處理的異常時，該事件被觸發。 
        //·Application_Start：在HttpApplication 類的第一個實例被創建時，該事件被觸發。它允許你創建可以由所有HttpApplication 實例訪問的對象。 
        //·Application_End：在HttpApplication 類的最后一個實例被銷毀時，該事件被觸發。在一個應用程序的生命周期內它只被觸發一次。 
        //·Application_BeginRequest：在接收到一個應用程序請求時觸發。對于一個請求來說，它是第一個被觸發的事件，請求一般是用戶輸入的一個頁面請求（URL）。 
        //·Application_EndRequest：針對應用程序請求的最后一個事件。 
        //·Application_PreRequestHandlerExecute：在 ASP.NET 頁面框架開始執行諸如頁面或 Web 服務之類的事件處理程序之前，該事件被觸發。 
        //·Application_PostRequestHandlerExecute：在 ASP.NET 頁面框架結束執行一個事件處理程序時，該事件被觸發。 
        //·Applcation_PreSendRequestHeaders：在 ASP.NET 頁面框架發送 HTTP 頭給請求客戶（瀏覽器）時，該事件被觸發。 
        //·Application_PreSendContent：在 ASP.NET 頁面框架發送內容給請求客戶（瀏覽器）時，該事件被觸發。 
        //·Application_AcquireRequestState：在 ASP.NET 頁面框架得到與當前請求相關的當前狀態（Session 狀態）時，該事件被觸發。 
        //·Application_ReleaseRequestState：在 ASP.NET 頁面框架執行完所有的事件處理程序時，該事件被觸發。這將導致所有的狀態模塊保存它們當前的狀態數據。 
        //·Application_ResolveRequestCache：在 ASP.NET 頁面框架完成一個授權請求時，該事件被觸發。它允許緩存模塊從緩存中為請求提供服務，從而繞過事件處理程序的執行。 
        //·Application_UpdateRequestCache：在 ASP.NET 頁面框架完成事件處理程序的執行時，該事件被觸發，從而使緩存模塊存儲響應數據，以供響應后續的請求時使用。 
        //·Application_AuthenticateRequest：在安全模塊建立起當前用戶的有效的身份時，該事件被觸發。在這個時候，用戶的憑據將會被驗證。 
        //·Application_AuthorizeRequest：當安全模塊確認一個用戶可以訪問資源之后，該事件被觸發。 
        //·Session_Start：在一個新用戶訪問應用程序 Web 站點時，該事件被觸發。 
        //·Session_End：在一個用戶的會話超時、結束或他們離開應用程序 Web 站點時，該事件被觸發。 
        #endregion

        void Application_AcquireRequestState(object sender, EventArgs e)
        {
            InitSettingConfig();
        }

        void Session_Start(object sender, EventArgs e)
        {
            InitSettingConfig();
        }

        private void InitSettingConfig()
        {
            //連到Demo.Configuration的class
            string configkey = Configuration.ConfigKey;
            //這裡的Code都看得不是很懂了...
            if (HttpRuntime.Cache[configkey] != null)
                return;

            lock(LockObj)
            {
                if(HttpRuntime.Cache[configkey] == null)
                {
                    var entries = HttpRuntime.Cache.Cast<DictionaryEntry>()
                                    .Where(e => e.Key.ToString().StartsWith(configkey));
                    foreach(DictionaryEntry entry in entries)
                    {
                        HttpRuntime.Cache.Remove(entry.Key.ToString());
                    }
                }
            }
            // *** 取得/設定 SettingConfig ***
            this.ConfigSetting(configkey);
            //繁體中文??
            //System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("zh-TW");
            //System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            //System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
        }

        private void ConfigSetting(params string[] keys)
        {
            string appConfig;
            string filePath;
            
            foreach(string key in keys)
            {
                if(HttpRuntime.Cache[key] == null)
                {
                    appConfig = System.Configuration.ConfigurationManager.AppSettings[key];
                    filePath = Server.MapPath(appConfig);
                    Configuration.InitConfiguration(filePath);

                    //這個類別是幹嘛用的?
                    HttpRuntime.Cache.Insert(key, key, new System.Web.Caching.CacheDependency(filePath));
                }
            }
        }
    }
}
