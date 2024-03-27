﻿using BullPerks_TestWork.Domain.DB.Models;
using BullPerks_TestWork.Domain.Models.JSON;
using BullPerks_TestWork.Domain.ViewModels;
using System.ComponentModel;
using System.Globalization;

namespace BullPerks_TestWork.Services.Converters
{
    public class TokenViewModelConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(TokenViewModel);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var concreteValue = (DbToken)value;

            var result = new TokenViewModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = concreteValue.Name,
            };

            return result;
        }
    }
}
