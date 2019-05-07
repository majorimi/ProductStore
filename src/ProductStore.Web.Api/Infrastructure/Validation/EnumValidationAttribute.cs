using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductStore.Web.Api.Infrastructure.Validation
{
	public class EnumValidationAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			Type enumType = value?.GetType();
			if(enumType is null)
			{
				return ValidationResult.Success;
			}

			if (enumType.IsGenericType && enumType.GetGenericTypeDefinition().Equals(typeof(List<>)))
			{
				enumType = enumType.GetGenericArguments()[0];
				foreach (var item in (value as IEnumerable))
				{
					var result = ValidateEnumValue(enumType, item);
					if (result != null)
					{
						return result;
					}
				}

				return ValidationResult.Success;
			}

			return ValidateEnumValue(enumType, value) ?? ValidationResult.Success;
		}

		private ValidationResult ValidateEnumValue(Type enumType, object value)
		{
			bool valid = Enum.IsDefined(enumType, value);
			if (!valid)
			{
				return new ValidationResult($"{value} is not a valid value for type {enumType.Name}");
			}

			return null;
		}
	}
}