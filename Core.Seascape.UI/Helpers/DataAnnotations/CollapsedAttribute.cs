using System.ComponentModel.DataAnnotations;

namespace System.ComponentModel.ExtendedDataAnnotations
{
    public class CollapsedAttribute : ValidationAttribute
    {
        public bool Collapse { get; set; }

        public CollapsedAttribute(bool value)
        {
            Collapse = value;
        }

        public override bool IsValid(object value)
        {
            return value is bool;
        }
    }
}
