//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExamTeamManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TechnicalIssue
    {
        public int TechnicalIssueID { get; set; }
        public int BuildingNo { get; set; }
        public string LabNo { get; set; }
        public int PCNo { get; set; }
        public string ProblemType { get; set; }
        public System.DateTime Date_Tech { get; set; }
        public System.TimeSpan Time_Tech { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
    
        public virtual Building Building { get; set; }
        public virtual Computer Computer { get; set; }
        public virtual Lab Lab { get; set; }
    }
}
