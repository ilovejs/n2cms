﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using N2.Plugin;
using System.Web.UI;

namespace N2.Templates.Wiki
{
    /// <summary>
    /// User controls marked with this attribute may be used as templates in
    /// wiki text. Unless the Name is set explicitly the class name is used
    /// for template lookup, e.g. {{LatestChanges}} -> LatestChanges.ascx.cs
    /// </summary>
    public class WikiTemplateAttribute : Attribute, ITemplateRenderer
    {
        public WikiTemplateAttribute(string virtualPath)
        {
            VirtualPath = virtualPath;
        }

        public string VirtualPath { get; set; }

        #region IPlugin Members

        public string Name { get; set; }

        public int SortOrder { get; set; }

        public bool IsAuthorized(IPrincipal user)
        {
            return true;
        }

        #endregion

        #region IComparable<IPlugin> Members

        public int CompareTo(IPlugin other)
        {
            return this.SortOrder - other.SortOrder;
        }

        #endregion

        #region IRenderer Members

        public Control AddTo(Control container, RenderingContext context)
        {
            Control template = container.Page.LoadControl(VirtualPath);
            if (template is IWikiTemplate)
                (template as IWikiTemplate).WikiContext = context;
            container.Controls.Add(template);
            return template;
        }

        #endregion
    }
}
