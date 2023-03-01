using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLaverie
{
    public class Machine
    {
   
          
            public int IdMachine { get; set; }

      
            public Laverie Laverie { get; set; }
            public enum Etat { enMarche, arret, horsService };
         
            public Etat etat { get; set; }
            public int DureeToalDeFonctionnement { get; set; }
            public int NumeroCode { get; set; }

        public Machine(Etat et)
            
        {
            this.etat = et;

        }
        public Machine()

        {
           

        }
        public override string ToString()
        {
            return "Machine Numero " + IdMachine + " etat= " + etat;
        }
    }
  
    }

