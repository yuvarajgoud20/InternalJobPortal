using InternalJobPortalMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InternalJobPortalMvc;

using System.Security.Cryptography;
using InternalJobPortalMVC;
using Microsoft.AspNetCore.Authorization;
namespace InternalJobPortalMVCApp.Controllers
{
    [Authorize]
    public class SkillController : Controller
    {
        static HttpClient client = new HttpClient { BaseAddress = new Uri("http://localhost:5102/api/Skill/") };
        public async Task<ActionResult> Index()
        {
            string userName = User.Identity.Name;
            string userRole = User.Claims.ToArray()[4].Value;
            string secretKey = "Johny Johny yes papa....open your laptop HAHAHA!!!";
            HttpClient client2 = new HttpClient();
            string token = await client2.GetStringAsync("http://localhost:5102/api/Auth/" + userName + "/" + userRole + "/" + secretKey);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<Skill> skill = await client.GetFromJsonAsync<List<Skill>>("");
            return View(skill); 
            
        }

        // GET: SkillController/Details/5
        [Route("Skill/Details/{sid}")]
        public async Task<ActionResult> Details(string sid)
        {
            Skill skill = await client.GetFromJsonAsync<Skill>("" + sid);
            return View(skill);
        }

        // GET: SkillController/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            Skill skill = new Skill();
            return View(skill);
        }

        // POST: SkillController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(Skill skill)
        {
            var response = await client.PostAsJsonAsync<Skill>("", skill);
            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Skill Added Succesfully";
                return RedirectToAction(nameof(Index));
            }
                
            else
            {
                string msg = await response.Content.ReadAsStringAsync();
                throw new InternalJobPortalException(msg);
            }
        }

        // GET: SkillController/Edit/5
        [Route("Skill/Edit/{sid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string sid)
        {
            Skill skill = await client.GetFromJsonAsync<Skill>("" + sid);
            return View(skill);
        }

        // POST: SkillController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Skill/Edit/{sid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string sid, Skill skill)
        {
            try
            {
                await client.PutAsJsonAsync<Skill>("" + sid, skill);
                TempData["success"] = "Skill Updated Succesfully";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SkillController/Delete/5
        [Route("Skill/Delete/{sid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string sid)
        {
            Skill skill = await client.GetFromJsonAsync<Skill>("" + sid);
            return View(skill);
        }

        // POST: SkillController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Skill/Delete/{sid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string sid, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync("" + sid);
                TempData["success"] = "Skill Deleted Succesfully";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
