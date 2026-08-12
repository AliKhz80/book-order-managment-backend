using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CatalogService.Application.Common.Validation
{
    public static class ValidationBehavior
    {
        public static (bool IsValid, IDictionary<string, string[]> Errors) Validate<T>(this T model) where T : class
        {
            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

            var errors = new Dictionary<string, string[]>();
            if (!isValid)
            {
                foreach (var result in validationResults)
                {
                    var members = result.MemberNames.Any() ? result.MemberNames : new[] { "General" };
                    foreach (var member in members)
                    {
                        if (!errors.ContainsKey(member))
                        {
                            errors[member] = Array.Empty<string>();
                        }
                        errors[member] = errors[member].Append(result.ErrorMessage ?? "Validation error").ToArray();
                    }
                }
            }

            return (isValid, errors);
        }
    }
}
