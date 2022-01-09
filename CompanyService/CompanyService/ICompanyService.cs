using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CompanyService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICompanyService" in both code and config file together.
    [ServiceContract]
    public interface ICompanyService
    {
        [OperationContract]
        string SaveFamilyDetails(FamilyMember member);

        [OperationContract]
        List<FamilyMember> GetFamilyMembers();

        [OperationContract]
        List<FamilyMemberRelation> GetFamilyRelations();

        [OperationContract]
        void AddRelations(FamilyMemberRelation familyMemberRelation);
        [OperationContract]
        List<FamilyMember> SearchApplication(string ApplicationId);
    }
}
