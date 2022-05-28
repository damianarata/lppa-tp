<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Respuesta.aspx.cs" Inherits="Respuesta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #form1 {
            height: 381px;
        }
    </style>
</head>
<body style="height: 522px">
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
            Las acciones que el usuario puede realizar son:<br />
            <br />
        </div>
        <div style="margin-left: 80px">
            <asp:ListBox ID="ListBox1" runat="server" Height="105px" Width="209px"></asp:ListBox>
        </div>
    </form>
</body>
</html>
