﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InternalJobPortalMVC;
using InternalJobPortalMVC.Models;


namespace InternalJobPortalMVC.Controllers 
{ 
    [Authorize]

public class JobController : Controller
{
    static HttpClient client = new HttpClient { BaseAddress = new Uri("https://internaljobportalwebapi-d9e4f0fgf2bccmcp.eastus2-01.azurewebsites.net/api/Job/") };
    // GET: JobController
    public async Task<ActionResult> Index()
    {
            string userName = User.Identity.Name;
            string userRole = User.Claims.ToArray()[4].Value;
            string secretKey = "Johny Johny yes papa....open your laptop HAHAHA!!!";
            HttpClient client2 = new HttpClient();
            string token = await client2.GetStringAsync("https://internaljobportalwebapi-d9e4f0fgf2bccmcp.eastus2-01.azurewebsites.net/api/Auth/" + userName + "/" + userRole + "/" + secretKey);
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
    [Authorize(Roles ="Admin")]
    public ActionResult Create()
    {
        Job job = new Job();
        return View(job);
    }

    // POST: JobController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles ="Admin")]
    public async Task<ActionResult> Create(Job job)
    {
        var response = await client.PostAsJsonAsync<Job>("", job);
        if (response.IsSuccessStatusCode)
        {
                TempData["success"] = "Job Created Succesfully";
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
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Edit(string jid)
    {
        Job job = await client.GetFromJsonAsync<Job>("" + jid);
        return View(job);
    }


    [Route("Job/Edit/{jid}")]
    // POST: JobController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Edit(string jid, Job job)
    {
        try
        {
            await client.PutAsJsonAsync<Job>("" + jid, job);
                TempData["success"] = "Job Updated Succesfully";
                return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    [Route("Job/Delete/{jid}")]
    [Authorize(Roles = "Admin")]
        // GET: JobController/Delete/5
    public async Task<ActionResult> Delete(string jid)
    {
        Job job = await client.GetFromJsonAsync<Job>("" + jid);
        return View(job);
    }

    [Route("Job/Delete/{jid}")]
    [Authorize(Roles = "Admin")]
        // POST: JobController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(string jid, IFormCollection collection)
    {
        var response = await client.DeleteAsync("" + jid);
        if (response.IsSuccessStatusCode)
        {
                TempData["success"] = "Job Deleted Succesfully";
                return RedirectToAction(nameof(Index));
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new InternalJobPortalException(errorMessage);
        }
    }
    static HttpClient client3 = new HttpClient { BaseAddress = new Uri("https://internaljobportalwebapi-d9e4f0fgf2bccmcp.eastus2-01.azurewebsites.net/api/JobSkill/") };
        // GET: JobSkillController
    public async Task<ActionResult> JobSkillIndex(string jobID)
    {
            
            List<JobSkill> jobSkills = await client3.GetFromJsonAsync<List<JobSkill>>("ByJobID/" + jobID);
            return View(jobSkills);
    }

    // GET: JobSkillController/Details/5
    [Route("JobSkill/{jobID}/{skillID}")]
    public async Task<ActionResult> JobSkillDetails(string jobID, string skillID)
    {
        JobSkill jobSkill = await client3.GetFromJsonAsync<JobSkill>("" + jobID + "/" + skillID);
        return View(jobSkill);
    }

    // GET: JobSkillController/Create
    [Authorize(Roles ="Admin")]
    public ActionResult CreateJobSkill(string jobID)
    {
        JobSkill jobSkill = new JobSkill();
            jobSkill.JobID = jobID;
        return View(jobSkill);
    }

    // POST: JobSkillController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles ="Admin")]
    public async Task<ActionResult> CreateJobSkill(JobSkill jobSkill)
    {
        var response = await client3.PostAsJsonAsync<JobSkill>("", jobSkill);
        if (response.IsSuccessStatusCode)
        {
                TempData["success"] = "JobSkill Created Succesfully";
                return RedirectToAction(nameof(JobSkillByJobID), new { jobID = jobSkill.JobID });
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new InternalJobPortalException(errorMessage);
        }
    }

    // GET: FlightController/Edit/5
    [Route("JobSkill/Edit/{jobID}/{skillID}")]
    [Authorize(Roles ="Admin")]
    public async Task<ActionResult> EditJobSkill(string jobID, string skillID)
    {
        JobSkill jobSkill = await client3.GetFromJsonAsync<JobSkill>("" + jobID + "/" + skillID);
        return View(jobSkill);
    }

    // POST: FlightController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("JobSkill/Edit/{jobID}/{skillID}")]
    [Authorize(Roles ="Admin")]
    public async Task<ActionResult> EditJobSkill(string jobID, string skillID, JobSkill jobSkill)
    {
        try
        {
            await client3.PutAsJsonAsync<JobSkill>("" + jobID + "/" + skillID, jobSkill);
                TempData["success"] = "JobSkill Updated Succesfully";
                return RedirectToAction(nameof(JobSkillByJobID), new { jobID });
        }
        catch
        {
            return View();
        }
    }

    // GET: JobSkillController/Delete/5
    [Route("JobSkill/Delete/{jobID}/{skillID}")]
    [Authorize(Roles ="Admin")]
    public async Task<ActionResult> DeleteJobSkill(string jobID, string skillID)
    {
        JobSkill jobSkill = await client3.GetFromJsonAsync<JobSkill>("" + jobID + "/" + skillID);
        return View(jobSkill);
    }

    // POST: JobSkillController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("JobSkill/Delete/{jobID}/{skillID}")]
    [Authorize(Roles ="Admin")]
    public async Task<ActionResult> DeleteJobSkill(string jobID, string skillID, IFormCollection collection)
    {
        try
        {
            await client3.DeleteAsync("" + jobID + "/" + skillID);
                TempData["success"] = "JobSkill Deleted Succesfully";
                return RedirectToAction(nameof(JobSkillIndex), new { jobID });
        }
        catch
        {
            return View();
        }
    }
    //[Authorize(Roles ="Manager")]
    public async Task<ActionResult> JobSkillByJobID(string jobID)
    {
            string userName = User.Identity.Name;
            string userRole = User.Claims.ToArray()[4].Value;
            string secretKey = "Johny Johny yes papa....open your laptop HAHAHA!!!";
            HttpClient client2 = new HttpClient();
            string token = await client2.GetStringAsync("https://internaljobportalwebapi-d9e4f0fgf2bccmcp.eastus2-01.azurewebsites.net/api/Auth/" + userName + "/" + userRole + "/" + secretKey);
            client3.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            List<JobSkill> jobSkillsByJobID = await client3.GetFromJsonAsync<List<JobSkill>>("" + "ByJobID/" + jobID);
            ViewData["jobID"] = jobID;
        return View(jobSkillsByJobID);
    }
    [Authorize(Roles ="Admin")]
    public async Task<ActionResult> JobSkillBySkillID(string skillID)
    {
        List<JobSkill> jobSkillsBySkillID = await client3.GetFromJsonAsync<List<JobSkill>>("" + "BySkillID/" + skillID);
        return View(jobSkillsBySkillID);
    }
}
}