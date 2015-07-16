using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Windows.Forms;

namespace ScrumBoard.Common
{
    class Config
    {
        public const int TODO_SPACING = 25;

        public static DirectoryInfo StoragePath
        {
            get
            {
                Object v = Registry.GetValue("HKEY_CURRENT_USER\\Scrumboard", "StoragePath", null);
                if (v == null)
                    v = "C:\\";
                return new DirectoryInfo(v.ToString());
            }
            set
            {
                Registry.SetValue("HKEY_CURRENT_USER\\Scrumboard", "StoragePath", value.FullName);
            }
        }

        public static bool ViewOnly
        {
            get
            {
                Object v = Registry.GetValue("HKEY_CURRENT_USER\\Scrumboard", "ViewOnly", null);
                if (v == null)
                    v = "true";
                return Boolean.Parse(v.ToString());
            }
            set
            {
                Registry.SetValue("HKEY_CURRENT_USER\\Scrumboard", "ViewOnly", value);
            }
        }
        public static bool AutoEditDetails
        {
            get
            {
                Object v = Registry.GetValue("HKEY_CURRENT_USER\\Scrumboard", "AutoEditDetails", null);
                if (v == null)
                    v = "true";
                return Boolean.Parse(v.ToString());
            }
            set
            {
                Registry.SetValue("HKEY_CURRENT_USER\\Scrumboard", "AutoEditDetails", value);
            }
        }
        private static int activeSprint = -1;
        public static int ActiveSprint
        {
            get
            {
                if (activeSprint == -1)
                {
                    Object v = Registry.GetValue("HKEY_CURRENT_USER\\Scrumboard", "ActiveSprint", null);
                    if (v == null)
                        v = -1;
                    activeSprint = Int32.Parse(v.ToString());
                }
                return activeSprint;
            }
            set
            {
                activeSprint = value;
                Registry.SetValue("HKEY_CURRENT_USER\\Scrumboard", "ActiveSprint", value);
            }
        }
        public static int MyTeam
        {
            get
            {
                Object v = Registry.GetValue("HKEY_CURRENT_USER\\Scrumboard", "MyTeam", null);
                if (v == null)
                    v = -1;
                return Int32.Parse(v.ToString());
            }
            set
            {
                Object v = Registry.GetValue("HKEY_CURRENT_USER\\Scrumboard", "MyTeam", null);
                if (v == null)
                    v = -1;
                int cur = Int32.Parse(v.ToString());
                if (value != cur)
                {
                    Registry.SetValue("HKEY_CURRENT_USER\\Scrumboard", "MyTeam", value);
                    ActiveSprint = -1;
                }
            }
        }
        public static int DefaultEstimate
        {
            get
            {
                Object v = Registry.GetValue("HKEY_CURRENT_USER\\Scrumboard", "DefaultEstimate", null);
                if (v == null)
                    v = 8;
                return Int32.Parse(v.ToString());
            }
            set
            {
                Registry.SetValue("HKEY_CURRENT_USER\\Scrumboard", "DefaultEstimate", value);
            }
        }

        public static int DefaultBackColor
        {
            get
            {
                Object v = Registry.GetValue("HKEY_CURRENT_USER\\Scrumboard", "DefaultBackColor", null);
                if (v == null)
                    v = -128;
                return Int32.Parse(v.ToString());
            }
            set
            {
                Registry.SetValue("HKEY_CURRENT_USER\\Scrumboard", "DefaultBackColor", value);
            }
        }

        public static int DefaultTodoBackColor
        {
            get
            {
                Object v = Registry.GetValue("HKEY_CURRENT_USER\\Scrumboard", "DefaultTodoBackColor", null);
                if (v == null)
                    v = -128;
                return Int32.Parse(v.ToString());
            }
            set
            {
                Registry.SetValue("HKEY_CURRENT_USER\\Scrumboard", "DefaultTodoBackColor", value);
            }
        }
        public static String DefaultStoryType
        {
            get
            {
                Object v = Registry.GetValue("HKEY_CURRENT_USER\\Scrumboard", "DefaultStoryType", null);
                if (v == null)
                    v = "Planned";
                return v.ToString();
            }
            set
            {
                Registry.SetValue("HKEY_CURRENT_USER\\Scrumboard", "DefaultStoryType", value);
            }
        }
        public static String IssueTrackingSystemURL
        {
            get
            {
                Object v = Registry.GetValue("HKEY_CURRENT_USER\\Scrumboard", "IssueTrackingSystemURL", null);
                if (v == null)
                    v = @"https://your.url.nl/issue.aspx?ID={0}";
                return v.ToString();
            }
            set
            {
                Registry.SetValue("HKEY_CURRENT_USER\\Scrumboard", "IssueTrackingSystemURL", value);
            }
        }

        public static FormWindowState MainWindowState
        {
            get
            {
                Object v = Registry.GetValue("HKEY_CURRENT_USER\\Scrumboard", "MainWindowState", null);
                if (v == null)
                    v = FormWindowState.Maximized;
                return (FormWindowState)Enum.Parse(typeof(FormWindowState), v.ToString());
            }
            set
            {
                Registry.SetValue("HKEY_CURRENT_USER\\Scrumboard", "MainWindowState", value);
            }
        }

        public static int MainWindowLeft
        {
            get
            {
                Object v = Registry.GetValue("HKEY_CURRENT_USER\\Scrumboard", "MainWindowLeft", null);
                if (v == null)
                    v = 0;
                return Int32.Parse(v.ToString());
            }
            set
            {
                Registry.SetValue("HKEY_CURRENT_USER\\Scrumboard", "MainWindowLeft", value);
            }
        }

        public static int MainWindowTop
        {
            get
            {
                Object v = Registry.GetValue("HKEY_CURRENT_USER\\Scrumboard", "MainWindowTop", null);
                if (v == null)
                    v = 0;
                return Int32.Parse(v.ToString());
            }
            set
            {
                Registry.SetValue("HKEY_CURRENT_USER\\Scrumboard", "MainWindowTop", value);
            }
        }

        public static int MainWindowWidth
        {
            get
            {
                Object v = Registry.GetValue("HKEY_CURRENT_USER\\Scrumboard", "MainWindowWidth", null);
                if (v == null)
                    v = 0;
                return Int32.Parse(v.ToString());
            }
            set
            {
                Registry.SetValue("HKEY_CURRENT_USER\\Scrumboard", "MainWindowWidth", value);
            }
        }

        public static int MainWindowHeight
        {
            get
            {
                Object v = Registry.GetValue("HKEY_CURRENT_USER\\Scrumboard", "MainWindowHeight", null);
                if (v == null)
                    v = 0;
                return Int32.Parse(v.ToString());
            }
            set
            {
                Registry.SetValue("HKEY_CURRENT_USER\\Scrumboard", "MainWindowHeight", value);
            }
        }
    }
}
