using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace st10293982CLD6212POE1.Extensions
{
    public static class SessionExtensions
    {
        // Store an object in the session
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        // Retrieve an object from the session
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
