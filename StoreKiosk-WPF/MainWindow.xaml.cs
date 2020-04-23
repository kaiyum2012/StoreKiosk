using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;

namespace StoreKiosk_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string TIRE_PATH = "./icons/tire.png";
        const string WIPER_PATH = "./icons/wiper.png";
        const string BATTERY_PATH = "./icons/battery.png";

        const string TIRE_NAME_FORM_NAME = "tireName";
        const string TIRE_DIA_FORM_NAME = "tireDiameter";
        const string WIPER_NAME_FORM_NAME = "wiperName";
        const string WIPER_LENGTH_FORM_NAME = "wiperLength";
        const string BATTERY_NAME_FORM_NAME = "batteryName";
        const string BATTERY_VOLTAGE_FORM_NAME = "batteryVoltage";


        List<UIItem>  templateItems = new List<UIItem>() 
        {
            new TireUIItem(new Tire()),
            new WindshildWiperUiItem(new WindshildWiper()),
            new BatteryUIItem(new Battery())
        };

        public List<Item> storeItems = new List<Item>();

        public List<StoreGridDataRow> purchaseItems = new List<StoreGridDataRow>();

        public MainWindow()
        {
            InitializeComponent();
            SetupItemTemplates();
            SetupStoreGataGrid();
        }

        private void SetupStoreGataGrid()
        {
            //DataGridTextColumn itemNo = new DataGridTextColumn
            //{
            //    Header = "Item No",
            //    Binding = new Binding("item.ItemNo")
            //};
            //storeItemsGrid.Columns.Add(itemNo);

            //DataGridTextColumn name = new DataGridTextColumn
            //{
            //    Header = "Item Name",
            //    Binding = new Binding("ICON_PATH")
            //};
            //storeItemsGrid.Columns.Add(name);


            //DataGridTextColumn imageCol = new DataGridTextColumn();
            //imageCol.Header = "type";
            //imageCol.Binding = new Binding("Extra");
            //storeItemsGrid.Columns.Add(imageCol);
        }

        private void SetupItemTemplates()
        {
            CreateItemTemplateItems();
        }

        private void CreateItemTemplateItems()
        {

            foreach (var tempateItem in templateItems)
            {
                StackPanel stack = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                
                var button = new Button
                {
                    Width = 40,
                    Height = 40,
                    DataContext = tempateItem.GetItem().ItemNo,
                    Background = System.Windows.Media.Brushes.Transparent,
                    BorderThickness = new Thickness(0),
                    Margin = new Thickness(10)
                };

                button.Click += UiItem_Click;


                var icon = new System.Windows.Controls.Image
                {
                    Source = new BitmapImage(new Uri(tempateItem.ICON_PATH, UriKind.Relative)),
                    Height = 40,
                    Stretch = Stretch.Uniform,
                    HorizontalAlignment = HorizontalAlignment.Center,
                };

                button.Content = icon;

                stack.Children.Add(button);

                itemTampleStackPanel.Children.Add(stack);
            }
        }

        private void UiItem_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as Button ;
            // TODO:: MAKE IT MORE SUPPLE 

            switch (item.DataContext)
            {
                case 1:
                    // Tire 
                    Console.WriteLine("Tire");
                    CreateTireForm();

                    break;
                case 2:
                    // WindshildWiper
                    Console.WriteLine("WindshildWiper");

                    CreateWiperForm();

                    break;
                case 3:
                    // Battery
                    Console.WriteLine("Battery");
                    CreateBatteryForm();

                    break;
                default:
                    break;
            }

            void CreateTireForm()
            {
                createItemForm.Children.Clear();
                createItemForm.RowDefinitions.Clear();
                createItemForm.ColumnDefinitions.Clear();
                _deregisterForm();

                Label label = new Label
                {
                    Name = "tireNameLabel",
                    Content = "Tire Name",
                    Height = 30,
                    HorizontalContentAlignment = HorizontalAlignment.Right
                };

                label.SetValue(Grid.RowProperty, 0);
                label.SetValue(Grid.ColumnProperty, 0);

                TextBox textBox = new TextBox
                {
                    //Width = 100,
                    Height = 30,
                };

                RegisterName(TIRE_NAME_FORM_NAME, textBox);
                textBox.PreviewTextInput += StringInputValidation;

                textBox.SetValue(Grid.RowProperty, 0);
                textBox.SetValue(Grid.ColumnProperty, 1);
                textBox.VerticalContentAlignment = VerticalAlignment.Center;
                createItemForm.RowDefinitions.Add(new RowDefinition());

                Label label1 = new Label
                {
                    Name = "tireDiaLabel",
                    Content = "Wheel Diameter",
                    //Width = 100
                    Height = 30,
                    HorizontalContentAlignment = HorizontalAlignment.Right

                };

                label1.SetValue(Grid.RowProperty, 1);
                label1.SetValue(Grid.ColumnProperty, 0);

                TextBox textBox1 = new TextBox
                {
                    Height = 30
                };
                RegisterName(TIRE_DIA_FORM_NAME, textBox1);
                textBox1.PreviewTextInput += NumericInputValidation;

                textBox1.SetValue(Grid.RowProperty, 1);
                textBox1.SetValue(Grid.ColumnProperty, 1);
                textBox1.VerticalContentAlignment = VerticalAlignment.Center;

                createItemForm.RowDefinitions.Add(new RowDefinition());

                Button button = new Button
                {
                    Content = "Create Tire",
                    Height = 30,
                    DataContext = 1
                };

                button.Click += ItemCreate_Clicked;
                button.SetValue(Grid.RowProperty, 2);
                button.SetValue(Grid.ColumnProperty, 1);

                createItemForm.RowDefinitions.Add(new RowDefinition());

                var col1 = new ColumnDefinition();
                GridLength length1 = new GridLength(4.0, GridUnitType.Star);
                col1.Width = length1;
                createItemForm.ColumnDefinitions.Add(col1);

                var col2 = new ColumnDefinition();
                GridLength length2 = new GridLength(6.0, GridUnitType.Star);
                col2.Width = length2;
                createItemForm.ColumnDefinitions.Add(col2);

                createItemForm.Children.Add(label);
                createItemForm.Children.Add(textBox);
                createItemForm.Children.Add(label1);
                createItemForm.Children.Add(textBox1);
                createItemForm.Children.Add(button);

                void _deregisterForm()
                {
                    if( createItemForm.FindName(TIRE_NAME_FORM_NAME) != null)
                    {
                        UnregisterName(TIRE_NAME_FORM_NAME);
                    }
                    if(createItemForm.FindName(TIRE_DIA_FORM_NAME) != null)
                    {
                        UnregisterName(TIRE_DIA_FORM_NAME);
                    }
                }
            }

            void CreateWiperForm()
            {
                createItemForm.Children.Clear();
                createItemForm.RowDefinitions.Clear();
                createItemForm.ColumnDefinitions.Clear();
                _deregisterForm();

                Label label = new Label
                {
                    Name = "wiperNameLabel",
                    Content = "Wiper Name",
                    Height = 30,
                    HorizontalContentAlignment = HorizontalAlignment.Right
                };

                label.SetValue(Grid.RowProperty, 0);
                label.SetValue(Grid.ColumnProperty, 0);

                TextBox textBox = new TextBox
                {
                    Height = 30
                };
                RegisterName(WIPER_NAME_FORM_NAME, textBox);

                textBox.PreviewTextInput += StringInputValidation;

                textBox.SetValue(Grid.RowProperty, 0);
                textBox.SetValue(Grid.ColumnProperty, 1);
                textBox.VerticalContentAlignment = VerticalAlignment.Center;
                createItemForm.RowDefinitions.Add(new RowDefinition());

                Label label1 = new Label
                {
                    Name = "wiperLengthLabel",
                    Content = "Wheel Diameter",
                    //Width = 100
                    Height = 30,
                    HorizontalContentAlignment = HorizontalAlignment.Right

                };

                label1.SetValue(Grid.RowProperty, 1);
                label1.SetValue(Grid.ColumnProperty, 0);

                TextBox textBox1 = new TextBox
                {
                    Height = 30
                };
                RegisterName(WIPER_LENGTH_FORM_NAME, textBox1);
                textBox1.PreviewTextInput += NumericInputValidation;

                textBox1.SetValue(Grid.RowProperty, 1);
                textBox1.SetValue(Grid.ColumnProperty, 1);
                textBox1.VerticalContentAlignment = VerticalAlignment.Center;

                createItemForm.RowDefinitions.Add(new RowDefinition());

                Button button = new Button
                {
                    Content = "Create Windshild Wiper",
                    //Width = 100
                    Height = 30,
                    DataContext = 2
                };

                button.Click += ItemCreate_Clicked;
                button.SetValue(Grid.RowProperty, 2);
                button.SetValue(Grid.ColumnProperty, 1);

                createItemForm.RowDefinitions.Add(new RowDefinition());

                var col1 = new ColumnDefinition();
                GridLength length1 = new GridLength(4.0, GridUnitType.Star);
                col1.Width = length1;
                createItemForm.ColumnDefinitions.Add(col1);

                var col2 = new ColumnDefinition();
                GridLength length2 = new GridLength(6.0, GridUnitType.Star);
                col2.Width = length2;
                createItemForm.ColumnDefinitions.Add(col2);

                createItemForm.Children.Add(label);
                createItemForm.Children.Add(textBox);
                createItemForm.Children.Add(label1);
                createItemForm.Children.Add(textBox1);
                createItemForm.Children.Add(button);

                void _deregisterForm()
                {
                    if (createItemForm.FindName(WIPER_NAME_FORM_NAME) != null)
                    {
                        UnregisterName(WIPER_NAME_FORM_NAME);
                    }
                    if (createItemForm.FindName(WIPER_LENGTH_FORM_NAME) != null)
                    {
                        UnregisterName(WIPER_LENGTH_FORM_NAME);
                    }
                }
            }

            void CreateBatteryForm()
            {
                createItemForm.Children.Clear();
                createItemForm.RowDefinitions.Clear();
                createItemForm.ColumnDefinitions.Clear();
                _deregisterForm();

                Label label = new Label
                {
                    Name = "batteryNameLabel",
                    Content = "Battery Name",
                    Height = 30,
                    HorizontalContentAlignment = HorizontalAlignment.Right
                };

                label.SetValue(Grid.RowProperty, 0);
                label.SetValue(Grid.ColumnProperty, 0);

                TextBox textBox = new TextBox
                {
                    Height = 30
                };
                RegisterName(BATTERY_NAME_FORM_NAME, textBox);
                textBox.PreviewTextInput += StringInputValidation;

                textBox.SetValue(Grid.RowProperty, 0);
                textBox.SetValue(Grid.ColumnProperty, 1);
                textBox.VerticalContentAlignment = VerticalAlignment.Center;
                createItemForm.RowDefinitions.Add(new RowDefinition());

                Label label1 = new Label
                {
                    Name = "batteryVoltLabel",
                    Content = "Battery Voltage",
                    //Width = 100
                    Height = 30,
                    HorizontalContentAlignment = HorizontalAlignment.Right

                };

                label1.SetValue(Grid.RowProperty, 1);
                label1.SetValue(Grid.ColumnProperty, 0);

                TextBox textBox1 = new TextBox
                {
                    Height = 30
                };

                RegisterName(BATTERY_VOLTAGE_FORM_NAME, textBox1);
                textBox1.PreviewTextInput += NumericInputValidation;

                textBox1.SetValue(Grid.RowProperty, 1);
                textBox1.SetValue(Grid.ColumnProperty, 1);
                textBox1.VerticalContentAlignment = VerticalAlignment.Center;

                createItemForm.RowDefinitions.Add(new RowDefinition());

                Button button = new Button
                {
                    Content = "Create Battery",
                    //Width = 100
                    Height = 30,
                    DataContext = 3
                };

                button.Click += ItemCreate_Clicked;
                button.SetValue(Grid.RowProperty, 2);
                button.SetValue(Grid.ColumnProperty, 1);

                createItemForm.RowDefinitions.Add(new RowDefinition());

                var col1 = new ColumnDefinition();
                GridLength length1 = new GridLength(4.0, GridUnitType.Star);
                col1.Width = length1;
                createItemForm.ColumnDefinitions.Add(col1);

                var col2 = new ColumnDefinition();
                GridLength length2 = new GridLength(6.0, GridUnitType.Star);
                col2.Width = length2;
                createItemForm.ColumnDefinitions.Add(col2);

                createItemForm.Children.Add(label);
                createItemForm.Children.Add(textBox);
                createItemForm.Children.Add(label1);
                createItemForm.Children.Add(textBox1);
                createItemForm.Children.Add(button);

                void _deregisterForm()
                {
                    if (createItemForm.FindName(BATTERY_NAME_FORM_NAME) != null)
                    {
                        UnregisterName(BATTERY_NAME_FORM_NAME);
                    }
                    if (createItemForm.FindName(BATTERY_VOLTAGE_FORM_NAME) != null)
                    {
                        UnregisterName(BATTERY_VOLTAGE_FORM_NAME);
                    }
                }
            }
        }


        public bool IsEmptyOrNullInput(TextBox text)
        {
            if (string.IsNullOrWhiteSpace(text.Text))
            {
                MessageBox.Show($"{text.Name} must not be Empty", "error");
                return true;
            }
            return false;
        }

        private void ItemCreate_Clicked(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;
            switch (item.DataContext)
            {
                case 1:
                    // Tire 
                    Console.WriteLine("Tire add");

                    TextBox tire = (TextBox)createItemForm.FindName(TIRE_NAME_FORM_NAME);
                    TextBox diameter = (TextBox)createItemForm.FindName(TIRE_DIA_FORM_NAME);

                    if (!IsEmptyOrNullInput(tire) && !IsEmptyOrNullInput(diameter))
                    {
                        try
                        {
                            storeItems.Add (new Tire(name: tire.Text, diameter: int.Parse(diameter.Text)));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        ReloadStoreItemsDataGrid();
                    }
                    break;
                case 2:
                    // WindshildWiper
                    Console.WriteLine("WindshildWiper add");

                    TextBox wiper = (TextBox)createItemForm.FindName(WIPER_NAME_FORM_NAME);
                    TextBox length = (TextBox)createItemForm.FindName(WIPER_LENGTH_FORM_NAME);

                    if (!IsEmptyOrNullInput(wiper) && !IsEmptyOrNullInput(length))
                    {
                        try
                        {
                            storeItems.Add(new WindshildWiper(name: wiper.Text, length: int.Parse(length.Text)));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                  
                        ReloadStoreItemsDataGrid();
                    }

                    break;
                case 3:
                    // Battery
                    Console.WriteLine("Battery add");

                    TextBox battery = (TextBox)createItemForm.FindName(BATTERY_NAME_FORM_NAME);
                    TextBox voltage = (TextBox)createItemForm.FindName(BATTERY_VOLTAGE_FORM_NAME);

                    if (!IsEmptyOrNullInput(battery) && !IsEmptyOrNullInput(voltage))
                    {
                        try
                        {
                            storeItems.Add(new Battery(name: battery.Text, voltage: int.Parse(voltage.Text)));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        ReloadStoreItemsDataGrid();
                    }
                    break;
                default:
                    break;
            }
        }

        private void ReloadStoreItemsDataGrid()
        {
            storeItemsGrid.ItemsSource = GetStoreGridDataRows();
            storeItemsGrid.Items.Refresh();
        }

        private IEnumerable GetStoreGridDataRows()
        {
            List<StoreGridDataRow> rows = new List<StoreGridDataRow>();
            foreach (var item in storeItems)
            {
                if(storeItems.Count == 0)
                {
                    rows.Add(new StoreGridDataRow(1, item));
                }
                else
                {
                    rows.Add(new StoreGridDataRow(storeItems.Count, item));
                }
            }
            return rows;
        }

        //private Image GetItemIcon(Item item)
        //{
        //    Image image = null;
        //    switch (item.ItemNo) 
        //    {
        //        case 1:
        //            // Tire
        //            image = new Image{
        //                Source =    new BitmapImage(new Uri(@TIRE_PATH, UriKind.Relative)) 
        //            };
        //            break;
        //        case 2:
        //            // Wiper
        //            image = new Image
        //            {
        //                Source = new BitmapImage(new Uri(WIPER_PATH, UriKind.Relative))
        //            };
        //            break;
        //        case 3:
        //            // Battery
        //            image = new Image
        //            {
        //                Source = new BitmapImage(new Uri(BATTERY_PATH, UriKind.Relative))
        //            };
        //            break;
        //    }

        //    return image;
        //}

        private void StringInputValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void NumericInputValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void StoreItemsGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

            //// Reference :: https://stackoverflow.com/questions/1951839/how-do-i-show-image-in-wpf-datagrid-column-programmatically
            //if (e.Column.Header.ToString() == "Type")
            //{
            //    DataGridTemplateColumn col1 = new DataGridTemplateColumn();
            //    col1.Header = "Item Type";
            //    FrameworkElementFactory factory1 = new FrameworkElementFactory(typeof(Image));
            //    Binding b1 = new Binding("Type")
            //    {
            //        Mode = BindingMode.OneWayToSource
            //    };
            //    factory1.SetValue(Image.SourceProperty, b1);
            //    DataTemplate cellTemplate1 = new DataTemplate
            //    {
            //        VisualTree = factory1
            //    };
            //    col1.CellTemplate = cellTemplate1;
            //    storeItemsGrid.Columns.Add(col1);
            //}
            if(e.Column.Header.ToString() == "RowId")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }

            if (e.Column.Header.ToString() == "ItemNumber")
            {
                e.Column.Header = "Item Number";
                e.Column.DisplayIndex = 0;
            }

            if (e.Column.Header.ToString() == "ItemName")
            {
                e.Column.Header = "Item Name";
                e.Column.DisplayIndex = 1;
            }

            if (e.Column.Header.ToString() == "Weight")
            {
                e.Column.DisplayIndex = 2;
            }

            if (e.Column.Header.ToString() == "Cost")
            {
                e.Column.DisplayIndex = 3;
            }

        }

        private void Purchase_Click(object sender, RoutedEventArgs e)
        {
            var row = storeItemsGrid.SelectedItem as StoreGridDataRow;
            if(row != null)
            {
                if (purchaseItems.Count == 0)
                {
                    row.RowId = 1;
                }
                else
                {
                    row.RowId = purchaseItems.Count;
                }
                purchaseItems.Add(row);

                ReloadPurchaseDataGrid();

            }
            else
            {
                MessageBox.Show("Something Went wrong, Please try later!");
                Console.WriteLine("Unable to cast selected row");
            }
            Console.WriteLine(storeItemsGrid.SelectedIndex);
        }

        private void ReloadPurchaseDataGrid()
        {
            purchaseDataGrid.ItemsSource = purchaseItems;

            purchasedQty.Content = purchaseItems.Count.ToString();



            var total = purchaseItems.Sum(i => i.TotalCost);
            purchaseTotal.Content = total.ToString();


            purchaseDataGrid.Items.Refresh();
        }

        private void purchaseDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "RowId")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }

            if (e.Column.Header.ToString() == "ItemNumber")
            {
                e.Column.Header = "Item Number";
                e.Column.DisplayIndex = 0;
            }

            if (e.Column.Header.ToString() == "ItemName")
            {
                e.Column.Header = "Item Name";
                e.Column.DisplayIndex = 1;
            }

            if (e.Column.Header.ToString() == "Weight")
            {
                e.Column.DisplayIndex = 2;
            }

            if (e.Column.Header.ToString() == "Cost")
            {
                e.Column.DisplayIndex = 3;
            }
        }

        private void MenuItemSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (purchaseItems.Count == 0)
                {
                    MessageBox.Show("No Item purchased, hence Exiting !");
                    return;
                }

                var filename = "Purchase_Order.xml";
                ExportToxml(filename, purchaseItems);
                MessageBox.Show("Succesfully Exported to name: " + filename, "Export Purchase Order - XML");
            }
            catch (Exception ex)
            {
                Console.WriteLine("SaveMenuItem_Click -> Error: {0}", ex.ToString());
            }
        }

        private void ExportToxml(string filename, Object data)
        {
            try
            {
                var fileStream = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                XmlSerializer serializer = new XmlSerializer(typeof(List<StoreGridDataRow>));

                serializer.Serialize(fileStream, data);
                fileStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Error occured while exporting: {0}", ex.ToString());
            }

        }

        private void PurchaseClear_Click(object sender, RoutedEventArgs e)
        {
            purchaseItems.Clear();
            ReloadPurchaseDataGrid();
        }

        private void PurchaseItemDelete_Click(object sender, RoutedEventArgs e)
        {
            int index = purchaseDataGrid.SelectedIndex;
            if(index != -1)
            {
                purchaseItems.RemoveAt(index);
                ReloadPurchaseDataGrid();
            }
        }

        private void StoreItemDelete_Click(object sender, RoutedEventArgs e)
        {
            int index = storeItemsGrid.SelectedIndex;
            if (index != -1)
            {
                storeItems.RemoveAt(index);
                ReloadStoreItemsDataGrid();
            }
        }

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (purchaseItems.Count > 0)
                {
                    MessageBoxResult result = MessageBox.Show("Would you like to save current reservations before loading backup?", "", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        ExportCurrentPurchaseOrderToXml();
                    }
                }

                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Multiselect = false,
                    Filter = "XML Files (*.xml)|*.xml"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    ImportFromXml(openFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("OpenMenuItem_Click -> Error: {0}", ex.ToString());
            }
        }

        private void ImportFromXml(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<StoreGridDataRow>));

            XmlTextReader reader = new XmlTextReader(filename);
            var importedPurchaseorder = serializer.Deserialize(reader) as List<StoreGridDataRow>;

            if (importedPurchaseorder == null || importedPurchaseorder.Count == 0)
            {
                MessageBox.Show($"There is no record in the file", "Information");
                return;
            }

            purchaseItems.Clear();
            reader.Close();


            StoreGridDataRow[] items = new StoreGridDataRow[importedPurchaseorder.Count];


            importedPurchaseorder.CopyTo(items);

            purchaseItems = items.ToList();

            ReloadPurchaseDataGrid();
        }

        private void ExportCurrentPurchaseOrderToXml()
        {
            var filename = "_tempPurchaseOrder.xml";
            ExportToxml(filename, purchaseItems);
        }

        private void MostPurchased_Click(object sender, RoutedEventArgs e)
        {
            var result = purchaseItems.GroupBy(i => i.ItemNumber).OrderByDescending(i => i.Count()).FirstOrDefault().ToList();

            if (result != null || result.Count != 0)
            {
                MessageBox.Show($"Item : '{result.First().Name} of Item Number : {result.First().ItemNumber} is Purchased : {result.Count} times", "Most purchase Item");

            }
            else
            {
                MessageBox.Show("Something went wrong, Please try later", "Warning - Most Purchase Items");

            }
        }
    }
}