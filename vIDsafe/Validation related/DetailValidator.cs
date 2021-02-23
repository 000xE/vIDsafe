namespace vIDsafe
{
    public class DetailValidator : NotificationManager
    {
        /// <summary>
        /// Checks if the value is empty
        /// </summary>
        /// <returns>
        /// True if valid, false if not
        /// </returns>
        protected static bool isEmpty(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            return true;
        }
    }
}