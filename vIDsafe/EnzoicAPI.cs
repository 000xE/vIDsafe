using EnzoicClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vIDsafe
{
    class EnzoicAPI
    {
        //https://www.enzoic.com/docs-dotnet-quick-start/

        private static readonly Enzoic _enzoic = new Enzoic("API", "Secret");

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
