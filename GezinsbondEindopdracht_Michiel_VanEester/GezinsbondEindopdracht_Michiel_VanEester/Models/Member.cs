using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GezinsbondEindopdracht_Michiel_VanEester.Models
{
    public enum MemberType
    {
        [Display(Name = "Special")]SPECIAL,
        [Display(Name = "Normal")] NORMAL
    }
    public class Member
    {
        public int Id { get; set; }
        public string MemberNumber { get; set; }
        public string ParentName { get; set; }
        public string ChildName { get; set; }
        public int ExpiryYear { get; set; }
        public MemberType Type { get; set; }
    }
}
