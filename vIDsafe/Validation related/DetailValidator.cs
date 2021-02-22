namespace vIDsafe
{
    public class DetailValidator : NotificationManager
    {
        /// <summary>
        /// Checks if the name is valid
        /// </summary>
        /// <returns>
        /// True if valid, false if not
        /// </returns>
        protected static bool ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                ShowError("Validation error", "Please enter a name");
                return false;
            }
            else
            {
                if (name.Length < 8)
                {
                    ShowError("Validation error", "Name is lower than 8 characters");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if the password is valid
        /// </summary>
        /// <returns>
        /// True if valid, false if not
        /// </returns>
        protected static bool ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                ShowError("Validation error", "Please enter a password");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if the username is valid
        /// </summary>
        /// <returns>
        /// True if valid, false if not
        /// </returns>
        protected static bool ValidateUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                ShowError("Validation error", "Please enter a username");
                return false;
            }

            return true;
        }
    }
}