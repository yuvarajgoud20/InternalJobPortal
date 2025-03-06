﻿
using InternalJobPortalMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using System.Net.Http.Json;

namespace InternalJobPortalMVC.Controllers
{
    [Authorize]
    public class JobPostController : Controller
    {
        static HttpClient client = new HttpClient { BaseAddress = new Uri("http://localhost:5102/api/JobPost/") };
        static HttpClient client3 = new HttpClient { BaseAddress = new Uri("http://localhost:5102/api/Application/") };

        // GET: JobPostController
        public async Task<ActionResult> Index()
        {
            
            //string userName = User.Identity.Name;
            //string userRole = User.Claims.ToArray()[4].Value;
            //string secretKey = "Johny Johny yes papa....open your laptop HAHAHA!!!";
            //HttpClient client2 = new HttpClient();
            //string token = await client2.GetStringAsync("http://localhost:5227/api/Auth/" + userName + "/" + userRole + "/" + secretKey);
            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            

            List<JobPost> jobPosts = await client.GetFromJsonAsync<List<JobPost>>("");
            return View(jobPosts);

        }

        // GET: JobPostController/Details/5
        public async Task<ActionResult> Details(int postID)
        {
            JobPost jobPost = await client.GetFromJsonAsync<JobPost>(Convert.ToString(postID));
           

            return View(jobPost);

        }

        // GET: JobPostController/Create
        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            JobPost jobpost  = new JobPost();
            return View(jobpost);
        }

        // POST: JobPostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> Create(JobPost jobpost)
        {
            var response = await client.PostAsJsonAsync("", jobpost);
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
        [Route("JobPost/Edit/{postID}")]
        // GET: JobPostController/Edit/5
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> Edit(int postID)
        {
            JobPost jobPost = await client.GetFromJsonAsync<JobPost>(Convert.ToString(postID));
            return View(jobPost);

        }

        // POST: JobPostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("JobPost/Edit/{postID}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> Edit(int postID, JobPost jobpost)
        {
            try
            {
                await client.PutAsJsonAsync(Convert.ToString(postID), jobpost);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route("JobPost/Delete/{postID}")]
        [Authorize(Roles = "Manager")]

        // GET: JobPostController/Delete/5
        public async Task<ActionResult> Delete(int postID)
        {
            JobPost jobPost = await client.GetFromJsonAsync<JobPost>(Convert.ToString(postID));
            return View(jobPost);

        }

        // POST: JobPostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("JobPost/Delete/{postID}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> Delete(int postID, IFormCollection collection)
        {
            var response = await client.DeleteAsync("" + postID);
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

        [HttpGet]
        [Route("JobPost/GetByJobId/{jobId}")]
        
        public async Task<ActionResult> ByJobId(string jobId)
        {
            try
            {
                List<JobPost>jobPosts = await client.GetFromJsonAsync<List<JobPost>>($"ByJobId/{jobId}");

                /*Ensure jobPosts is not null
                if (jobPosts == null)
                {
                    jobPosts = new List<JobPost>(); // Avoid passing null to the view
                }*/

                return View("ByJobId", jobPosts);
            }
            catch 
            {
                return View();
            }
            
        }
        [Route("Application/{postID}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> ApplicationIndex(int postID)
        {
            //string userName = User.Identity.Name;
            //string role = User.Claims.ToArray()[4].Value;
            //string secretKey = "Johny Johny yes papa....open your laptop HAHAHA!!!";
            //HttpClient client2 = new HttpClient();
            //string requestedUrl = "http://localhost:5227/api/Auth/" + userName + "/" + role + "/" + secretKey;
            //string token = await client2.GetStringAsync(requestedUrl);
            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            //List<ApplyJob> jobs = await client3.GetFromJsonAsync<List<ApplyJob>>("ByPostID/"+postID);
            //return View(jobs);
            List<ApplyJob> applyJobs = await client3.GetFromJsonAsync<List<ApplyJob>>($"ByPostId/{postID}");
            ViewData["postID"] = postID;
            return View( applyJobs);

        }



        // GET: JobPostController/Details/5
        [Route("Application/Details/{postID}/{employeeId}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> ApplicationDetails(int postID, string employeeId)
        {
            ApplyJob job = await client3.GetFromJsonAsync<ApplyJob>(Convert.ToString("" + postID + "/" + employeeId));
            return View(job);

        }
        [Authorize(Roles = "Manager")]
        // GET: JobPostController/Create
        public ActionResult ApplicationCreate(int postID)
        {
            ApplyJob job = new ApplyJob();
            job.PostID = postID;
            return View(job);
        }

        // POST: JobPostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> ApplicationCreate(ApplyJob job)
        {
            var response = await client3.PostAsJsonAsync("", job);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(ApplicationIndex), new { postID = job.PostID });
            else
            {
                string msg = await response.Content.ReadAsStringAsync();
                throw new InternalJobPortalException(msg);
            }
        }
        [Route("Application/Edit/{postID}/{employeeId}")]
        [Authorize(Roles = "Manager")]
        // GET: JobPostController/Edit/5
        public async Task<ActionResult> ApplicationEdit(int postID, string employeeId)
        {
            ApplyJob job = await client3.GetFromJsonAsync<ApplyJob>("" + Convert.ToString(postID) + "/" + employeeId);
            return View(job);

        }

        // POST: JobPostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Application/Edit/{postID}/{employeeId}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> ApplicationEdit(int postID, string employeeId, ApplyJob job)
        {
            try
            {
                await client3.PutAsJsonAsync("" + Convert.ToString(postID) + "/" + employeeId, job);
                return RedirectToAction(nameof(ApplicationIndex), new { postID  });
            }
            catch
            {
                return View();
            }
        }

        [Route("Application/Delete/{postID}/{employeeId}")]
        [Authorize(Roles = "Manager")]

        // GET: JobPostController/Delete/5
        public async Task<ActionResult> ApplicationDelete(int postID, string employeeId)
        {
            ApplyJob job = await client3.GetFromJsonAsync<ApplyJob>("" + Convert.ToString(postID) + "/" + employeeId);
            return View(job);

        }

        // POST: JobPostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Application/Delete/{postID}/{employeeId}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> ApplicationDelete(int postID, string employeeId, IFormCollection collection)
        {
            try
            {
                await client3.DeleteAsync("" + Convert.ToString(postID) + "/" + employeeId);
                return RedirectToAction(nameof(ApplicationIndex), new { postID });
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        [Route("Application/ByPostId/{postID}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> ApplicationByPostId(string postID)
        {
            try
            {
                List<ApplyJob> applyJobs = await client3.GetFromJsonAsync<List<ApplyJob>>($"ByPostId/{postID}");
                return View("ByPostID", applyJobs);
            }
            catch
            {
                return View();
            }

        }
        [HttpGet]
        [Route("Application/ByEmpId/{employeeId}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> ApplicationByEmpId(string employeeId)
        {
            try
            {
                List<ApplyJob> applyJobs = await client3.GetFromJsonAsync<List<ApplyJob>>($"ByEmpId/{employeeId}");
                return View("ByEmpId", applyJobs);
            }
            catch
            {
                return View();
            }

        }
    }
}


