using Newtonsoft.Json;

using System.Text;

using ViewModel.Interfaces.Services.Data;

namespace ViewModel.Dependencies.Data
{
    public class JsonSerializer : ISerializer
    {
        /// <summary>
        /// Настройки Json-сериализатора.
        /// </summary>
        private static readonly JsonSerializerSettings _jsonSerializerSettings =
            new JsonSerializerSettings()
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };

        public T? Deserialize<T>(byte[] data)
        {
            var text = Encoding.Default.GetString(data);
            return JsonConvert.DeserializeObject<T>(text, _jsonSerializerSettings);
        }

        public byte[] Serialize(object value)
        {
            var text = JsonConvert.SerializeObject(value, _jsonSerializerSettings);
            return Encoding.Default.GetBytes(text);
        }
    }
}
