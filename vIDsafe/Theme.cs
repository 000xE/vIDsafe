using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vIDsafe
{
    public abstract class Theme
    {
        protected Dictionary<string, Color> BackColors;
        protected Dictionary<string, Color> ForeColors;

        protected Dictionary<string, Color> ThemeColors;

        public Theme ()
        {
            BackColors = new Dictionary<string, Color>();
            ForeColors = new Dictionary<string, Color>();

            ThemeColors = new Dictionary<string, Color>();
        }

        protected abstract void AddThemeColors();
        protected abstract void SetTags();

        protected abstract Color GetForeColor(string tag);

        protected abstract Color GetBackColor(string tag);

        public void SetControlColors(Control control)
        {
            if (control.Tag != null)
            {
                if (control.Tag.ToString().Length > 0)
                {
                    control.ForeColor = GetForeColor(control.Tag.ToString());
                    control.BackColor = GetBackColor(control.Tag.ToString());
                }
            }
        }
        protected void SetTagColors(string tag, Color foreColor, Color backColor)
        {
            if (ForeColors.ContainsKey(tag))
            {
                ForeColors[tag] = foreColor;
            }
            else
            {
                ForeColors.Add(tag, foreColor);
            }

            if (BackColors.ContainsKey(tag))
            {
                BackColors[tag] = backColor;
            }
            else
            {
                BackColors.Add(tag, backColor);
            }
        }
    }
}
