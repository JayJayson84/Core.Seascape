using Core.Seascape.UI.Models.Forms.Seascape;
using System.Collections.Generic;
using System.Linq;

namespace Core.Seascape.UI.Models.Layouts
{
    public class LayoutModel
    {
        public const string RootPrefix = "PageModel";

        public LayoutModel()
        {
            SeascapeForm = new SeascapeForm();
            StylesheetPaths = Enumerable.Empty<string>();
        }

        public LayoutModel(string title)
        {
            Title = title;
        }

        public string Title { get; set; }
        public string OpengraphTitle { get; set; }
        public string OpengraphDescription { get; set; }
        public string OpengraphImage { get; set; }
        public string OpengraphUrl { get; set; }
        public string TwitterCard { get; set; }
        public IEnumerable<string> StylesheetPaths { get; set; }
        public SeascapeForm SeascapeForm { get; set; }
    }

    public class LayoutModel<T> : LayoutModel
    {
        public LayoutModel() { }
        public LayoutModel(T pageModel) : this(pageModel, null) { }
        public LayoutModel(T pageModel, string title = null) : base(title) { PageModel = pageModel; }

        public T PageModel { get; }
    }
}
