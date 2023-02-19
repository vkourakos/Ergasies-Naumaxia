using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergasia1
{
    class Ship
    {
        //length of the ship
        public int length;
        //determines if the ship is horizontal or vertical
        public bool hor;
        //determines if the ship is destroyed
        public bool destroyed;

        //constructor
        public Ship(int length)
        {
            
            this.length = length;
            this.destroyed = false;

        }


    }

    
       
}
