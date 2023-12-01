using GQLService.GraphQL;
using GraphQL.Server.Ui.Voyager;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    // .AddType<HumanType>()
    .AddSorting()
    .AddFiltering();
    // .AddInMemorySubscriptions();

var app = builder.Build();

app.UseWebSockets(); //增加Websocket的宣告，這部分需要優先宣告

app.MapGet("/", () => "Hello World!");

//增加graphql的路由設定
app.UseRouting().UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

// 增加Voyager中間件並配置URL
// app.UseGraphQLVoyager(new VoyagerOptions { GraphQLEndPoint = "/graphql", },
// "/graphql-voyager");

app.Run();
