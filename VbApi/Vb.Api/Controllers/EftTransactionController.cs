using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vb.Api.Dtos;
using Vb.Data;
using Vb.Data.Entity;

namespace VbApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EftTransactionController : ControllerBase
{
    private readonly VbDbContext _dbContext;

    public EftTransactionController(VbDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<List<EftTransaction>> Get()
    {
        return await _dbContext.Set<EftTransaction>().Where(x => x.IsActive == true).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<EftTransaction> Get(int id)
    {
        var eftTransaction = await _dbContext.Set<EftTransaction>()
            .FirstOrDefaultAsync(x => x.Id == id && x.IsActive == true);
            
        return eftTransaction;
    }

    [HttpPost]
    public async Task Post([FromBody] EftTransactionDto eftTransactionDto)
    {
        var eftTransaction = new EftTransaction{
            AccountId = eftTransactionDto.AccountId,
            ReferenceNumber = eftTransactionDto.ReferenceNumber,
            TransactionDate = eftTransactionDto.TransactionDate,
            Amount = eftTransactionDto.Amount,
            Description = eftTransactionDto.Description,
            SenderAccount = eftTransactionDto.SenderAccount,
            SenderIban = eftTransactionDto.SenderIban,
            SenderName = eftTransactionDto.SenderName
        };
        await _dbContext.Set<EftTransaction>().AddAsync(eftTransaction);
        await _dbContext.SaveChangesAsync();
    }

    [HttpPut("{id}")]
    public async Task Put(int id, [FromBody]EftTransactionDto eftTransaction)
    {
        var eftFromDb = await _dbContext.Set<EftTransaction>().FirstOrDefaultAsync(x => x.Id == id);
        if(eftFromDb != null)
        {
            eftFromDb.TransactionDate = eftTransaction.TransactionDate;
            eftFromDb.Amount = eftTransaction.Amount;
        }
        await _dbContext.SaveChangesAsync();
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        var eft = await _dbContext.Set<EftTransaction>().FirstOrDefaultAsync(x => x.Id == id);
        eft.IsActive = false;
        await _dbContext.SaveChangesAsync();
    }

}
