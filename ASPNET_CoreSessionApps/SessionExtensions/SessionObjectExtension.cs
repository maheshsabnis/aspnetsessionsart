using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ASPNET_CoreSessionApps.SessionExtensions
{
    public static class SessionExtensions
    {
        public static void SetSessionData<T>(this ISession session, string sessionKey, T sessionValue)
        {
            session.SetString(sessionKey, JsonConvert.SerializeObject(sessionValue));
        }

        public static T GetSessionData<T>(this ISession session, string sessionKey)
        {
            var data = session.GetString(sessionKey);
            if (data == null) {
                return default(T);
            } else {
                return JsonConvert.DeserializeObject<T>(data);
            }
        }
    }
}
