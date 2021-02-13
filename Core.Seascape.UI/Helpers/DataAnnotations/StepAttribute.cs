using System.ComponentModel.DataAnnotations;

namespace System.ComponentModel.ExtendedDataAnnotations
{
    public class StepAttribute : ValidationAttribute
    {
        public int Step { get; set; }

        public StepAttribute(int value)
        {
            Step = value;
        }

        public override bool IsValid(object value)
        {
            return value is int;
        }
    }
}
