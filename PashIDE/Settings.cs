using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace PashIDE
{
    public class Settings
    {
        public static Settings CurrentSettings => Main.inst.settings;

        public static Settings LoadIDESettings()
        {
            if (!File.Exists("./Settings.json"))
            {
                Settings settings = new Settings();
                JsonSerializer serializer = new JsonSerializer();
                string output = JsonConvert.SerializeObject(settings);
                StreamWriter sw = new StreamWriter("./Settings.json");
                sw.Write(output);
                sw.Close();
                return settings;
            }
            else
            {
                StreamReader reader = new StreamReader("./Settings.json");
                string raw = reader.ReadToEnd();
                reader.Close();
                return JsonConvert.DeserializeObject<Settings>(raw);
            }
        }

        public Theme theme;
        public bool showLineNumber = true;
        public bool showConsoleWarnings = true;
        public string test = "Hello World";

        public void LoadProjectSettings(Project project)
        {
            //TODO: implement JSON to project folder.
        }
    }
}
