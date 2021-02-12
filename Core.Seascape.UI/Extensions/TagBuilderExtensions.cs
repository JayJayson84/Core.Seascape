using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;

namespace Core.Seascape.UI.Extensions
{
    public static class TagBuilderExtensions
    {

        /// <summary>
        /// Merges a collection of Html Attributes into the <see cref="TagBuilder"/> instance.
        /// </summary>
        /// <param name="tagBuilder">Instance type to extend.</param>
        /// <param name="htmlAttributes">A <see cref="Dictionary{TKey, TValue}"/>, <see cref="RouteValueDictionary"/> or two dimensional collection of paired attribute names and values.</param>
        /// <param name="replaceExisting"><see langword="true"/> to replace or <see langword="false"/> to merge the existing attributes.</param>
        public static void MergeHtmlAttributes(this TagBuilder tagBuilder, dynamic htmlAttributes, bool replaceExisting = false)
        {
            if (htmlAttributes != null)
            {
                var T = htmlAttributes.GetType();
                if (T.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                {
                    tagBuilder.MergeAttributes(htmlAttributes, replaceExisting);
                }
                else
                {
                    tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes), replaceExisting);
                }
            }
        }

    }
}
