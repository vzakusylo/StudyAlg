using Dotmim.Sync;
using Dotmim.Sync.SampleConsole;
using Dotmim.Sync.SqlServer;
using System;
using System.IO;
using System.Threading.Tasks;
using Dotmim.Sync.Enumerations;

internal class Program
{
    public static string serverDbName = "Server";
    public static string clientDbName = "Client";
    public static string[] allTables = new string[] {"ProductCategory",
                                                    "ProductModel", "Product",
                                                    "Address", "Customer", "CustomerAddress",
                                                    "SalesOrderHeader", "SalesOrderDetail" };
    private static void Main(string[] args)
    {


        SyncAdvAsync().GetAwaiter().GetResult();
        Console.ReadLine();
    }

    private async static Task RunAsync()
    {
        // Create databases 
        await DbHelper.EnsureDatabasesAsync(serverDbName);
        await DbHelper.CreateDatabaseAsync(clientDbName);

        // Launch Sync
        await SynchronizeAsync();
    }


    private static async Task SyncAdvAsync()
    {
        // Sql Server provider, the master.
        var serverProvider = new SqlSyncProvider(@"Server=tcp:127.0.0.1,5433;Initial Catalog=Usavc.Services.AppointmentDb;User Id=sa;Password=Pass@word");
        //Server=192.168.5.125;Database=medman;UID=pub; PWD=0c95PYIg;


        // Sql Client provider for a Sql Server <=> Sql Server
        var clientProvider = new SqlSyncProvider("Server=tcp:127.0.0.1,5433;Initial Catalog=Usavc.Services.AppointmentDb1;User Id=sa;Password=Pass@word");
        //Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;
        //Server=tcp:127.0.0.1,5433;Initial Catalog=Usavc.Services.AppointmentDb;User Id=sa;Password=Pass@word

        // Tables involved in the sync process:
        var tables = new string[] { "ReasonCode" };

        // Sync orchestrator
        var agent = new SyncAgent(clientProvider, serverProvider, tables);

        do
        {
            var s = await agent.SynchronizeAsync(SyncType.Normal);
            Console.WriteLine($"Total Changes downloaded : {s.TotalChangesDownloaded}");

        } while (Console.ReadKey().Key != ConsoleKey.Escape);
    }


