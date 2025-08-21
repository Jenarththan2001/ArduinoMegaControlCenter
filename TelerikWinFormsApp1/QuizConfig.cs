// QuizConfig.cs
using Newtonsoft.Json; // Install-Package Newtonsoft.Json
using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace TelerikWinFormsApp1
{
    public static class QuizConfig
    {
        // ====== Defaults ======
        public static int QuestionDurationSec { get; set; } = 30;  // time per question
        public static int NumSchools { get; set; } = 5;            // 2..5
        public static int NumOptions { get; set; } = 5;            // 3..5
        public static string[] SchoolNames { get; private set; } =
            new[] { "School1", "School2", "School3", "School4", "School5" };

        // ====== Persistence ======
        private class PersistModel
        {
            public int QuestionDurationSec { get; set; }
            public int NumSchools { get; set; }
            public int NumOptions { get; set; }
            public string[] SchoolNames { get; set; }
        }

        // %AppData%\TelerikWinFormsApp1\quizsettings.json
        private static string ConfigDir =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TelerikWinFormsApp1");
        private static string ConfigPath => Path.Combine(ConfigDir, "quizsettings.json");

        public static void Load()
        {
            try
            {
                if (!File.Exists(ConfigPath))
                {
                    EnsureDir();
                    Save(); // write defaults first time
                    return;
                }

                var json = File.ReadAllText(ConfigPath);
                var data = JsonConvert.DeserializeObject<PersistModel>(json);

                if (data == null) return;

                QuestionDurationSec = Math.Max(0, data.QuestionDurationSec);
                NumSchools = Clamp(data.NumSchools, 2, 5);
                NumOptions = Clamp(data.NumOptions, 3, 5);

                // normalize school names length = 5
                var names = data.SchoolNames ?? Array.Empty<string>();
                SetSchoolNames(names);
            }
            catch
            {
                // swallow errors: stick with defaults if something goes wrong
            }
        }

        public static void Save()
        {
            try
            {
                EnsureDir();
                var data = new PersistModel
                {
                    QuestionDurationSec = QuestionDurationSec,
                    NumSchools = Clamp(NumSchools, 2, 5),
                    NumOptions = Clamp(NumOptions, 3, 5),
                    SchoolNames = NormalizeNames(SchoolNames)
                };
                var json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(ConfigPath, json);
            }
            catch
            {
                // ignore write errors to avoid crashing UI
            }
        }

        public static void SetSchoolNames(string[] names)
        {
            SchoolNames = NormalizeNames(names);
        }

        public static char[] GetOptionLetters()
        {
            // Returns ['A','B','C']..['A'..'E'] based on NumOptions
            int n = Clamp(NumOptions, 3, 5);
            return Enumerable.Range(0, n).Select(i => (char)('A' + i)).ToArray();
        }

        private static string[] NormalizeNames(string[] names)
        {
            var arr = new string[5];
            for (int i = 0; i < 5; i++)
            {
                string fallback = $"School{i + 1}";
                arr[i] = (names != null && i < names.Length && !string.IsNullOrWhiteSpace(names[i]))
                    ? names[i].Trim()
                    : fallback;
            }
            return arr;
        }

        private static int Clamp(int v, int lo, int hi) => Math.Max(lo, Math.Min(hi, v));

        private static void EnsureDir()
        {
            if (!Directory.Exists(ConfigDir)) Directory.CreateDirectory(ConfigDir);
        }
    }
}
