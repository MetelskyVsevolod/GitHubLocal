<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="EditAdd.aspx.cs" Inherits="TaskManager.EditAdd" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="bootstrap/css/bootstrap.css" rel="stylesheet" />

    <div>
        <h1><asp:Label ID="EditOrAddLabel" runat="server" Text="Label"></asp:Label></h1>
        <br />
        <asp:Label ID="NameOfTheTaskLabel" runat="server" Text="Name:"></asp:Label>
        <br />
        <asp:TextBox ID="NameOfTheTaskTB" runat="server" Height="24px" Width="399px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="TextOfTheTaskLabel" runat="server" Text="Text:"></asp:Label>
        <br />
        <asp:TextBox ID="TextOFTheTaskTB" runat="server" Height="57px" Width="402px" TextMode="MultiLine"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:Label ID="TaskStartAtLabel" runat="server" Text="Task starts at:"></asp:Label>
        <br />
        <asp:DropDownList ID="StartTaskDay" runat="server" Width="50px">
        </asp:DropDownList>
        <asp:DropDownList ID="StartTaskMonth" runat="server" Width="50px">
        </asp:DropDownList>
        <asp:DropDownList ID="StartTaskYear" runat="server" Width="80px">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="TaskEndAtLabel" runat="server" Text="Task ends at:"></asp:Label>
        <br />
        <asp:DropDownList ID="EndTaskDay" runat="server" Width="50px">
        </asp:DropDownList>
        <asp:DropDownList ID="EndTaskMonth" runat="server" Width="50px">
        </asp:DropDownList>
        <asp:DropDownList ID="EndTaskYear" runat="server" Width="80px">
        </asp:DropDownList>
        <br />
    
    </div>
        <asp:Button ID="SubmitChanges" CssClass="btn btn-primary" runat="server" Height="45px" OnClick="SubmitChanges_Click" Text="Submit" Width="162px" />

</asp:Content> 