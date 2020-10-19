using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ExamTeamManagementSystem.Models.BLL
{
    public class LabBLL
    {
        int labno;
        String building;
        String zone; 

        public List<LabBLL> AllLabs()
        {
            List<LabBLL> lbll = new List<LabBLL>();

            return lbll;
        }

        public LabBLL ObtainLabBLL()
        {
            LabBLL l = new LabBLL();

            return l;
        }

        public List<string> AllBuildings()
        {
            List < string > lbuil = new List<string>();

            return lbuil;
        }

       /* public string ObtainBuilding()
        {
            string bui;
            return bui;
        }*/
    }
}