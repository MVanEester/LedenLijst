using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GezinsbondEindopdracht_Michiel_VanEester.Models
{
    public interface IMemberRepository
    {
        IQueryable<Member> GetAll();

        Member Get(int id);

        void Create(Member member);

        void Delete(Member member);

        void Update(Member member);
    }
}
