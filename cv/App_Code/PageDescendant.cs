//using System;
//using System.Data;
//using System.Configuration;
//using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;


/*
 * 
 * TODO think to put in App_Code the localization.
 * 
 * 
 */



///// <summary>
///// localization element: for a single control.
///// </summary>
//struct ControlLocales
//{
//    public string controlName;
//    public object controlContents;//suitable to box a NameValueCollection.

//    public ControlLocales(string controlName, object controlContents)
//    {
//        this.controlName = controlName;
//        this.controlContents = controlContents;
//    }// end Ctor
//}//


///// <summary>
///// Summary description for PageDescendant
///// </summary>
//public partial class PageDescendant : System.Web.UI.Page
//{


//    protected void Page_Load(object sender, EventArgs e)
//    {
//        /* TODO 
//         *
//         * VerificaLasciapassare()
//         * ChechReEntrant()
//         * 
//         * 
//         */

//        System.Collections.Hashtable h = new System.Collections.Hashtable();
//        //
//        // prepare a single control's structure:
//        System.Collections.Specialized.NameValueCollection tmp =
//            new System.Collections.Specialized.NameValueCollection();
//        tmp.Add("it", "Consultazione Candidati");
//        tmp.Add("de", "Abfrage Bewerber");
//        tmp.Add("en", "Candidate Consultation");
//        ControlLocales cl_pnlPageNumber = new ControlLocales(
//            "pnlPageNumber"
//            , tmp
//        );

//        //
//        h.Add("pippo", tmp);
//        System.Collections.Specialized.NameValueCollection z =
//            (System.Collections.Specialized.NameValueCollection)(h["pippo"]);
//        // retrieve test
//        // it
//        string fake_localMark = "it";
//        string fake_result = z[fake_localMark];
//        // de
//        fake_localMark = "de";
//        fake_result = z[fake_localMark];
//        // en
//        fake_localMark = "en";
//        fake_result = z[fake_localMark];
//        //
//        //
//    }// end Page_Load


//}
