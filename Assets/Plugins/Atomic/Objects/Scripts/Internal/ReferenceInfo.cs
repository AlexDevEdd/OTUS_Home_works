using System;

namespace Plugins.Atomic.Objects.Scripts.Internal
{
    internal sealed class ReferenceInfo
    {
        internal readonly string id;
        internal readonly bool @override;
        internal readonly Func<object, object> value;

        internal ReferenceInfo(string id, bool @override, Func<object, object> value)
        {
            this.id = id;
            this.@override = @override;
            this.value = value;
        }
    }
}