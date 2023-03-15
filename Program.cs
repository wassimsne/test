using Microsoft.AspNetCore.SignalR.Client;
using RestSharp;
using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

using TestLaverie;
using static TestLaverie.Machine;

 public class LoadTest
{
      
    static int numberOfRequests = 100;
    static string baseUrl = "https://192.168.176.1:45455/";

    static void Main(string[] args)
    {
        Laverie lav = new Laverie
        {
            IdLav = 12,
            Nom = "Le Printemp",
            Adresse = "Rue Monji Slim",
            Telephone = 54195779,
            Responsable = "Wassim",
            Username = "wassimsne",
            Password = "rec360"
        };
        /*        Machine mach = new Machine { IdMachine = 16, Laverie = lav, DureeToalDeFonctionnement = 9, etat = Machine.Etat.horsService, NumeroCode = 20 };
        */
        Machine[] machines = new Machine[]
      {
            new Machine{IdMachine = 23,Laverie = lav,DureeToalDeFonctionnement = 9,etat = Machine.Etat.horsService,NumeroCode = 1},
            new Machine {IdMachine = 24,Laverie = lav,DureeToalDeFonctionnement = 9,etat = Machine.Etat.horsService,NumeroCode = 2 },
            new Machine {IdMachine = 25,Laverie = lav,DureeToalDeFonctionnement = 9,etat = Machine.Etat.horsService,NumeroCode = 3 }
      };
        for (int e = 0; e < machines.Length; e++)
        {
            PutRequest(machines[e].IdMachine, machines[e]);

        }

        SignalRService signalRClient = new(baseUrl + "datahub");
        Task task = signalRClient.Initialize();
        signalRClient.HubConnection.On<Machine>("MachineAdded", async (machi) =>
        {

            MachineAddedEvent(machi, machines);
        });



        for (int j = 0; j < numberOfRequests; j++)
        {

            // Define the request method

            Listmachine(machines);



            // Ask the user to choose an option.  
            Console.WriteLine("qu'est ce que vous voulez faire:");
            Console.WriteLine("\ta - Demarrer une machine");
            Console.WriteLine("\tb - Arreter une machine");
            Console.WriteLine("\tc - Demarrer tout les machines");
            Console.WriteLine("\td - Arreter tout les machines");
            Console.Write("Vos options? ");
            // Use a switch statement to do the math.  
            switch (Console.ReadLine())
            {
                case "a":
                    {
                        Console.WriteLine("Choisir l'ID de la machine a demarrer ");
                        int idmachine = int.Parse(Console.ReadLine());
                        var x = machines.FirstOrDefault(u => u.IdMachine.Equals(Convert.ToInt32(idmachine)));
                        if (x is not null)
                        {
                            x.etat = Etat.enMarche;
                        }

                        PutRequest(idmachine, x);
                        signalRClient.SendMessage(Convert.ToInt32(idmachine), Etat.enMarche);

                    }
                    break;
                case "b":
                    Console.WriteLine("Choisir l'ID de la machine a arreter ");
                    int idmachin = int.Parse(Console.ReadLine());
                    var a = machines.FirstOrDefault(u => u.IdMachine.Equals(Convert.ToInt32(idmachin)));
                    if (a is not null)
                    {
                        a.etat = Etat.arret;
                    }

                    PutRequest(idmachin, a);
                    signalRClient.SendMessage(Convert.ToInt32(idmachin), Etat.arret);
                    /* //signalRClient.SendMessage(idmachine, false);                       
                     var client = new RestClient(baseUrl + "api/machine/" + idmachin);
                     var request = new RestRequest(Method.PUT);
                     request.AddJsonBody(a);
                     var response = client.Execute(request);
                     // Print the status code of the response
                     Console.WriteLine(response.StatusCode);
                     signalRClient.SendMessage(idmachin,false);*/
                    break;
                case "c":
                    Console.WriteLine("Demarrer tout les machines");

                    //signalRClient.SendMessage(idmachine, false);
                    for (int y = 0; y < machines.Length; y++)
                    {
                        Machine machine = machines[y];

                        if (machine is not null)
                        {
                            machine.etat = Etat.enMarche;
                        }

                        PutRequest(machine.IdMachine, machine);
                        signalRClient.SendMessage(machine.IdMachine, Etat.enMarche);
                    }
                    break;
                case "d":
                    Console.WriteLine("Arreter tout les machines");
                    for (int y = 0; y < machines.Length; y++)
                    {
                        Machine machine = machines[y];

                        if (machine is not null)
                        {
                            machine.etat = Etat.arret;
                        }
                        PutRequest(machine.IdMachine, machine);
                        signalRClient.SendMessage(machine.IdMachine, Etat.arret);
                        /* var cl = new RestClient(baseUrl + "api/machine/" + machine.IdMachine);
                         var req = new RestRequest(Method.PUT);
                         req.AddJsonBody(machine);
                         var resp = cl.Execute(req);
                         // Print the status code of the response
                         Console.WriteLine(resp.StatusCode);*/
                    }
                    break;
            }
            // Wait for the user to respond before closing.  
            /*  Console.Write("Press any key to close the Calculator console app...");
              Console.ReadKey();*/



            /* // Use an if-else statement to handle different request methods
             if (requestMethod == Method.GET)
             {
                 var client = new RestClient("https://goodaquastone11.conveyor.cloud/swagger/api/machine");
                 var request = new RestRequest(Method.GET);
                 var response = client.Execute(request);

                 // Print the status code of the response
                 Console.WriteLine(response.StatusCode);
             }*/
            /*else if (requestMethod == Method.POST)
            {
                var client = new RestClient(baseUrl + "/post");
                var request = new RestRequest(Method.POST);
                request.AddJsonBody(new { idLav = "1", nom = "value2" ,  adresse: "dddd",
"telephone": 0,
"responsable": "eee",
"username": "eee",
"password": "eeee"
}
                var response = client.Execute(request);

                // Print the status code of the response
                Console.WriteLine(response.StatusCode);
            }*/
            /*else*/
            /*if (requestMethod == Method.PUT)
            {
                for (int o =0; o <machines.Length; o++)
                {
                    int id = machines[o].IdMachine;
                    signalRClient.SendMessage(id, false);
                    Console.WriteLine(machines[o].IdMachine);
                    var client = new RestClient(baseUrl +"api/machine/"+id);
                    var request = new RestRequest(Method.PUT);
                    *//* request.AddJsonBody(machines.All<Machine>);*//*
                    request.AddJsonBody(machines[o]);
                    var response = client.Execute(request);

                    // Print the status code of the response
                    Console.WriteLine(response.StatusCode);
                }
            }*/
        }
    }

    private static void Listmachine(Machine[] machines)
    {
        foreach (var machinee in machines)
        {
            Console.WriteLine(machinee);

        }
    }

    private static void MachineAddedEvent(Machine machi,Machine[]ma)
    {
        for(int i=0; i < ma.Length; i++)
        {
            if(machi.IdMachine==ma[i].IdMachine)
            {
                ma[i] = machi;
            }
        }
               
       // Listmachine(ma);

    }


    public static void PutRequest(int id, Machine machine)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(baseUrl+"api/machine/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PutAsJsonAsync(id.ToString(), machine).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Le changement d'état de la machine a été mis à jour avec succès");
            }
            else

            {
                Console.WriteLine("Echec de la mise à jour du changement d'état de la machine");
            }
        }

    }
   
    
}
  /*      );
        }

}*/
