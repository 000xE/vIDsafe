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
        protected Dictionary<string, Color> BackColors = new Dictionary<string, Color>();
        protected Dictionary<string, Color> ForeColors = new Dictionary<string, Color>();

        protected Dictionary<string, Color> ThemeColors = new Dictionary<string, Color>();

        private readonly object _lock = new object();

        /// <summary>
        /// Assigns colours to colour names to be used
        /// </summary>
        protected abstract void AddThemeColors();

        /// <summary>
        /// Assigns foreground and background colours to tags
        /// </summary>
        protected abstract void SetTags();

        /// <summary>
        /// Gets an assigned foreground colour for a tag
        /// </summary>        
        /// <returns>
        /// The foreground colour
        /// </returns>
        protected abstract Color GetForeColor(string tag);

        /// <summary>
        /// Gets an assigned background colour for a tag
        /// </summary>        
        /// <returns>
        /// The background colour
        /// </returns>
        protected abstract Color GetBackColor(string tag);

        /// <summary>
        /// Set the foreground and background colour of a control based on a tag
        /// </summary>
        public void SetControlColors(Control control)
        {
            lock (_lock)
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
        }

        /// <summary>
        /// Sets the foreground and background colour of a tag if it exists
        /// </summary>
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
