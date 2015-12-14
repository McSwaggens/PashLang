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
        
        public Thread ConsoleThread;

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
            Console.WriteLine("Project Working Directory has changed... Reloading...");
            Explorer.Invoke(new MethodInvoker(delegate
            {
                Explorer.ScanRoot();
            }));
        }

        public bool isRunningCode = false;

        public void StartProject()
        {
            CompileProject();
            StartInstance();
        }

        public void CompileProject()
        {
            foreach (CodeFile cf in Explorer.CodeFiles)
            {
                cf.Compile();
            }
        }

        private void StartInstance()
        {
            isRunningCode = true;

            Standard.inst = new DebugConsole();
            ConsoleThread = new Thread(() =>
            {
                Application.Run(Standard.inst);
            });
            ConsoleThread.Start();
        }

        public void StopInstance()
        {
            Standard.inst.Invoke(new MethodInvoker(delegate { Standard.inst.Close(); }));
        }

        public class DebugConsole : Form
        {
            public Thread DebugThread;
            public RichTextBox rtb = new RichTextBox();
            public DebugConsole()
            {
                Width = 800;
                Height = 400;
                rtb.Location = new Point(0, 0);
                rtb.Size = new Size(Width, Height);
                rtb.ReadOnly = true;
                rtb.BackColor = Color.Black;
                rtb.ForeColor = Color.White;
                rtb.Font = new Font("Consolas", 14f);
                Controls.Add(rtb);

                DebugThread = new Thread(() =>
                {
                    Engine engine = new Engine();

                    string[] code = Main.inst.Explorer.CodeFiles[1].Code.Split('\n');
                    List<string> g = new List<string>();
                    foreach (string f in code) if (f != "\r") g.Add(f);

                    engine.Load(g.ToArray());
                    engine.ReferenceLibrary(typeof(Standard));
                    engine.Execute();
                    inst.isRunningCode = false;
                    DebugThread.Abort();
                    Close();
                });
            }

            public CodeFile getBootCF()
            {
                foreach (CodeFile cf in inst.Explorer.CodeFiles)
                {
                    if (cf.Name == "boot.p") return cf;
                }
                return null;
            }

            protected override void OnLoad(EventArgs e)
            {
                DebugThread.Start();
            }

            protected override void OnClosing(CancelEventArgs e)
            {
                inst.isRunningCode = false;
                Main.inst.Invoke(new MethodInvoker(delegate { inst.bar1.startButton.Refresh(); }));
                Main.inst.isRunningCode = false;
                DebugThread.Abort();
            }

            protected override void OnSizeChanged(EventArgs e)
            {
                rtb.Size = new Size(Width, Height);
            }
        }

        public void onDebugStopped()
        {
            
            bar1.startButton.Refresh();
            Standard.inst = null;
        }

        public class Standard
        {
            public static DebugConsole inst;
            public static void println(object c)
            {
                inst.Invoke(new MethodInvoker(delegate { inst.rtb.Text += c + "\n"; }));
            }
            public static string GetString()
            {
                return "Ayy Lmao";
            }
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
                    Code.Language = FastColoredTextBoxNS.Language.PashBASIC;
                    break;
                case Language.PASM:
                    Code.Language = FastColoredTextBoxNS.Language.PashASM;
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
