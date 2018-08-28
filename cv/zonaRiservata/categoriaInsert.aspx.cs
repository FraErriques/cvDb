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


public partial class zonaRiservata_categoriaInsert : System.Web.UI.Page
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
            "zonaRiservata_categoriaInsert"
            , this.Request
            , this.IsPostBack
            , this.Session
        );
        //----------------------------------------------- END  page state check.-----------------
        //
        if (!this.IsPostBack)
        {
            this.loadData();
        }// else don't.
    }// end Page_Load.


    /// <summary>
    /// </summary>
    private void loadData()
    {
        System.Data.DataTable resultset =
            Entity.Proxies.usp_settoreCandidatura_LOOKUP_LOAD_SERVICE.usp_settoreCandidatura_LOOKUP_LOAD();
        // ready
        this.grdCategorie.DataSource = resultset;
        this.grdCategorie.DataBind();
    }// end pseudo-service


    protected void btnCategoriaInsert_Click(object sender, EventArgs e)
    {
        bool result = true;// bool mask.
        //
        for (int c = 0; c < this.grdCategorie.Rows.Count; c++)
        {
            try
            {
                string curCategory = this.grdCategorie.Rows[c].Cells[1].Text;
                if (this.txtCategoriaInsert.Text.ToLower() == curCategory.ToLower())
                {
                    result &= false;
                    break;// first matching entry -> failure.
                }
                else
                {
                    result &= true;
                    continue;// comparing.
                }
            }
            catch (System.Exception ex)
            {
                result &= false;
                string dbg = ex.Message;
            }
        }
        //
        if (!result)
        {
            this.lblCategoriaInsert.Text = " la categoria proposta risulta omonima ad una in essere.";
            this.lblCategoriaInsert.BackColor = System.Drawing.Color.Red;
            return;// on page.
        }
        else
        {
            this.lblCategoriaInsert.Text = "";
            this.lblCategoriaInsert.BackColor = System.Drawing.Color.Transparent;
            // TODO call Proxy for INSERT and check result.
            int insertionesult =
                Entity.Proxies.usp_settoreCandidatura_LOOKUP_INSERT_SERVICE.usp_settoreCandidatura_LOOKUP_INSERT(
                    this.txtCategoriaInsert.Text.Trim(),// NB. let the empty strings become DbNull,
                    null // trx
                );
            if (0 == insertionesult)
            {
                this.Response.Redirect("../home.aspx");
            }
            else
            {
                this.lblCategoriaInsert.Text = " inserimento fallito.";
                this.lblCategoriaInsert.BackColor = System.Drawing.Color.Red;
                return;// on page.
            }
        }
    }// end btnCategoriaInsert_Click


}// end class


#region cantina

