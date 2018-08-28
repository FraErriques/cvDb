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

public partial class zonaRiservata_candidatoInsert : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!VerificaLasciapassare.CanLogOn(
                this.Session,
                this.Request.UserHostAddress
                )
            )
        {
            this.Response.Redirect("../errore.aspx");
        }// else il lasciapassare e' valido -> get in.
        //
        /*
         * NB. page state check.-----------------------------------------------------------------
         * 
         */
        PageStateChecker.PageStateChecker_SERVICE(
            "zonaRiservata_candidatoInsert"
            , this.Request
            , this.IsPostBack
            , this.Session
        );
        //----------------------------------------------- END  page state check.-----------------
        if (!this.IsPostBack)
        {
            ComboManager.populate_Combo_ddlSettore_for_INSERT(
                this.ddlSettori,
                null // no preselection. Instead to preselect choose the ordinal; eg. 2=="Appalti".
            );
        }// else DO NOT refresh the combo: it deletes the performed selection.
    }// end Page_Load




    protected bool validateForWriting(
        out int settore
        ,out string nominativo
        ,out string note
        )
    {
        bool result = true;// bool mask.
        //
        try
        {
            settore = int.Parse(this.ddlSettori.SelectedItem.Value);
            if (0 < settore)
            {
                result &= true;
            }
            else
            {
                result &= false;// sector choise is mandatory.
            }
        }
        catch( System.Exception ex)
        {
            string dbg = ex.Message;
            settore = -1;// invalid sector.
            result &= false;
        }
        //
        nominativo = this.txtNominativo.Text;
        if (
            null == nominativo
            || "" == nominativo
            )
        {
            nominativo = ""; // -> sobstitutes to DbNull.
            result &= false;
        }
        else
        {
            result &= true;
        }
        //
        note = this.txtNote.Text;
        if (
            null == note
            || "" == note
            )
        {
            note = ""; // -> sobstitutes to DbNull.
            result &= false;
        }
        else
        {
            result &= true;
        }
        //
        // ready.
        return result;
    }// end validateForWriting


    protected void btnCommit_Click(object sender, EventArgs e)
    {
        int settore;
        string nominativo;
        string note;
        bool validForWriting =
            this.validateForWriting(
                out settore
                ,out nominativo
                ,out note
            );
        int candidateInsertionResult = -1;// init to invalid.
        if (validForWriting)
        {
            candidateInsertionResult =
                Entity.Proxies.usp_candidato_INSERT_SERVICE.usp_candidato_INSERT(
                    nominativo
                    , settore
                    , note
                    , null // trx
                );
        }// else do not write.
        if (0 == candidateInsertionResult)
        {
            this.Response.Redirect("candidatoLoad.aspx");// in home, the new insertion will be visible, within the general list.
            this.lblResult.BackColor = System.Drawing.Color.Transparent;
        }
        else
        {
            this.lblResult.Text = "Non e' stato possibile inserire il candidato.";
            this.lblResult.BackColor = System.Drawing.Color.Red;
        }
        //
    }//


}//
