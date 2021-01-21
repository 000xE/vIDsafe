﻿using EnzoicClient;
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

        private static Enzoic _enzoic = new Enzoic("API", "Secret");

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
            ExposuresResponse exposures = _enzoic.GetExposuresForUser(email);

            List<ExposureDetails> exposureDetails = new List<ExposureDetails>();

            foreach (string exposure in exposures.Exposures)
            {
                exposureDetails.Add(_enzoic.GetExposureDetails(exposure));
            }

            return exposureDetails;
        }
    }
}
