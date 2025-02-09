﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using portfolioApi.Context;
using portfolioApi.Models;
using portfolioApi.Services;

[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly ProfileContext _context;
    private readonly EmailService _emailService;

    public ProfileController(ProfileContext context, EmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    [HttpGet("ProfileData")]
    public async Task<IActionResult> GetProfileInfo()
    {
        var profile = await _context.Profiles.FirstOrDefaultAsync();

        if (profile == null)
        {
            return NotFound("User not found");
        }

        return Ok(profile);
    }

    [HttpGet("ProfileEducations")]
    public async Task<IActionResult> GetProfileEducations()
    {
        var educations = await _context.ProfileEducations.ToListAsync();

        if (educations == null || !educations.Any())
        {
            return NotFound("No education found");
        }

        return Ok(educations);
    }

    [HttpGet("ProfileEducation")]
    public async Task<IActionResult> GetProfileEducation(int id)
    {
        var profileEducation = await _context.ProfileEducations.FindAsync(id);

        if (profileEducation == null)
        {
            return NotFound("No project found");
        }

        return Ok(profileEducation);
    }

    [HttpGet("ProfileExperiences")]
    public async Task<IActionResult> GetProfileExperiences()
    {
        var experiences = await _context.ProfileExperiences.ToListAsync();

        if (experiences == null || !experiences.Any())
        {
            return NotFound("No experiences found");
        }

        return Ok(experiences);
    }

    [HttpGet("ProfileExperience")]
    public async Task<IActionResult> GetProfileExperience(int id)
    {
        var profileExperience = await _context.ProfileExperiences.FindAsync(id);

        if (profileExperience == null)
        {
            return NotFound("No project found");
        }

        return Ok(profileExperience);
    }

    [HttpGet("ProfileLinks")]
    public async Task<IActionResult> GetProfileLinks()
    {
        var links = await _context.ProfileLinks.ToListAsync();

        if (links == null || !links.Any())
        {
            return NotFound("No links found");
        }

        return Ok(links);
    }

    [HttpGet("Projects")]
    public async Task<IActionResult> GetProjects()
    {
        var projects = await _context.Projects.ToListAsync();

        if (projects == null || !projects.Any())
        {
            return NotFound("No projects found");
        }

        return Ok(projects);
    }

    [HttpGet("Project")]
    public async Task<IActionResult> GetProject(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        
        if (project == null)
        {
            return NotFound("No project found");
        }

        return Ok(project);
    }

    [HttpPost("Contact")]
    public async Task<IActionResult> AddContact([FromBody] Contact contact)
    {
        if (contact == null)
        {
            return BadRequest("Invalid data");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            // Save the contact data to the database or perform other actions
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            // Send email notification
            await _emailService.SendEmailAsync(
               "lirije11@hotmail.com",
               "New Contact Form Submission",
               $"Name: {contact.Name}\nEmail: {contact.Email}\nPhonenumber: {contact.Phone}\nMessage: {contact.Message}");

            return Ok("Form submitted successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing the form");
        }
    }

    [HttpGet("Skills")]
    public async Task<IActionResult> GetSkills()
    {
        var skills = await _context.Skills.ToListAsync();

        if (skills == null || !skills.Any())
        {
            return NotFound("No projects found");
        }

        return Ok(skills);
    }

    [HttpGet("Skill")]
    public async Task<IActionResult> GetSkill(int id)
    {
        var skill = await _context.Skills.FindAsync(id);

        if (skill == null)
        {
            return NotFound("No project found");
        }

        return Ok(skill);
    }

    #region utilites

    [HttpGet("Utilities")]
    public async Task<IActionResult> GetUtilities()
    {
        var utilities = await _context.Utilities.ToListAsync();

        if (utilities == null || !utilities.Any())
        {
            return NotFound("No projects found");
        }

        return Ok(utilities);
    }

    [HttpGet("Utility")]
    public async Task<IActionResult> GetUtility(int id)
    {
        var utility = await _context.Utilities.FindAsync(id);

        if (utility == null)
        {
            return NotFound("No project found");
        }

        return Ok(utility);
    }

    [HttpDelete("Projects/{projectId}/Utilities/{utilityId}")]
    public async Task<IActionResult> RemoveUtilityFromProject(int projectId, int utilityId)
    {
        var projectUtility = await _context.ProjectUtilities
            .FirstOrDefaultAsync(pu => pu.ProjectId == projectId && pu.UtilityId == utilityId);

        if (projectUtility == null)
        {
            return NotFound("The association between Project and Utility does not exist.");
        }

        _context.ProjectUtilities.Remove(projectUtility);
        await _context.SaveChangesAsync();

        return Ok("Utility removed from Project successfully.");
    }

    [HttpGet("Projects/{projectId}/Utilities")]
    public async Task<IActionResult> GetUtilitiesForProject(int projectId)
    {
        var project = await _context.Projects
            .Include(p => p.ProjectUtilities)
            .ThenInclude(pu => pu.Utility)
            .FirstOrDefaultAsync(p => p.Id == projectId);

        if (project == null)
        {
            return NotFound("Project not found.");
        }

        var utilities = project.ProjectUtilities.Select(pu => pu.Utility).ToList();
        return Ok(utilities);
    }

    [HttpPost("Projects/{projectId}/Utilities/{utilityId}")]
    public async Task<IActionResult> AddUtilityToProject(int projectId, int utilityId)
    {
        var project = await _context.Projects.FindAsync(projectId);
        var utility = await _context.Utilities.FindAsync(utilityId);

        if (project == null || utility == null)
        {
            return NotFound("Project or Utility not found.");
        }

        // check if relation already exists
        if (await _context.ProjectUtilities.AnyAsync(pu => pu.ProjectId == projectId && pu.UtilityId == utilityId))
        {
            return Conflict("Utility is already associated with this Project.");
        }

        // create relation
        var projectUtility = new ProjectUtility
        {
            ProjectId = projectId,
            UtilityId = utilityId
        };

        _context.ProjectUtilities.Add(projectUtility);
        await _context.SaveChangesAsync();

        return Ok("Utility added to Project successfully.");
    }
    #endregion
}