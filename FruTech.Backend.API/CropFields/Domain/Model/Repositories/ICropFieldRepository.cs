using FruTech.Backend.API.CropFields.Domain.Model.Entities;
using FruTech.Backend.API.Shared.Domain.Repositories;

namespace FruTech.Backend.API.CropFields.Domain.Model.Repositories
{
    public interface ICropFieldRepository : IBaseRepository<CropField>
    {
        Task<IEnumerable<CropField>> GetAllAsync();
        Task<CropField?> GetByIdAsync(int id);
        new Task<CropField?> FindByIdAsync(int id);
        Task<CropField?> FindByFieldIdAsync(int fieldId);
        Task<CropField?> FindAnyByFieldIdAsync(int fieldId); // incluye borrados
        new Task AddAsync(CropField cropField);
        new void Update(CropField cropField);
        void Delete(CropField cropField);
    }
}
