using Core.Seascape.UI.Enums;
using System;

namespace Core.Seascape.UI.Models.Forms
{
    /// <summary>
    /// A base class containing properties related to ajax form attribites.
    /// </summary>
    public abstract class AjaxForm
    {
        public bool IsAjaxForm { get; set; }
        public string FormId { get; set; }
        public string ConfirmationMessage { get; set; }
        public AjaxMethod Method { get; set; }
        public AjaxMode Mode { get; set; }
        public int LoadingDuration { get; set; }
        public string LoadingElementId { get; set; }
        public string OnBegin { get; set; }
        public string OnComplete { get; set; }
        public string OnFailed { get; set; }
        public string OnSuccess { get; set; }
        public string UpdateTargetId { get; set; }
        public string RequestUrl { get; set; }

        public T Cast<T>() where T : class
        {
            return (T)Convert.ChangeType(this, typeof(T));
        }
    }
}
