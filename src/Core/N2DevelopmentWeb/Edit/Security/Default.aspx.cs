using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using N2.Security;

namespace N2.Edit.Security
{
	[N2.Edit.ToolbarPlugIn("", "security", "~/Edit/Security/Default.aspx?selected={selected}", ToolbarArea.Preview, "preview", "~/Edit/Img/Ico/lock.gif", 100, ToolTip = "allowed roles for selected item")]
	public partial class Default : Web.EditPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			hlCancel.NavigateUrl = SelectedItem.Url;
		}

		protected void cblAllowedRoles_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		protected void btnSave_Command(object sender, CommandEventArgs e)
		{
            ApplyRoles(SelectedItem);
		}

        protected void btnSaveRecursive_Command(object sender, CommandEventArgs e)
		{
            ApplyRolesRecursive(SelectedItem);
		}

        private void ApplyRolesRecursive(ContentItem item)
        {
            ApplyRoles(item);
            foreach (ContentItem child in item.GetChildren())
            {
                ApplyRolesRecursive(child);
            }
        }

        private void ApplyRoles(ContentItem item)
        {
            if (AllSelected(cblAllowedRoles))
            {
                item.AuthorizedRoles.Clear();
            }
            else
            {
                foreach (ListItem li in cblAllowedRoles.Items)
                {
                    AuthorizedRole temp = new AuthorizedRole(item, li.Value);
                    int roleIndex = item.AuthorizedRoles.IndexOf(temp);
                    if (!li.Selected && roleIndex >= 0)
                        item.AuthorizedRoles.RemoveAt(roleIndex);
                    else if (li.Selected && roleIndex < 0)
                        item.AuthorizedRoles.Add(temp);
                }
            }
            Engine.Persister.Save(item);
        }

		private bool AllSelected(CheckBoxList cbl)
		{
			foreach (ListItem item in cbl.Items)
				if (!item.Selected)
					return false;
			return true;
		}

		private bool NoneSelected(CheckBoxList cbl)
		{
			foreach (ListItem item in cbl.Items)
				if (item.Selected)
					return false;
			return true;
		}

		protected void cblAllowedRoles_DataBound(object sender, EventArgs e)
		{
			if (SelectedItem.AuthorizedRoles.Count == 0)
			{
				foreach (ListItem item in cblAllowedRoles.Items)
					item.Selected = true;
			}
			else
			{
				foreach (N2.Security.AuthorizedRole allowedRole in SelectedItem.AuthorizedRoles)
				{
					cblAllowedRoles.Items.FindByValue(allowedRole.Role).Selected = true;
				}
			}
		}

		protected void cvSomethingSelected_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = !NoneSelected(this.cblAllowedRoles);
		}

		protected override void OnError(EventArgs e)
		{
			if(Server.GetLastError().GetType() == typeof(System.Reflection.TargetInvocationException))
			{
                string html = @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html>
    <head>
	    <link rel=""stylesheet"" href=""../Css/All.css"" type=""text/css"" />
        <link rel=""stylesheet"" href=""../Css/Framed.css"" type=""text/css"" />
    </head>
    <body><div class='content'>
        <h1>This feature might not have been enabled in web.config. Please look into ASP.NET roles configuration.</h1>
        <p><i>Check &lt;configuration&gt; &lt;system.web&gt; &lt;roleManager&gt; ... in web.config</i></p><pre><h3>"
                    + Server.GetLastError().Message 
                    + "</h3>" 
                    + Server.GetLastError().ToString() + "</pre></div></body></html>";

                Response.Write(html);
				Server.ClearError();
			}
			else
				base.OnError(e);
		}
	}
}
