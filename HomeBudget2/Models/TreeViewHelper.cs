using System.Collections.Generic;
using System.Web.Mvc;

namespace HomeBudget2.Models
{
    public static class TreeViewHelper
    {
        /// <summary>  
        /// Create an HTML tree from a recursive collection of items  
        /// </summary>  
        public static TreeView<T> TreeView<T>(this HtmlHelper html, IEnumerable<T> items)
        {
            return new TreeView<T>(html, items);
        }
    }
}