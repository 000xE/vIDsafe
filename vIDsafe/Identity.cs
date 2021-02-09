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
        private readonly Vault _vault;

        private int _healthScore;

        private readonly Dictionary<string, Credential> _credentials;

        private readonly Dictionary<Credential.CredentialStatus, int> _credentialCounts;

        private readonly Dictionary<string, string> _breachedDomains;

        [Name("name")]
        public string Name { get; set; }

        [Name("email")]
        public string Email { get; set; }

        [Name("usage")]
        public string Usage { get; set; }

        [Ignore]
        [JsonIgnore]
        public int HealthScore => _healthScore;

        [Ignore]
        [JsonIgnore]
        public Dictionary<Credential.CredentialStatus, int> CredentialCounts => _credentialCounts;

        [Ignore]
        [JsonIgnore]
        public int SafeCredentials => _credentialCounts[Credential.CredentialStatus.Safe];

        [Ignore]
        [JsonIgnore]
        public int CompromisedCredentials => _credentialCounts[Credential.CredentialStatus.Compromised];

        [Ignore]
        [JsonIgnore]
        public int WeakCredentials => _credentialCounts[Credential.CredentialStatus.Weak];

        [Ignore]
        [JsonIgnore]
        public int ConflictCredentials => _credentialCounts[Credential.CredentialStatus.Conflicted];

        public Dictionary<string, string> BreachedDomains => _breachedDomains;

        public Dictionary<string, Credential> Credentials => _credentials;

        public Identity(Vault vault, string name, string email, string usage)
        {
            _vault = vault;

            Name = name;
            Email = email;
            Usage = usage;

            _credentials = new Dictionary<string, Credential>();
            _breachedDomains = new Dictionary<string, string>();

            _credentialCounts = new Dictionary<Credential.CredentialStatus, int>()
            {
                [Credential.CredentialStatus.Safe] = 0,
                [Credential.CredentialStatus.Compromised] = 0,
                [Credential.CredentialStatus.Conflicted] = 0,
                [Credential.CredentialStatus.Weak] = 0
            };
        }

        public Credential GenerateCredential()
        {
            string GUID = Guid.NewGuid().ToString();

            string url = "";
            string notes = "";

            string username = CredentialGeneration.GenerateUsername(Name);
            string password = CredentialGeneration.GeneratePassword();

            Credential credential = FindOrCreateCredential(GUID, username, password, url, notes);

            return credential;
        }

        public Credential FindOrCreateCredential(string GUID, string username, string password, string url, string notes)
        {
            Credential credential;

            if (Credentials.ContainsKey(GUID))
            {
                credential = Credentials[GUID];
            }
            else
            {
                credential = new Credential(GUID, username, password, url, notes);
                _credentials.Add(GUID, credential);
            }

            return credential;
        }

        public void DeleteAllCredentials()
        {
            Credentials.Clear();
        }

        public void DeleteCredential(string key)
        {
            if (_credentials.ContainsKey(key))
            {
                _credentials.Remove(key);
            }
        }

        public async Task<Dictionary<string, string>> GetBreaches(string email, bool useAPI)
        {
            await Task.Run(() =>
            {
                if (useAPI)
                {
                    List<ExposureDetails> exposureDetails = EnzoicAPI.GetExposureDetails(email);

                    _breachedDomains.Clear();

                    if (exposureDetails.Count > 0)
                    {
                        foreach (ExposureDetails detail in exposureDetails)
                        {
                            if (!_breachedDomains.ContainsKey(detail.Title))
                            {
                                _breachedDomains.Add(detail.Title, detail.Date.ToString());
                            }
                        }
                    }
                }
            });

            return _breachedDomains;
        }

        private void ResetCredentialCounts()
        {
            foreach (Credential.CredentialStatus status in Enum.GetValues(typeof(Credential.CredentialStatus)))
            {
                _credentialCounts[status] = 0;
            }
        }

        private void CountCredentialStatus()
        {
            ResetCredentialCounts();

            foreach (KeyValuePair<string, Credential> credentialPair in _credentials)
            {
                Credential credential = credentialPair.Value;

                _credentialCounts[credential.Status]++;
            }
        }

        private void SetCredentialStatuses()
        {
            foreach (KeyValuePair<string, Credential> credentialPair in _credentials)
            {
                Credential credential = credentialPair.Value;

                credential.SetStatus(credential.GetStatus(_vault, this));
            }
        }

        public void CalculateHealthScore(bool calculateStatuses)
        {
            if (calculateStatuses)
            {
                SetCredentialStatuses();
            }

            CountCredentialStatus();

            if (_credentials.Count > 0)
            {
                _healthScore = (int)((double)_credentialCounts[Credential.CredentialStatus.Safe] / _credentials.Count * 100);
            }
            else
            {
                _healthScore = 0;
            }
        }
    }
}
