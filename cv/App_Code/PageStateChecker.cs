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
/// PageStateChecker centralizes the code, devoted to check the page entrance 
/// nature( i.e. Postback, ReEntry).
/// </summary>
public static class PageStateChecker
{

    public static void PageStateChecker_SERVICE(
        string PageSignature,
        System.Web.HttpRequest Request,
        bool IsPostBack,
        System.Web.SessionState.HttpSessionState Session
      )
    {
        //
        ReEntrantChecker.ReEntrantChecker_SERVICE(//-------------NB.---------------------
            Request
            , Session
        );
        //
        /*
         * NB. page state check.-----------------------------------------------------------------
         * 
         */
        if (
            ! IsPostBack//------------------------------------------------------false
            && ! ((bool)(Session["IsReEntrant"]))//-----------------------------false
            )
        {// first absolute entrance
            LoggingToolsContainerNamespace.LoggingToolsContainer.LogBothSinks_DbFs(
                PageSignature + " : IsPostBack, IsReEntrant = false, false. "
                , 0
            );
            RunTimeDebugger.RunTimeDebugger_SERVICE( Session);
        }
        else if (
            ! IsPostBack//----------------------------------------------------false
            && (bool)(Session["IsReEntrant"])//-_-----------------------------true
            )
        {// coming from html-numbers of pager
            LoggingToolsContainerNamespace.LoggingToolsContainer.LogBothSinks_DbFs(
                PageSignature + " : IsPostBack, IsReEntrant = false, true. "
                , 0
            );
            RunTimeDebugger.RunTimeDebugger_SERVICE( Session);
            // needed combo-refresh, but re-select combo-Value from Session --------
        }
        else if (
            IsPostBack//------------------------------------------------------true
            && !(bool)(Session["IsReEntrant"])//------------------------------false
            )
        {
            LoggingToolsContainerNamespace.LoggingToolsContainer.LogBothSinks_DbFs(
                PageSignature + " : IsPostBack, IsReEntrant = true, false. "
                , 0
            );
            RunTimeDebugger.RunTimeDebugger_SERVICE( Session);
        }
        else if (
            IsPostBack//------------------------------------------------------true
            && (bool)(Session["IsReEntrant"])//-------------------------------true
            )
        {// coming from combo-index-changed.
            LoggingToolsContainerNamespace.LoggingToolsContainer.LogBothSinks_DbFs(
                PageSignature + " : IsPostBack, IsReEntrant = true, true. "
                , 0
            );
            RunTimeDebugger.RunTimeDebugger_SERVICE( Session);
        }// no "else" possible: case mapping is complete.
    }// end SERVICE


}// end class
