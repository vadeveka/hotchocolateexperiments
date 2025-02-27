using Demo;

var builder = WebApplication.CreateBuilder(args);



builder.Services
    .AddGraphQLServer()
    .AllowIntrospection(false)
    .AddMaxExecutionDepthRule(10)
    .UseTimeout()
    .ModifyRequestOptions(o =>
    {
        o.Complexity.Enable = true;
        o.Complexity.DefaultComplexity = 1;
        o.Complexity.DefaultResolverComplexity = 1;

        // Limits you to a max operation involving 100*100 fields.
        o.Complexity.MaximumAllowed = 1000;

    })
    .UseDefaultPipeline()
    .AddQueryType<Query>();

/* 500 internal error
{
	"errors": [
		{
			"message": "The specified root type `Subscription` is not supported by this server."
		}
	]
}

builder.Services
    .AddGraphQLServer()
    .ModifyRequestOptions(o =>
     {
         o.Complexity.ApplyDefaults = true;
         o.Complexity.DefaultComplexity = 1;
         o.Complexity.DefaultResolverComplexity = 5;
     })
        .AddQueryType<Query>();
*/

var app = builder.Build();

app.MapGraphQL();

app.Run();
