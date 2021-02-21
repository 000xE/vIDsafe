using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace vIDsafe
{
    [Serializable]
    public abstract class Health
    {
        [Ignore]
        [JsonIgnore]
        public Dictionary<Credential.CredentialStatus, int> CredentialCounts { get; private set; } = new Dictionary<Credential.CredentialStatus, int>()
        {
            [Credential.CredentialStatus.Safe] = 0,
            [Credential.CredentialStatus.Compromised] = 0,
            [Credential.CredentialStatus.Conflicted] = 0,
            [Credential.CredentialStatus.Weak] = 0
        };

        ///<value>Get or set the health score</value>
        [JsonIgnore]
        public int HealthScore { get; protected set; } = 0;

        ///<value>Gets the safe credential count</value>
        [JsonIgnore]
        public int SafeCredentialCount => CredentialCounts[Credential.CredentialStatus.Safe];

        ///<value>Gets the compromised credential count</value>
        [JsonIgnore]
        public int CompromisedCredentialCount => CredentialCounts[Credential.CredentialStatus.Compromised];

        ///<value>Gets the conflicted credential count</value>
        [JsonIgnore]
        public int ConflictCredentialCount => CredentialCounts[Credential.CredentialStatus.Conflicted];

        ///<value>Gets the weak credential count</value>
        [JsonIgnore]
        public int WeakCredentialCount => CredentialCounts[Credential.CredentialStatus.Weak];

        /// <summary>
        /// Resets the total credential status counts
        /// </summary>
        protected abstract void ResetCredentialCounts();

        /// <summary>
        /// Counts the total credential statuses
        /// </summary>
        protected abstract void CountCredentialStatus(bool calculateStatuses);

        /// <summary>
        /// Calculates the health score
        /// </summary>
        public abstract void CalculateHealthScore(bool calculateStatuses);
    }
}