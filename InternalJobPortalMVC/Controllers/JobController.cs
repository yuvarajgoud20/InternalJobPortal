using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InternalJobPortalMVC;
using InternalJobPortalMVC.Models;


namespace InternalJobPortalMVC.Controllers 
{ 
    [Authorize]

public class JobController : Controller
{
    static HttpClient client = new HttpClient { BaseAddress = new Uri("http://localhost:5102/api/Job/") };
    // GET: JobController
    public async Task<ActionResult> Index()
    {
            string userName = User.Identity.Name;
            string userRole = User.Claims.ToArray()[4].Value;
            string secretKey = "Johny Johny yes papa....open your laptop HAHAHA!!!";
            HttpClient client2 = new HttpClient();
            string token = await client2.GetStringAsync("http://localhost:5102/api/Auth/" + userName + "/" + userRole + "/" + secretKey);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        List<Job> jobs = await client.GetFromJsonAsync<List<Job>>("");
        return View(jobs);
    }

    // GET: JobController/Details/5
    [Route("Job/Details/{jid}")]
    public async Task<ActionResult> Details(string jid)
    {
        Job job = await client.GetFromJsonAsync<Job>("" + jid);
        return View(job);
    }


        // GET: JobController/Create
    [Authorize(Roles ="Manager")]
    public ActionResult Create()
    {
        Job job = new Job();
        return View(job);
    }

    // POST: JobController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles ="Manager")]
    public async Task<ActionResult> Create(Job job)
    {
        var response = await client.PostAsJsonAsync<Job>("", job);
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

    // GET: JobController/Edit/5
    [Route("Job/Edit/{jid}")]
    [Authorize(Roles = "Manager")]
    public async Task<ActionResult> Edit(string jid)
    {
        Job job = await client.GetFromJsonAsync<Job>("" + jid);
        return View(job);
    }


    [Route("Job/Edit/{jid}")]
    // POST: JobController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Manager")]
    public async Task<ActionResult> Edit(string jid, Job job)
    {
        try
        {
            await client.PutAsJsonAsync<Job>("" + jid, job);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    [Route("Job/Delete/{jid}")]
    [Authorize(Roles = "Manager")]
        // GET: JobController/Delete/5
    public async Task<ActionResult> Delete(string jid)
    {
        Job job = await client.GetFromJsonAsync<Job>("" + jid);
        return View(job);
    }

    [Route("Job/Delete/{jid}")]
    [Authorize(Roles = "Manager")]
        // POST: JobController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(string jid, IFormCollection collection)
    {
        var response = await client.DeleteAsync("" + jid);
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
    static HttpClient client3 = new HttpClient { BaseAddress = new Uri("http://localhost:5102/api/JobSkill/") };
        // GET: JobSkillController
    [Authorize(Roles = "Manager")]
    public async Task<ActionResult> JobSkillIndex(string jobID)
    {
            string userName = User.Identity.Name;
            string userRole = User.Claims.ToArray()[4].Value;
            string secretKey = "Johny Johny yes papa....open your laptop HAHAHA!!!";
            HttpClient client2 = new HttpClient();
            string token = await client2.GetStringAsync("http://localhost:5102/api/Auth/" + userName + "/" + userRole + "/" + secretKey);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        List<JobSkill> jobSkills = await client.GetFromJsonAsync<List<JobSkill>>("ByJobID/" + jobID);
        return View(jobSkills);
    }

    // GET: JobSkillController/Details/5
    [Route("JobSkill/{jobID}/{skillID}")]
    [Authorize(Roles = "Manager")]
    public async Task<ActionResult> JobSkillDetails(string jobID, string skillID)
    {
        JobSkill jobSkill = await client3.GetFromJsonAsync<JobSkill>("" + jobID + "/" + skillID);
        return View(jobSkill);
    }

    // GET: JobSkillController/Create
    [Authorize(Roles ="Manager")]
    public ActionResult CreateJobSkill()
    {
        JobSkill jobSkill = new JobSkill();
        return View(jobSkill);
    }

    // POST: JobSkillController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles ="Manager")]
    public async Task<ActionResult> CreateJobSkill(JobSkill jobSkill)
    {
        var response = await client.PostAsJsonAsync<JobSkill>("", jobSkill);
        if (response.IsSuccessStatusCode)
        {

            return RedirectToAction(nameof(JobSkillIndex), new { id = jobSkill.JobID });
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new InternalJobPortalException(errorMessage);
        }
    }

    // GET: FlightController/Edit/5
    [Route("JobSkill/Edit/{jobID}/{skillID}")]
    [Authorize(Roles ="Manager")]
    public async Task<ActionResult> EditJobSkill(string jobID, string skillID)
    {
        JobSkill jobSkill = await client3.GetFromJsonAsync<JobSkill>("" + jobID + "/" + skillID);
        return View(jobSkill);
    }

    // POST: FlightController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("JobSkill/Edit/{jobID}/{skillID}")]
    [Authorize(Roles ="Manager")]
    public async Task<ActionResult> EditJobSkill(string jobID, string skillID, JobSkill jobSkill)
    {
        try
        {
            await client3.PutAsJsonAsync<JobSkill>("" + jobID + "/" + skillID, jobSkill);
            return RedirectToAction(nameof(JobSkillByJobID), new { jobID });
        }
        catch
        {
            return View();
        }
    }

    // GET: JobSkillController/Delete/5
    [Route("JobSkill/Delete/{jobID}/{skillID}")]
    [Authorize(Roles ="Manager")]
    public async Task<ActionResult> DeleteJobSkill(string jobID, string skillID)
    {
        JobSkill jobSkill = await client3.GetFromJsonAsync<JobSkill>("" + jobID + "/" + skillID);
        return View(jobSkill);
    }

    // POST: JobSkillController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("JobSkill/Delete/{jobID}/{skillID}")]
    [Authorize(Roles ="Manager")]
    public async Task<ActionResult> DeleteJobSkill(string jobID, string skillID, IFormCollection collection)
    {
        try
        {
            await client3.DeleteAsync("" + jobID + "/" + skillID);
            return RedirectToAction(nameof(JobSkillIndex), new { jobID });
        }
        catch
        {
            return View();
        }
    }
    [Authorize(Roles ="Manager")]
    public async Task<ActionResult> JobSkillByJobID(string jobID)
    {
        List<JobSkill> jobSkillsByJobID = await client3.GetFromJsonAsync<List<JobSkill>>("" + "ByJobID/" + jobID);
        return View(jobSkillsByJobID);
    }
    [Authorize(Roles ="Manager")]
    public async Task<ActionResult> JobSkillBySkillID(string skillID)
    {
        List<JobSkill> jobSkillsBySkillID = await client3.GetFromJsonAsync<List<JobSkill>>("" + "BySkillID/" + skillID);
        return View(jobSkillsBySkillID);
    }
}
}