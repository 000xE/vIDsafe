using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;
using EnzoicClient;
using Newtonsoft.Json;

namespace vIDsafe
{
    [Serializable]
    public class Identity
    {
        private readonly Vault _vault = new Vault();

        ///<value>Get or set the identity name</value>
        [Name("name")]
        public string Name { get; set; } = "";

        ///<value>Get or set the identity email</value>
        [Name("email")]
        public string Email { get; set; } = "";

        ///<value>Get or set the identity usage</value>
        [Name("usage")]
        public string Usage { get; set; } = "";

        ///<value>Get or set the identity health score</value>
        [Ignore]
        [JsonIgnore]
        public int HealthScore { get; private set; } = 0;

        ///<value>Get or set the dictionary of breached doamins</value>
        public Dictionary<string, string> BreachedDomains { get; private set; } = new Dictionary<string, string>();

        ///<value>Get or set the dictionary of credentials</value>
        public Dictionary<string, Credential> Credentials { get; private set; } = new Dictionary<string, Credential>();

        [Ignore]
        [JsonIgnore]
        public Dictionary<Credential.CredentialStatus, int> CredentialCounts { get; private set; } = new Dictionary<Credential.CredentialStatus, int>()
        {
            [Credential.CredentialStatus.Safe] = 0,
            [Credential.CredentialStatus.Compromised] = 0,
            [Credential.CredentialStatus.Conflicted] = 0,
            [Credential.CredentialStatus.Weak] = 0
        };

        [Ignore]
        [JsonIgnore]
        public int SafeCredentialCount => CredentialCounts[Credential.CredentialStatus.Safe];

        [Ignore]
        [JsonIgnore]
        public int CompromisedCredentialCount => CredentialCounts[Credential.CredentialStatus.Compromised];

        [Ignore]
        [JsonIgnore]
        public int WeakCredentialCount => CredentialCounts[Credential.CredentialStatus.Weak];

        [Ignore]
        [JsonIgnore]
        public int ConflictCredentialCount => CredentialCounts[Credential.CredentialStatus.Conflicted];

        /// <summary>
        /// Creates an identity
        /// </summary>
        /// <returns>
        /// The identity
        /// </returns>
        public Identity(Vault vault, string name, string email, string usage)
        {
            _vault = vault;

            Name = name;
            Email = email;
            Usage = usage;
        }

        /// <summary>
        /// Creates a credential with a generated ID, username and a password
        /// </summary>
        /// <returns>
        /// The credential
        /// </returns>
        public Credential GenerateCredential()
        {
            lock (this)
            {
                string GUID = Guid.NewGuid().ToString();

                string url = "";
                string notes = "";

                string username = CredentialGeneration.GenerateUsername(Name);
                string password = CredentialGeneration.GeneratePassword();

                Credential credential = FindOrCreateCredential(GUID, username, password, url, notes);

                return credential;
            }
        }

        /// <summary>
        /// Find a credential by its ID if not create one
        /// </summary>
        /// <returns>
        /// The credential
        /// </returns>
        public Credential FindOrCreateCredential(string GUID, string username, string password, string url, string notes)
        {
            lock (this)
            {
                Credential credential;

                if (Credentials.ContainsKey(GUID))
                {
                    credential = Credentials[GUID];
                }
                else
                {
                    credential = new Credential(GUID, username, password, url, notes);
                    Credentials.Add(GUID, credential);
                }

                return credential;
            }
        }

        /// <summary>
        /// Deletes a credential in the identity
        /// </summary>
        public void DeleteCredential(string key)
        {
            lock (this)
            {
                if (Credentials.ContainsKey(key))
                {
                    Credentials.Remove(key);
                }
            }
        }

        /// <summary>
        /// Deletes all credentials in the identity
        /// </summary>
        public void DeleteAllCredentials()
        {
            lock (this)
            {
                Credentials.Clear();
            }
        }

        /// <summary>
        /// Gets the breached data for an email address
        /// </summary>
        /// <returns>
        /// The breached domains with their date/time
        /// </returns>
        public Dictionary<string, string> GetBreaches(string email, bool useAPI)
        {
            lock (this)
            {
                if (useAPI)
                {
                    List<ExposureDetails> exposureDetails = EnzoicAPI.GetExposureDetails(email);

                    BreachedDomains.Clear();

                    if (exposureDetails.Count > 0)
                    {
                        foreach (ExposureDetails detail in exposureDetails)
                        {
                            if (!BreachedDomains.ContainsKey(detail.Title))
                            {
                                BreachedDomains.Add(detail.Title, detail.Date.ToString());
                            }
                        }
                    }
                }

                return BreachedDomains;
            }
        }

        /// <summary>
        /// Resets the credential status counts in the identity
        /// </summary>
        private void ResetCredentialCounts()
        {
            foreach (Credential.CredentialStatus status in Enum.GetValues(typeof(Credential.CredentialStatus)))
            {
                CredentialCounts[status] = 0;
            }
        }

        /// <summary>
        /// Counts the credential statuses in the identity
        /// </summary>
        private void CountCredentialStatus()
        {
            ResetCredentialCounts();

            foreach (KeyValuePair<string, Credential> credentialPair in Credentials)
            {
                Credential credential = credentialPair.Value;

                CredentialCounts[credential.Status]++;
            }
        }

        /// <summary>
        /// Calculates the credential statuses in the identity
        /// </summary>
        private void SetCredentialStatuses()
        {
            foreach (KeyValuePair<string, Credential> credentialPair in Credentials)
            {
                Credential credential = credentialPair.Value;

                credential.CalculateStatus(_vault, this);
            }
        }

        /// <summary>
        /// Calculates the health score for the identity
        /// </summary>
        public void CalculateHealthScore(bool calculateStatuses)
        {
            lock (this)
            {
                if (calculateStatuses)
                {
                    SetCredentialStatuses();
                }

                CountCredentialStatus();

                if (Credentials.Count > 0)
                {
                    HealthScore = (int)((double)CredentialCounts[Credential.CredentialStatus.Safe] / Credentials.Count * 100);
                }
                else
                {
                    HealthScore = 0;
                }
            }
        }
    }
}
