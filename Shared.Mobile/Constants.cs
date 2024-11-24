using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Mobile
{
    public static class Constants
    {
        public const string AccessToken = "tk";
        public static string BasePath = FileSystem.AppDataDirectory;
        public static string DatabasePath => Path.Combine(BasePath, "database.db");
        public static class Api
        {
            public const string BASE_URL = $"https://tracking.bicicreteiro.app/{BASE_API_PREFIX}{BASE_API_VERSION}";
            public const string BASE_API_PREFIX = "api/";
            public const string BASE_API_VERSION = "v1";
        }
        public static class ApiRoutes
        {
            public const string USER_REGENERATE_USER = "/users/generate-new-user";
            public const string AUTH_LOGIN = "/auth/login";
            public const string ORDER_MY_ORDERS = "/orders/my-orders";
        }
    }
}
