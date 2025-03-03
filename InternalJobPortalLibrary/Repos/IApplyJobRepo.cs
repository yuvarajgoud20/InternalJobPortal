using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternalJobPortalLibrary.Models;

namespace InternalJobPortalLibrary.Repos
{
    public interface IApplyJobRepo
    {
        Task InsertApplyJob(ApplyJob applyJob);
        Task UpdateApplyJob(int postId , string EmployeeID , ApplyJob applyJob );
        Task DeleteApplyJob(int postId, string EmployeeID);
        Task<ApplyJob> GetApplyJob(int postId, string EmployeeID);
        Task<List<ApplyJob>> GetAllApplyJobs();
        Task<List<ApplyJob>> GetApplyJobsByPostID(int postID);
        Task<List<ApplyJob>> GetApplyJobsByEmployeeID(string EmployeeID);
    }
}
