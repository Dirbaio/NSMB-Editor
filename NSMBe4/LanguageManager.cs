using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4 {
    public static class LanguageManager {
        private static Dictionary<string, Dictionary<string, string>> Contents;

        public static void Load(string[] LangFile) {
            Contents = new Dictionary<string, Dictionary<string, string>>();

            Dictionary<string, string> CurrentSection = null;
            foreach (string Line in LangFile) {
                if (Line == "") continue;
                if (Line[0] == ';') continue;
                string CheckLine = Line.Trim();

                if (CheckLine.StartsWith("[") && CheckLine.EndsWith("]")) {
                    string SectionName = CheckLine.Substring(1, CheckLine.Length - 2);

                    if (Contents.ContainsKey(SectionName)) {
                        CurrentSection = Contents[SectionName];
                    } else {
                        CurrentSection = new Dictionary<string, string>();
                        Contents[SectionName] = CurrentSection;
                    }

                } else if (CheckLine.Contains("=")) {
                    int EqPos = CheckLine.IndexOf('=');
                    CurrentSection[CheckLine.Substring(0, EqPos)] = CheckLine.Substring(EqPos + 1).Replace("\\n", "\n");
                }
            }
        }

        public static string Get(string Area, string Key) {
            if (Contents.ContainsKey(Area)) {
                Dictionary<string, string> Referred = Contents[Area];
                if (Referred.ContainsKey(Key)) {
                    return Referred[Key];
                }
            }

            return string.Format("<ERROR {0}:{1}>", Area, Key);
        }

        public static void ApplyToContainer(System.Windows.Forms.Control Container, string Area) {
            if (Contents.ContainsKey(Area)) {
                Dictionary<string, string> Referred = Contents[Area];

                foreach (System.Windows.Forms.Control Control in Container.Controls) {
                    if (Referred.ContainsKey(Control.Name)) {
                        Control.Text = Referred[Control.Name];
                    }
                }

                if (Referred.ContainsKey("_TITLE")) {
                    Container.Text = Referred["_TITLE"];
                }

            } else {
                System.Windows.Forms.MessageBox.Show(
                    Area + " was missing from the language file.\nThe editor cannot continue.",
                    "NSMB Editor 4",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }
    }
}
