using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace vIDsafe
{
    class DarkTheme : Theme
    {
        public DarkTheme()
        {
            AddThemeColors();
            SetTags();
        }

        protected override void AddThemeColors()
        {
            ThemeColors.Add("Black", Color.Black);
            ThemeColors.Add("Gainsboro", Color.Gainsboro);
            ThemeColors.Add("White", Color.White);
            ThemeColors.Add("LightBlack", Color.FromArgb(26, 26, 26));
            ThemeColors.Add("LightGray", Color.FromArgb(204, 204, 204));
            ThemeColors.Add("DarkGray", Color.DarkGray); //Edited: Color.FromArgb(36, 36, 36
            ThemeColors.Add("NavyBlack", Color.FromArgb(29, 32, 36));
            ThemeColors.Add("NavyGray", Color.FromArgb(56, 59, 66));
            ThemeColors.Add("DarkNavyGray", Color.FromArgb(46, 49, 56));
            ThemeColors.Add("Transparent", Color.Transparent);
        }

        protected override void SetTags()
        {
            SetTagColors("MasterNamePanel", ThemeColors["White"], ThemeColors["LightBlack"]);
            SetTagColors("MANameLabel", ThemeColors["White"], ThemeColors["LightBlack"]);

            SetTagColors("NavigationPanel", ThemeColors["Black"], ThemeColors["NavyBlack"]);
            SetTagColors("SubMenuButton", ThemeColors["White"], ThemeColors["NavyGray"]);
            SetTagColors("NavButton", ThemeColors["LightGray"], ThemeColors["NavyBlack"]);
            SetTagColors("NavButton selected", ThemeColors["Black"], ThemeColors["Gainsboro"]);

            SetTagColors("BackPanel", ThemeColors["White"], ThemeColors["DarkNavyGray"]);
            SetTagColors("MainPanel", ThemeColors["White"], ThemeColors["NavyGray"]);
            SetTagColors("FrontTitleLabel", ThemeColors["DarkGray"], ThemeColors["Transparent"]);
            SetTagColors("FrontSubTitleLabel", ThemeColors["LightGray"], ThemeColors["Transparent"]);

            SetTagColors("PasswordLabel", ThemeColors["White"], ThemeColors["NavyBlack"]);

            SetTagColors("SubPanel", ThemeColors["White"], ThemeColors["NavyBlack"]);
            SetTagColors("InnerSubPanel", ThemeColors["White"], ThemeColors["NavyGray"]);
            SetTagColors("SmallSubPanel", ThemeColors["White"], ThemeColors["DarkNavyGray"]);

            SetTagColors("TitlePanel", ThemeColors["Black"], ThemeColors["DarkNavyGray"]);
            SetTagColors("TitleLabel", ThemeColors["LightGray"], ThemeColors["Transparent"]);
            SetTagColors("SubTitleLabel", ThemeColors["DarkGray"], ThemeColors["Transparent"]);
        }

        protected override Color GetForeColor(string tag)
        {
            if (ForeColors.ContainsKey(tag))
            {
                return ForeColors[tag];
            }

            return Color.White;
        }

        protected override Color GetBackColor(string tag)
        {
            if (BackColors.ContainsKey(tag))
            {
                return BackColors[tag];
            }

            return Color.Black;
        }

        public override string ToString()
        {
            return "Dark";
        }
    }
}
