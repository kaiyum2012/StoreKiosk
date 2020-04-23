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
        }

        public WindshildWiper(string name, int length) : base(name)
        {
            ItemNo = _no;
            Length = length;
            Cost = 15;
            Weight = 1;
        }


        public int Length { get => length; set => length = value; }

        public bool Ship => true;

        public override string Display()
        {
            return $"Wiper Length: {Length}, Shippping cost : {Shipitem()}";
        }

        public int Shipitem()
        {
            return 10;
        }
    }
}
