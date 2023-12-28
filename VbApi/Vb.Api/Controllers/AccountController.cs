using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vb.Api.Dtos;
using Vb.Data;
using Vb.Data.Entity;
namespace VbApi.Controllers;

[Route("api/[controller]")]
[ApiController]

public class AccountController : ControllerBase
{
    private readonly VbDbContext _dbContext;

    public AccountController(VbDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<List<Account>> Get(){
        return await _dbContext.Set<Account>().Where(x => x.IsActive == true).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<Account> Get(int id){
        var account = await _dbContext.Set<Account>()
            .Include(x => x.AccountTransactions)
            .Include(x => x.EftTransactions)
            .Where(x => x.Id == id && x.IsActive == true ).FirstOrDefaultAsync();
        return account;    
    }

    [HttpPost]
    public async Task Post([FromBody] AccountDto accountDto)
    {
        var account = new Account{
            CustomerId = accountDto.CustomerId,
            AccountNumber = accountDto.AccountNumber,
            IBAN = accountDto.IBAN,
            Balance = accountDto.Balance,
            CurrencyType = accountDto.CurrencyType,
            Name = accountDto.Name,
            OpenDate = accountDto.OpenDate
        };
        await _dbContext.Set<Account>().AddAsync(account);
        await _dbContext.SaveChangesAsync();
    }

    [HttpPut("{id}")]
    public async Task Put(int id, [FromBody]AccountDto accountDto)
    {
        var account = await _dbContext.Set<Account>().FirstOrDefaultAsync(x => x.Id == id);
        if(account != null)
        {
            account.Name = accountDto.Name;
            account.Balance = accountDto.Balance;
        }
        await _dbContext.SaveChangesAsync();
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        var account = await _dbContext.Set<Account>().FirstOrDefaultAsync(x => x.Id == id);
        account.IsActive = false;
        await _dbContext.SaveChangesAsync();
    }
}