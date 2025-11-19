using FruTech.Backend.API.CropFields.Domain.Model.Entities;
using FruTech.Backend.API.CropFields.Domain.Model.Repositories;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FruTech.Backend.API.CropFields.Infrastructure.Persistence.EFC.Repositories
{
    public class CropFieldRepository : BaseRepository<CropField>, ICropFieldRepository
    {
        public CropFieldRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CropField>> GetAllAsync()
        {
            return await Context.CropFields
                .Include(c => c.Field)
                .Where(c => !c.Deleted)
                .ToListAsync();
        }

        public async Task<CropField?> GetByIdAsync(int id)
        {
            return await Context.CropFields
                .Include(cf => cf.Field)
                .FirstOrDefaultAsync(cf => cf.Id == id && !cf.Deleted);
        }

        public new async Task<CropField?> FindByIdAsync(int id)
        {
            return await Context.CropFields
                .Include(cf => cf.Field)
                .FirstOrDefaultAsync(cf => cf.Id == id && !cf.Deleted);
        }

        public async Task<CropField?> FindByFieldIdAsync(int fieldId)
        {
            return await Context.CropFields
                .Include(cf => cf.Field)
                .FirstOrDefaultAsync(cf => cf.FieldId == fieldId && !cf.Deleted);
        }

        public async Task<CropField?> FindAnyByFieldIdAsync(int fieldId)
        {
            return await Context.CropFields
                .Include(cf => cf.Field)
                .FirstOrDefaultAsync(cf => cf.FieldId == fieldId); // incluye borrados
        }

        public new async Task AddAsync(CropField cropField)
        {
            await Context.CropFields.AddAsync(cropField);
        }

        public new void Update(CropField cropField)
        {
            Context.CropFields.Update(cropField);
        }

        public void Delete(CropField cropField)
        {
            Context.CropFields.Remove(cropField);
        }
    }
}
