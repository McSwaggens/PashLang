using BASIC_Compiler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashIDE
{
    public class CodeFile
    {
        public bool Saved = true;
        public string Name;
        public string HardName;
        public string Code;
        public Language language;
        public string path;
        public CodeFile(string path)
        {
            this.path = path;
            Code = File.ReadAllText(path);
            Name = Path.GetFileName(path);
            HardName = Name;
            Name = Name.Remove(Name.Length - 2);
            string lang = Path.GetExtension(path);
            if (lang == ".p") language = Language.PASM;
            else
            if (lang == ".k") language = Language.KIS;
            else
            if (lang == ".b") language = Language.BASIC;
            else language = Language.Unknown;
            Console.WriteLine("CodeFile Language set to " + language + " for CodeFile " + Name);
        }

        public void Compile()
        {
            if (language == Language.BASIC)
            {
                if (!File.Exists(Main.inst.Explorer.WorkingDirectory + "/" + Name + ".p"))
                {
                    FileStream fs = File.Create(Main.inst.Explorer.WorkingDirectory + "/" + Name + ".p");
                    fs.Close();
                }

                List<string> tocompcode = Code.Split('\n').ToList();

                for (int i = 0; i < tocompcode.Count; i++)
                {
                    tocompcode[i] = tocompcode[i].TrimEnd('\r');
                }
                Compiler compiler = new Compiler();
                File.WriteAllLines(Main.inst.Explorer.WorkingDirectory + "/" + Name + ".p", compiler.Compile(tocompcode.ToArray()).ToArray());

                Console.WriteLine("Compilation Successfull...");
            }
            else if (language == Language.PASM)
            {
                Code = File.ReadAllText(Main.inst.Explorer.WorkingDirectory + "/" + HardName);
                List<string> fixedcode = Code.Split('\n').ToList();

                string construct = "";

                for (int i = 0; i < fixedcode.Count; i++)
                {
                    construct += fixedcode[i].TrimEnd('\r') + "\n";
                }
                Code = construct;
            }
        }

        public void Save(string Code)
        {
            Console.WriteLine("Saved " + Name);
            this.Code = Code;
            File.WriteAllText(path, Code);
            Saved = true;
        }
    }
}
