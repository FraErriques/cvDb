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
/// Summary description for ComboManager
/// </summary>
public static class ComboManager
{


    public static void populate_Combo_ddlSettore_for_LOAD(
        System.Web.UI.WebControls.DropDownList ddlSettore,
        object selectedElement,
        out int indexOfAllSectors // it's one after the last id in the table.
      )
    {
        indexOfAllSectors = default(int);// compulsory init, for out pars; the func_body will let it actual.
        //--------------popolamento-----------------
        DataTable dtSettore = Entity.Proxies.usp_settoreCandidatura_LOOKUP_LOAD_SERVICE.usp_settoreCandidatura_LOOKUP_LOAD();
        ddlSettore.Items.Clear();
        ddlSettore.Items.Add(
            new System.Web.UI.WebControls.ListItem(
                "Auswahl des Geschäftsbereichs der Bewerbung -/- selezione del settore aziendale della candidatura",//--no query for this selection ---
                "0"
            )
        );
        int c = 0;// zero is the first row in the datatable.
        int max_identity = 0;
        if (null != dtSettore)
        {
            for (; c < dtSettore.Rows.Count; c++)
            {
                ddlSettore.Items.Add(
                    new System.Web.UI.WebControls.ListItem(
                        (string)(dtSettore.Rows[c]["nomeSettore"]),
                        ((int)(dtSettore.Rows[c]["id"])).ToString() // the identity in the table, to be used for the query.
                    )
                );
                int tmp_identity = (int)(dtSettore.Rows[c]["id"]);
                if (tmp_identity > max_identity)
                {
                    max_identity = tmp_identity;
                }// else skip.
            }// end for.
            ddlSettore.Items.Add(
                new System.Web.UI.WebControls.ListItem(
                    "Alle -/- tutti i settori aziendali",//--select without "where-tail" -----
                    (max_identity+1).ToString()
                )
            );
            indexOfAllSectors = max_identity + 1;//--------NB.------report combo_cardinality to the caller.-------
        }// else skip.
        //-------------- END popolamento Stati Lavorazione----------------------------------
        int int_selectedElement = default(int);
        if (
            System.DBNull.Value != selectedElement
            && null != selectedElement
            )
        {
            int_selectedElement = (int)selectedElement;
            if (0 >= int_selectedElement)
            {
                return;// no preselection.
            }// else continue pre-selecting.
            int r = 0;
            for (; r < dtSettore.Rows.Count; r++)
            {
                if (// selectedElement contains an [id], i.e. an encoded FK.
                    int_selectedElement == (int)(dtSettore.Rows[r]["id"])
                    )
                {
                    break;// selectedElement's row is "r".
                }// else continue.
            }
            if (r < dtSettore.Rows.Count)
            {
                int theIndex =
                    ddlSettore.Items.IndexOf(
                        new ListItem(
                            (string)(dtSettore.Rows[r]["nomeSettore"]),
                            ((int)(dtSettore.Rows[r]["id"])).ToString()
                        )
                    );
                ddlSettore.SelectedIndex = theIndex;
            }
            else
            {
                int theIndex =
                    ddlSettore.Items.IndexOf(
                    new System.Web.UI.WebControls.ListItem(
                        "Alle -/- tutti i settori aziendali",//--select without "where-tail" -----
                        indexOfAllSectors.ToString()
                    )
                );
                ddlSettore.SelectedIndex = theIndex;
            }
        }// else no pre-selection.
    }// end _for_LOAD


    public static void populate_Combo_ddlSettore_for_INSERT(
        System.Web.UI.WebControls.DropDownList ddlSettore,
        object selectedElement
      )
    {
        //--------------popolamento-----------------
        DataTable dtSettore = Entity.Proxies.usp_settoreCandidatura_LOOKUP_LOAD_SERVICE.usp_settoreCandidatura_LOOKUP_LOAD();
        ddlSettore.Items.Clear();
        ddlSettore.Items.Add(
            new System.Web.UI.WebControls.ListItem(
                "Auswahl des Geschäftsbereichs der Bewerbung -/- selezione del settore aziendale della candidatura",//--no query for this selection ---
                "0"
            )
        );
        int c = 0;
        if (null != dtSettore)
        {
            for (; c < dtSettore.Rows.Count; c++)
            {
                ddlSettore.Items.Add(
                    new System.Web.UI.WebControls.ListItem(
                        (string)(dtSettore.Rows[c]["nomeSettore"]),
                        ((int)(dtSettore.Rows[c]["id"])).ToString()
                    )
                );
            }// end for.
        }// else skip.
        //-------------- END popolamento Stati Lavorazione----------------------------------
        int int_selectedElement = default(int);
        if (
            System.DBNull.Value != selectedElement
            && null != selectedElement
            )
        {
            int_selectedElement = (int)selectedElement;
            if (0 >= int_selectedElement)
            {
                return;// no preselection.
            }// else continue pre-selecting.
            int r = 0;
            for (; r < dtSettore.Rows.Count; r++)
            {
                if (// selectedElement contains an [id], i.e. an encoded FK.
                    int_selectedElement == (int)(dtSettore.Rows[r]["id"])
                    )
                {
                    break;// selectedElement's row is "r".
                }// else continue.
            }
            int theIndex =
                ddlSettore.Items.IndexOf(
                    new ListItem(
                        (string)(dtSettore.Rows[r]["nomeSettore"]),
                        ((int)(dtSettore.Rows[r]["id"])).ToString()
                    )
                );
            ddlSettore.SelectedIndex = theIndex;
        }// else no pre-selection.
    }// end _for_INSERT


}//
