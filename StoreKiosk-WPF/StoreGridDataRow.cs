using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StoreKiosk_WPF
{
    [Serializable]
    public class StoreGridDataRow
    {
        //Image type;
        int rowId;
        int itemNumber;
        string name;
        float weight;
        int cost;
        bool ship;
        string extra;
        int shippingCost;

        public StoreGridDataRow() { }

        public StoreGridDataRow(int id, Item item)
        {
            RowId = id;
            Name = item.ItemName;
            ItemNumber = item.ItemNo;
            Cost = item.Cost;
            Weight = item.Weight;
            try
            {
                if (IsShippable(item))
                {
                    var i = item as IShipItem;
                    shippingCost = i.Ship ? i.Shipitem() : 0;
                    Ship = i.Ship;
                }
                else
                {
                    ship = false;
                    ShippingCost = 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Break at IShipItem interface search  {e.Message}");
            }

            Extra = item.Display();
        }

        private bool IsShippable(Item item)
        {
            return item is IShipItem;
        }

        public string Extra { get => extra; set => extra = value; }

        public string Name { get => name; set => name = value; }
        public int ItemNumber { get => itemNumber; set => itemNumber = value; }
        public float Weight { get => weight; set => weight = value; }
        public int Cost { get => cost; set => cost = value; }
        public bool Ship { get => ship; set => ship = value; }
        public int RowId { get => rowId; set => rowId = value; }
        public int ShippingCost { get => shippingCost; set => shippingCost = value; }

        public int TotalCost { get => Cost + ShippingCost; }

    }
}
