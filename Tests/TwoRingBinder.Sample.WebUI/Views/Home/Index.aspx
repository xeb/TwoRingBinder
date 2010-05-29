<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SignUp>" %>
<%@ Import Namespace="TwoRingBinder.Sample.WebUI.Models"%>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>SignUp</h2>
    
    <%=Html.ValidationSummary("Please correct the following:") %>
    
    <form action='<%=Url.Action("Index", "Home") %>' method="post">
		<p>
			<label for="firstname">First Name:</label>
			<input type="text" id="firstname" name="FirstName" /> 
		</p>
		<p>
			<label for="lastname">Last Name:</label>
			<input type="text" id="lastname" name="LastName" /> 
		</p>
		<input type="submit" />
    </form>
</asp:Content>
