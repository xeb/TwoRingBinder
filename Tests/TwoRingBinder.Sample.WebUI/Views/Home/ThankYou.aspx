<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SignUp>" %>
<%@ Import Namespace="TwoRingBinder.Sample.WebUI.Models"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ThankYou
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ThankYou</h2>

	<table>
		<tr>
			<th>First Name</th>
			<td><%=Model.FirstName %></td>
		</tr>
		<tr>
			<th>Last Name</th>
			<td><%=Model.LastName %></td>
		</tr>
		<tr>
			<th>Origin</th>
			<td><%=Model.Origin %></td>
		</tr>
		<tr>
			<th>IsFemale</th>
			<td><%=Model.IsFemale %></td>
		</tr>
		<tr>
			<th>FromRoot</th>
			<td><%=Model.FromRoot %></td>
		</tr>
	</table>

</asp:Content>
