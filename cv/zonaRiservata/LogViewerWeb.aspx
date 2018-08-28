<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogViewerWeb.aspx.cs" Inherits="zonaRiservata_LogViewerWeb" %>

<%@ Register src="../Timbro.ascx" tagname="Timbro" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script language="javascript" type="text/javascript" src="~/codiceClient/scripts.js"></script>
    <script language="javascript" type="text/javascript" src="../codiceClient/date.js"></script>
    <script language="javascript" type="text/javascript" src="../codiceClient/LoginSquareClient.js"></script>
</head>
<body onload="setBrowserTitleBar('Log Viewer Web')" style="background-color:#EAC117">
    <form id="frmLogViewerWeb" runat="server">
            <table id="tblImpaginazione">
                <tr align="center">
                    <td align="center">
                        <uc1:Timbro ID="Timbro1" runat="server" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="grdLogging" runat="server">
            </asp:GridView>
    </form>
</body>
</html>
