using System.Windows.Forms;

namespace vIDsafe
{
    public class NotificationManager
    {
        public static void ShowError(string title, string error)
        {
            FormvIDsafe.ShowNotification(ToolTipIcon.Error, title, error);
        }

        public static void ShowInfo(string title, string error)
        {
            FormvIDsafe.ShowNotification(ToolTipIcon.Info, title, error);
        }

        public static void ShowWarning(string title, string error)
        {
            FormvIDsafe.ShowNotification(ToolTipIcon.Warning, title, error);
        }
    }
}