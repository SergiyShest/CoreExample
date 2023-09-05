using System.ComponentModel.DataAnnotations;

namespace DAL.Core
{
    public static class ValidationHelper
    {
        public static List<string> Validate(object entity, bool throwEx = false)
        {
            var ret = new List<string>();

            if (entity == null) return ret;
            var results = new List<ValidationResult>();
            var context = new ValidationContext(entity);

            if (throwEx)
            {
                Validator.ValidateObject(entity, context, true);
            }
            else
            if (!Validator.TryValidateObject(entity, context, results, true))
            {
                foreach (var error in results)
                {
                    var members = error.MemberNames;
                    ret.Add(members + "  " + error.ErrorMessage);
                }
            }

            return ret;
        }

    }
}
