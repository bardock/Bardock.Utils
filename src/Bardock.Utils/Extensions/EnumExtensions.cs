using System;
using System.Collections.Generic;
using System.Linq;

namespace Bardock.Utils.Extensions
{
    public static class EnumExtensions
	{
        public static Dictionary<string, int> ToDictionary<TEnum>(this TEnum enumInstance) where TEnum : struct
		{
			if ((typeof(TEnum).IsEnum == false)) {
				throw new ArgumentException("TEnum must be an enumerated type");
			}
			return Enum.GetValues(typeof(TEnum)).Cast<int>().ToDictionary(x => Enum.ToObject(typeof(TEnum), x).ToString());
		}
	}
}