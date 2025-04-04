using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace api.Validators;

public class StudentId : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string studentId)
        {
            string pattern = @"[a-z]{2}-\d{4}";
            bool isMatch = Regex.IsMatch(studentId, pattern);
            if (isMatch) return ValidationResult.Success!;
        }

        return new ValidationResult("Invalid studentId");
    }
}
