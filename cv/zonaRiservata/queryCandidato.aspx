<%@ Page Language="C#" AutoEventWireup="true" CodeFile="queryCandidato.aspx.cs" Inherits="zonaRiservata_queryCandidato" %>

<%@ Register src="../Timbro.ascx" tagname="Timbro" tagprefix="uc1" %>

<%@ Register src="../tools/PageLengthManager.ascx" tagname="PageLengthManager" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>select Candidato</title>
</head>
<body style="background-color:LightGreen">
    <form id="frmQueryCandidato" runat="server" defaultbutton="btnDoPostback">
            <table id="tblImpaginazione">
                <tr align="center">
                    <td align="center"> 

                        <uc1:timbro ID="Timbro1" runat="server" />

                    </td>
                </tr>
                <tr align="center">
                    <td align="center">
                        <!--  LoginSquareClient -->
                        <uc2:PageLengthManager ID="PageLengthManager1" runat="server" />
                    </td>
                </tr>
            </table>

        <table  Width="80%">
        <tr Width="80%">
        <br />
           <asp:Label ID="lblSettori" runat="server">indicazione facoltativa del <b><u>settore</u></b> di appartenenza del candidato</asp:Label>
        </tr>
        <br />
        <tr>
           <asp:DropDownList ID="ddlSettori" runat="server" Width="80%"></asp:DropDownList>
        </tr>
        <br />
        <br />
        <br />
        <tr>
           <asp:Label ID="lblConnectorOne" runat="server">il connettore logico e' obbligatoriamente <b><u>and</u></b> per queste clausole</asp:Label>
        </tr>        
        <tr>
           <asp:RadioButtonList ID="rbtAndOr_settore_nominativo" runat="server" Enabled="false">
                <asp:ListItem id="rdeAnd_settore_nominativo" Enabled="true" Selected="True" Text="And" Value="And"></asp:ListItem>
                <asp:ListItem id="rdeOr_settore_nominativo" Enabled="true" Selected="False" Text="Or" Value="Or"></asp:ListItem>
           </asp:RadioButtonList>
        </tr>
        <tr>
           <asp:Label ID="lblNominativo" runat="server">porzioni, facoltative, di testo da riconoscere nel <b><u>nominativo</u></b> del candidato</asp:Label>
        </tr>
        <br />
        <tr>
           <asp:TextBox ID="txtNominativo" runat="server"></asp:TextBox>
        </tr>
        <br />
        <br />
        <br />
        <tr>
           <asp:Label ID="lblConnectorTwo" runat="server">il connettore logico e' obbligatoriamente <b><u>and</u></b> per queste clausole</asp:Label>
        </tr>
        <tr>
           <asp:RadioButtonList ID="rbtAndOr_nominativo_abstract" runat="server" Enabled="false">
                <asp:ListItem id="rdeAnd_nominativo_abstract" Enabled="true" Selected="True" Text="And" Value="And"></asp:ListItem>
                <asp:ListItem id="rdeOr_nominativo_abstract" Enabled="true" Selected="False" Text="Or" Value="Or"></asp:ListItem>
           </asp:RadioButtonList>
        </tr>
        <tr>
           <asp:Label ID="lblAbstract" runat="server">porzioni, facoltative, di testo da riconoscere nella <b><u>descrizione</u></b> del candidato</asp:Label>
        </tr>
        <br />
        <tr>
           <asp:TextBox ID="txtAbstract" runat="server"></asp:TextBox>
        </tr>
        <br />
        <br />
        <br />        
        <tr>
           <asp:Button ID="btnDoPostback" runat="server" Text="invio richiesta" OnClick="btnDoPostback_Click"></asp:Button>
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
