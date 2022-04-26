using Core.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly ApplicationDbContext _context;

    public CurrencyRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public DbSet<Currency> GetAll()
    {
        return  _context.Currencies;
    }

    public Currency Get(string name)
    {
        return _context.Currencies.FirstOrDefault(currency => currency.Name == name)!;
    }

    public Currency Add(Currency currency)
    {
        _context.Currencies.Add(currency);
        _context.SaveChanges();
        
        return currency;
    }

    public bool Delete(string name)
    {
        var currency =  _context.Currencies.FirstOrDefault(cur => cur.Name==name)!;
        if (currency != null)
        {
            currency.IsActive = false;
            Update(currency);
            return true;
        }
        return false;
    }

    public Currency Update(Currency updatedCurrency)
    {
        var model = _context.Currencies.Attach(updatedCurrency);
        model.State = EntityState.Modified;
        _context.SaveChanges();
        
        return updatedCurrency;
    }
}