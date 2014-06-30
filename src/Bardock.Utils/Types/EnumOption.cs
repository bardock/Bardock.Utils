using System;

namespace Bardock.Utils.Types
{
    /// <summary>
    /// Wraps an enum option
    /// </summary>
    /// <typeparam name="TEnum">Enum type</typeparam>
    /// <typeparam name="TValue">Value type. TEnum must can be casted to TValue.</typeparam>
    public class EnumOption<TEnum, TValue>
        where TEnum : struct, IConvertible
        where TValue : struct, IConvertible
    {
        public TEnum Enum { get; private set; }
        public string Name { get; private set; }
        public TValue Value { get; private set; }

        public EnumOption(TEnum @enum)
        {
			if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("TEnum must be an enumerated type");

            this.Enum = @enum;
            this.Name = @enum.ToString();
            this.Value = (TValue)Convert.ChangeType(@enum, typeof(TValue));
        }
    }

    /// <summary>
    /// Wraps an enum option using int as value type
    /// </summary>
    /// <typeparam name="TEnum">Enum type</typeparam>
    public class EnumOption<TEnum> : EnumOption<TEnum, int>
        where TEnum : struct, IConvertible 
    {
        public EnumOption(TEnum @enum)
            : base(@enum) { }
    }

    /// <summary>
    /// Provides type inference for EnumOption creation
    /// </summary>
    public static class EnumOption
    {
        public static EnumOption<TEnum, TValue> Create<TEnum, TValue>(TEnum @enum)
            where TEnum : struct, IConvertible
            where TValue : struct, IConvertible
        {
            return new EnumOption<TEnum, TValue>(@enum);
        }

        public static EnumOption<TEnum> Create<TEnum>(TEnum @enum)
            where TEnum : struct, IConvertible
        {
            return new EnumOption<TEnum>(@enum);
        }
    }
}
