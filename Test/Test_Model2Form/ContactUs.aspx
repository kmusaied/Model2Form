<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="Test_Model2Form.ContactUs" %>

<%@ Register Assembly="EasyWebControls" Namespace="EasyWebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <cc1:Model2Form ID="Model2Form1" runat="server" 
        onsaveclick="Model2Form1_SaveClick" />
        <div>
            <asp:Literal runat="server" ID="ltrMessage"></asp:Literal>
        </div>
        
        <a href='#' id='showCode'>Show Model</a>
        
  <!-- code formatted by http://manoli.net/csharpformat/ -->
<div class="csharpcode">
<pre><span class="lnum">   1:  </span>            [Serializable]</pre>
<pre><span class="lnum">   2:  </span>            <span class="kwrd">public</span> <span class="kwrd">class</span> ContactUsInfo</pre>
<pre><span class="lnum">   3:  </span>            {</pre>
<pre><span class="lnum">   4:  </span>                <span class="kwrd">public</span> <span class="kwrd">string</span> FullName { get; set; }</pre>
<pre><span class="lnum">   5:  </span>                <span class="kwrd">public</span> <span class="kwrd">string</span> Email { get; set; }</pre>
<pre><span class="lnum">   6:  </span>                <span class="kwrd">public</span> <span class="kwrd">string</span> Subject { get; set; }</pre>
<pre><span class="lnum">   7:  </span>                <span class="kwrd">public</span> <span class="kwrd">string</span> Message { get; set; }</pre>
<pre><span class="lnum">   8:  </span>                </pre>
<pre><span class="lnum">   9:  </span>            }</pre>
</div></asp:Content>
