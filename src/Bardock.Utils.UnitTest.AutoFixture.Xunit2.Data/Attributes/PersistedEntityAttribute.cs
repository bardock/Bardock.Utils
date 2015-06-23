﻿using Bardock.Utils.UnitTest.Data.AutoFixture.Customizations;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using System.Reflection;

namespace Bardock.Utils.UnitTest.AutoFixture.Xunit2.Data.Attributes
{
    /// <summary>
    /// Applies a <see cref="PersistedEntityCustomization"/> to parameters in methods
    /// decorated with Ploeh.AutoFixture.Xunit2.AutoDataAttribute
    /// </summary>
    public class PersistedEntityAttribute : CustomizeAttribute
    {
        public override ICustomization GetCustomization(ParameterInfo parameter)
        {
            return new PersistedEntityCustomization(parameter);
        }
    }
}