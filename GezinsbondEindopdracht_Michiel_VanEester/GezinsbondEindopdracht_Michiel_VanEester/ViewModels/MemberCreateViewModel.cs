using GezinsbondEindopdracht_Michiel_VanEester.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GezinsbondEindopdracht_Michiel_VanEester.ViewModels
{
    
    public class MemberCreateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Lidkaartnummer is een verplicht veld")]
        public string MemberNumber { get; set; }

        [Required(ErrorMessage = "Naam ouders is een verplicht veld")]
        public string ParentName { get; set; }

        [Required(ErrorMessage = "Naam kind is een verplicht veld")]
        public string ChildName { get; set; }

        [Range(2021, 2030, ErrorMessage = "Vervaljaar moet tussen 2021 en 2030 liggen")]
        [Required(ErrorMessage = "Vervaljaar is een verplicht veld")]
        public int ExpiryYear { get; set; }

        public MemberType Type { get; set; }
    }
}
