<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Tarantino.Commons.UI.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
	<head runat="server">
		<title>Tarantino Project</title>
	</head>
	<body>
		<form id="frm" runat="server">
			<table>
				<tr>
					<td>E-mail Address:</td>
					<td><asp:TextBox ID="txtEmailAddress" runat="server" /></td>
				</tr>
				<tr>
					<td>Password:</td>
					<td>
						<asp:TextBox ID="txtPassword" TextMode="Password" runat="server" />
						&nbsp;&nbsp;&nbsp;
						<asp:LinkButton runat="Server" ID="btnForgotPassword" Text="Send it to me" /></td>
				</tr>
				<tr>
					<td colspan="2">Remember me: <asp:CheckBox ID="chkRememberMe" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="2"><asp:Button ID="btnLogin" runat="server" Text="Login" /></td>
				</tr>
				<tr>
					<td colspan="2"><asp:Label ID="lblResults" runat="server" /></td>
				</tr>
			</table>
		</form>
	</body>
</html>