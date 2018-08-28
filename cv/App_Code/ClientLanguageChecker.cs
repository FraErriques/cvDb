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
/// ClientLanguageChecker queries in the Request packet, for the locale of the client machine,
/// on which the requesting-browser is running.
/// This allows to localize( i.e. customize the labels), depending on the requesting-browser.
/// </summary>
public static class ClientLanguageChecker
{


    public static string ClientLanguageGet(
        System.Web.HttpRequest Request
      )
    {
        string res = null;
        //
        if (Request.UserLanguages != null)
        {
            if (0 < Request.UserLanguages.Length)
            {
                res = Request.UserLanguages[0];// the first one, in the favourite-list.
            }// else skip
        }// else skip
        // ready.
        return res;
    }//


    /// <summary>
    /// for monetary GUIs.
    /// NB. a "set" omologous is feasible.
    /// </summary>
    /// <returns></returns>
    public static string ClientCurrencySymbolGet()
    {
        string currentCurrencySymbol =
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol;
        return currentCurrencySymbol;
    }//

}// end class


#region cantina
/* ---------------------------- tips & tricks ----------------------------------
 * 
 * System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Lang) ;
 * if (CurrencySymbol != null && CurrencySymbol != "")
 * {
 *      CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol = CurrencySymbol;
 * }// else skip
 * 
 */
#endregion
