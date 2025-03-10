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
        static HttpClient client = new HttpClient { BaseAddress = new Uri("https://internaljobportalwebapi-d9e4f0fgf2bccmcp.eastus2-01.azurewebsites.net/api/Employee/") };
        static HttpClient client2 = new HttpClient { BaseAddress = new Uri("https://internaljobportalwebapi-d9e4f0fgf2bccmcp.eastus2-01.azurewebsites.net/api/EmployeeSkill/") };
        public async Task<ActionResult> Index()
        {
            string userName = User.Identity.Name;
            string role = User.Claims.ToArray()[4].Value;
            string secretKey = "Johny Johny yes papa....open your laptop HAHAHA!!!";
            HttpClient client3 = new HttpClient();
            string requestedUrl = "https://internaljobportalwebapi-d9e4f0fgf2bccmcp.eastus2-01.azurewebsites.net/api/Auth/" + userName + "/" + role + "/" + secretKey;
            string token = await client3.GetStringAsync(requestedUrl);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
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
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            Employee employee = new Employee();
            return View(employee);
        }
        [Authorize(Roles = "Admin")]
        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Employee employee)
        {
            var response = await client.PostAsJsonAsync<Employee>("", employee);
            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Employee Created Succesfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new InternalJobPortalException(errorMessage);
            }
        }
        [Route("Employee/Edit/{id}")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string id, Employee employee)
        {
            try
            {
                await client.PutAsJsonAsync("" + id, employee);
                TempData["success"] = "Employee Updated Succesfully";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Route("Employee/Delete/{id}")]
        // GET: EmployeeController/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string id)
        {
            Employee employee = await client.GetFromJsonAsync<Employee>("" + id);
            return View(employee); ;
        }
        [Route("Employee/Delete/{id}")]
        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            var response = await client.DeleteAsync("" + id);
            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Employee Deleted Succesfully";
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult> EmployeeSkillIndex(string id)
        {
            string userName = User.Identity.Name;
            Employee employee = await client.GetFromJsonAsync<Employee>("" + id);
            if(employee.EmailID != userName)
            {
                throw new InternalJobPortalException("You Only View And Enter Your Skills. Only Employee with mail same as mail that is used while logging in can View and Edit ");
            }
            string role = User.Claims.ToArray()[4].Value;
            string secretKey = "Johny Johny yes papa....open your laptop HAHAHA!!!";
            HttpClient client3 = new HttpClient();
            string requestedUrl = "https://internaljobportalwebapi-d9e4f0fgf2bccmcp.eastus2-01.azurewebsites.net/api/Auth/" + userName + "/" + role + "/" + secretKey;
            string token = await client3.GetStringAsync(requestedUrl);
            client2.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<EmployeeSkill> empSkills = await client2.GetFromJsonAsync<List<EmployeeSkill>>("ByEmployeeID/"+id);
            ViewData["id"] = id;
            return View(empSkills);
        }

        // GET: EmployeeSkillController/Details/5
        [Route("EmployeeSkill/Details/{empId}/{skillId}")]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult> EmployeeSkillDetails(string empId, string skillId)
        {
            EmployeeSkill empSkill = await client2.GetFromJsonAsync<EmployeeSkill>("" + empId + "/" + skillId);
            return View(empSkill);
        }

        // GET: EmployeeSkillController/Create
        [Route("EmployeeSkill/Create")]
        [Authorize(Roles = "Employee")]
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
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult> EmployeeSkillCreate(EmployeeSkill es)
        {

            var response = await client2.PostAsJsonAsync<EmployeeSkill>("", es);
            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "EmployeeSkill Created Succesfully";
                return RedirectToAction(nameof(EmployeeSkillIndex), new { id = es.EmployeeID });
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new InternalJobPortalException(errorMessage);
            }
        }
        [Route("EmployeeSkill/Edit/{empId}/{skillId}")]
        [Authorize(Roles = "Employee")]
        // GET: EmployeeSkillController/Edit/5
        public async Task<ActionResult> EmployeeSkillEdit(string empId, string skillId)
        {
            EmployeeSkill empSkill = await client2.GetFromJsonAsync<EmployeeSkill>("" + empId + "/" + skillId);
            return View(empSkill);
        }

        // POST: EmployeeSkillController/Edit/5
        [Route("EmployeeSkill/Edit/{empId}/{skillId}")]
        [Authorize(Roles = "Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EmployeeSkillEdit(string empId, string skillId, EmployeeSkill es)
        {
            try
            {
                await client2.PutAsJsonAsync<EmployeeSkill>("" + empId + "/" + skillId, es);
                TempData["success"] = "EmployeeSkill Updated Succesfully";
                return RedirectToAction(nameof(EmployeeSkillIndex), new {id = empId});
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeSkillController/Delete/5
        [Authorize(Roles = "Employee")]
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
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult> EmployeeSkillDelete(string empId, string skillId, IFormCollection collection)
        {
            try
            {
                await client2.DeleteAsync("" + empId + "/" + skillId);
                TempData["success"] = "EmployeeSkill Deleted Succesfully";
                return RedirectToAction(nameof(EmployeeSkillIndex), new { id = empId });
            }
            catch
            {
                return View();
            }
        }
        [Route("EmployeeSkill/ByEmployeeId/{empId}")]
        [Authorize(Roles = "Employee")]
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
        [Authorize(Roles = "Employee")]
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
