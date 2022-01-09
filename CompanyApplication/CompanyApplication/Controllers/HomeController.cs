using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CompanyService;
using Microsoft.Ajax.Utilities;

namespace CompanyApplication.Controllers
{
    public class HomeController : Controller
    {
        string randomsession = null;
      
        CompanyRef.CompanyServiceClient client = new CompanyRef.CompanyServiceClient();
        public ActionResult Index()
        {
            Random generator = new Random();
            randomsession = generator.Next(0, 1000000).ToString("D5");
            Session["AppId"] = randomsession;
            return View();
        }

        [HttpPost]

        public PartialViewResult CreateAppHelper(FormCollection member)
        {

            FamilyMember family = new FamilyMember();
            string[] d = member["DateOfBirth"].ToString().Split('-');
            //    System.Diagnostics.Process p = System.Diagnostics.Process.GetCurrentProcess();
            // DateTime dob = new DateTime(int.Parse(d[2]), int.Parse(d[0]), int.Parse(d[1]));
            string middlename = null;
            if (!string.IsNullOrEmpty(member["MiddleName"]))
            {
                middlename= member["MiddleName"];
            }
                if (ModelState.IsValid)
            {
                FamilyMember familymember = new FamilyMember()
                {
                    FirstName = member["FirstName"].ToString(),
                    MiddleName = middlename,
                    LastName = member["LastName"].ToString(),
                    Gender = member["Gender"].ToString(),
                    DateOfBirth =Convert.ToDateTime(member["DateOfBirth"]),
                    Suffix = member["Suffix"].ToString(),
                    ApplicationId = Session["AppId"].ToString(),

                };

                client.SaveFamilyDetails(familymember);

                System.Threading.Thread.Sleep(2000);
            }
            ViewBag.familycount = client.GetFamilyMembers().ToList().Count+1;
            return PartialView("_CreateApplication", family);
        }
        [HttpGet]

        public ActionResult CreateApplication()
        {

            FamilyMember family = new FamilyMember();
            return View(family);
        }


        [HttpPost]
        public ActionResult CreateApplication(FamilyMember member)
        {

            return View();
        }

        [HttpGet]
        public ActionResult Relations()
        {
            if (Session["AppId"] != null)
            {
                ViewBag.memberslist = client.GetFamilyMembers().ToList().Where(x => x.ApplicationId == Session["AppId"].ToString()).ToList();
            }
                Session["family"] = ViewBag.memberslist;
            List<FamilyMemberRelation> family = new List<FamilyMemberRelation>();
            return View(family);
        }

        [HttpPost]
        public ActionResult Relations(FormCollection familys)
        {

            FamilyMemberRelation fr = new FamilyMemberRelation();
            FamilyMemberRelation frpost = new FamilyMemberRelation();
            fr.ApplicationId = familys["ApplicationId"];
            fr.RelationName = familys["RelationName"];
            string familyids = familys["FamilyMemberId"];
            for (int i = 0; i < fr.ApplicationId.Split(',').Length; i++)
            {
                frpost.RelationName = fr.RelationName.Split(',')[i];
                frpost.ApplicationId = fr.ApplicationId.Split(',')[i];
                frpost.FamilyMemberId = int.Parse(familyids.Split(',')[i]);

                client.AddRelations(frpost);
            }

            return RedirectToAction("Confirmation");
        }

        [HttpGet]
        public ActionResult Confirmation()
        {
            ViewBag.sessionid = Session["AppId"].ToString();
            return View();
        }

        [HttpGet]
        public ActionResult SearchApplication()
        {
            List<FamilyMemberRelation> relations = client.GetFamilyRelations().ToList();
            ViewBag.relations = relations;
            return View(client.GetFamilyMembers().ToList());
        }
        [HttpPost]
        public ActionResult SearchApplication(string ApplicationId)
        {
            List<FamilyMemberRelation> relations = client.GetFamilyRelations().ToList();
            ViewBag.relations = relations;
            if (string.IsNullOrEmpty(ApplicationId))
            {
                return View(client.GetFamilyMembers().ToList());
            }
            else
            {
                List<FamilyMember> searchfamily = client.SearchApplication(ApplicationId).ToList();
                return View(searchfamily);
            }
        }


    }
}