using BullPerks_TestWork.Domain.DB.Models;
using BullPerks_TestWork.Domain.Models.JSON;
using BullPerks_TestWork.Domain.ViewModels;
using System.ComponentModel;
using System.Globalization;

namespace BullPerks_TestWork.Services.Converters
{
    public class CoinStatsTokenConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(TokenViewModel) || destinationType == typeof(DbToken);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var concreteValue = (CoinStatsGetWalletBalanceModel)value;

            object result = null;

            if (destinationType == typeof(TokenViewModel))
            {
                result = new TokenViewModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = concreteValue.CoinId.Length > 42 ? concreteValue.CoinId.Substring(0, concreteValue.CoinId.Length - 42) : concreteValue.CoinId,
                    ContractAddress = concreteValue.CoinId.Length > 42 ? concreteValue.CoinId.Substring(concreteValue.CoinId.Length - 42) : "",
                };
            } else if (destinationType == typeof(DbToken))
            {
                result = new DbToken
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = concreteValue.CoinId.Length > 42 ? concreteValue.CoinId.Substring(0, concreteValue.CoinId.Length - 42) : concreteValue.CoinId,
                };
            }

           
            return result;
        }
    }
}
