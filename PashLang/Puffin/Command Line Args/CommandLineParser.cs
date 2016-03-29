using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puffin.Command_Line_Args
{
    public class CommandLineParser
    {
        private string[] args;
        private string inputFile;
        private int strictness = 4;
        private string outputFile = "";
        public CommandLineParser(string[] args)
        {
            this.args = args;
        }

        public bool Start()
        {
            if (args.Length < 1)
            {
                Logger.WriteError("No input file");
                return false;
            }
            for (int index = 0; index < args.Length; index++)
            {
                if (index == 0)
                    inputFile = args[index];
                else if (args[index].StartsWith("strict="))
                    if (!int.TryParse(args[index].Substring(7, args[index].Length - 7), out strictness))
                    {
                        Logger.WriteError("Strictness must be a number");
                        Logger.WriteError(args[index].Substring(6, args[index].Length - 7));
                        return false;
                    }
                    else if (index == args.Length - 1)
                    outputFile = args[index];
                Logger.Write(args[index]);
            }
            if (outputFile.Equals(""))
                outputFile = inputFile.Replace(".puff", ".p");
            return true;
        }

        public string InputFile
        {
            get { return inputFile; }
        }

        public int Strictness
        {
            get { return strictness; }
        }

        public string OutputFile
        {
            get { return outputFile; }
        }
    }
}
