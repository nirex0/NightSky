using System;
using System.Collections.Generic;
using System.Text;

namespace NightSkyPlayer
{
    public class PlayList
    {
        public string FileName { get; set; }
        public SortedDictionary<string, string> Values;
        public List<string> Keys;

        // ctor
        public PlayList(string FileName = "default")
        {
            Values = new SortedDictionary<string, string>();
            Keys = new List<string>();
            this.FileName = FileName;
        }

        // names = Key
        // paths = Value
        public void Add(string[] names, string[] paths)
        {
            int maxIndex = Math.Min(names.Length, paths.Length);

            for (int i = 0; i < maxIndex; i++)
            {
                Values.Add(names[i], paths[i]);
            }

            foreach (var item in Values)
            {
                Keys.Add(item.Key);
            }
        }

        // names = Key
        // paths = Value
        public void Remove(string Key)
        {
            Values.Remove(Key);
            Keys.Remove(Key);
            
        }

        // Save to file
        public void Save()
        {
            string toSave = string.Empty;

            foreach (var item in Values)
            {
                toSave += item.Key + "[SEP]" + item.Value + "\n";
            }

            System.IO.File.WriteAllBytes("Playlists\\" + FileName + ".NSP", Encoding.ASCII.GetBytes(toSave));
        }

        // Load from file
        public void Load()
        {
            if (!System.IO.File.Exists("Playlists\\" + FileName + ".NSP"))
            {
                return;
            }
            string toResolve = Encoding.ASCII.GetString(System.IO.File.ReadAllBytes("Playlists\\" + FileName + ".NSP"));
            Values = new SortedDictionary<string, string>();

            string[] splitStr = new string[1];
            splitStr[0] = "\n";

            string[] blocks = toResolve.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
            splitStr[0] = "[SEP]";

            string[] holder = new string[2];
            foreach (string item in blocks)
            {
                holder = item.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
                Values.Add(holder[0], holder[1]);
            }

            foreach (var item in Values)
            {
                Keys.Add(item.Key);
            }
        }

    }
}
