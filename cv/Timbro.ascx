<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Timbro.ascx.cs" Inherits="Timbro" %>
<asp:Panel id="tblTimbro" runat="server" style="position:relative; background-color:#99C68E" width="100%">

    <table>
    <tr align="center">
    

        <td width="16%">
            <asp:HyperLink id="hplCandidatoLoad" runat="server" Text=" Abfrage Bewerber /<br/> Consultazione Candidati "
                NavigateUrl="~/zonaRiservata/candidatoLoad.aspx"></asp:HyperLink>
        </td>
        <!-- -->
        <td width="16%">
            <asp:HyperLink id="hplQueryCandidato" runat="server" Text=" ___ /<br/> Query Candidato "
            NavigateUrl="~/zonaRiservata/queryCandidato.aspx"></asp:HyperLink>
        </td>          
        <!-- -->        
        <td width="30%"></td>
        <!-- -->
         <asp:Panel ID="pnlInsert" runat="server" Visible="false" >
            <td width="16%">
                <asp:HyperLink id="hplCandidatoInsert" runat="server" Text=" Eingabe Bewerber /<br/> Inserimento Candidato "
                    NavigateUrl="~/zonaRiservata/candidatoInsert.aspx"></asp:HyperLink>
            </td>
            <!-- -->      
            <td width="16%">
                <asp:HyperLink id="hplCategoriaInsert" runat="server" Text=" Eingabe Fachbereich /<br/> Inserimento Settore "
                NavigateUrl="~/zonaRiservata/categoriaInsert.aspx"></asp:HyperLink>
            </td>
            <!-- -->

         </asp:Panel>

        <td>
            <asp:Panel  ID="pnlAdminLinks" runat="server" Visible="false">
                <table>
                    <tr align="center"  >
                        <td width="16%">
                            <asp:HyperLink id="hplPrimes" runat="server" Text="Primes"
                                NavigateUrl="~/zonaRiservata/PrimeDataGrid.aspx"></asp:HyperLink>
                        </td>
                        <!-- -->
                        <td width="16%">
                            <asp:HyperLink id="hplLogViewerWeb" runat="server" Text="Log Viewer Web"
                                NavigateUrl="~/zonaRiservata/LogViewerWeb.aspx"></asp:HyperLink>
                        </td>
                        <!-- -->
                    </tr>
                </table>
            </asp:Panel >
        </td>

        <td width="16%">
            <asp:HyperLink id="hplChangePwd" runat="server" Text=" Änderung Passwort /<br/> Cambio Password"
                NavigateUrl="~/zonaRiservata/changePwd.aspx" ></asp:HyperLink>
        </td>
        <!-- -->
        <td width="16%">
            <asp:Button id="btnLogout" runat="server" Text=" ausloggen / logout"
                 onclick="btnLogout_Click"></asp:Button>
        </td>
        <!-- -->
        <td width="16%">
            <asp:Image ID="imgLogo" runat="server" ImageAlign="Right" ImageUrl="~/img/logo.bmp" />
        </td>
        <!-- -->
    </tr>
    </table>
    
</asp:Panel>
<asp:Label ID="lblStato" runat="server" BackColor="#66FF66" Font-Bold="False" Font-Italic="True" Font-Size="Large" Width="100%" ></asp:Label>
