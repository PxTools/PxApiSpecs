using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace yamlGen
{
    public class BaseFirstContractResolver : DefaultContractResolver
    {
        public BaseFirstContractResolver()
        {
            NamingStrategy = new CamelCaseNamingStrategy();
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            return base.CreateProperties(type, memberSerialization)
                .OrderBy(p => BaseTypesAndSelf(p.DeclaringType).Count()).ToList();

            IEnumerable<Type> BaseTypesAndSelf(Type t)
            {
                while (t != null)
                {
                    yield return t;
                    t = t.BaseType;
                }
            }
        }
    }
}
