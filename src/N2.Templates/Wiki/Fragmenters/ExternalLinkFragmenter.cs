﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

namespace N2.Templates.Wiki.Fragmenters
{
    public class ExternalLinkFragmenter : AbstractFragmenter
    {
        public ExternalLinkFragmenter()
        {
            Expression = CreateExpression(@"(\[[^\[\]]*?\])|(\w+://[\w.:/?=&;#]+)");
        }
    }
}
