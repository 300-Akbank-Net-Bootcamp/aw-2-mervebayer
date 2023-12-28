using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vb.Api.Dtos;
using Vb.Data;
using Vb.Data.Entity;

namespace VbApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly VbDbContext dbContext;

    public CustomersController(VbDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<List<Customer>> Get()
    {
        return await dbContext.Set<Customer>().Where(x => x.IsActive == true)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<Customer> Get(int id)
    {
        var customer =  await dbContext.Set<Customer>()
            .Include(x=> x.Accounts)
            .Include(x=> x.Addresses)
            .Include(x=> x.Contacts)
            .FirstOrDefaultAsync(x => x.Id == id && x.IsActive == true);
       
        return customer;
    }

    [HttpPost]
    public async Task Post([FromBody] CustomerDto customerDto)
    {
        var customer = new Customer{
            IdentityNumber = customerDto.IdentityNumber,
            FirstName = customerDto.FirstName,
            LastName = customerDto.LastName,
            CustomerNumber = customerDto.CustomerNumber,
            DateOfBirth = customerDto.DateOfBirth,
            LastActivityDate = customerDto.LastActivityDate
        };
        await dbContext.Set<Customer>().AddAsync(customer);
        await dbContext.SaveChangesAsync();
    }

    [HttpPut("{id}")]
    public async Task Put(int id, [FromBody] CustomerDto customerDto)
    {
        var fromdb = await dbContext.Set<Customer>().FirstOrDefaultAsync(x => x.Id == id);
        if(fromdb != null)
        {
            fromdb.FirstName = customerDto.FirstName;
            fromdb.LastName = customerDto.LastName;
        }
        await dbContext.SaveChangesAsync();
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        var fromdb = await dbContext.Set<Customer>().Where(x => x.Id == id).FirstOrDefaultAsync();
        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync();
    }
}