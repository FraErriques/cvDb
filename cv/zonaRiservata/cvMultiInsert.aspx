<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cvMultiInsert.aspx.cs" Inherits="zonaRiservata_cvMultiInsert" %>

<%@ Register src="../Timbro.ascx" tagname="Timbro" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Inserimento Documentazione del Candidato</title>
        <script language="javascript" type="text/javascript" src="../codiceClient/scripts.js"></script>
        <script language="javascript" type="text/javascript" src="../codiceClient/date.js"></script>
        <script language="javascript" type="text/javascript" src="../codiceClient/LoginSquareClient.js"></script>    
</head>
<body  style="background-color:Orange">
    <form id="frmDocMultiInsert" runat="server">
    

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


    

          <table>
            <tr align="left">
                <td align="center" valign="top" >
                </td>
                <td align="center" valign="top" >
                </td>
                <td align="center" valign="top" ></td>
            </tr>
            <!-- -->
            <tr align="left">
             <td align="center" valign="top" ></td>
             <td align="left" valign="top" >
                <asp:Panel id="divUpload" runat="server" visible="true">
                    <table>
                    <tr align="left" valign="top">
                        <td align="left" valign="top">
                        <asp:Label ID="lblAbstract" runat="server">Anmerkungen zum Dokument -/- Osservazioni caratterizzanti il documento</asp:Label>
                        </td>
                        <td align="left" valign="top">
                        <asp:Label ID="lblFileselection" runat="server">Wählen Sie das Dokument auf der Festplatte des Client aus, um es an den Server zu schicken. -/- Scegliere il documento sul disco del client, per inviarlo al server.</asp:Label>
                        </td>
                    </tr>
                    <!-- -->
                    <tr align="left" valign="top">
                        <td align="left" valign="top">
                        <asp:TextBox ID="txtAbstract" runat="server" TextMode="MultiLine" Width="356px" 
                                Height="370px"></asp:TextBox>
                        </td>
                        <td align="left" valign="top">
                            <p><input id="uploadFile" type="file" runat="server" /></p>
                        </td>
                    </tr>
                    <!-- -->
                    <tr align="left" valign="top">
                        <td align="left" valign="top">
                        </td>
                        <td align="left" valign="top">
                        <p>
                            <asp:CheckBoxList ID="chkMultiDoc" runat="server" Visible="true" Enabled="true"></asp:CheckBoxList>        
                            <p><asp:Button ID="btnAllega" runat="server" Text="Ausgewähltes Dokument anhängen -/- Allegare il documento prescelto" Visible="true" 
                                Enabled="true" onclick="btnAllega_Click" /></p>
                            <p><asp:Button ID="btnDocsFromWebToDb" runat="server" Text="In die Datenbank einfügen -/- Inserire nella base dati" Visible="true" 
                                Enabled="true" onclick="btnDocsFromWebToDb_Click" /></p>                            
                            <p>
                            </p>
                        </p>
                        </td>
                    </tr>
                   </table>
                </asp:Panel>
               </td>
               <td align="center" valign="top" ></td>
            </tr>
            <!-- -->
            <!-- -->
            <tr align="left">
                <td align="left" valign="top" >
                    <asp:Label ID="lblEsito" runat="server" Text=""  align="left" Width="150px" ></asp:Label>
                </td>
            </tr>
    </table>    
    
    

    </form>
</body>
</html>
