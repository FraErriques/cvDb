<%@ Page Language="C#" AutoEventWireup="true" CodeFile="categoriaInsert.aspx.cs" Inherits="zonaRiservata_categoriaInsert" %>

<%@ Register src="../Timbro.ascx" tagname="Timbro" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inserimento di una nuova Categoria</title>
</head>
<body style="background-color:Silver">
    <form id="frmInsertCategoria" runat="server">
    

            <table id="tblImpaginazione">
                <tr align="center">
                    <td align="center">
                        
                        
                        
                        <uc1:Timbro ID="Timbro1" runat="server" />
                        
                        
                        
                    </td>
                </tr>
                <tr align="center">
                    <td align="center">
                        <!--  LoginSquareClient -->
                    </td>
                </tr>
            </table>


            <asp:GridView ID="grdCategorie" runat="server" AutoGenerateColumns="false" Width="60%" HeaderStyle-BackColor="Bisque" HeaderStyle-Font-Bold="true">
                <Columns>
                    <asp:BoundField DataField="id" Visible="false"  />
                    <asp:BoundField DataField="nomeSettore"  HeaderText="Derzeit bestehende Bewerbungsbereiche -/- Settori di Candidatura attualmente in essere" HeaderStyle-Font-Bold="true"  Visible="true"  ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="Beige" ItemStyle-BorderColor="AntiqueWhite"  ></asp:BoundField>
                </Columns>
            </asp:GridView>



        <asp:TextBox ID="txtCategoriaInsert" runat="server" TextMode="SingleLine" Width="717px" Text=""></asp:TextBox>
        <asp:Button ID="btnCategoriaInsert" runat="server" Text="Insert" OnClick="btnCategoriaInsert_Click" />
        <asp:Label ID="lblCategoriaInsert" runat="server" Text=""></asp:Label>

            


    </form>
</body>
</html>
