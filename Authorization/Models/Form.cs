using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Authorization.Models
{
    public abstract class Form
    {
        public virtual bool Validate()
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(this);
            if (!Validator.TryValidateObject(this, context, results, true))
            {
                foreach (var error in results)
                {
                    return false;
                }
                return true;
            }
            else return true;
        }
    }
}