using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Test.Utilities.Extensibility
{
    [DataContract(Namespace="")]
    public class ExtensibleGetOptions
    {
        [DataMember(Name="parts", IsRequired = false)]
        public List<string> Parts { get; set; }
        [DataMember(Name="sourceUrl", IsRequired = false)]
        public string SourceUrl { get; set; }

        public bool PartIsUsed(string extension)
        {
            if (Parts == null)
                return false;

            for (int i = 0; i < Parts.Count; i++)
            {
                if (Parts[i].ToLower() == extension.ToLower())
                    return true;
            }

            return false;
        }
    }
}