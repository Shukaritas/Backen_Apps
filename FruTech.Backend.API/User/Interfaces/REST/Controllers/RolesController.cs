using Microsoft.AspNetCore.Mvc;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FruTech.Backend.API.User.Interfaces.REST.Controllers;

/// <summary>
/// Controlador para gestionar roles de usuarios
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly AppDbContext _context;

    public RolesController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene todos los roles disponibles
    /// </summary>
    /// <returns>Lista de roles</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        var roles = await _context.Roles.ToListAsync();
        return Ok(roles);
    }
}

