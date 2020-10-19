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
    
    public partial class Computer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Computer()
        {
            this.LogisticsIssues = new HashSet<LogisticsIssue>();
            this.TechnicalIssues = new HashSet<TechnicalIssue>();
        }
    
        public int PCNo { get; set; }
        public string PCDescription { get; set; }
        public int PCNoInLab { get; set; }
        public string LabNo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LogisticsIssue> LogisticsIssues { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TechnicalIssue> TechnicalIssues { get; set; }
    }
}