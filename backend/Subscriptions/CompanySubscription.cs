using Entities;
using EntityGraphQL.Schema;

public class CompanySubscriptions
{
    [GraphQLSubscription("Subscription to new messages")]
    public static IObservable<Company> OnMessage(ComapnyService comapnyService)
    {
       return comapnyService.Subscribe(); 
    }
}