using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.AspNetCore.DataProtection;

namespace Api.Models.Encryption
{
    public static class DataProtectionExtensions
    {
        public static string PersistentUnprotect(
            this IPersistedDataProtector dp,
            string protectedData,
            out bool requiresMigration,
            out bool wasRevoked)
        {
            byte[] protectedBytes = Convert.FromBase64String(protectedData);
            byte[] unprotectedBytes = dp.DangerousUnprotect(protectedBytes, ignoreRevocationErrors: true, requiresMigration: out requiresMigration, wasRevoked: out wasRevoked);

            return Encoding.UTF8.GetString(unprotectedBytes);
        }

        public static string PersistentProtect(
            this IPersistedDataProtector dp,
            string clearText)
        {
            byte[] clearBytes = Encoding.UTF8.GetBytes(clearText);
            byte[] protectedBytes = dp.Protect(clearBytes);

            string result = Convert.ToBase64String(protectedBytes);
            return result;

        }

    }

}