using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vb.Api.Dtos;
using Vb.Data;
using Vb.Data.Entity;

namespace VbApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly VbDbContext _dbContext;

    public ContactController(VbDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<List<Contact>> Get()
    {
        return await _dbContext.Set<Contact>().Where(x => x.IsActive == true)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<Contact> Get(int id)
    {
        var contact =  await _dbContext.Set<Contact>()

           .FirstOrDefaultAsync(x => x.Id == id && x.IsActive == true);
       
        return contact;
    }   

    [HttpPost]
    public async Task Post([FromBody] ContactDto contactDto)
    {
        var contact = new Contact{
            CustomerId = contactDto.CustomerId,
            ContactType = contactDto.ContactType,
            Information = contactDto.Information,
            IsDefault = contactDto.IsDefault
        };
        await _dbContext.Set<Contact>().AddAsync(contact);
        await _dbContext.SaveChangesAsync();
    }

    [HttpPut("{id}")]
    public async Task Put(int id, [FromBody]ContactDto contactDto)
    {
        var contact = await _dbContext.Set<Contact>().FirstOrDefaultAsync(x => x.Id == id);
        if(contact != null)
        {
            contact.ContactType = contactDto.ContactType;
            contact.Information = contactDto.Information;
            contact.IsDefault = contactDto.IsDefault;
        };
        await _dbContext.SaveChangesAsync();
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        var contact = await _dbContext.Set<Contact>().FirstOrDefaultAsync(x => x.Id == id);
        contact.IsActive = false;
        await _dbContext.SaveChangesAsync();
    }
    
}