///// <summary>
///// delegato della freccetta di cambio pagina, in un asp:DataGrid che abbia l'attributo
///// AllowPaging="true".
///// </summary>
///// <param name="source"></param>
///// <param name="e"></param>
//protected void dgrCategorie_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
//{
//    /*
//     * delegato della freccetta di cambio pagina, nel datagrid. Qui va implementata la 
//     * chiamata ad una query parametrica sul range di identity da estrarre. Tutto cio'
//     * funziona solo se non ci sono buchi nell'indice di tabella; in quest'ultimo caso
//     * capitera' di chiedere (ad es.) un range [10, 20] ed estrarre un paio di righe, causa
//     * buchi nell'identity; tali buchi sono generati da "delete" e successive insert, che
//     * "bruciano" gli indici cancellati. Se c'e' un database-administrator che garantisce 
//     * la completezza dell'insieme di identity, questo delegato e' ammissibile.
//     * In caso contrario si deve implementare un "Cacher" modello "Sardegna", che metta in 
//     * Sessione( o in un analoga collection, in caso di non-web-application) il datatable 
//     * estratto, senza fare riduzioni sul range di righe; in questo caso ci sara' un delegato 
//     * che prende a parametro il numero di pagina richiesto( chiamiamolo m) ed una sezione in
//     * configurazione che contiene la cardinalita' della pagina( chiamiamola n). Alla richiesta
//     * della pagina "m", il delegato va a prendere dalla Sessione le righe 
//     * dall'indice m*n-n+1 a  m*n. Ovvero dalla riga n*(m-1)+1, che e' la prima riga successiva 
//     * alla pagina precedente, fino a m*n che e' l'ultima riga della pagina richiesta. Tutto cio'
//     * funziona molto bene fintantoche' il resultset ha una dimensione compatibile con gli spazi
//     * che puo' occupare una collection in memoria applicativa(ram). Se si estraggono migliaia di
//     * righe, questo tipo di cacher blocca l'applicazione.
//     * Una terza via, che permette di spostare l'onere dalla memoria applicativa a quella 
//     * del db-server, e':
//     * implementare una stored che crei una vista( in user_db, perche' in tempdb non e' consentito), 
//     * con la logica di join voluta e con una chiave primaria identity(1,1) 
//     * che sara' completa, in quanto nessuno puo' andare a
//     * fare delete su tale vista. Quindi, dal delegato della freccetta di cambio pagina,
//     * invocare una stored che faccia una query su tale vista, con una clausola 
//     * where identity_index between  n*(m-1)+1  and  n*m  (vedasi sopra per il significato di "m"
//     * ed "n"). La strategia e' chiamare una tryCreateView che fa: if !exists( view) create it;
//     * quindi la select where identity_index between  n*(m-1)+1  and  n*m.
//     * 
//     * -- NB. segue il tipo di query da fare sulla temp_view. -------
//     *          select 
//     *              * 
//     *          from [cv_db].[dbo].[20100708_tmp_v_]
//     *          where 
//     *              id between 900 and 912
//     *
//     * 
//     * 
//     * -- segue sintassi di creazione della view ----------
//     * 
//     *       create view [dbo].[20100708_tmp_v_]
//     *       as
//     *           select * from [ProcBBT].[dbo].[job]
//     * 
//     * -- NB. le views vengono salvate allo spegnimento dell'istanza di sqlServer; se si ha
//     *        una logica applicativa che ne produce molte, bisogna prevedere la drop esplicita.
//     * 
//     * -- NB. la sintassi per avere una colonna che simuli una primary key identity(1,1)
//     *        e' la seguente:
//     *        
//     * 
//     *      USE ProcBBT;
//     *       GO
//     *       WITH nomeResultsetTemporaneo AS
//     *       (
//     *           SELECT *,
//     *           ROW_NUMBER() OVER (ORDER BY id) AS 'RowNumber'
//     *           FROM job
//     *       ) 
//     *       SELECT * 
//     *       FROM nomeResultsetTemporaneo
//     *       WHERE RowNumber BETWEEN 55 AND 57;
//     *
//     * 
//     * -- NB. in generale si puo' utilizzare la seguente sintassi:
//     * 
//     * 
//     *           SELECT
//     *               ROW_NUMBER() OVER (ORDER BY c.id_settore) AS 'RowNumber'
//     *               ,c.id as id_candidato
//     *               ,c.nominativo
//     *               ,c.id_settore
//     *               ,c.note
//     *               ,s.nomeSettore
//     *           from 
//     *               candidato c
//     *               , settoreCandidatura_LOOKUP s
//     *           where 
//     *               c.id_settore = s.id
//     *
//     *------- NB.----------------in sintesi--------------------------------
//     *
//     * 
//     *      create view tmp_20100708
//     *       as
//     *           SELECT
//     *               ROW_NUMBER() OVER (ORDER BY c.id_settore) AS 'RowNumber'
//     *               ,c.id as id_candidato
//     *               ,c.nominativo
//     *               ,c.id_settore
//     *               ,c.note
//     *               ,s.nomeSettore
//     *           from 
//     *               candidato c
//     *               , settoreCandidatura_LOOKUP s
//     *           where 
//     *               c.id_settore = s.id
//     *       GO
//     *
//     * e quindi, all'occorrenza:
//     *       select * from tmp_20100708 where RowNumber between 7 and 9
//     *
//     * 
//     * 
//     * 
//     */
//}

#endregion cantina
