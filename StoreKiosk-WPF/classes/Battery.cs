using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreKiosk_WPF
{
    public class Battery : Item, IShipItem
    {

        readonly int _no = 3;

        int voltage;

        public Battery()
        {
            ItemNo = _no;
            Cost = 100;
            Weight = 10;
        }

        public Battery(string name, int voltage) : base(name)
        {
            ItemNo = _no;
            Voltage = voltage;
            Cost = 100;
            Weight = 10;
        }

        public bool Ship => true;

        public int Voltage { get => voltage; set => voltage = value; }

        public override string Display()
        {
            return $"Battery Voltage: {Voltage}, Shippping cost : {Shipitem()}";
        }

        public int Shipitem()
        {
            return 30;
        }
    }
}
