<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Login.aspx.cs" Inherits="TaskManager.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="bootstrap/css/bootstrap.css" rel="stylesheet" />
        <div align="center">
            <br/>
            <asp:Label BackColor="#FF0000" ForeColor="#FFFFFF" ID="LoginError" runat="server"></asp:Label><asp:Label BackColor="#4BB30B" ForeColor="#FFFFFF" ID="RegisterSuccess" runat="server"></asp:Label>
            <br/>
            <br/>
            <table style="width: 37%;">
                <tr>
                    <td>&nbsp;</td>
                    <td><asp:Label align="left" BackColor="Red" ForeColor="White"  ID="UsernameErrorLabel" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td align="right"><asp:Label ID="UsernameLabel" runat="server" align ="right" style="text-align: right;" Text="Username:" Visible="False"></asp:Label></td>
                    <td><asp:TextBox ID="UsernameLoginTextbox" runat="server" Visible="False"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td><asp:Label BackColor="Red" ForeColor="White"  ID="PasswordErrorLabel" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td align="right"><asp:Label ID="PasswordLabel" runat="server" Text="Password:" Visible="False"></asp:Label></td>
                    <td><asp:TextBox ID="PasswordLoginTextbox" runat="server" Visible="False" TextMode="Password">ffffffffff</asp:TextBox></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td><asp:Label BackColor="Red" ForeColor="White"  ID="ErrorInEmail" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td align="right"><asp:Label ID="EmailLabel" runat="server" Text="E-mail:" Visible="False"></asp:Label></td>
                    <td><asp:TextBox ID="EmailLoginTextbox" runat="server" Visible="False"></asp:TextBox></td>
                </tr>
            </table>

            <asp:Panel ID="Panel1" runat="server" Width="250px">
                <p>
                    <asp:Button ID="RegisterButton" runat="server" CssClass="btn btn-large btn-primary btn-block" OnClick="RegisterButton_Click" Text="Register" Visible="False" /></p>
                <p>
                    <asp:Button ID="LoginButton" runat="server" CssClass="btn btn-large btn-primary btn-block" OnClick="LoginButton_Click1" Text="Login" Visible="False" /></p>
                <p>
                    <asp:Button ID="IAmRegisteredButton" runat="server" CssClass="btn btn-large btn-primary btn-block" OnClick="LoginButton_Click" Text="I am already registered" /></p>
                <p>
                    <asp:Button ID="CreateNewUserButton" runat="server" CssClass="btn btn-large btn-primary btn-block" OnClick="CreateNewUserButton_Click" Text="Create new user" /></p>
            </asp:Panel>
        </div>

</asp:Content>
