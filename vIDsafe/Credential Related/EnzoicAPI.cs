using EnzoicClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vIDsafe
{
    static class EnzoicAPI
    {
        //https://www.enzoic.com/docs-dotnet-quick-start/

        //Include API here
        private static readonly Enzoic _enzoic = new Enzoic("API", "Secret");

        /// <summary>
        /// Checks if a password is compromised
        /// </summary>
        /// <returns>
        /// True if compromised, false if not
        /// </returns>
        public static bool CheckPassword(string password)
        {
            // Check whether a password has been compromised
            if (_enzoic.CheckPassword(password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if a username and password together is compromised
        /// </summary>
        /// <returns>
        /// True if compromised, false if not
        /// </returns>
        public static bool CheckCredential(string email, string password)
        {
            // Check whether a specific set of credentials are compromised
            if (_enzoic.CheckCredentials(email, password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the exposure details for an email
        /// </summary>
        /// <returns>
        /// The list of exposures with their details
        /// </returns>
        public static List<ExposureDetails> GetExposureDetails (string email)
        {
            // get all exposures for a given user

            List<ExposureDetails> exposureDetails = new List<ExposureDetails>();

            try
            {
                ExposuresResponse exposures = _enzoic.GetExposuresForUser(email);

                foreach (string exposure in exposures.Exposures)
                {
                    exposureDetails.Add(_enzoic.GetExposureDetails(exposure));
                }
            }
            catch (System.Net.WebException e)
            {
                Console.WriteLine("API/Secret may be incorrect, error: " + e);
            }

            return exposureDetails;
        }
    }
}
