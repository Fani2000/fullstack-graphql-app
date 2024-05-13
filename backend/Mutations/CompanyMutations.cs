using System.Linq.Expressions;
using ApplicationContext;
using Entities;
using EntityGraphQL.Schema;

public class CompanyMutations
{
    [GraphQLMutation("Add a new company to the system")]
    public static Expression<Func<CibContext, Company>> AddCompany(CibContext db, string name, int id, string inn, string status, ComapnyService service)
    {
        var company = service.PostCompany(db, name, id, inn, status);

        return (ctx) => ctx.Companies.First(x => x.Id == company.Id);
    }
}