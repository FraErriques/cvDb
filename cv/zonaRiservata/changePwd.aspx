<%@ Page Language="C#" AutoEventWireup="true" CodeFile="changePwd.aspx.cs" Inherits="zonaRiservata_changePwd" %>

<%@ Register src="../Timbro.ascx" tagname="Timbro" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Change Password</title>
    <script language="javascript" type="text/javascript" src="../codiceClient/scripts.js"></script>
    <script language="javascript" type="text/javascript" src="../codiceClient/date.js"></script>
    <script language="javascript" type="text/javascript" src="../codiceClient/LoginSquareClient.js"></script>    
</head>
<body onload="Page_Load_OnClient('txtOldPwd')">
    <form id="frmChangePwd" runat="server" style="background-color:White" defaultbutton="btnChangePwd">
    <table >
    <tr align="center">
        <td align="center">
            
            <uc1:Timbro ID="Timbro1" runat="server" />
            
        </td>
    </tr>
    <tr align="center">
        <td align="center">
        
<div id="divChangePwd" runat="server" visible="true"
     style="background-color:#CCFFFF; position:relative; width:470px; left: 4px; top: 4px; border-color:Black; border:solid thin black">
        <br />
        <!-- usr-->
            <asp:Label ID="lblOldPwd" runat="server"  Height="15px"  Width="170px" Style="position: relative">&nbsp;Old Password</asp:Label>
            <asp:TextBox ID="txtOldPwd" runat="server" Height="15px"  Width="150px" 
            TabIndex="1" TextMode="Password"></asp:TextBox>
        <!-- end usr -->
        <br />
        <!-- pwd-->
            <asp:Label ID="lblNewPwd" runat="server"  Height="15px"   Width="170px" Style="position: relative" >&nbsp;New Password</asp:Label>
            <asp:TextBox ID="txtNewPwd" runat="server" Height="15px"  Width="150px" 
            TextMode="Password" TabIndex="2" ></asp:TextBox>
        <!-- end pwd -->
        <br />
        <!-- Confirm_pwd-->
            <asp:Label ID="lblConfirmNewPwd" runat="server"  visible="true" Enabled="true" Height="15px"  Width="170px" Style="position: relative"  >&nbsp;Confirm New Password</asp:Label>
            <asp:TextBox ID="txtConfirmNewPwd" runat="server" visible="true"  
            Enabled="true" Height="15px"  Width="150px" TextMode="Password" TabIndex="3" ></asp:TextBox>
        <!-- end Confirm_pwd -->
        
        
        <!-- btn -->
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblStato" runat="server"  Height="65px" Style="position: relative"  Width="127px"></asp:Label>
                </td>

                <td>
                    <table>
                        <tr>
                            <asp:Button ID="btnChangePwd" runat="server" Text="Cambio Password" 
                                TabIndex="4" Width="120px" onclick="btnChangePwd_Click"></asp:Button>
                        </tr>
                        </table>
                 </td>
            </tr>
        </table>
</div>
        </td>
    </tr>
</table>
    </form>
</body>
</html>
