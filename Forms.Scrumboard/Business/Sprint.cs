using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScrumBoard.Common;
using ScrumBoard.ScrumboardService;
using System.Globalization;

namespace ScrumBoard.Business
{
    public class Sprint : ScrumboardService.Sprint
    {
        ScrumboardService.ScrumboardSoapClient client = ServiceConn.getClient();
        ScrumboardService.Layout layout;
        ScrumboardService.Panel[] panels;

        public ScrumBoard.ScrumboardService.Team SprintTeam { get; set; }

        public Sprint(int id)
        {

            ScrumboardService.Sprint sprint = null;
            try
            {
                sprint = client.SprintGet(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
            if (sprint != null)
            {
                Id = sprint.Id;
                LayoutId = sprint.LayoutId;
                Name = sprint.Name;
                StartDate = sprint.StartDate;
                TargetDate = sprint.TargetDate;
                TeamId = sprint.TeamId;
                Velocity = sprint.Velocity;
                FocusFactor = sprint.FocusFactor;
                layout = client.LayoutGet(LayoutId);
                panels = client.LayoutPanelSelectByLayout(LayoutId);
                SprintTeam = client.TeamGet(sprint.TeamId);
            }
            else
            {
                Id = -1;
                LayoutId = -1;
                Name = "Please open/import sprint first";
                StartDate = DateTime.Now;
                TargetDate = DateTime.MinValue;
                layout = null;
                panels = null;
                Velocity = 0;
                FocusFactor = 0;
                SprintTeam = new ScrumBoard.ScrumboardService.Team();
            }

        }

        public ScrumboardService.Layout Layout
        {
            get
            {
                return layout;
            }
        }
        public ScrumboardService.Panel[] Panels
        {
            get
            {
                return panels;
            }
        }

        public decimal BurndownRateOfDay(DateTime dt)
        {
            if (FocusFactor > 0 && Velocity > 0 && dt.DayOfWeek != DayOfWeek.Saturday && dt.DayOfWeek != DayOfWeek.Sunday)
            {
                decimal factor = ((decimal)FocusFactor) / 100;
                return factor * ((decimal)Velocity);
            }
            return 0;
        }

        public void ImportStories(FileInfo sprintFile)
        {
            if (!Config.ViewOnly)
            {
                if (sprintFile.Exists)
                {
                    int lineNr = 0;
                    StreamReader sr = sprintFile.OpenText();
                    while (!sr.EndOfStream)
                    {
                        lineNr++;
                        String line = sr.ReadLine();
                        String[] inf = line.Split('\t');
                        String extId = inf[0];
                        String desc = inf[1];
                        decimal estimate = Config.DefaultEstimate;
                        if (!Decimal.TryParse(inf[2], System.Globalization.NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US").NumberFormat, out estimate))
                        {
                            throw new FormatException("Estimate has invalid format, use 0 or 0.0 at line " + lineNr);
                        }
                        Data.getInstance().insertStory(Id, extId, 1, 1, desc, estimate, Config.DefaultBackColor, 30, 30, "");
                    }
                    sr.Close();
                }
            }
        }
        
        public void ExportStories(FileInfo file)
        {
            if (file != null)
            {
                if (File.Exists(file.FullName))
                {
                    File.Delete(file.FullName);
                }
                StreamWriter sw = File.CreateText(file.FullName);
                ScrumboardService.Story[] stories = client.StoryGetSprintStories(Id);
                foreach (ScrumboardService.Story s in stories)
                {
                    try
                    {
                        sw.Write(s.ExternalId);
                        sw.Write(",");
                        sw.Write(statusOf(s));
                        sw.Write(",");
                        sw.Write(s.Description);
                        sw.Write(",");
                        sw.Write(s.Estimate);

                        sw.Write(",");
                        if (s.ClosedDate != null && s.ClosedDate > DateTime.MinValue)
                            sw.Write(s.ClosedDate);
                        sw.WriteLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                sw.Close();
            }
        }

        ScrumboardService.State[] states;
        private String statusOf(ScrumboardService.Story story)
        {
            if (states == null) states = client.StateSelectAll();
            foreach (ScrumboardService.State state in states)
            {
                if (state.Id == story.StatusId)
                    return state.Name;
            }
            return "";
        }


    }
}