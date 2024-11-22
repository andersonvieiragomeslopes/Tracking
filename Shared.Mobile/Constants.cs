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
        public static class Api
        {
            public const string BASE_URL = $"https://tracking.bicicreteiro.app{BASE_API_PREFIX}{BASE_API_VERSION}";
            public const string BASE_API_PREFIX = "/api/";
            public const string BASE_API_VERSION = "v1/";
        }
        public static class ApiRoutes
        {
            public const string REGENERATE_USER = "users/generate-new-user";
        }
    }
}
