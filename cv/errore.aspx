<%@ Page Language="C#" AutoEventWireup="true" CodeFile="errore.aspx.cs" Inherits="errore" %>

<%@ Register src="Timbro.ascx" tagname="Timbro" tagprefix="uc1" %>
<%@ Register src="LoginSquareClient.ascx" tagname="LoginSquareClient" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title> __________ Error Page: si e' verificato un errore. __________ </title>
        <script language="javascript" type="text/javascript" src="codiceClient/scripts.js"></script>
        <script language="javascript" type="text/javascript" src="codiceClient/date.js"></script>
        <script language="javascript" type="text/javascript" src="codiceClient/LoginSquareClient.js"></script>
</head>
<body onload="Page_Load_OnClient('LoginSquareClient1_txtUser');setBrowserTitleBar('Notifica di Errore');">
    <form id="frmErrore" runat="server" style="background-color:#FFFF99" defaultbutton="LoginSquareClient1$itnLogin">
            <table id="tblTimbro">
                <tr align="center">
                    <td align="center">
                        <uc1:Timbro ID="Timbro1" runat="server" />
                    </td>
                </tr>
            </table>    
            <table id="tblImpaginazione">
                <tr align="center">
                    <td align="center">
                        <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Overline="True" 
                            Font-Size="Medium" ForeColor="Maroon" Text="">
                        </asp:Label>
                    </td>
                </tr>
                <tr align="center">
                    <td align="center">
                        <uc2:LoginSquareClient ID="LoginSquareClient1" runat="server" />
                    </td>
                </tr>
                <tr align="center">
                    <td align="center">
                        <asp:Label ID="lblStato" runat="server" Font-Bold="True" Font-Overline="True" 
                            Font-Size="Medium" ForeColor="Maroon" Text="">
                        </asp:Label>
                    </td>
                </tr>                                 
            </table>
    </form>
</body>
</html>
