using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using ApplicationContext;
using Entities;
using EntityGraphQL.Schema;

public class CompanyMutations
{
    [GraphQLMutation("Add a new company to the system")]
    public static Expression<Func<CibContext, Company>> AddCompany(CibContext db, AddCompanyArgs args, ComapnyService service)
    {
        var company = service.PostCompany(db, args.Name, args.Id, args.Inn, args.Status);

        return (ctx) => ctx.Companies.First(x => x.Id == company.Id);
    }
}

[GraphQLArguments]
public class AddCompanyArgs
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Id is required")]
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Inn is required")]
    public string Inn { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Status is required")]
    public string Status { get; set; }
}