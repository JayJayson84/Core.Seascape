using Core.Seascape.UI.Enums;
using Core.Seascape.UI.Extensions;
using Core.Seascape.UI.Models.Partials;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.ExtendedDataAnnotations;

namespace Core.Seascape.UI.Models.Forms.Seascape
{
    [Bind("Attributes,Colourscheme,Advanced,AnimatedSeascapeImage")]
    public class SeascapeForm : AjaxForm
    {
        public const string AjaxUpdateTargetId = "seascape-image";

        public SeascapeForm()
        {
            Attributes = new StandardAttributes();
            Colourscheme = new ColourAttributes();
            Advanced = new AdvancedAttributes();
            AnimatedSeascapeImage = new AnimatedSeascapeImageViewModel();
        }

        public SeascapeForm(string requestUrl) : this()
        {
            IsAjaxForm = true;
            FormId = AjaxFormIds.SeascapeFormId.GetText();
            Method = AjaxMethod.Post;
            Mode = AjaxMode.Replace;
            LoadingElementId = "ajax-loader";
            UpdateTargetId = AjaxUpdateTargetId;
            RequestUrl = requestUrl;
            OnBegin = "$.SeascapeForm.onBegin();";
            OnComplete = "$.SeascapeForm.onComplete();";
            OnSuccess = "$.SeascapeForm.onSuccess();";
            OnFailed = "$.SeascapeForm.onFailed(xhr.responseText);";
        }

        [UIHint("CollapsibleCardTemplate")]
        [Display(Name = "Attributes")]
        public StandardAttributes Attributes { get; set; }

        [UIHint("CollapsibleCardTemplate")]
        [Display(Name = "Colourscheme")]
        [Collapsed(true)]
        public ColourAttributes Colourscheme { get; set; }

        [UIHint("CollapsibleCardTemplate")]
        [Display(Name = "Advanced")]
        [Collapsed(true)]
        public AdvancedAttributes Advanced { get; set; }

        public AnimatedSeascapeImageViewModel AnimatedSeascapeImage { get; set; }
    }
}
