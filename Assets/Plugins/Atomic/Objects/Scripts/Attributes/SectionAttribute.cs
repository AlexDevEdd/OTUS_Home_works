using System;
using JetBrains.Annotations;

namespace Plugins.Atomic.Objects.Scripts.Attributes
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class SectionAttribute : Attribute
    {
    }
}