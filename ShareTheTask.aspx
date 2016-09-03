<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ShareTheTask.aspx.cs" Inherits="TaskManager.ShareTheTask" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="bootstrap/css/bootstrap.css" rel="stylesheet" />
    <div>
        <h1><asp:Label ID="ShareTaskLabel" runat="server" Text="Share the task with other users"></asp:Label></h1>
        <hr/>
        <asp:Label ID="TaskCreatedByLabel" runat="server" Text="Created by __"></asp:Label>
        <br />
        <asp:Label ID="TaskLastEditedByLabel" runat="server" Text="Last edited on __ by __"></asp:Label>
        <br />
        <asp:Label ID="StartDateEndDateLabel" runat="server" Text="start and end dates"></asp:Label>
        <br />
        <asp:Label ID="TextOfTaskLabel" runat="server" Text="task text"></asp:Label>
        <br />
        <br />
        <asp:Label ForeColor="White" BackColor="Red" ID="SharingTasksError" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Label ID="EnterUsersEmailLabel" runat="server" Text="Please, enter the emails of users you want to share this task with in the field below.<br/>Use coma as a separator."></asp:Label>
        <br />
        <br />
        <asp:TextBox ID="UsersEmailsToShare" runat="server" Height="106px" TextMode="MultiLine" Width="510px"></asp:TextBox>
        <br />
        <asp:Button ID="ShareButton" class="btn btn-primary" runat="server" OnClick="ShareButton_Click" Text="Share" />

    </div>

</asp:Content>
