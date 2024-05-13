using ApplicationContext;
using Entities;
using EntityGraphQL.Subscriptions;

public class ComapnyService
{
    private readonly Broadcaster<Company> broadcaster = new();

    public Company PostCompany(CibContext db, string name, int id, string inn, string status)
    {

        var company = new Company
        {
            Name = name,
            Id = id,
            Inn = inn,
            Status = status
        };

        db.Companies.Add(company);
        db.SaveChanges();

        broadcaster.OnNext(company);

        return company;
    }
    public IObservable<Company> Subscribe()
    {
        return broadcaster;
    }
}