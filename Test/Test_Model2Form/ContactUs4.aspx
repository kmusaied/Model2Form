<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ContactUs4.aspx.cs" Inherits="Test_Model2Form.ContactUs4" %>

<%@ Register Assembly="EasyWebControls" Namespace="EasyWebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <cc1:Model2Form ID="Model2Form1" runat="server" 
        onlookupcontrolcreated="Model2Form1_LookupControlCreated" />
</asp:Content>
