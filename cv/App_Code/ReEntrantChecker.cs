using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public static class ReEntrantChecker
{


    public static void ReEntrantChecker_SERVICE(
        HttpRequest Request
        ,System.Web.SessionState.HttpSessionState Session
        )
    {
        //
        string relativeUrl = Request.AppRelativeCurrentExecutionFilePath;
        string pageName = relativeUrl.Substring(
            relativeUrl.LastIndexOf("/") + 1
            );
        pageName = pageName.Substring(0, pageName.Length - 5);// 5==(4 of .ext + 1 for zero-based -str)
        //
        if (
            null != Session["currentPage"]
            && null != Session["IsReEntrant"]
            )
        {
            if (
                pageName == (string)(Session["currentPage"])
                )
            {
                Session["IsReEntrant"] = true;
            }
            else
            {
                Session["IsReEntrant"] = false;
            }
            // now, update  Session["currentPage"]
            Session["currentPage"] = pageName;// NB.----------------------update---
            //
        }
        else
        {
            throw new System.Exception("candidatoLoad:: missing Session[currentPage & IsReEntrant]");
        }
        //
    }// end SERVICE.


}// end class.
