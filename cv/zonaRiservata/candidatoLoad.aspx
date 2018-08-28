<%@ Page Language="C#" AutoEventWireup="true" CodeFile="candidatoLoad.aspx.cs" Inherits="zonaRiservata_candidatoLoad" %>

<%@ Register src="../Timbro.ascx" tagname="Timbro" tagprefix="uc1" %>



<%@ Register src="../tools/PageLengthManager.ascx" tagname="PageLengthManager" tagprefix="uc2" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Elenco dei candidati alle posizioni di lavoro aperte in BBT SE</title>
        <script language="javascript" type="text/javascript" src="../codiceClient/scripts.js"></script>
        <script language="javascript" type="text/javascript" src="../codiceClient/date.js"></script>
        <script language="javascript" type="text/javascript" src="../codiceClient/LoginSquareClient.js"></script>    
</head>
<body  style="background-color:Aqua">
    <form id="frmDefault" runat="server" defaultbutton="PageLengthManager1$btnRowsInPage">

            <table id="tblImpaginazione">
                <tr align="center">
                    <td align="center">
                        <uc1:Timbro ID="Timbro1" runat="server" />
                    </td>
                </tr>
                <tr align="center">
                    <td align="center">
                        <!--  LoginSquareClient -->
                        <uc2:PageLengthManager ID="PageLengthManager1" runat="server" />
                    </td>
                </tr>
            </table>
            
            <table>
            <tr>
                <asp:DropDownList ID="ddlSettori" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSettoriRefreshQuery"></asp:DropDownList>
            </tr>
            </table>


        <asp:GridView ID="grdDatiPaginati" runat="server" AutoGenerateColumns="false" 
        OnRowCommand="grdDatiPaginati_RowCommand" >
            <Columns>
            
            <asp:BoundField DataField="id" Visible="false" ></asp:BoundField>
            
            <asp:BoundField DataField="nominativo"  HeaderText="Nominativo" HeaderStyle-Font-Bold="true"  Visible="true"  ItemStyle-Width="6%" >
                <HeaderStyle Font-Bold="True"></HeaderStyle>
                <ItemStyle Width="6%"></ItemStyle>
            </asp:BoundField>
            
            <asp:BoundField DataField="nomeSettore" Visible="true"  HeaderText="Settore" HeaderStyle-Font-Bold="true"  ItemStyle-Width="4%">
                <HeaderStyle Font-Bold="True"></HeaderStyle>
                <ItemStyle Width="4%"></ItemStyle>
            </asp:BoundField>
            
            <asp:BoundField DataField="note" Visible="true"  HeaderText="Considerazioni generali sul Candidato" HeaderStyle-Font-Bold="true"  ItemStyle-Width="60%">
                <HeaderStyle Font-Bold="True"></HeaderStyle>
                <ItemStyle Width="60%"></ItemStyle>
            </asp:BoundField>

            <asp:TemplateField HeaderText="Aggiunta documenti del Candidato"   HeaderStyle-Font-Bold="true" ItemStyle-Width="3%">
                <ItemTemplate>
                    <table>
                    <tr align="center" valign="middle">
                        <td align="center" valign="middle">
                            <asp:ImageButton ID="btnAddCv" runat="server" ImageUrl="~/img/btnAddDoc.bmp"  Enabled="True" Visible="True" CommandName="AddDocuments" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>'></asp:ImageButton>
                        </td>
                    </tr>
                    </table>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True"></HeaderStyle>
                <ItemStyle Width="3%"></ItemStyle>
            </asp:TemplateField>					    

            <asp:TemplateField HeaderText="Consultazione documenti del Candidato"  HeaderStyle-Font-Bold="true"  ItemStyle-Width="3%">
                <ItemTemplate>
                    <table>
                        <tr align="center" valign="middle">
                            <td align="center" valign="middle">
                                <asp:ImageButton ID="btnReadCv" runat="server" ImageUrl="~/img/btnMailRead.bmp"  Enabled="True" Visible="True" CommandName="GeneralEdit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>'></asp:ImageButton>
                            </td>
                        </tr>
                    </table>					            
                </ItemTemplate>
                <HeaderStyle Font-Bold="True"></HeaderStyle>
                <ItemStyle Width="3%"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Update Abstract"  HeaderStyle-Font-Bold="true"  ItemStyle-Width="3%">
                <ItemTemplate>
                    <table>
                        <tr align="center" valign="middle">
                            <td align="center" valign="middle">
                                <asp:ImageButton ID="btnUpdateAbstract" runat="server" ImageUrl="~/img/btnUpdateAbstract.bmp"  Enabled="True" Visible="True" CommandName="UpdateAbstract" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>'></asp:ImageButton>
                            </td>
                        </tr>
                    </table>					            
                </ItemTemplate>
                <HeaderStyle Font-Bold="True"></HeaderStyle>
                <ItemStyle Width="3%"></ItemStyle>
            </asp:TemplateField>

            </Columns>
        </asp:GridView>


<asp:Panel ID="pnlPageNumber" runat="server" Visible="true" align="center"  style="position:relative; left: 0px; top: 0px;" Width="" >
<br /><br />
</asp:Panel>


    </form>
</body>
</html>
