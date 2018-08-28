using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class PagerSardegnaTest : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        //
        /*
         * NB. page state check.-----------------------------------------------------------------
         * 
         */
        PageStateChecker.PageStateChecker_SERVICE(
            "zonaRiservata_candidatoLoad"
            , this.Request
            , this.IsPostBack
            , this.Session
        );
        //----------------------------------------------- END  page state check.-----------------
        if (!this.IsPostBack)
        {
            System.Data.DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("name", typeof(string));
            //
            for (int c = 33; c < 200; c++)
            {
                object[] filler = new object[2];
                filler[0] = c;
                filler[1] = (char)c;
                dt.Rows.Add(filler);
                //
                filler = null;
            }
            //
            Cacher cacher = new Cacher(dt);
            this.Session["Cacher"] = cacher;
        }// else already built.
    }// end Page_Load().


}// end class.
