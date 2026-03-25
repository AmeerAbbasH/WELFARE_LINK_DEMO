using WELFARE_Application_MVCF.Models;

namespace WELFARE_Application_MVCF.Interfaces
{
    public interface IResourceRepository
    {
        Task<IEnumerable<Resource>> GetAllResourcesAsync();
        Task<IEnumerable<Resource>>GetResourcesByProgramIdAsync(int programId);
        Task AddResourcesAsync(Resource resource);
        Task UpdateResourceAsync(Resource resource);
    }
}
