using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


/// <summary>
/// RunTimeDebugger prints variable values, by means of the log sinks, allowing a debug
/// of production-release-deployed applications.
/// </summary>
public static class RunTimeDebugger
{


    /// <summary>
    /// list inside all the Session elements that the current application needs to log.
    /// </summary>
    /// <param name="Session"></param>
    public static void RunTimeDebugger_SERVICE(
        System.Web.SessionState.HttpSessionState Session
      )
    {
        //
        LoggingToolsContainerNamespace.LoggingToolsContainer.LogBothSinks_DbFs(
            " Session[IsReEntrant] = " + ElementPrinter(
                Session["IsReEntrant"]
            )
            , 0);
        //
        LoggingToolsContainerNamespace.LoggingToolsContainer.LogBothSinks_DbFs(
            " Session[lasciapassare] = " + ElementPrinter(
                Session["lasciapassare"]
            )
            , 0);
        //
        LoggingToolsContainerNamespace.LoggingToolsContainer.LogBothSinks_DbFs(
            " Session[RoleChecker] = " + ElementPrinter(
                Session["RoleChecker"]
            )
            , 0);
        //
        LoggingToolsContainerNamespace.LoggingToolsContainer.LogBothSinks_DbFs(
            " Session[CacherDbView] = " + ElementPrinter(
                Session["CacherDbView"]
            )
            , 0);
        //
        LoggingToolsContainerNamespace.LoggingToolsContainer.LogBothSinks_DbFs(
            " Session[DynamicPortionPtr] = " + ElementPrinter(
                Session["DynamicPortionPtr"]
            )
            , 0);
        //
    }// end service RunTimeDebugger_SERVICE


    private static string ElementPrinter(object theElement)
    {
        string res;
        //
        if (
            null == theElement)
        {
            res = " is null. ";
        }
        else
        {
            res = theElement.ToString();
        }
        // ready
        return res;
    }//


}// end class
