using PashIDE.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PashIDE
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            ShowIcon = false;
            Text = "";
        }

        private void Home_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;

            LoadProjects();

        }

        public string WorkingDirectory = @"C:\Users\" + Environment.UserName + @"\Documents\Pash Projects";

        private void LoadProjects()
        {

            Projects.Clear();
            if (!Directory.Exists(WorkingDirectory)) { Directory.CreateDirectory(WorkingDirectory); return; }

            foreach (string dir in Directory.GetDirectories(WorkingDirectory))
            {
                ProjectPreload p = new ProjectPreload(dir.Replace(WorkingDirectory, "").Substring(1), dir);
                Projects.Add(p);
            }

            RenderProjects();
        }

        public List<ProjectButton> projbuttons = new List<ProjectButton>();

        public List<ProjectPreload> Projects = new List<ProjectPreload>();

        private void RenderProjects()
        {



            foreach (ProjectButton pb in projbuttons) pb.Dispose();
            L_NoProjects.Visible = Projects.Count == 0;
            L_Projects.Visible = Projects.Count != 0;
            int i = 0;
            foreach (ProjectPreload p in Projects)
            {
                ProjectButton pb = new ProjectButton();
                pb.Width = Width;
                pb.Location = new Point(0, 30 + ((5 + pb.Height) * i));
                pb.Text = p.Name;
                pb.ForeColor = Color.White;
                pb.project = p;
                pb.BringToFront();
                groupBox1.Controls.Add(pb);
                projbuttons.Add(pb);
                i++;
            }
        }

        public void OpenProject(ProjectPreload preLoadProject)
        {
            MainPanel.Visible = false;
            Loading_Title.Text = "Loading " + preLoadProject.Name;
            Loading_Title.Location = new Point((Width / 2) - (Loading_Title.Width / 2), Loading_Title.Location.Y);
            Loading_Title.Visible = true;

            Thread loadingTitleThread = new Thread(() =>
            {
                int dots = 0;
                while (true)
                {
                    Thread.Sleep(110);
                    dots++;
                    if (dots == 4) dots = 1;
                    string str_dots = "";
                    for (int i = 0; i < dots; i++) str_dots += ".";
                    try
                    {
                        Invoke(new MethodInvoker(delegate { Loading_Title.Text = "Loading " + preLoadProject.Name + str_dots; }));
                    }
                    catch (Exception e) { }
                }
            });
            loadingTitleThread.Start();

            new Thread(() =>
            {
                Settings settings = Settings.LoadIDESettings();
                Main main = new Main();
                main.settings = settings;
                Project project = new Project(preLoadProject);
                main.project = project;
                settings.LoadProjectSettings(project);
                Thread.Sleep(1000); //Until we have heavy loading, want to show off loading screen :3
                main.InitializeConsole();
                main.Show();
                loadingTitleThread.Abort();
                try {
                    Invoke(new MethodInvoker(delegate { Close(); }));
                }
                catch (Exception e) { Environment.Exit(0); }
                Application.Run(main);
            }).Start();
        }

        public bool creatednew = false;

        private void Create_Click(object sender, EventArgs e)
        {
            creatednew = false;
            CreateProjectWindow win = new CreateProjectWindow();
            win.prnt = this;
            win.ShowDialog();
            if (creatednew)
            {
                LoadProjects();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Open_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string path = dialog.SelectedPath;
                string name = Path.GetDirectoryName(path);
                ProjectPreload project = new ProjectPreload(name, path);
                OpenProject(project);
            }
        }
    }
    public class ProjectPreload
    {
        public string Name;
        public string WorkingDirectory;
        public ProjectPreload(string Name, string FileLocation)
        {
            this.Name = Name;
            this.WorkingDirectory = FileLocation;
        }
    }
}
