using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyService
{

   [Serializable]
    public class FamilyMemberMetaData
    {
        public int Id { get; }
       
        [Required(ErrorMessage ="First Name Required")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Last Name Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Suffix Required")]
        public string Suffix { get; set; }
        [Required(ErrorMessage = "DateOfBirth Required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Gender Required")]

        public string Gender { get; set; }
        public string ApplicationId { get; set; }

    }
    [MetadataType(typeof(FamilyMemberMetaData))]
    public partial class FamilyMember
    {
        
    }
}
