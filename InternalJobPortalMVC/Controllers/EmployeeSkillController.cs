using System.Net.Http.Json;
using InternalJobPortalMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace InternalJobPortalMVC.Controllers
{
    public class EmployeeSkillController : Controller

    {
        HttpClient client = new HttpClient { BaseAddress = new Uri("http://localhost:5102/api/EmployeeSkill/") };
        // GET: EmployeeSkillController
        public async Task<ActionResult> Index()
        {
            List<EmployeeSkill> empSkills = await client.GetFromJsonAsync<List<EmployeeSkill>>("");
            return View(empSkills);
        }

        // GET: EmployeeSkillController/Details/5
        public async Task<ActionResult> Details(string empId,string skillId)
        {
            EmployeeSkill empSkill = await client.GetFromJsonAsync<EmployeeSkill>(""+empId+"/"+skillId);
            return View(empSkill);
        }

        // GET: EmployeeSkillController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeSkillController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeSkill es)
        {
            try
            {
                await client.PostAsJsonAsync<EmployeeSkill>("" , es);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Route("Edit/{empId}/{skillId}")]
        // GET: EmployeeSkillController/Edit/5
        public async Task<ActionResult> Edit(string empId,string skillId)
        {
            EmployeeSkill empSkill = await client.GetFromJsonAsync<EmployeeSkill>("" + empId + "/" + skillId);
            return View(empSkill);
        }

        // POST: EmployeeSkillController/Edit/5
        [Route("Edit/{empId}/{skillId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string empId,string skillId,EmployeeSkill es)
        {
            try
            {
                await client.PutAsJsonAsync<EmployeeSkill>("" + empId + "/" + skillId,es);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeSkillController/Delete/5
        [Route("Delete/{empId}/{skillId}")]
        public async Task<ActionResult> Delete(string empId,string skillId)
        {
            EmployeeSkill empSkill = await client.GetFromJsonAsync<EmployeeSkill>("" + empId + "/" + skillId);
            return View(empSkill);
        }

        // POST: EmployeeSkillController/Delete/5
        [Route("Delete/{empId}/{skillId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string empId, string skillId, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync(""+empId+"/"+skillId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Route("ByEmployeeId/{empId}")]
        public async Task<ActionResult> ByEmployeeId(string empId)
        {
            try
            {
                List<EmployeeSkill> empSkills = await client.GetFromJsonAsync<List<EmployeeSkill>>(""+"ByEmployeeId"+"/"+empId);
                return View(empSkills);
            }
            catch
            {
                return View();            
            }
        }
        [Route("BySkillId{skillId}")]
        public async Task<ActionResult> BySkillId(string skillId)
        {
            try
            {
                List<EmployeeSkill> empSkills = await client.GetFromJsonAsync<List<EmployeeSkill>>(""+"BySkillId"+"/"+skillId);
                return View(empSkills);
            }
            catch
            {
                return View();
            }
        }
    }
}
