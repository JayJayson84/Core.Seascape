using Core.Seascape.UI.Models.Forms;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Encodings.Web;

namespace Core.Seascape.UI.Extensions
{
    public static class HtmlHelperExtensions
    {

        #region " Public Static Methods "

        /// <summary>
        /// A HtmlHelper method to render a meta tag element with the given <paramref name="property"/> and <paramref name="content"/> attributes.
        /// </summary>
        /// <param name="htmlHelper">Instance type to extend.</param>
        /// <param name="property">A value for the property attribute.</param>
        /// <param name="content">A value for the content attribute.</param>
        /// <returns>A Html meta tag element if a valid <paramref name="property"/> and <paramref name="content"/> parameter is provided. Otherwise <see langword="null"/>.</returns>
        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Unreferenced extension type")]
        public static IHtmlContent MetaProperty(this IHtmlHelper htmlHelper, string property, string content)
        {
            if (!property.HasValue() || !content.HasValue()) return null;

            return new HtmlContentBuilder().AppendHtmlLine($"<meta property=\"{property}\" content=\"{content}\">");
        }

        /// <summary>
        /// A HtmlHelper method to render a meta tag element with the given <paramref name="name"/> and <paramref name="content"/> attributes.
        /// </summary>
        /// <param name="htmlHelper">Instance type to extend.</param>
        /// <param name="name">A value for the name attribute.</param>
        /// <param name="content">A value for the content attribute.</param>
        /// <returns>A Html meta tag element if a valid <paramref name="name"/> and <paramref name="content"/> parameter is provided. Otherwise <see langword="null"/>.</returns>
        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Unreferenced extension type")]
        public static IHtmlContent MetaName(this IHtmlHelper htmlHelper, string name, string content)
        {
            if (!name.HasValue() || !content.HasValue()) return null;

            return new HtmlContentBuilder().AppendHtmlLine($"<meta name=\"{name}\" content=\"{content}\">");
        }

        /// <summary>
        /// A HtmlHelper method to render an ajax form element within a using statement.
        /// </summary>
        /// <param name="htmlHelper">Instance type to extend.</param>
        /// <param name="model">An <see cref="AjaxForm"/> model containing data ajax attributes.</param>
        /// <param name="htmlAttributes">Optional Html Attributes to merge into the <form> element.</param>
        /// <returns>An ajax form element that wraps any innerhtml enclosed within the using statement.</returns>
        public static IDisposable AjaxBeginForm(this IHtmlHelper htmlHelper, AjaxForm model, object htmlAttributes = null)
        {
            var tagBuilder = new TagBuilder("form")
            {
                TagRenderMode = TagRenderMode.StartTag
            };

            var attributes = new Dictionary<string, object>
            {
                { "method", model.Method.GetText() },
                { "data-ajax", model.IsAjaxForm.ToString().ToLower() },
                { "data-ajax-mode", model.Mode.GetText() },
                { "data-ajax-method", model.Method.GetText() }
            };

            if (model.FormId.HasValue()) attributes.Add("id", model.FormId);
            if (model.ConfirmationMessage.HasValue()) attributes.Add("data-ajax-confirm", model.ConfirmationMessage);
            if (model.LoadingDuration > 0) attributes.Add("data-ajax-duration", model.LoadingDuration);
            if (model.LoadingElementId.HasValue()) attributes.Add("data-ajax-loading", $"#{model.LoadingElementId}");
            if (model.OnBegin.HasValue()) attributes.Add("data-ajax-begin", model.OnBegin);
            if (model.OnComplete.HasValue()) attributes.Add("data-ajax-complete", model.OnComplete);
            if (model.OnFailed.HasValue()) attributes.Add("data-ajax-failure", model.OnFailed);
            if (model.OnSuccess.HasValue()) attributes.Add("data-ajax-success", model.OnSuccess);
            if (model.UpdateTargetId.HasValue()) attributes.Add("data-ajax-update", $"#{model.UpdateTargetId}");
            if (model.RequestUrl.HasValue()) attributes.Add("data-ajax-url", model.RequestUrl);

            var writer = htmlHelper.ViewContext.Writer;

            tagBuilder.MergeAttributes(attributes);
            tagBuilder.MergeHtmlAttributes(htmlAttributes);
            tagBuilder.WriteTo(writer, HtmlEncoder.Default);

            return new Container(writer, tagBuilder);
        }

        #endregion

        #region " Private Methods "

        private class Container : IDisposable
        {
            private readonly TextWriter _writer;
            private readonly TagBuilder _builder;

            public Container(TextWriter writer, TagBuilder tagBuilder)
            {
                _writer = writer;
                _builder = tagBuilder;
            }

            public void Dispose()
            {
                _writer.Write(_builder.RenderEndTag());
            }
        }

        #endregion

    }
}
