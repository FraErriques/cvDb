<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginSquareClient.ascx.cs" Inherits="LoginSquareClient" %>
<div id="divLoginSquareContent" runat="server" visible="true"
     style="background-color:#CCFFFF; position:relative; width:270px; left: 4px; top: 4px; border-color:Black; border:solid thin black">
        <br />
        <!-- usr-->
            <asp:Label ID="lblUser" runat="server"  Height="15px"  Width="90px" Style="position: relative">&nbsp;Username</asp:Label>
            <asp:TextBox ID="txtUser" runat="server" Height="15px"  Width="150px" 
            TabIndex="1"></asp:TextBox>
        <!-- end usr -->
        <br />
        <!-- pwd-->
            <asp:Label ID="lblPwd" runat="server"  Height="15px"   Width="90px" Style="position: relative" >&nbsp;Password</asp:Label>
            <asp:TextBox ID="txtPwd" runat="server" Height="15px"  Width="150px" 
            TextMode="Password" TabIndex="2" ></asp:TextBox>
        <!-- end pwd -->
        <br />        
        <!-- btn -->
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblStato" runat="server"  Height="65px" Style="position: relative"  Width="127px"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:ImageButton ID="itnLogin" runat="server" ImageUrl="~/img/btnLogin.bmp" 
                                    OnClientClick="return canLogOn()" OnClick="itnLogin_Click" TabIndex="3"  />
                            </td>
                        </tr>
                    </table>
                 </td>
            </tr>
        </table>
</div>
