using System;
using Nucleus.DependencyInjection;
using Nucleus.Security.Encryption;

namespace Nucleus.Settings
{
    public class SettingEncryptionService : ISettingEncryptionService, ITransientDependency
    {
        protected IStringEncryptionService StringEncryptionService { get; }

        public SettingEncryptionService(IStringEncryptionService stringEncryptionService)
        {
            StringEncryptionService = stringEncryptionService;
        }

        public virtual string Encrypt(SettingDefinition settingDefinition, string plainValue)
        {
            if (plainValue.IsNullOrEmpty())
            {
                return plainValue;
            }

            return StringEncryptionService.Encrypt(plainValue);
        }

        public virtual string Decrypt(SettingDefinition settingDefinition, string encryptedValue)
        {
            if (encryptedValue.IsNullOrEmpty())
            {
                return encryptedValue;
            }

            return StringEncryptionService.Decrypt(encryptedValue);
        }
    }
}

