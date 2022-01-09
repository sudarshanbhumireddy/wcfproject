using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CompanyService
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CompanyService" in both code and config file together.
    [Synchronization()]
    public class CompanyService : ICompanyService
    {

        SampleCodeDB context = new SampleCodeDB();

        public void AddRelations(FamilyMemberRelation familyMemberRelation)
        {
            context.FamilyMemberRelations.Add(familyMemberRelation);
            context.SaveChanges();
        }

        public List<FamilyMember> GetFamilyMembers()
        {
            return context.FamilyMembers.ToList();
        }


        public List<FamilyMemberRelation> GetFamilyRelations()
        {
            return context.FamilyMemberRelations.ToList();
        }

        [STAThread]
        public string SaveFamilyDetails(FamilyMember member)
        {

            FamilyMember familymember = new FamilyMember();

            familymember.FirstName = member.FirstName;
            familymember.MiddleName = member.MiddleName;
            familymember.LastName = member.LastName;
            familymember.Gender = member.Gender;
            familymember.DateOfBirth = member.DateOfBirth;
            familymember.Suffix = member.Suffix;
            familymember.ApplicationId = member.ApplicationId;
            context.FamilyMembers.Add(familymember);
            context.SaveChanges();

            return familymember.ApplicationId;
        }

        public List<FamilyMember> SearchApplication(string ApplicationId)
        {
            if (!string.IsNullOrEmpty(ApplicationId))
            {
                return context.FamilyMembers.ToList().FindAll(x => x.ApplicationId == ApplicationId).ToList();
            }
            return null;
        }

    }
}

