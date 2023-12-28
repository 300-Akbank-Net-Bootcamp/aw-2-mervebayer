using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vb.Api.Dtos;
using Vb.Data;
using Vb.Data.Entity;

namespace VbApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressContoller : ControllerBase
{
    private readonly VbDbContext _dbContext;

    public AddressContoller(VbDbContext dbContext)
    {
        _dbContext = dbContext;   
    }
 
    [HttpGet]
    public async Task<List<Address>> Get()
    {
        return await _dbContext.Set<Address>()
            .Where(x => x.IsActive == true)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<Address> Get (int id){
        var address = await _dbContext.Set<Address>().FirstOrDefaultAsync(x => x.Id == id && x.IsActive == true);
        return address;
    }

    [HttpPost]
    public async Task Post([FromBody] AddressDto addressDto)
    {
        var address = new Address{
            CustomerId = addressDto.CustomerId,
            Address1 = addressDto.Address1,
            Address2 = addressDto.Address2,
            Country = addressDto.Country,
            City = addressDto.City,
            County = addressDto.County,
            PostalCode = addressDto.PostalCode,
            IsDefault = addressDto.IsDefault
        };

        await _dbContext.Set<Address>().AddAsync(address);
        await _dbContext.SaveChangesAsync();
    }

    [HttpPut("{id}")] 
    public async Task Put(int id, [FromBody]AddressDto addressDto)
    {
        var address = await _dbContext.Set<Address>().FirstOrDefaultAsync(x => x.Id == id);
        if(address != null)
        {
            address.Address1 = addressDto.Address1 ?? address.Address1;
            address.Address2 = addressDto.Address2 ?? address.Address2;
            address.Country = addressDto.Country ?? address.Country;
            address.City = addressDto.City ?? address.City;
            address.County = addressDto.County ?? address.County;
            address.PostalCode = addressDto.PostalCode ?? address.PostalCode;
            address.IsDefault = addressDto.IsDefault != default;
        }
        await _dbContext.SaveChangesAsync();
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        var address = await _dbContext.Set<Address>().FirstOrDefaultAsync(x => x.Id == id);
        address.IsActive = false;
        await _dbContext.SaveChangesAsync();
    }

}