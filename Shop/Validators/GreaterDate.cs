using System.ComponentModel.DataAnnotations;

namespace Shop.Validators
{
    public class GreaterDate : ValidationAttribute
    {
        public GreaterDate() : base("{0} should be greater than the actual date")
        {

        }

        public override bool IsValid(object? value)
        {
            DateTime propvalue = Convert.ToDateTime(value);
            if (propvalue >= DateTime.Now)
                return true;
            else
                return false;
        }
    }
}