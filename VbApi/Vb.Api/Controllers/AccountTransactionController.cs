using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vb.Api.Dtos;
using Vb.Data;
using Vb.Data.Entity;

namespace VbApi.Controllers;

[Route("api/[controller]")]
[ApiController]

public class AccountTransactionController : ControllerBase
{
    private readonly VbDbContext _dbcontext;

    public AccountTransactionController(VbDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    [HttpGet]
    public async Task<List<AccountTransaction>> Get()
    {
        return await _dbcontext.Set<AccountTransaction>().Where(x => x.IsActive == true).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<AccountTransaction> Get(int id)
    {
        var accountTransaction = await _dbcontext.Set<AccountTransaction>()
            .Where(x => x.Id == id).FirstOrDefaultAsync();
        
        return accountTransaction;
    }

    [HttpPost]
    public async Task Post([FromBody] AccountTransactionDto accountTransactionDto)
    {
        var accountTransaction = new AccountTransaction{
            AccountId = accountTransactionDto.AccountId,
            ReferenceNumber = accountTransactionDto.ReferenceNumber,
            TransactionDate = accountTransactionDto.TransactionDate,
            Amount = accountTransactionDto.Amount,
            Description = accountTransactionDto.Description,
            TransferType = accountTransactionDto.TransferType
        };

        await _dbcontext.Set<AccountTransaction>().AddAsync(accountTransaction);
        await _dbcontext.SaveChangesAsync();
    } 

    [HttpPut("{id}")]
    public async Task Put(int id, [FromBody] AccountTransactionDto accountTransactionDto)
    {
        var accountTransaction = new AccountTransaction{
            AccountId = accountTransactionDto.AccountId,
            ReferenceNumber = accountTransactionDto.ReferenceNumber,
            TransactionDate = accountTransactionDto.TransactionDate,
            Amount = accountTransactionDto.Amount,
            Description = accountTransactionDto.Description,
            TransferType = accountTransactionDto.TransferType
        };

        await _dbcontext.Set<AccountTransaction>().AddAsync(accountTransaction);
        await _dbcontext.SaveChangesAsync();
    } 


    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        var accountTransaction = await _dbcontext.Set<AccountTransaction>().FirstOrDefaultAsync(x => x.Id == id);
        accountTransaction.IsActive = false;
        await _dbcontext.SaveChangesAsync();
    }
}