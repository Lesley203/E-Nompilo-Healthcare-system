using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace E_Nompilo_Healthcare_system.Services
{
   

    [AttributeUsage(AttributeTargets.Property)]
    public class IdentityNoValidationAttribute : ValidationAttribute, IClientModelValidator
    {
        public string UserIdProperty { get; set; }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-identitynovalidation", GetErrorMessage());
            context.Attributes.Add("data-val-useridproperty", UserIdProperty);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Allow for optional validation
            }

            if (validationContext == null)
            {
                throw new ArgumentNullException(nameof(validationContext));
            }

            // Extract the user ID property name from the custom attribute
            string userIdPropertyName = UserIdProperty;
            if (string.IsNullOrEmpty(userIdPropertyName))
            {
                throw new InvalidOperationException("UserIdProperty is not set.");
            }

            // Get the user ID property value using reflection
            var userIdProperty = validationContext.ObjectType.GetProperty(userIdPropertyName);
            if (userIdProperty == null)
            {
                throw new InvalidOperationException($"Property '{userIdPropertyName}' not found on type '{validationContext.ObjectType.FullName}'.");
            }

            var userIdValue = userIdProperty.GetValue(validationContext.ObjectInstance);

            // Perform your validation logic here, comparing IdentityNo with DateofBirth and user-specific rules
            // For example:
            // var identityNo = (string)value;
            // var dateOfBirth = (DateTime)userIdValue;
            // ...

            // If the validation passes, return ValidationResult.Success
            // Otherwise, return an appropriate ValidationResult with an error message

            return ValidationResult.Success; // Replace this with your actual validation logic
        }

        private string GetErrorMessage()
        {
            return ErrorMessage ?? "Invalid IdentityNo.";
        }
    }

}
