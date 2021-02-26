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
        public Dictionary<Status.CredentialStatus, int> CredentialCounts { get; private set; } = new Dictionary<Status.CredentialStatus, int>()
        {
            [Status.CredentialStatus.Safe] = 0,
            [Status.CredentialStatus.Compromised] = 0,
            [Status.CredentialStatus.Conflicted] = 0,
            [Status.CredentialStatus.Weak] = 0
        };

        ///<value>Get or set the health score</value>
        [Ignore]
        [JsonIgnore]
        public int HealthScore { get; protected set; }

        ///<value>Gets the safe credential count</value>
        [Ignore]
        [JsonIgnore]
        public int SafeCredentialCount => CredentialCounts[Status.CredentialStatus.Safe];

        ///<value>Gets the compromised credential count</value>
        [Ignore]
        [JsonIgnore]
        public int CompromisedCredentialCount => CredentialCounts[Status.CredentialStatus.Compromised];

        ///<value>Gets the conflicted credential count</value>
        [Ignore]
        [JsonIgnore]
        public int ConflictCredentialCount => CredentialCounts[Status.CredentialStatus.Conflicted];

        ///<value>Gets the weak credential count</value>
        [Ignore]
        [JsonIgnore]
        public int WeakCredentialCount => CredentialCounts[Status.CredentialStatus.Weak];

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