using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreKiosk_WPF
{
    public class Tire : Item
    {
        int wheelDiameter;

        public Tire()
        {
            ItemNo = 1;
            Cost = 200;
            Weight = 30;
        }

        public Tire(string name, int diameter) : base(name)
        {
            ItemNo = 1;
            WheelDiameter = diameter;
            Cost = 200;
            Weight = 30;
        }
    
        public int WheelDiameter { get => wheelDiameter; set => wheelDiameter = value; }

        public override string Display()
        {
            return $"Wheel Diameter: {WheelDiameter}";
        }
    }
}
