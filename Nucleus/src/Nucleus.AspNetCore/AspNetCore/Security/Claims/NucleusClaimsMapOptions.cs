using System;
using System.Collections.Generic;
using Nucleus.Security.Claims;

namespace Nucleus.AspNetCore.Security.Claims
{
    public class NucleusClaimsMapOptions
    {
        public Dictionary<string, Func<string>> Maps { get; }

        public NucleusClaimsMapOptions()
        {
            Maps = new Dictionary<string, Func<string>>()
            {
                { "sub", () => NucleusClaimTypes.UserId },
                { "role", () => NucleusClaimTypes.Role },
                { "email", () => NucleusClaimTypes.Email },
                { "name", () => NucleusClaimTypes.UserName },
                { "family_name", () => NucleusClaimTypes.SurName },
                { "given_name", () => NucleusClaimTypes.Name }
            };
        }
    }
}




