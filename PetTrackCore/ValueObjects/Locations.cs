using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PetTrackCore.ValueObjects
{
    
    public struct Location
    {
        public double Enlem { get; set; }
        public double Boylam { get; set; }

        
        public Location(double enlem, double boylam)
        {
            Enlem = enlem;
            Boylam = boylam;
        }
    }
}
