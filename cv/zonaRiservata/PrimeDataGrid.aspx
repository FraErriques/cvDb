<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrimeDataGrid.aspx.cs" Inherits="zonaRiservata_PrimeDataGrid" %>

<%@ Register src="../Timbro.ascx" tagname="Timbro" tagprefix="uc1" %>

<%@ Register src="../tools/PageLengthManager.ascx" tagname="PageLengthManager" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Prime Grid</title>
</head>
<body>
    <form id="frmPrimes" runat="server">
    

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


        <asp:GridView ID="grdDatiPaginati" runat="server" AutoGenerateColumns="false" 
        OnRowCommand="grdDatiPaginati_RowCommand" >
            <Columns>
            
            <asp:BoundField DataField="RowNumber" Visible="true"  HeaderText="RowNumber" HeaderStyle-Font-Bold="true"  ItemStyle-Width="4%">
                <HeaderStyle Font-Bold="True"></HeaderStyle>
                <ItemStyle Width="4%"></ItemStyle>
            </asp:BoundField>


            <asp:BoundField DataField="ordinal" Visible="true"  HeaderText="Ordinale" HeaderStyle-Font-Bold="true"  ItemStyle-Width="4%">
                <HeaderStyle Font-Bold="True"></HeaderStyle>
                <ItemStyle Width="4%"></ItemStyle>
            </asp:BoundField>
            
            
            <asp:BoundField DataField="prime"  HeaderText=" Prime Number" HeaderStyle-Font-Bold="true"  Visible="true"  ItemStyle-Width="6%" >
                <HeaderStyle Font-Bold="True"></HeaderStyle>
                <ItemStyle Width="6%"></ItemStyle>
            </asp:BoundField>
            
            </Columns>
          </asp:GridView>    
    
    
        <table>

            <tr>
                <td>
                    <asp:Label ID="lblMin" runat="server" Text="min"></asp:Label>
                    <asp:TextBox ID="txtMin" runat="server" Text="1"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnMin" runat="server" Text="min" OnClick="btn_Family_ChangeViewBoundary_Click"></asp:Button>
                </td>
                
                <!-- column separator-->
                <td>
                    <asp:Label ID="lblViewCockpit" runat="server" Text="controll to manage the View cardinality. It doesn't affect the page cardinality. It determines the min and max of the primary key, in View creation."></asp:Label>
                </td>
                
                <td>
                    <asp:Label ID="lblMax" runat="server" Text="max"></asp:Label>
                    <asp:TextBox ID="txtMax" runat="server" Text="9000"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnMax" runat="server" Text="max" OnClick="btn_Family_ChangeViewBoundary_Click"></asp:Button>
                </td>
            </tr>
            <tr>
                <asp:Label ID="lblStato" runat="server" Text=""></asp:Label>
            </tr>
        </table>

        <asp:Panel ID="pnlPageNumber" runat="server" Visible="true" align="center"  style="position:relative; left: 0px; top: 0px;" Width="" >
            <br /><br />
        </asp:Panel>

    </form>
</body>
</html>
