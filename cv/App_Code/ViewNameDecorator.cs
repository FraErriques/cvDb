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
/// ViewNameDecorator puts square brackets around the this.Session.SessionID string, to
/// let it acceptable for the DB, as ViewName, even when it starts with a figure( i.e. [1, 9]).
/// The DB raises an exception when a numeric-starting sessionId is passed as ViewName.
/// </summary>
public static class ViewNameDecorator
{


    public static string ViewNameDecorator_SERVICE( string SessionID)
    {
        string res = "[";
        res += SessionID;
        res += "]";
        return res;
    }// end SERVICE.


}// end class
