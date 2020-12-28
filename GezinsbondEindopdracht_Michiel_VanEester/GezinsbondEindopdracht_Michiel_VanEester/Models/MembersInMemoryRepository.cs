using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GezinsbondEindopdracht_Michiel_VanEester.Models;

namespace GezinsbondEindopdracht_Michiel_VanEester.Models
{
    public class MembersInMemoryRepository : IMemberRepository
    {
        private List<Member> members;

        public MembersInMemoryRepository()
        {
            this.members = new List<Member>();
            
            Create(new Member { MemberNumber = "G-20200001", ParentName = "Tim", ChildName="Lien", ExpiryYear = 2025, Type = MemberType.SPECIAL});
            Create(new Member { MemberNumber = "G-20200002", ParentName = "Geert", ChildName = "Daan", ExpiryYear = 2022, Type = MemberType.NORMAL });
            Create(new Member { MemberNumber = "G-20200003", ParentName = "Ilse", ChildName = "Berre", ExpiryYear = 2030, Type = MemberType.SPECIAL });
            Create(new Member { MemberNumber = "G-20200004", ParentName = "Bart", ChildName = "Yanick", ExpiryYear = 2024, Type = MemberType.NORMAL });
            Create(new Member { MemberNumber = "G-20200005", ParentName = "Leen", ChildName = "Sienna", ExpiryYear = 2022, Type = MemberType.NORMAL });
            Create(new Member { MemberNumber = "G-20200006", ParentName = "Paul", ChildName = "Sven", ExpiryYear = 2026, Type = MemberType.NORMAL });
            Create(new Member { MemberNumber = "G-20200007", ParentName = "Jip", ChildName = "Janeke", ExpiryYear = 2026, Type = MemberType.SPECIAL });
            Create(new Member { MemberNumber = "G-20200008", ParentName = "Mark", ChildName = "Gert", ExpiryYear = 2023, Type = MemberType.SPECIAL });
        }

        public void Create(Member member)
        {
            int max = 0;
            foreach (Member m in members)
            {
                if (m.Id > max)
                {
                    max = m.Id;
                }
            }
            member.Id = max + 1;
            this.members.Add(member);
        }

        public void Delete(Member member)
        {
            this.members.Remove(member);
        }

        public Member Get(int id)
        {
            foreach (Member member in members)
            {
                if (member.Id == id)
                {
                    return member;
                }
            }
            return null;
        }

        public IQueryable<Member> GetAll()
        {
            return members.AsQueryable();
        }

        public void Update(Member member)
        {
            var oldMember = Get(member.Id);
            oldMember.MemberNumber = member.MemberNumber;
            oldMember.ParentName = member.ParentName;
            oldMember.ChildName = member.ChildName;
            oldMember.ExpiryYear = member.ExpiryYear;
            oldMember.Type = member.Type;
        }
    }
}
