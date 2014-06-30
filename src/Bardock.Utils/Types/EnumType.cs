using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Bardock.Utils.Types
{
    /// <summary>
    /// Represents an enumerated type that can be iterated
    /// </summary>
    /// <typeparam name="TEnum">Enum type</typeparam>
    /// <typeparam name="TValue">Value type. TEnum must can be casted to TValue.</typeparam>
    public class EnumType<TEnum, TValue> : IEnumerable<EnumOption<TEnum, TValue>>
        where TEnum : struct, IConvertible
        where TValue : struct, IConvertible
    {
        public IEnumerable<EnumOption<TEnum, TValue>> Options { get; private set; }

        public EnumType()
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("TEnum must be an enumerated type");

            this.Options = Enum.GetValues(typeof(TEnum))
                .Cast<TValue>()
                .Select(val => EnumOption.Create<TEnum, TValue>((TEnum)Enum.ToObject(typeof(TEnum), val)));
        }

        public IEnumerator<EnumOption<TEnum, TValue>> GetEnumerator()
        {
            return this.Options.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Options.GetEnumerator();
        }
    }

    /// <summary>
    /// Represents an enumerated type that can be iterated
    /// </summary>
    /// <typeparam name="TEnum">Enum type</typeparam>
    public class EnumType<TEnum> : EnumType<TEnum, int>
        where TEnum : struct, IConvertible
    {
        public EnumType()
            : base() { }
    }

    /// <summary>
    /// Provides type inference for EnumType creation
    /// </summary>
    public static class EnumType
    {
        public static EnumType<TEnum, TValue> Create<TEnum, TValue>(TEnum @enum = default(TEnum))
            where TEnum : struct, IConvertible
            where TValue : struct, IConvertible
        {
            return new EnumType<TEnum, TValue>();
        }

        public static EnumType<TEnum> Create<TEnum>(TEnum @enum = default(TEnum))
            where TEnum : struct, IConvertible
        {
            return new EnumType<TEnum>();
        }
    }
}
