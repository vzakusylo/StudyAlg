using System.Text;
using Neo4j.Driver;

public class HelloWorldExample : IDisposable
{
    public static async Task Main()
    {
        using var greeting = new HelloWorldExample("bolt://localhost:7687", "neo4j", "password");
        
        await greeting.CreateNode("hello word");
    }

    private bool _disposed;
    private readonly IDriver _driver;

    ~HelloWorldExample() => Dispose(false);

    public HelloWorldExample(string uri, string user, string password)
    {
        _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
    }

    public async Task PrintGreeting(string message)
    {
        await using var session = _driver.AsyncSession();
        var greeting = session.WriteTransactionAsync( async tx =>  
        {
            var resultCursor = await  tx.RunAsync("CREATE (a:Greeting) " +
                                                  "SET a.message = $message " +
                                                  "RETURN a.message + ', from node ' + id(a)", new {message});
            var result = await resultCursor.SingleAsync();
            return result[0].As<string>();
        });
        Console.Write(greeting);


    }

    public async Task CreateNode(string name)
    {
        var statementText = new StringBuilder();
        statementText.Append("CREATE (person:Person {name: $name})");
        var statementParameters = new Dictionary<string, object>
        {
            {"name", name }
        };

        var session = this._driver.AsyncSession();
        var result = await session.WriteTransactionAsync(tx => tx.RunAsync(statementText.ToString(), statementParameters));
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _driver?.Dispose();
        }

        _disposed = true;
    }
}