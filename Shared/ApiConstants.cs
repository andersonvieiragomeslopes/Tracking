﻿using System;
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
        public const string JTW_DEBUG = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMzhiNWU4MjUtNjNmMi00NTZiLTgwOTUtYTk2MGY3MDk1NzVlIiwiZXhwIjoxNzM1MTY1MDM5fQ.isIrd9Ch0Q6fTakvo7VzpFcHNaA6PIQYEWoHdoyBTPU";
        //public const string JTW_DEBUG = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiOWQyMWZhYTItMjg3MC00NTNiLWIzM2UtYWRkZDcwNGRhZGVhIiwiZXhwIjoxNzM1MDc0Mjg3fQ.pFnh9YwiNjSm80fgPUpRXcuSjjSk4EysEu9FfirTEcQ";

    }
}
