using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InternalJobPortalMVC.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.AspNetCore.Authorization;

namespace InternalJobPortalMVC.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        // GET: EmployeeController
        static HttpClient client = new HttpClient { BaseAddress = new Uri("http://localhost:5102/api/Employee/") };
        static HttpClient client2 = new HttpClient { BaseAddress = new Uri("http://localhost:5102/api/EmployeeSkill/") };
        public async Task<ActionResult> Index()
        {
            //string userName = User.Identity.Name;
            //string role = User.Claims.ToArray()[4].Value;
            //string secretKey = "Johny Johny yes papa....open your laptop HAHAHA!!!";
            //HttpClient client2 = new HttpClient();
            //string requestedUrl = "http://localhost:5195/api/Auth/" + userName + "/" + role + "/" + secretKey;
            //string token = await client2.GetStringAsync(requestedUrl);
            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<Employee> employees = await client.GetFromJsonAsync<List<Employee>>("");
            return View(employees);
        }

        // GET: EmployeeController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            Employee employee = await client.GetFromJsonAsync<Employee>("" + id);
            return View(employee);
        }

        // GET: EmployeeController/Create
        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            Employee employee = new Employee();
            return View(employee);
        }
        [Authorize(Roles = "Manager")]
        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Employee employee)
        {
            var response = await client.PostAsJsonAsync<Employee>("", employee);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new InternalJobPortalException(errorMessage);
            }
        }
        [Route("Employee/Edit/{id}")]
        [Authorize(Roles = "Manager")]
        // GET: EmployeeController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            Employee employee = await client.GetFromJsonAsync<Employee>("" + id);
            return View(employee); ;
        }
        [Route("Employee/Edit/{id}")]
        // POST: EmployeeController/Edit/5


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> Edit(string id, Employee employee)
        {
            try
            {
                await client.PutAsJsonAsync("" + id, employee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Route("Employee/Delete/{id}")]
        // GET: EmployeeController/Delete/5
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> Delete(string id)
        {
            Employee employee = await client.GetFromJsonAsync<Employee>("" + id);
            return View(employee); ;
        }
        [Route("Employee/Delete/{id}")]
        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            var response = await client.DeleteAsync("" + id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new InternalJobPortalException(errorMessage);
            }
        }
        [Route("Employee/ByJobId/{JobID}")]
        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> ByJobId(string JobID)
        {
            try
            {
                List<Employee> jobPosts = await client.GetFromJsonAsync<List<Employee>>(""+ $"ByJobId/{JobID}");
                return View(jobPosts);
            }
            catch
            {
                return View();
            }
        }
        [Route("EmployeeSkill/{id}")]
        public async Task<ActionResult> EmployeeSkillIndex(string id)
        {
            List<EmployeeSkill> empSkills = await client2.GetFromJsonAsync<List<EmployeeSkill>>("ByEmployeeID/"+id);
            ViewData["id"] = id;
            return View(empSkills);
        }

        // GET: EmployeeSkillController/Details/5
        [Route("EmployeeSkill/Details/{empId}/{skillId}")]
        public async Task<ActionResult> EmployeeSkillDetails(string empId, string skillId)
        {
            EmployeeSkill empSkill = await client2.GetFromJsonAsync<EmployeeSkill>("" + empId + "/" + skillId);
            return View(empSkill);
        }

        // GET: EmployeeSkillController/Create
        [Route("EmployeeSkill/Create")]
        public ActionResult EmployeeSkillCreate(string id)
        {
            EmployeeSkill es = new EmployeeSkill();
            es.EmployeeID = id;
            return View(es);
        }

        // POST: EmployeeSkillController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("EmployeeSkill/Create")]
        public async Task<ActionResult> EmployeeSkillCreate(EmployeeSkill es)
        {

            var response = await client.PostAsJsonAsync<EmployeeSkill>("", es);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(EmployeeSkillIndex), new { id = es.EmployeeID });
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new InternalJobPortalException(errorMessage);
            }
        }
        [Route("EmployeeSkill/Edit/{empId}/{skillId}")]
        // GET: EmployeeSkillController/Edit/5
        public async Task<ActionResult> EmployeeSkillEdit(string empId, string skillId)
        {
            EmployeeSkill empSkill = await client2.GetFromJsonAsync<EmployeeSkill>("" + empId + "/" + skillId);
            return View(empSkill);
        }

        // POST: EmployeeSkillController/Edit/5
        [Route("EmployeeSkill/Edit/{empId}/{skillId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EmployeeSkillEdit(string empId, string skillId, EmployeeSkill es)
        {
            try
            {
                await client2.PutAsJsonAsync<EmployeeSkill>("" + empId + "/" + skillId, es);
                return RedirectToAction(nameof(EmployeeSkillIndex), new {id = empId});
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeSkillController/Delete/5
        [Route("EmployeeSkill/Delete/{empId}/{skillId}")]
        public async Task<ActionResult> EmployeeSkillDelete(string empId, string skillId)
        {
            EmployeeSkill empSkill = await client2.GetFromJsonAsync<EmployeeSkill>("" + empId + "/" + skillId);
            return View(empSkill);
        }

        // POST: EmployeeSkillController/Delete/5
        [Route("EmployeeSkill/Delete/{empId}/{skillId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EmployeeSkillDelete(string empId, string skillId, IFormCollection collection)
        {
            try
            {
                await client2.DeleteAsync("" + empId + "/" + skillId);
                return RedirectToAction(nameof(EmployeeSkillIndex), new { id = empId });
            }
            catch
            {
                return View();
            }
        }
        [Route("EmployeeSkill/ByEmployeeId/{empId}")]
        public async Task<ActionResult> EmployeeSkillByEmployeeId(string empId)
        {
            try
            {
                List<EmployeeSkill> empSkills = await client2.GetFromJsonAsync<List<EmployeeSkill>>("" + "ByEmployeeId" + "/" + empId);
                return View(empSkills);
            }
            catch
            {
                return View();
            }
        }
        [Route("EmployeeSkill/BySkillId{skillId}")]
        public async Task<ActionResult> EmployeeSkillBySkillId(string skillId)
        {
            try
            {
                List<EmployeeSkill> empSkills = await client2.GetFromJsonAsync<List<EmployeeSkill>>("" + "BySkillId" + "/" + skillId);
                return View(empSkills);
            }
            catch
            {
                return View();
            }
        }
    }
}
