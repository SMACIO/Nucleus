namespace Nucleus.Authorization
{
    public static class NucleusAuthorizationErrorCodes
    {
        public const string GivenPolicyHasNotGranted = "Nucleus.Authorization:010001";

        public const string GivenPolicyHasNotGrantedWithPolicyName = "Nucleus.Authorization:010002";

        public const string GivenPolicyHasNotGrantedForGivenResource = "Nucleus.Authorization:010003";

        public const string GivenRequirementHasNotGrantedForGivenResource = "Nucleus.Authorization:010004";

        public const string GivenRequirementsHasNotGrantedForGivenResource = "Nucleus.Authorization:010005";
    }
}



