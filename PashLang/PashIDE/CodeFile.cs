using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CrocodileScript;
using PASM;
using static PashIDE.Logger;
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
            if (lang == ".b") language = Language.PASM;
            else language = Language.Unknown;
            Log("CodeFile Language set to " + language + " for CodeFile " + Name);
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
                CrocCompiler compiler = new CrocCompiler(tocompcode.ToArray());
                CrocResult CompiledResult = compiler.Compile();
                File.WriteAllLines(Main.inst.Explorer.WorkingDirectory + "/" + Name + ".p", CompiledResult.PASM);
                //TODO: Write out the header file.

                Log("Compilation Successfull...");
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
            Log("Saved " + Name);
            this.Code = Code;
            File.WriteAllText(path, Code);
            Saved = true;
        }

        public void Backup()
        {
            
        }

        public void Delete()
        {
            Log("Deleted CodeFile at " + path);
            File.Delete(path);
        }
    }
}
