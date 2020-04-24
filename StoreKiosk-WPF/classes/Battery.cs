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
            Ship = false;
        }

        public Battery(string name, int voltage, bool ship = false) : base(name)
        {
            ItemNo = _no;
            Voltage = voltage;
            Cost = 100;
            Weight = 10;
            Ship = ship;
        }

        public bool Ship { get; set; }

        public int Voltage { get => voltage; set => voltage = value; }

        public override string Display()
        {
            if(Ship)
                return $"Battery Voltage: {Voltage}, Shippping cost : {Shipitem()}";
            else
                return $"Battery Voltage: {Voltage}";

        }

        public int Shipitem()
        {
            return 30;
        }
    }
}
