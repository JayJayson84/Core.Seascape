using Core.Seascape.UI.Helpers.Attributes;

namespace Core.Seascape.UI.Enums
{
    /// <summary>
    /// An enumeration containing ajax form modes.
    /// </summary>
    public enum AjaxMode
    {
        [EnumText("replace")] Replace,
        [EnumText("before")] Before,
        [EnumText("after")] After
    }
}
