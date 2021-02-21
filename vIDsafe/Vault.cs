using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vIDsafe
{
    [Serializable]
    public class Vault : Health
    {
        public enum LogType
        {
            Account,
            Passwords,
            Porting
        }

        private readonly Dictionary<LogType, Dictionary<DateTime, string>> _logs = new Dictionary<LogType, Dictionary<DateTime, string>>
        {
            [LogType.Account] = new Dictionary<DateTime, string>(),
            [LogType.Passwords] = new Dictionary<DateTime, string>(),
            [LogType.Porting] = new Dictionary<DateTime, string>()
        };

        ///<value>Get or set the dictionary of identities</value>
        public ConcurrentDictionary<string, Identity> Identities { get; private set; } = new ConcurrentDictionary<string, Identity>();

        ///<value>Get or set the total credential count</value>
        [JsonIgnore]
        public int CredentialCount { get; protected set; } = 0;

        /// <summary>
        /// Creates an identity with a generated email and name
        /// </summary>
        /// <returns>
        /// The credential
        /// </returns>
        public Identity GenerateIdentity()
        {
            string nameToRandomise = "abcdefghijklmnopqrstuvwxyz";

            string email = CredentialGeneration.GenerateUsername(nameToRandomise).ToLower() + "@test.com";
            string name = CredentialGeneration.GenerateUsername(nameToRandomise);
            string usage = "";

            Identity identity = FindOrCreateIdentity(name, email, usage);

            return identity;
        }

        /// <summary>
        /// Find an identity by its ID if not create one
        /// </summary>
        /// <returns>
        /// The identity
        /// </returns>
        public Identity FindOrCreateIdentity(string name, string email, string usage)
        {
            Identity identity = new Identity(name, email, usage)
            {
                Vault = this
            };

            identity = Identities.GetOrAdd(email, identity);

            return identity;
        }

        /// <summary>
        /// Try change an identity's email
        /// </summary>
        /// <returns>
        /// True if doesn't exist and changed, false if not
        /// </returns>
        public bool TryChangeIdentityEmail(Identity identity, string newEmail)
        {
            bool changed = false;

            if (TryDeleteIdentity(identity.Email))
            {
                identity.Email = newEmail;

                changed = TryAddIdentity(identity);
            }

            return changed;
        }

        /// <summary>
        /// Add an identity to the vault
        /// </summary>
        /// <returns>
        /// True if added, false if not
        /// </returns>
        public bool TryAddIdentity(Identity identity)
        {
            string identityEmail = identity.Email;

            bool added = Identities.TryAdd(identityEmail, identity);

            return added;
        }

        /// <summary>
        /// Gets an identity in the vault using an email
        /// </summary>
        /// <returns>
        /// The identity
        /// </returns>
        public Identity TryGetIdentity(string email)
        {
            Identities.TryGetValue(email, out Identity identity);

            return identity;
        }

        /// <summary>
        /// Deletes an identity in the vault
        /// </summary>
        /// <returns>
        /// True if deleted, false if not
        /// </returns>
        public bool TryDeleteIdentity(string identityEmail)
        {
            bool deleted = Identities.TryRemove(identityEmail, out Identity deletedIdentity);

            return deleted;
        }

        /// <summary>
        /// Deletes all identities in the vault
        /// </summary>
        public void DeleteAllIdentities()
        {
            Identities.Clear();
        }

        /// <summary>
        /// Deletes all credentials in the vault
        /// </summary>
        public void DeleteAllCredentials()
        {
            foreach (KeyValuePair<string, Identity> identity in Identities)
            {
                identity.Value.DeleteAllCredentials();
            }
        }

        /// <summary>
        /// Get logs for a selected type
        /// </summary>
        /// <returns>
        /// Logs for a type if exists, create one if not
        /// </returns>
        public Dictionary<DateTime, string> GetLogs(LogType key)
        {
            if (_logs.ContainsKey(key))
            {
                return _logs[key];
            }
            else
            {
                return new Dictionary<DateTime, string>();
            }
        }

        /// <summary>
        /// Create a log for a type
        /// </summary>
        /// <returns>
        /// Created log
        /// </returns>
        public KeyValuePair<DateTime, string> Log(LogType key, string log)
        {
            DateTime currentTime = DateTime.Now;

            _logs[key].Add(currentTime, log);

            return new KeyValuePair<DateTime, string>(currentTime, log);
        }

        /// <summary>
        /// Resets the total credential status counts in the vault
        /// </summary>
        protected override void ResetCredentialCounts()
        {
            CredentialCount = 0;

            foreach (Credential.CredentialStatus status in Enum.GetValues(typeof(Credential.CredentialStatus)))
            {
                CredentialCounts[status] = 0;
            }
        }

        /// <summary>
        /// Counts the total credential statuses in the vault
        /// </summary>
        protected override void CountCredentialStatus(bool calculateStatuses)
        {
            ResetCredentialCounts();

            foreach (KeyValuePair<string, Identity> identityPair in Identities)
            {
                Identity identity = identityPair.Value;

                identity.CalculateHealthScore(calculateStatuses);

                foreach (KeyValuePair<Credential.CredentialStatus, int> status in identity.CredentialCounts)
                {
                    CredentialCounts[status.Key] += status.Value;
                }

                CredentialCount += identity.Credentials.Count;
            }
        }

        /// <summary>
        /// Calculates the overall health score for the vault
        /// </summary>
        public override void CalculateHealthScore(bool calculateStatuses)
        {
            CountCredentialStatus(calculateStatuses);

            if (CredentialCount > 0)
            {
                HealthScore = (int)((double)CredentialCounts[Credential.CredentialStatus.Safe] / CredentialCount * 100);
            }
            else
            {
                HealthScore = 0;
            }
        }
    }
}
