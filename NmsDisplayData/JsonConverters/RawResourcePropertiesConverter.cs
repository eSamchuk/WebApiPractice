using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NmsDisplayData.JsonConverters
{
    public class RawResourcePropertiesConverter : JsonConverter
    {
        private readonly Type _targetType;

        public RawResourcePropertiesConverter(Type type)
        {
            this._targetType = type;
        }

        public override bool CanConvert(Type objectType)
        {
            return this._targetType == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value);

            if (t.Type != JTokenType.Object)
            {
                t.WriteTo(writer);
            }
            else
            {
                JObject o = (JObject)t;
                IList<string> propertyNames = o.Properties().Select(p => p.Name).ToList();

                foreach (var property in o.Properties())
                {
                    ///writer.WritePropertyName();
                }

            }
        }
    }
}
