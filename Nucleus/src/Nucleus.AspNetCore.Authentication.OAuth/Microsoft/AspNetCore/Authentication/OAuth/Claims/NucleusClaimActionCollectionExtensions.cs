using Nucleus.AspNetCore.Authentication.OAuth.Claims;
using Nucleus.Security.Claims;

namespace Microsoft.AspNetCore.Authentication.OAuth.Claims
{
    public static class NucleusClaimActionCollectionExtensions
    {
        public static void MapNucleusClaimTypes(this ClaimActionCollection claimActions)
        {
            if (NucleusClaimTypes.UserName != "name")
            {
                claimActions.MapJsonKey(NucleusClaimTypes.UserName, "name");
                claimActions.DeleteClaim("name");
                claimActions.RemoveDuplicate(NucleusClaimTypes.UserName);
            }

            if (NucleusClaimTypes.Email != "email")
            {
                claimActions.MapJsonKey(NucleusClaimTypes.Email, "email");
                claimActions.DeleteClaim("email");
                claimActions.RemoveDuplicate(NucleusClaimTypes.Email);
            }

            if (NucleusClaimTypes.EmailVerified != "email_verified")
            {
                claimActions.MapJsonKey(NucleusClaimTypes.EmailVerified, "email_verified");
            }

            if (NucleusClaimTypes.PhoneNumber != "phone_number")
            {
                claimActions.MapJsonKey(NucleusClaimTypes.PhoneNumber, "phone_number");
            }

            if (NucleusClaimTypes.PhoneNumberVerified != "phone_number_verified")
            {
                claimActions.MapJsonKey(NucleusClaimTypes.PhoneNumberVerified, "phone_number_verified");
            }

            if (NucleusClaimTypes.Role != "role")
            {
                claimActions.MapJsonKeyMultiple(NucleusClaimTypes.Role, "role");
            }
            
            claimActions.RemoveDuplicate(NucleusClaimTypes.Name);
        }

        public static void MapJsonKeyMultiple(this ClaimActionCollection claimActions, string claimType, string jsonKey)
        {
            claimActions.Add(new MultipleClaimAction(claimType, jsonKey));
        }
        
        public static void RemoveDuplicate(this ClaimActionCollection claimActions, string claimType)
        {
            claimActions.Add(new RemoveDuplicateClaimAction(claimType));
        }
    }
}



