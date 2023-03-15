
using Microsoft.AspNetCore.SignalR.Client;
using TestLaverie;
using static TestLaverie.Machine;

public class SignalRService
    {
        public SignalRService(string hubconnectionURL)
        {
            HubConnectionURL = hubconnectionURL;
        }

        public bool Initialized { get; set; }

        public string HubConnectionURL { get; set; }
        public HubConnection HubConnection { get; set; }

        public async Task Initialize()
        {

            HubConnection = new HubConnectionBuilder()
                    .WithUrl(HubConnectionURL, options =>
                    {

                    }).WithAutomaticReconnect()
                    .Build();

            await Connect();
         await   ListenForChanges();

        }
        public async Task SendMessage(int id,Machine.EtatMachine etat)
        {
            if (HubConnection.State == HubConnectionState.Connected)
            {
                await HubConnection.InvokeAsync("test", "set machine with id "+id+" to "+etat);
            
            }
        }

        public async Task ListenForChanges()
        {
      
        if (HubConnection.State == HubConnectionState.Connected)
            {
                {
                
                    HubConnection.On<Machine>("MachineAdded", async (data) =>
                    {
                        Console.WriteLine("Nouveau etat machine recu: machine numero  "+data.IdMachine +" "+data.etatMachine);
                     
                    });
                }
            }

        
        }


        public async Task Connect()
        {
            if (this.HubConnection.State == HubConnectionState.Disconnected)
            {
                try
                {
                    await HubConnection.StartAsync();
                }
                catch (Exception ex)
                {
                }
            }
        }
    public async Task<Machine> GetDataFromSignalR()
    {
        /*  if (HubConnection.State == HubConnectionState.Connected)*/

        Machine machi = null;
            {
                HubConnection.On<Machine>("MachineAdded", async (data) =>
                {
                   
                    machi = data;
                });
            }
          

        
        return machi;
    }

}






