<%@ Page Language="C#" AutoEventWireup="true" CodeFile="candidatoInsert.aspx.cs" Inherits="zonaRiservata_candidatoInsert" %>

<%@ Register src="../Timbro.ascx" tagname="Timbro" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Inserimento del profilo personale di un nuovo candidato</title>
        <script language="javascript" type="text/javascript" src="../codiceClient/scripts.js"></script>
        <script language="javascript" type="text/javascript" src="../codiceClient/date.js"></script>
        <script language="javascript" type="text/javascript" src="../codiceClient/LoginSquareClient.js"></script>    
</head>
<body  style="background-color:Green">
    <form id="frmCandidatoInsert" runat="server">



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




    <table id="tblCandidato" >    
        <tr>
            <td></td>
            <td>
                <asp:Label ID="lblNominativo" Text="Name des Bewerbers -/- nominativo del candidato" runat="server" ></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNote" Text="Geben Sie hier Anmerkungen zum Bewerber ein -/- inserire qui ogni annotazione utile riguardo al candidato" runat="server" ></asp:Label>
            </td>
        </tr>
        <!-- -->
        <tr>
            <td>
                <asp:TextBox ID="txtNote" Text="" TextMode="MultiLine" Width="432px" 
                    runat="server" Height="361px"></asp:TextBox>
            </td>        
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ddlSettori" runat="server" AutoPostBack="false" EnableViewState="true"></asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtNominativo" Text="" TextMode="SingleLine" Width="328px" 
                    runat="server"></asp:TextBox>
            </td>
        </tr>
        <!-- -->
        <tr>
            <td></td>
            <td></td>
            <td>
                <asp:Button ID="btnCommint" Text=" Schreiben -/- Commit " runat="server" 
                    OnClick="btnCommit_Click" />
            </td>
        </tr>
        
        <tr>
            <td>
                <asp:Label ID="lblResult" runat="server" Text="" ></asp:Label>
            </td>
        </tr>
        
    </table>
    

</form>
</body>
</html>
