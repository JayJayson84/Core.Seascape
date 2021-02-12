using System;

namespace Core.Seascape.UI.Helpers.Attributes
{
    /// <summary>
    /// Used to attach a string value to Enum members.
    /// </summary>
    public class EnumTextAttribute : Attribute
    {

        #region Properties

        public string StringValue { get; protected set; }

        #endregion

        #region Constructor

        public EnumTextAttribute(string value)
        {
            StringValue = value;
        }

        #endregion

    }
}
