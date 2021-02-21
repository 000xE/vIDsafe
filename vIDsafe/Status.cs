using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace vIDsafe
{
    [Serializable]
    public class Status
    {
        /// <summary>
        /// Checks if the URL has been breached 
        /// </summary>
        /// <returns>
        /// True if the URL is valid and in the list, false if not
        /// </returns>
        protected bool CheckBreached(Dictionary<string, string> breachedDomains, string domain)
        {
            if (domain.Length > 0)
            {
                if (breachedDomains.ContainsKey(domain))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if the username or the password is present somewhere else
        /// </summary>
        /// <returns>
        /// True if either is present, false if not
        /// </returns>
        protected bool CheckConflict(ConcurrentDictionary<string, Identity> identities, string credentialID, string username, string password)
        {
            if (username.Length > 0 || password.Length > 0)
            {
                foreach (KeyValuePair<string, Identity> identityPair in identities)
                {
                    if (identityPair.Value.Credentials.Any(c => (c.Value.CredentialID != credentialID)
                    && (c.Value.Username.Equals(username, StringComparison.OrdinalIgnoreCase) || c.Value.Password.Equals(password))))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if a password's strength is below a threshold
        /// </summary>
        /// <returns>
        /// True if the URL is valid and in the list, false if not
        /// </returns>
        protected bool CheckWeak(string password)
        {
            if (password.Length > 0)
            {
                double strengthThreshold = 30.0;

                if (CredentialGeneration.CheckStrength(password) < strengthThreshold)
                {
                    return true;
                }
            }

            return false;
        }
    }
}