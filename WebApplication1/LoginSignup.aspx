<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginSignup.aspx.cs" Inherits="WebApplication1.LoginSignup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:TextBox ID="Username" runat="server"></asp:TextBox>
        <p>
            <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
        </p>
        <asp:Button ID="SubmitButton" runat="server" Text="submit" OnClick="SubmitButton_Click" />
    </form>
</body>
</html>
