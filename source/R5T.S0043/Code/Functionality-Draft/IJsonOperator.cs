using System;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using R5T.T0132;


namespace R5T.S0043
{
    [DraftFunctionalityMarker]
    public partial interface IJsonOperator : IDraftFunctionalityMarker
    {
        private static Internal.IJsonOperator Internal { get; } = S0043.Internal.JsonOperator.Instance;


        public async Task<T> LoadFromFile<T>(string jsonFilePath, string keyName)
        {
            var jObject = await Internal.LoadAsJObject(jsonFilePath);

            var keyedJObject = jObject[keyName];

            var output = keyedJObject.ToObject<T>();
            return output;
        }

        public T LoadFromFile_Synchronous<T>(string jsonFilePath, string keyName)
        {
            var jObject = Internal.LoadAsJObject_Synchronous(jsonFilePath);

            var keyedJObject = jObject[keyName];

            var output = keyedJObject.ToObject<T>();
            return output;
        }
    }


    namespace Internal
    {
        public partial interface IJsonOperator : IDraftFunctionalityMarker
        {
            public JsonSerializer GetJsonSerializer(Formatting formatting = Formatting.Indented)
            {
                var jsonSerializer = new JsonSerializer
                {
                    Formatting = formatting,
                };

                return jsonSerializer;
            }

            public async Task<JObject> LoadAsJObject(string jsonFilePath)
            {
                using var streamReader = Instances.StreamReaderOperator.GetNew(jsonFilePath);
                using var jsonTextReader = new JsonTextReader(streamReader);

                var output = await JObject.LoadAsync(jsonTextReader);
                return output;
            }

            public JObject LoadAsJObject_Synchronous(string jsonFilePath)
            {
                using var streamReader = Instances.StreamReaderOperator.GetNew(jsonFilePath);
                using var jsonTextReader = new JsonTextReader(streamReader);

                var output = JObject.Load(jsonTextReader);
                return output;
            }
        }
    }
}