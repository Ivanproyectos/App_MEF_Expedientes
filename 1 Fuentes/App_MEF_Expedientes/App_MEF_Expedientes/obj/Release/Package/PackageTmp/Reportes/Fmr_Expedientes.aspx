﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fmr_Expedientes.aspx.cs" Inherits="App_MEF_Expedientes.Reportes.Expedientes" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
               <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        </div>
         <center>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" BackColor="White" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="99%" 
          Height="7.1in"></rsweb:ReportViewer>
              </center>
    </form>
</body>
</html>
