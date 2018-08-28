<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateAbstract.aspx.cs" Inherits="zonaRiservata_UpdateAbstract" %>

<%@ Register src="../Timbro.ascx" tagname="Timbro" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update Abstract</title>
</head>
<body style="background-color:Teal">
    <form id="frmUpdateAbstract" runat="server">
    
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
    

        <asp:TextBox ID="txtUpdateAbstract" runat="server" TextMode="MultiLine" 
               Width="717px" Height="552px" Text=""></asp:TextBox>
        <asp:Button ID="btnUpdateAbstract" runat="server" Text="Update" OnClick="btnUpdateAbstract_Click" />
        
    </form>
</body>
</html>
