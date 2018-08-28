<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PagerSardegnaTest.aspx.cs" Inherits="PagerSardegnaTest" %>

<%@ Register src="tools/Pager.ascx" tagname="Pager" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>frmPagerSardegna</title>
</head>
<body>



    <form id="frmPagerSardegna" runat="server">

        <asp:GridView ID="grdDatiPaginati" runat="server" AutoGenerateColumns="true" Visible="true" >
        </asp:GridView>

        <uc1:Pager ID="Pager1" runat="server" />

    </form>
</body>
</html>
