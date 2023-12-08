using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using portfolioApi.Context;
using portfolioApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly ProfileContext _context;

    public ProfileController(ProfileContext context)
    {
        _context = context;
    }

    [HttpGet("ProtectedData")]
    [Authorize]
    public IActionResult GetProtectedData()
    {
        // Retrieve and return protected data
        // ...

        return Ok(new { data = "This is protected data" });
    }

    [HttpPost("SavePhoneNumber")]
    public IActionResult SavePhoneNumber([FromBody] PhoneNumberRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                return BadRequest("Phone number is required.");
            }

            var phoneNumberRequest = new PhoneNumberRequest
            {
                PhoneNumber = request.PhoneNumber
            };

            _context.PhoneNumberRequest.Add(phoneNumberRequest);
            _context.SaveChanges();

            // Generate a verification code
            string verificationCode = GenerateVerificationCode();

            return Ok(new { success = true, verificationCode });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    private string GenerateVerificationCode()
    {
        Random random = new Random();
        return random.Next(100000, 999999).ToString();
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
            _context.SaveChanges();

            return Ok("Form submitted successfully");
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
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
}