<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TaskManager._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="bootstrap/css/bootstrap.css" rel="stylesheet" />
    <div align="center">
        <asp:Table ID="Table1" runat="server" CellPadding="5" CellSpacing="30" GridLines="Horizontal">
            <asp:TableRow runat="server">
            </asp:TableRow>
            <asp:TableRow runat="server">
            </asp:TableRow>
        </asp:Table>
    </div>
    <br/>
    <br/>
    <table style="width: 100%;">
        <tr>
            <td><asp:Label ID="TaskCount" runat="server" Font-Size="XX-Large" Text="You don't have any tasks... for now ;)" Visible="False"></asp:Label></td>
            <td><asp:Button ID="AddNewTask" style="float: right" runat="server" CssClass="btn btn-large btn-primary" OnClick="AddNewTask_Click" Text="Add new Task" /></td>
        </tr>
    </table>
    </br>
    <p align="center"><asp:Label BackColor="Green" ForeColor="White" ID="sharingTasks" runat="server" Text="Label"></asp:Label></p>
</asp:Content> 
