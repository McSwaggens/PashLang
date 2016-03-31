using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using System.Threading;
using PASM;
using System.Runtime.InteropServices;
using System.IO;
using static PashIDE.Logger;
namespace PashIDE
{
    public partial class Main : Form
    {
        public Project project;

        public CodeFile CurrentOpen;

        public static Main inst;

        public SettingsWindow settingsWindow;

        public Settings settings;

        public Main()
        {
            InitializeComponent();
            Code.KeyDown += Main_KeyDown;
            Code.TextChanged += Code_TextChanged;
            ExecutionThread = new Thread(StartInstance);
        }

        public void InitializeConsole()
        {
            AllocConsole();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("DO NOT CLOSE THIS WINDOW!");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("This window will show you in depth information on what is going on in the IDE, and will also show and get output and input from your application.");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void Code_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ChangeExpected)
            {
                ChangeExpected = false;
            }
            else if (CE2)
            {
                CE2 = false;
            }
            else
            if (CurrentOpen != null)
            {
                CurrentOpen.Saved = false;
                Explorer.Refresh();
            }
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.S)
            {
                if (CurrentOpen != null && !CurrentOpen.Saved)
                {
                    CurrentOpen.Save(Code.Text);
                    Explorer.Refresh();
                }
            }
        }

        public FileSystemWatcher watcher;

        private void Main_Load(object sender, EventArgs e)
        {
            inst = this;
            Explorer.mninst = this;
            Explorer.WorkingDirectory = project.WorkingDirectory;
            Explorer.ScanRoot();
            Code.SyntaxHighlighter.StaticClasses = "Standard|Threading|GL";
            Code.SyntaxHighlighter.InitPASMRegex();
            Code.Text = "#PashIDE by Daniel Jones\n#Use the explorer on the right side to open a CodeFile.\n#Use the controls above to compile and run your application\n#Happy Coding!";
            Code.ReadOnly = true;
            watcher = new FileSystemWatcher(project.WorkingDirectory);
            watcher.Created += ProjectDirectoryChanged;
            watcher.Renamed += ProjectDirectoryChanged;
            watcher.Deleted += ProjectDirectoryChanged;
            watcher.EnableRaisingEvents = true;
            //if (Explorer.CodeFiles.Count != 0) OpenCodeFile(Explorer.CodeFiles[0]);
        }

        private void ProjectDirectoryChanged(object sender, FileSystemEventArgs e)
        {
            Log("Project Working Directory has changed... Reloading...");
            Explorer.Invoke(new MethodInvoker(delegate
            {
                Explorer.ScanRoot();
            }));
        }

        private Thread ExecutionThread;

        public bool isRunningCode = false;

        public void StartProject()
        {
            ExecutionThread = new Thread(() =>
            {
                CompileProject();
                StartInstance();
            });
            ExecutionThread.Start();
        }

        public bool inCompileTime = false;

        CodeFile getStartupCodeFile() => Explorer.CodeFiles.Find(codeFile => codeFile.HardName == "main.p");

        public void CompileProject()
        {
            inCompileTime = true;
            List<CodeFile> codeFiles = new List<CodeFile>(Explorer.CodeFiles);
            foreach (CodeFile cf in codeFiles) cf.rep.Invoke(new MethodInvoker(delegate { cf.rep.Refresh(); }));
            foreach (CodeFile cf in codeFiles)
            {
                //if (cf.language == Language.SnapScript && !settings.compileSnapScriptFiles) continue;
                cf.Compile();
            }
            inCompileTime = false;
            foreach (CodeFile cf in codeFiles) { cf.compileStatus = CodeFile.CompileTimeStatus.None; try { cf.rep.Invoke(new MethodInvoker(delegate { cf.rep.Refresh(); })); } catch (Exception e) { } }
            Thread.Sleep(50);
        }

        protected override void OnShown(EventArgs e)
        {
            
        }

        [DllImport("kernel32.dll")]
        internal static extern Boolean AllocConsole();

        private void StartInstance()
        {
            CodeFile mainCodeFile = getStartupCodeFile();
            Log("Starting debugger on CodeFile: " + mainCodeFile.HardName);
            isRunningCode = true;
            Engine engine = null;
            try {
                engine = new Engine();
                string[] code = mainCodeFile.Code.Split('\n');
                for (int i = 0; i < code.Length; i++) code[i] = code[i].TrimEnd('\r');
                engine.Load(code);
                Log("Creating PASM engine with all stdlibs");
                engine.setMemory(1024);
                engine.ReferenceLibrary(typeof(stdlib.Standard), typeof(stdlib.Threading));
            }
            catch (PException e)
            {
                Error($"PASM Error initializing the engine: {e.Message}");
            }
            catch (Exception e)
            {
                Error($"There was an unknown error initializing the PASM Engine, more details is as follows; ERROR={e.ToString()} MESSAGE={e.Message} SOURCE={e.Source}");
            }
            finally {
                isRunningCode = false;
                onDebugStopped();
            }
            try {
                engine.Execute();
            }
            catch (PException e)
            {
                Error($"PASM Error at line {engine.CurrentLine}: {e.Message}");
            }
            catch (Exception e)
            {
                Error($"There was an unknown error initializing the PASM Engine, more details is as follows; ERROR={e.ToString()} MESSAGE={e.Message} SOURCE={e.Source}");
            }
            finally
            {
                isRunningCode = false;
                onDebugStopped();
            }
        }

        public void StopInstance()
        {
            ExecutionThread.Abort();
            isRunningCode = false;
            onDebugStopped();
        }

        public void onDebugStopped()
        {
            try
            {
                Invoke(new MethodInvoker(delegate { bar1.startButton.Refresh(); }));
            }
            catch (Exception e) { }
        }

        public bool ChangeExpected, CE2 = false;

        public void OpenCodeFile(CodeFile cf)
        {
            Code.ReadOnly = false;
            if (CurrentOpen != null)
            {
                CurrentOpen.Code = Code.Text;
            }

            switch (cf.language)
            {
                case Language.PUFFIN:
                    Code.Language = FastColoredTextBoxNS.Language.Puffin;
                    break;
                case Language.PASM:
                    Code.Language = FastColoredTextBoxNS.Language.PASM;
                    break;
                default:
                    Code.Language = FastColoredTextBoxNS.Language.Custom;
                    break;
            }

            ChangeExpected = true;
            CE2 = true;
            CurrentOpen = cf;
            Code.Text = CurrentOpen.Code;

            
        }
    }
}
