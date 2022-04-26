using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Repository;

public interface ICurrencyRepository
{
    DbSet<Currency> GetAll();
    Currency Get(string name);
    Currency Add(Currency currency);
    bool Delete(string name);
    Currency Update(Currency updatedCurrency);
}