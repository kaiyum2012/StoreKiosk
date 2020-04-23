using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreKiosk_WPF
{
    public abstract class Item
    {
        string itemName;
        int itemNo;
        int cost;
        float weight;

        public Item(string name)
        {
            ItemName = name;
        }

        public Item()
        {
        }

        public int ItemNo { get => itemNo; set => itemNo = value; }
        public int Cost { get => cost; set => cost = value; }
        public float Weight { get => weight; set => weight = value; }
        public string ItemName { get => itemName; set => itemName = value; }
  
        public virtual string Display()
        {
            return "";
        }

    }
}
