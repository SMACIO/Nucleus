namespace Nucleus.Data
{
    public static class NucleusCommonDbProperties
    {
        /// <summary>
        /// This table prefix is shared by most of the NUCLEUS modules.
        /// You can change it to set table prefix for all modules using this.
        /// 
        /// Default value: "Nucleus".
        /// </summary>
        public static string DbTablePrefix { get; set; } = "Nucleus";

        /// <summary>
        /// Default value: null.
        /// </summary>
        public static string DbSchema { get; set; } = null;
    }
}




