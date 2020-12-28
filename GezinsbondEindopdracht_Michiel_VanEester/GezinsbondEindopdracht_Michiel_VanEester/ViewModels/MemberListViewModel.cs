using GezinsbondEindopdracht_Michiel_VanEester.Controllers;
using GezinsbondEindopdracht_Michiel_VanEester.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GezinsbondEindopdracht_Michiel_VanEester.ViewModels
{
    public class MemberListViewModel
    {
        public SortDirection SortDirection { get; set; }
        public SortField SortField { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public IEnumerable<Member> Members { get; set; }
    }
}
