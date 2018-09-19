using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Configuration;

namespace NumberInWords.Core
{
    public static class Util
    {
        /// <summary>
        /// Helper method to get value from configuration file
        /// </summary>
        /// <param name="key">Config "key" name</param>
        /// <returns>Value for config key </returns>
        public static string GetConfigData(string key)
        {
            return string.IsNullOrEmpty(key) ? string.Empty : ConfigurationManager.AppSettings.Get(key);
        }

        /// <summary>
        /// get serialized data in camel case
        /// </summary>
        /// <param name="data">object to be serialized</param>
        /// <returns>serialized data</returns>
        public static string Serialize(object data)
        {
            return data == null ? string.Empty :
                JsonConvert.SerializeObject(data, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }

        /// <summary>
        /// get deserialized object
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="data">serialized data</param>
        /// <returns></returns>
        public static T Deserialize<T>(string data)
        {
            return string.IsNullOrEmpty(data) ? default(T) : JsonConvert.DeserializeObject<T>(data);
        }
    }
}
