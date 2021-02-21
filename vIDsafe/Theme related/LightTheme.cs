using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace vIDsafe
{
    class LightTheme : Theme
    {
        public LightTheme()
        {
            AddThemeColors();
            SetTags();
        }

        protected override void AddThemeColors()
        {
            ThemeColors.Add("Black", Color.Black);
            ThemeColors.Add("Gainsboro", Color.Gainsboro);
            ThemeColors.Add("White", Color.White);
            ThemeColors.Add("WhiteSmoke", Color.WhiteSmoke);
            ThemeColors.Add("LightBlack", Color.FromArgb(26, 26, 26));
            ThemeColors.Add("LightGray", Color.FromArgb(204, 204, 204));
            ThemeColors.Add("DarkGray", Color.DarkGray); //Edited: Color.FromArgb(36, 36, 36
            ThemeColors.Add("DimGray", Color.DimGray); //Edited: Color.FromArgb(36, 36, 36
            ThemeColors.Add("Gray", Color.Gray);
            ThemeColors.Add("Silver", Color.Silver);
            ThemeColors.Add("Transparent", Color.Transparent);
        }

        protected override void SetTags()
        {
            SetTagColors("MasterNamePanel", ThemeColors["Black"], ThemeColors["WhiteSmoke"]);
            SetTagColors("MANameLabel", ThemeColors["Black"], ThemeColors["WhiteSmoke"]);

            SetTagColors("NavigationPanel", ThemeColors["Black"], ThemeColors["LightGray"]);
            SetTagColors("SubMenuButton", ThemeColors["Black"], ThemeColors["Gainsboro"]);
            SetTagColors("NavButton", ThemeColors["Black"], ThemeColors["LightGray"]);
            SetTagColors("NavButton selected", ThemeColors["LightGray"], ThemeColors["Gray"]);

            SetTagColors("BackPanel", ThemeColors["Black"], ThemeColors["Gainsboro"]);
            SetTagColors("MainPanel", ThemeColors["Black"], ThemeColors["LightGray"]);
            SetTagColors("FrontTitleLabel", ThemeColors["DimGray"], ThemeColors["Transparent"]);
            SetTagColors("FrontSubTitleLabel", ThemeColors["LightGray"], ThemeColors["Transparent"]);

            SetTagColors("SubPanel", ThemeColors["White"], ThemeColors["Gray"]);
            SetTagColors("InnerSubPanel", ThemeColors["DimGray"], ThemeColors["Silver"]);
            SetTagColors("SmallSubPanel", ThemeColors["Silver"], ThemeColors["DimGray"]);

            SetTagColors("TitlePanel", ThemeColors["Black"], ThemeColors["Gainsboro"]);
            SetTagColors("TitleLabel", ThemeColors["Black"], ThemeColors["Transparent"]);
            SetTagColors("SubTitleLabel", ThemeColors["LightBlack"], ThemeColors["Transparent"]);
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
            return "Light";
        }
    }
}
