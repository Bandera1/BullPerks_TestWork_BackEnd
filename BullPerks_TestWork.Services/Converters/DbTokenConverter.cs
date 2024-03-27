using BullPerks_TestWork.Domain.DB.Models;
using BullPerks_TestWork.Domain.Models.JSON;
using System.ComponentModel;
using System.Globalization;

namespace BullPerks_TestWork.Services.Converters
{
    public class DbTokenConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(DbToken);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var concreteValue = (CoinStatsGetWalletBalanceModel)value;

            var result = new DbToken
            {
                Id = Guid.NewGuid().ToString(),
                Name = concreteValue.CoinId.Length > 42 ? concreteValue.CoinId.Substring(0, concreteValue.CoinId.Length - 42) : concreteValue.CoinId,
                ContractAddress = concreteValue.CoinId.Length > 42 ? concreteValue.CoinId.Substring(concreteValue.CoinId.Length - 42) : "",
            };

            return result;
        }
    }
}
