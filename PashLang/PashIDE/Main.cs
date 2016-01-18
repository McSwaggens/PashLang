using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using System.Threading;
using PASM;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using static PashIDE.Logger;
namespace PashIDE
{
    public partial class Main : Form
    {
        public Project project;

        public CodeFile CurrentOpen;

        public static Main inst;

        public Main()
        {
            InitializeComponent();
            Code.KeyDown += Main_KeyDown;
            Code.TextChanged += Code_TextChanged;
            AllocConsole();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("DO NOT CLOSE THIS WINDOW!");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("This window will show you in depth information on what is going on in the IDE, and will also show and get output and input from your application.");
            Console.ForegroundColor = ConsoleColor.White;
            ExecutionThread = new Thread(StartInstance);
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

        protected override void OnClosing(CancelEventArgs e)
        {
        }

        FileSystemWatcher watcher;

        private void Main_Load(object sender, EventArgs e)
        {
            inst = this;
            Explorer.mninst = this;
            Explorer.WorkingDirectory = project.WorkingDirectory;
            Explorer.ScanRoot();
            Code.Text = "#PashIDE by Daniel Jones\n#Use the explorer on the right side to open a CodeFile.\n#Happy Coding!";
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

        public void CompileProject()
        {
            foreach (CodeFile cf in Explorer.CodeFiles)
            {
                cf.Compile();
            }
        }
        [DllImport("kernel32.dll")]
        internal static extern Boolean AllocConsole();
        private void StartInstance()
        {
            isRunningCode = true;
            Engine engine = new Engine();
            engine.Load(Explorer.CodeFiles[0].Code.Split('\n'));
            engine.setMemory(1024);
            engine.ReferenceLibrary(typeof(Standard));
            engine.Execute();
            isRunningCode = false;
            onDebugStopped();
        }

        public void StopInstance()
        {
            ExecutionThread.Abort();
            isRunningCode = false;
            onDebugStopped();
        }

        public void onDebugStopped()
        {
            Invoke(new MethodInvoker(delegate { bar1.startButton.Refresh(); }));
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
                case Language.BASIC:
                    Code.Language = FastColoredTextBoxNS.Language.CrocScript;
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
