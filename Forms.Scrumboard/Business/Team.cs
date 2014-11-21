using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrumBoard.ScrumboardService;
using ScrumBoard.Common;

namespace ScrumBoard.Business
{
    public class Team
    {
        ScrumboardSoapClient client;
        TeamMember[] members;

        public Team(ScrumBoard.Business.Sprint sprint)
        {
            client = ServiceConn.getClient();
            members = client.TeamMemberSelectByTeam(sprint.TeamId);
        }

        public Decimal getBurndownRate(DateTime dt)
        {
            if (dt.DayOfWeek != DayOfWeek.Saturday && dt.DayOfWeek != DayOfWeek.Sunday)
            {
                Decimal hours = 0.0M;
                if (members.Length > 0)
                {
                    foreach (TeamMember m in members)
                    {
                        //NonWorkingHours[] nonWorkingHours = client.TeamGetNonWorkingHoursAtDay(m.Id, dt);
                        //decimal nwh = 0.0M;
                        //foreach (NonWorkingHours h in nonWorkingHours)
                        //{
                        //    nwh += h.Hours;
                        //}
                        //Decimal wh = (m.NormalWorkingHours - nwh) * m.AvailabilityFactor;
                        Decimal wh = (m.NormalWorkingHours * m.FocusFactor) * m.AvailabilityFactor;
                        hours += wh;
                    }
                    return hours;
                }
                else
                {
                    return 8.0M;
                }
            }
            return 0.0M;
        }
    }
}
