﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nucleus.Http.Client.ClientProxying
{
    public interface IObjectToFormData<in TValue>
    {
        Task<List<KeyValuePair<string, HttpContent>>> ConvertAsync(TValue value);
    }
}

