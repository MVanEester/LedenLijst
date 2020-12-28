using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GezinsbondEindopdracht_Michiel_VanEester.Models;
using GezinsbondEindopdracht_Michiel_VanEester.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GezinsbondEindopdracht_Michiel_VanEester.Controllers
{
    public enum SortField
    {
        Lidkaartnummer, NaamOuder, NaamKind, Vervaljaar, Type
    }

    public enum SortDirection
    {
        DESC, ASC
    }

    public class GezinsbondController : Controller
    {
        public static int PAGE_SIZE = 4;

        private IMemberRepository memberRepository;

        public GezinsbondController(IMemberRepository memberRepository)
        {
            this.memberRepository = memberRepository;
        }

        public IActionResult Index()
        {
            ViewBag.TimeStampGenerated = DateTime.Now.ToLongTimeString();

            return View();
        }

        public IActionResult Leden([FromQuery] SortField sort = SortField.Lidkaartnummer, [FromQuery] SortDirection sortDirection = SortDirection.ASC, [FromQuery] int page = 1)
        {
            var members = this.memberRepository.GetAll();

            switch (sort)
            {
                case SortField.Lidkaartnummer:
                    members = (sortDirection == SortDirection.ASC) ? members.OrderBy(member => member.MemberNumber) : members.OrderByDescending(member => member.MemberNumber);
                    break;
                case SortField.NaamOuder:
                    members = (sortDirection == SortDirection.ASC) ? members.OrderBy(member => member.ParentName) : members.OrderByDescending(member => member.ParentName);
                    break;
                case SortField.NaamKind:
                    members = (sortDirection == SortDirection.ASC) ? members.OrderBy(member => member.ChildName) : members.OrderByDescending(member => member.ChildName);
                    break;
                case SortField.Vervaljaar:
                    members = (sortDirection == SortDirection.ASC) ? members.OrderBy(member => member.ExpiryYear) : members.OrderByDescending(member => member.ExpiryYear);
                    break;
                case SortField.Type:
                    members = (sortDirection == SortDirection.ASC) ? members.OrderBy(member => member.Type) : members.OrderByDescending(member => member.Type);
                    break;
                default:
                    break;
            }

            MemberListViewModel memberListViewModel = new MemberListViewModel
            {
                SortDirection = sortDirection,
                SortField = sort,
                CurrentPage = page,
                TotalPages = (int) Math.Ceiling((double)members.Count() / PAGE_SIZE),
                Members = members.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE)
            };

            return View(memberListViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MemberCreateViewModel memberCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                Member member = new Member
                {
                    MemberNumber = memberCreateViewModel.MemberNumber,
                    ParentName = memberCreateViewModel.ParentName,
                    ChildName = memberCreateViewModel.ChildName,
                    ExpiryYear = memberCreateViewModel.ExpiryYear,
                    Type = memberCreateViewModel.Type
                };

                this.memberRepository.Create(member);
                TempData["Message"] = $"{member.ParentName} is aangemaakt";
                return RedirectToAction("Leden", "Gezinsbond");
            } 
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Member member = this.memberRepository.Get(id);
            if (member != null)
            {
                MemberUpdateViewModel memberUpdateViewModel = new MemberUpdateViewModel
                {
                    MemberNumber = member.MemberNumber,
                    ParentName = member.ParentName,
                    ChildName = member.ChildName,
                    ExpiryYear = member.ExpiryYear,
                    Type = member.Type
                };
                return View(memberUpdateViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Update(int id, MemberUpdateViewModel memberUpdateViewModel)
        {
            Member member = this.memberRepository.Get(id);
            if (member != null)
            {
                if (ModelState.IsValid)
                {
                    member.MemberNumber = memberUpdateViewModel.MemberNumber;
                    member.ParentName = memberUpdateViewModel.ParentName;
                    member.ChildName = memberUpdateViewModel.ChildName;
                    member.ExpiryYear = memberUpdateViewModel.ExpiryYear;
                    member.Type = memberUpdateViewModel.Type;

                    this.memberRepository.Update(member);
                    TempData["Message"] = $"{member.ParentName} is geüpdate";
                    return RedirectToAction("Leden", "Gezinsbond");
                }
                else
                {
                    return View(memberUpdateViewModel);
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Member member = this.memberRepository.Get(id);

            if (member != null)
            {
                this.memberRepository.Delete(member);
                TempData["Message"] = $"{member.ParentName} is verwijderd";
                return RedirectToAction("Leden", "Gezinsbond");
            }

            return NotFound();
        }
    }
}
