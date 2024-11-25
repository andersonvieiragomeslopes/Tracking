using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class ApiConstants
    {
        public static class ApiRoutes
        {
            public const string USER_REGENERATE_USER = "/users/generate-new-user";
            public const string AUTH_LOGIN = "/auth/login";
            public const string ORDER_MY_ORDERS = "/orders/my-orders";
            public const string ORDERS = "/orders";
            public const string DRIVING = "/driving";
            public const string USERS = "/users";
            public const string ADDRESS = "/address";
        }
    }
}
