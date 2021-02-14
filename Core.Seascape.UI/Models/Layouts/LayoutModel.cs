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

        public string Title { get; set; } = "Seascape Randomiser";
        public string OpengraphTitle { get; set; } = "Seascape Randomiser";
        public string OpengraphDescription { get; set; } = "Create random animated seascapes with a range of eye-catching foreground silhouettes.";
        public string OpengraphImage { get; set; } = "/images/seascape-randomiser.png";
        public string OpengraphUrl { get; set; } = "https://www.jayjayson.com/";
        public string TwitterCard { get; set; } = "summary_large_image";
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
