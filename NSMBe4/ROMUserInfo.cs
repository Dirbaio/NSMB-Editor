using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4
{
    public class ROMUserInfo
    {
        //public string FilePath;

        public Dictionary<int, List<string>> descriptions = new Dictionary<int, List<string>>();
        public Dictionary<string, Dictionary<int, string>> lists = new Dictionary<string, Dictionary<int, string>>();

        public ROMUserInfo(string ROMPath)
        {
            //FilePath = ROMPath.Substring(0, ROMPath.LastIndexOf('.') + 1) + "txt";
            int lineNum = 0;
            try
            {
                //if (!System.IO.File.Exists(FilePath)) return;
                //System.IO.StreamReader s = new System.IO.StreamReader(FilePath);
                DSFileSystem.File file = ROM.FS.getFileByName("00DUMMY");
                string[] lines = System.Text.Encoding.ASCII.GetString(file.getContents()).Split('\n');
                List<string> curList = null;
                Dictionary<int, string> curDict = null;
                bool readDescriptions = false;
                for (lineNum = 0; lineNum < lines.Length; lineNum++)
                {
                    string line = lines[lineNum];
                    if (line != "")
                    {
                        if (line.StartsWith("["))
                        {
                            line = line.Substring(1, line.Length - 2);
                            int num;
                            if (int.TryParse(line, out num)) {
                                readDescriptions = true;
                                curList = new List<string>();
                                for (int l = 0; l < 256; l++)
                                    curList.Add("");
                                descriptions.Add(num, curList);
                            } else {
                                readDescriptions = false;
                                curDict = new Dictionary<int,string>();
                                lists.Add(line, curDict);
                            }
                        }
                        else if (curList != null || curDict != null) {
                            int num = int.Parse(line.Substring(0, line.IndexOf("=")).Trim());
                            string name = line.Substring(line.IndexOf("=") + 1).Trim();
                            if (readDescriptions) {
                                if (num < 256)
                                    curList[num] = name;
                            } else
                                curDict.Add(num, name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(String.Format(LanguageManager.Get("ROMUserInfo", "ErrorRead"), lineNum + 1, ex.Message));
            }
        }

        public List<string> getFullList(string name)
        {
            List<string> fullList = LanguageManager.GetList(name);
            List<string> newList = new List<string>();
            foreach (string i in fullList)
                newList.Add(i);
            if (lists.ContainsKey(name))
                foreach (KeyValuePair<int, string> item in lists[name])
                    if (newList.Count > item.Key)
                        newList[item.Key] = item.Value;
            if (name == "Music")
                for (int l = 0; l < ROM.MusicNumbers.Length; l++)
                    newList[l] = string.Format("{0:X2}: {1}", ROM.MusicNumbers[l], newList[l]);

            return newList;
        }

        public void setListItem(string listName, int num, string value, bool rewriteFile)
        {
            if (!lists.ContainsKey(listName))
                lists.Add(listName, new Dictionary<int, string>());
            if (lists[listName].ContainsKey(num))
                lists[listName][num] = value;
            else
                lists[listName].Add(num, value);
            if (rewriteFile)
                SaveFile();
        }

        public void removeListItem(string listName, int num, bool rewriteFile)
        {
            if (!lists.ContainsKey(listName)) return;
            lists[listName].Remove(num);
            if (lists[listName].Count == 0)
                lists.Remove(listName);
            if (rewriteFile)
                SaveFile();
        }

        public void createDescriptions(int tilesetNum)
        {
            List<string> descList = new List<string>();
            for (int l = 0; l < 256; l++)
                descList.Add("");
            if (tilesetNum == 65535) {
                List<string> defaults = LanguageManager.GetList("ObjNotes");
                for (int l = 0; l < defaults.Count; l++) {
                    int idx = defaults[l].IndexOf("=");
                    int num;
                    if (int.TryParse(defaults[l].Substring(0, idx), out num))
                        descList[num] = defaults[l].Substring(idx + 1);
                }
            }
            descriptions.Add(tilesetNum, descList);
        }

        public void SaveFile()
        {
            try
            {
                string text = string.Empty;
                //System.IO.StreamWriter s = new System.IO.StreamWriter(new System.IO.FileStream(FilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write));
                // Write lists
                foreach (KeyValuePair<string, Dictionary<int, string>> list in lists)
                {
                    text += "[" + list.Key + "]\n";
                    foreach (KeyValuePair<int, string> item in list.Value)
                        text += item.Key.ToString() + "=" + item.Value + "\n";
                }
                // Write descriptions
                foreach (KeyValuePair<int, List<string>> item in descriptions)
                {
                    int num = 0;
                    text += "[" + item.Key.ToString() + "]\n";
                    foreach (string desc in item.Value) {
                        if (desc != string.Empty)
                            text += num.ToString() + "=" + desc + "\n";
                        num++;
                    }
                }
                DSFileSystem.File file = ROM.FS.getFileByName("00DUMMY");
                file.beginEdit(this);
                file.replace(System.Text.Encoding.ASCII.GetBytes(text), this);
                file.endEdit(this);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(String.Format(LanguageManager.Get("ROMUserInfo", "ErrorWrite"), ex.Message));
            }
        }
    }
}
