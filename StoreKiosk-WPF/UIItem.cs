using System.Windows.Controls;

namespace StoreKiosk_WPF
{
    public abstract class UIItem : StackPanel
    {
        Item item;

        public abstract string ICON_PATH { get; set; }

        public UIItem(Item item)
        {
            SetItem(item);
        }

        public Item GetItem()
        {
            return item;
        }

        public void SetItem(Item value)
        {
            item = value;
        }
    }

    public class TireUIItem : UIItem
    {
        
        public TireUIItem(Item item) : base(item)
        {
            ICON_PATH = "./icons/tire.png";
        }

        public override string ICON_PATH { get; set; }
    }

    public class BatteryUIItem : UIItem
    {
    
        public BatteryUIItem(Item item) : base(item)
        {
            ICON_PATH = "./icons/battery.png";
        }

        public override string ICON_PATH { get; set; }
    }

    public class WindshildWiperUiItem : UIItem
    {
        public WindshildWiperUiItem(Item item) : base(item)
        {
            ICON_PATH = "./icons/wiper.png";
        }

        public override string ICON_PATH { get; set; }
    }
}