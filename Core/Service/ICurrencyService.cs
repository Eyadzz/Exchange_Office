using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Service;

public interface ICurrencyService
{
    Currency Get(string name);
    DbSet<Currency> GetAll();
    Currency Add(Currency currency);
    void Delete(string name);
    Currency Update(Currency updatedCurrency);
}