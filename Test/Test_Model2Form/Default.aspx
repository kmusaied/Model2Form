<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Test_Model2Form.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul>
        <li>
            <a href='ContactUs.aspx'>Basic Form</a>
        </li>
        <li>
            <a href='ContactUs2.aspx'>Form with Captions</a>
        </li>
        <li>
            <a href='ContactUs3.aspx'>Form with Validation</a>
        </li>
        <li>
            <a href='ContactUs4.aspx'>Form with Dropdownlist</a>
        </li>
    </ul>
</asp:Content>
