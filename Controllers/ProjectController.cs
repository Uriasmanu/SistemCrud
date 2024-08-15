using Microsoft.AspNetCore.Mvc;
using SistemCrud.DTOs;
using SistemCrud.Models;
using SistemCrud.Services;

namespace SistemCrud.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;

        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET: api/Project/Active
        [HttpGet("Active")]
        public async Task<ActionResult<IEnumerable<Project>>> GetActiveProjects()
        {
            var projects = await _projectService.GetActiveProjectsAsync();
            return Ok(projects);
        }

        // GET: api/Project/Deleted
        [HttpGet("Deleted")]
        public async Task<ActionResult<IEnumerable<Project>>> GetDeletedProjects()
        {
            var projects = await _projectService.GetDeletedProjectsAsync();
            return Ok(projects);
        }

        // GET: api/Project/
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectById(Guid id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // POST: api/Project
        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject(ProjetoDTO projectDTO)
        {
            var createdProject = await _projectService.CreateProjectAsync(projectDTO);

            var response = new Project
            {

                Name = createdProject.Name,
                CreatedAt = createdProject.CreatedAt
            };

            return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.Id }, response);
        }

        // PUT: api/Project/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            try
            {
                await _projectService.UpdateProjectAsync(project);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Project/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            try
            {
                await _projectService.DeleteProjectAsync(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
