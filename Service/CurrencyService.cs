using Core.Exception;
using Core.Repository;
using Core.Service;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Service;

public class CurrencyService : ICurrencyService
{
    private readonly ICurrencyRepository _repository;

    public CurrencyService(ICurrencyRepository repository)
    {
        _repository = repository;
    }
    public Currency Get(string name)
    {
        return _repository.Get(name) ?? throw new CurrencyNotFoundException(name);
    }

    public DbSet<Currency> GetAll()
    {
        return _repository.GetAll();
    }

    public Currency Add(Currency currency)
    {
        return _repository.Add(currency);
    }

    public void Delete(string name)
    {
        if (!_repository.Delete(name))
            throw new CurrencyNotFoundException(name);
    }

    public Currency Update(Currency updatedCurrency)
    {
        return _repository.Update(updatedCurrency);
    }
}