    /// <summary>
    /// Launch a simple sync, over TCP network, each sql server (client and server are reachable through TCP cp
    /// </summary>
    /// <returns></returns>
    private static async Task SynchronizeAsync()
    {
        // Create 2 Sql Sync providers
        var serverProvider = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString(serverDbName));
        var clientProvider = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString(clientDbName));

        // Tables involved in the sync process:
        var tables = allTables;

        // Creating an agent that will handle all the process
        var agent = new SyncAgent(clientProvider, serverProvider, tables);

        // Using the Progress pattern to handle progession during the synchronization
        var progress = new Progress<ProgressArgs>(s => Console.WriteLine($"[client]: {s.Context.SyncStage}:\t{s.Message}"));


        // Setting configuration options
        agent.SetConfiguration(s =>
        {
            s.ScopeInfoTableName = "tscopeinfo";
            s.SerializationFormat = Dotmim.Sync.Enumerations.SerializationFormat.Binary;
            s.StoredProceduresPrefix = "s";
            s.StoredProceduresSuffix = "";
            s.TrackingTablesPrefix = "t";
            s.TrackingTablesSuffix = "";
        });

        agent.SetOptions(opt =>
        {
            opt.BatchDirectory = Path.Combine(SyncOptions.GetDefaultUserBatchDiretory(), "sync");
            opt.BatchSize = 100;
            opt.CleanMetadatas = true;
            opt.UseBulkOperations = true;
            opt.UseVerboseErrors = false;
        });


        do
        {
            Console.Clear();
            Console.WriteLine("Sync Start");
            try
            {
                // Launch the sync process
                var s1 = await agent.SynchronizeAsync(progress);

                // Write results
                Console.WriteLine(s1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            //Console.WriteLine("Sync Ended. Press a key to start again, or Escapte to end");
        } while (Console.ReadKey().Key != ConsoleKey.Escape);

        Console.WriteLine("End");
    }

    /// <summary>
    /// Launch a simple sync, over TCP network, each sql server (client and server are reachable through TCP cp
    /// </summary>
    /// <returns></returns>
    private static async Task SynchronizeExistingTablesAsync()
    {
        string serverName = "ServerTablesExist";
        string clientName = "ClientsTablesExist";

        await DbHelper.EnsureDatabasesAsync(serverName);
        await DbHelper.EnsureDatabasesAsync(clientName);

        // Create 2 Sql Sync providers
        var serverProvider = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString(serverName));
        var clientProvider = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString(clientName));

        // Tables involved in the sync process:
        var tables = allTables;

        // Creating an agent that will handle all the process
        var agent = new SyncAgent(clientProvider, serverProvider, tables);

        // Using the Progress pattern to handle progession during the synchronization
        var progress = new Progress<ProgressArgs>(s => Console.WriteLine($"[client]: {s.Context.SyncStage}:\t{s.Message}"));




        // Setting configuration options
        agent.SetConfiguration(s =>
        {
            s.ScopeInfoTableName = "tscopeinfo";
            s.SerializationFormat = Dotmim.Sync.Enumerations.SerializationFormat.Binary;
            s.StoredProceduresPrefix = "s";
            s.StoredProceduresSuffix = "";
            s.TrackingTablesPrefix = "t";
            s.TrackingTablesSuffix = "";
        });

        agent.SetOptions(opt =>
        {
            opt.BatchDirectory = Path.Combine(SyncOptions.GetDefaultUserBatchDiretory(), "sync");
            opt.BatchSize = 100;
            opt.CleanMetadatas = true;
            opt.UseBulkOperations = true;
            opt.UseVerboseErrors = false;
        });


        var remoteProvider = agent.RemoteProvider as CoreProvider;

        var dpAction = new Action<DatabaseProvisionedArgs>(args =>
        {
            Console.WriteLine($"-- [InterceptDatabaseProvisioned] -- ");

            var sql = $"Update tscopeinfo set scope_last_sync_timestamp = 0 where [scope_is_local] = 1";

            var cmd = args.Connection.CreateCommand();
            cmd.Transaction = args.Transaction;
            cmd.CommandText = sql;

            cmd.ExecuteNonQuery();

        });

        remoteProvider.InterceptDatabaseProvisioned(dpAction);

        agent.LocalProvider.InterceptDatabaseProvisioned(dpAction);

        do
        {
            Console.Clear();
            Console.WriteLine("Sync Start");
            try
            {
                // Launch the sync process
                var s1 = await agent.SynchronizeAsync(progress);

                // Write results
                Console.WriteLine(s1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            //Console.WriteLine("Sync Ended. Press a key to start again, or Escapte to end");
        } while (Console.ReadKey().Key != ConsoleKey.Escape);

        Console.WriteLine("End");
    }



    private static async Task SynchronizeOSAsync()
    {
        // Create 2 Sql Sync providers
        var serverProvider = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString("OptionsServer"));
        var clientProvider = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString("OptionsClient"));

        // Tables involved in the sync process:
        var tables = new string[] { "ObjectSettings", "ObjectSettingValues" };

        // Creating an agent that will handle all the process
        var agent = new SyncAgent(clientProvider, serverProvider, tables);

        // Using the Progress pattern to handle progession during the synchronization
        var progress = new Progress<ProgressArgs>(s => Console.WriteLine($"[client]: {s.Context.SyncStage}:\t{s.Message}"));

        do
        {
            Console.Clear();
            Console.WriteLine("Sync Start");
            try
            {
                // Launch the sync process
                var s1 = await agent.SynchronizeAsync(progress);

                // Write results
                Console.WriteLine(s1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            //Console.WriteLine("Sync Ended. Press a key to start again, or Escapte to end");
        } while (Console.ReadKey().Key != ConsoleKey.Escape);

        Console.WriteLine("End");
    }
}