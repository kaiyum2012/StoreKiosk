using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreKiosk_WPF
{
    public class WindshildWiper : Item, IShipItem
    {
        readonly int _no = 2;

        int length;



        public WindshildWiper()
        {
            ItemNo = _no;
            Cost = 15;
            Weight = 1;
            Ship = false;
        }

        public WindshildWiper(string name, int length,bool ship = false) : base(name)
        {
            ItemNo = _no;
            Length = length;
            Cost = 15;
            Weight = 1;
            Ship = ship;
        }


        public int Length { get => length; set => length = value; }

        public bool Ship {get; set;}

        public override string Display()
        {
            if(Ship)
                return $"Wiper Length: {Length}, Shippping cost : {Shipitem()}";
            else
                return $"Wiper Length: {Length}";
        }

        public int Shipitem()
        {
            return 10;
        }
    }
}
