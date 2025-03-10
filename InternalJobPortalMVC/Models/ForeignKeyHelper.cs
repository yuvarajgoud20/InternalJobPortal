using InternalJobPortalMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InternalJobPortalMvc.Models
{
    public class ForeignKeyHelper
    {
        public async static Task<List<SelectListItem>> GetEmployeeIds()
        {
            HttpClient client = new HttpClient { BaseAddress = new Uri("https://internaljobportalwebapi-d9e4f0fgf2bccmcp.eastus2-01.azurewebsites.net/api/Employee/") };

            string secretKey = "Johny Johny yes papa....open your laptop HAHAHA!!!";
            HttpClient client2 = new HttpClient();
            string requestedUrl = "https://internaljobportalwebapi-d9e4f0fgf2bccmcp.eastus2-01.azurewebsites.net/api/Auth/" + "myuvaraj.goud@zelis.com" + "/" + "Manager" + "/" + secretKey;
            string token = await client.GetStringAsync(requestedUrl);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            List<Employee> employees = await client.GetFromJsonAsync<List<Employee>>("");
            List<SelectListItem> empIds = employees.Select(emp => new SelectListItem
            {
                Text = emp.EmployeeID, // Assuming Employee has a Name property
                Value = emp.EmployeeID.ToString()
            }).ToList();

            return empIds;
        }

        public async static Task<List<SelectListItem>> GetSkillIds()
        {
            HttpClient client = new HttpClient { BaseAddress = new Uri("https://internaljobportalwebapi-d9e4f0fgf2bccmcp.eastus2-01.azurewebsites.net/api/Skill/") };

            string secretKey = "Johny Johny yes papa....open your laptop HAHAHA!!!";
            HttpClient client2 = new HttpClient();
            string requestedUrl = "https://internaljobportalwebapi-d9e4f0fgf2bccmcp.eastus2-01.azurewebsites.net/api/Auth/" + "myuvaraj.goud@zelis.com" + "/" + "Manager" + "/" + secretKey;
            string token = await client.GetStringAsync(requestedUrl);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            List<Skill> skills = await client.GetFromJsonAsync<List<Skill>>("");
            List<SelectListItem> skillIds = skills.Select(skill => new SelectListItem
            {
                Text = skill.SkillName, // Assuming Employee has a Name property
                Value = skill.SkillID.ToString()
            }).ToList();

            return skillIds;
        }

        public async static Task<List<SelectListItem>> GetJobIds()
        {
            HttpClient client = new HttpClient { BaseAddress = new Uri("https://internaljobportalwebapi-d9e4f0fgf2bccmcp.eastus2-01.azurewebsites.net/api/Job/") };

            string secretKey = "Johny Johny yes papa....open your laptop HAHAHA!!!";
            HttpClient client2 = new HttpClient();
            string requestedUrl = "https://internaljobportalwebapi-d9e4f0fgf2bccmcp.eastus2-01.azurewebsites.net/api/Auth/" + "myuvaraj.goud@zelis.com" + "/" + "Manager" + "/" + secretKey;
            string token = await client.GetStringAsync(requestedUrl);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            List<Job> jobs = await client.GetFromJsonAsync<List<Job>>("");
            List<SelectListItem> jobIds = jobs.Select(job => new SelectListItem
            {
                Text = job.JobTitle, // Assuming Employee has a Name property
                Value = job.JobID.ToString()
            }).ToList();

            return jobIds;
        }

        public async static Task<List<SelectListItem>> GetPostIds()
        {
            HttpClient client = new HttpClient { BaseAddress = new Uri("https://internaljobportalwebapi-d9e4f0fgf2bccmcp.eastus2-01.azurewebsites.net/api/JobPost/") };

            string secretKey = "Johny Johny yes papa....open your laptop HAHAHA!!!";
            HttpClient client2 = new HttpClient();
            string requestedUrl = "https://internaljobportalwebapi-d9e4f0fgf2bccmcp.eastus2-01.azurewebsites.net/api/Auth/" + "myuvaraj.goud@zelis.com" + "/" + "Manager" + "/" + secretKey;
            string token = await client.GetStringAsync(requestedUrl);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            List<JobPost> posts = await client.GetFromJsonAsync<List<JobPost>>("");
            List<SelectListItem> postIds = posts.Select(post => new SelectListItem
            {
                Text = post.PostID.ToString(), // Assuming Employee has a Name property
                Value = post.PostID.ToString()
            }).ToList();

            return postIds;
        }
    }
}