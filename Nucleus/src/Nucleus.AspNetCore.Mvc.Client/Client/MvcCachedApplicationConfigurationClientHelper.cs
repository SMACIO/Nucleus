﻿using System.Globalization;
using Nucleus.Users;

namespace Nucleus.AspNetCore.Mvc.Client
{
    internal static class MvcCachedApplicationConfigurationClientHelper
    {
        public static string CreateCacheKey(ICurrentUser currentUser)
        {
            var userKey = currentUser.Id?.ToString("N") ?? "Anonymous";
            return $"ApplicationConfiguration_{userKey}_{CultureInfo.CurrentUICulture.Name}";
        }
    }
}


