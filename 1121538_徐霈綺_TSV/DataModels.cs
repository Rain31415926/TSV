using System;
using System.Collections.ObjectModel;
using System.IO;

namespace _1121538_徐霈綺_TSV
{
    public class WordItem
    {
        public string Word { get; set; }
        public string Phonogram { get; set; }
        public string SoundPath { get; set; }
        public string Explain { get; set; }

        public WordItem(string str)
        {
            if (string.IsNullOrEmpty(str)) return;

            string[] parts = str.Split('\t');
            if (parts.Length > 0) Word = parts[0]?.Trim();
            if (parts.Length > 1) Phonogram = parts[1]?.Trim();
            if (parts.Length > 2) SoundPath = parts[2]?.Trim();

            if (parts.Length > 3)
            {
                var explainParts = new string[parts.Length - 3];
                Array.Copy(parts, 3, explainParts, 0, parts.Length - 3);
                // 為了避免多餘的空白行，我們可以使用過濾
                var filteredExplains = System.Linq.Enumerable.Where(explainParts, p => !string.IsNullOrWhiteSpace(p));
                Explain = string.Join(Environment.NewLine, filteredExplains);
                if (Explain != null)
                {
                    Explain = Explain.Replace("\\n", Environment.NewLine).Replace("\\r", "");
                }
            }
        }

        public override string ToString()
        {
            return Word;
        }

        public string ToLineString()
        {
            string strExplain = string.IsNullOrEmpty(Explain) ? "" : Explain.Replace(Environment.NewLine, "\t").Replace("\n", "\t").Replace("\r", "");
            return $"{Word}\t{Phonogram}\t{SoundPath}\t{strExplain}";
        }
    }

    public class WordCollection : Collection<WordItem>
    {
        public void LoadFromStringArray(string[] lines)
        {
            this.ClearItems();
            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    this.Add(new WordItem(line));
                }
            }
        }

        public void SaveToFile(string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
            {
                foreach (var item in this.Items)
                {
                    sw.WriteLine(item.ToLineString());
                }
            }
        }
    }
}