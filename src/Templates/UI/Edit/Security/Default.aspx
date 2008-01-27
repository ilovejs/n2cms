<%@ Page Language="C#" MasterPageFile="../Framed.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="N2.Edit.Security.Default" Title="Untitled Page" %>
<%@ Register TagPrefix="edit" Namespace="N2.Edit.Web.UI.Controls" Assembly="N2.Edit" %>
<asp:Content ID="ch" ContentPlaceHolderID="Head" runat="server">
	<script src="../Js/plugins.js?v5" type="text/javascript" ></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Toolbar" runat="server">
	<edit:OptionsMenu id="om" runat="server">
        <asp:LinkButton ID="btnSave" runat="server" CssClass="command" OnCommand="btnSave_Command">Save</asp:LinkButton>
        <asp:LinkButton ID="btnSaveRecursive" runat="server" CssClass="command" OnCommand="btnSaveRecursive_Command">Save including child pages</asp:LinkButton>
    </edit:OptionsMenu>
    <asp:HyperLink ID="hlCancel" runat="server" CssClass="cancel command">cancel</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Outside" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="server">
    <asp:CustomValidator ID="cvSomethingSelected" runat="server" Display="Dynamic" ErrorMessage="At least one role must be selected" OnServerValidate="cvSomethingSelected_ServerValidate" />
    <asp:CheckBoxList ID="cblAllowedRoles" runat="server" DataSourceID="odsRoles" CssClass="cbl" OnSelectedIndexChanged="cblAllowedRoles_SelectedIndexChanged" OnDataBound="cblAllowedRoles_DataBound" />
    <asp:ObjectDataSource ID="odsRoles" runat="server" TypeName="System.Web.Security.Roles" SelectMethod="GetAllRoles" />
</asp:Content>
