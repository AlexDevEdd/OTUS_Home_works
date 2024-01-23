using System.Collections.Generic;
using Atomic.Objects;

namespace Plugins.Atomic.Objects.Scripts.Internal
{
    internal sealed class AtomicObjectInfo
    {
        internal readonly IEnumerable<string> types;
        internal readonly IEnumerable<ReferenceInfo> references;
        internal readonly IEnumerable<SectionInfo> sections;

        internal AtomicObjectInfo(
            IEnumerable<string> types,
            IEnumerable<ReferenceInfo> references,
            IEnumerable<SectionInfo> sections
        )
        {
            this.types = types;
            this.references = references;
            this.sections = sections;
        }
    }
}