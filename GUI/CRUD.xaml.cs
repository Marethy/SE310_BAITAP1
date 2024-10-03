using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for CRUD.xaml
    /// </summary>
    public partial class CRUD : Window
    {
        private readonly BLL.FarmManager _context;
        public CRUD()
        {
            InitializeComponent(); // Ensure this is called first

            _context = new FarmManager();
            this.Loaded += CRUD_Loaded; // Subscribe to the Loaded event

        }
        private void CRUD_Loaded(object sender, RoutedEventArgs e)
        {
            InitGrid(); // Call InitGrid when the window has loaded
        }
        public void InitGrid()
        {
            List<Livestock> livestock = _context.GetAllLiveStock();
            ObservableCollection<Livestock> livestockList = new ObservableCollection<Livestock>(livestock);
            LivestockDataGrid.ItemsSource = livestockList;
            
        }
        
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin từ TextBox
            string name = NameTextBox.Text;
            int milkProduced = int.TryParse(MilkProducedTextBox.Text, out int result) ? result : 0;
            string type = TypeComboBox.Text;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(type))
            {
                MessageBox.Show("Please enter valid name and select the type.");
                return;
            }

            // Tạo đối tượng động vật dựa trên loại
            Livestock newLivestock;
            switch (type)
            {
                case "Goat":
                    newLivestock = new Goat { Name = name, MilkProduced = milkProduced };
                    break;
                case "Sheep":
                    newLivestock = new Sheep { Name = name, MilkProduced = milkProduced };
                    break;
                case "Cow":
                    newLivestock = new Cow { Name = name, MilkProduced = milkProduced };
                    break;
                default:
                    MessageBox.Show("Please select a valid type.");
                    return;
            }

            // Gọi phương thức thêm vào BLL
            _context.AddLivestock(newLivestock);
            MessageBox.Show($"{newLivestock.GetType().Name} has been added to the farm.");
            InitGrid();
        }


        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (LivestockDataGrid.SelectedItem != null)
            {
                Livestock selectedLivestock = (Livestock)LivestockDataGrid.SelectedItem;

                // Cập nhật thông tin từ TextBox
                selectedLivestock.Name = NameTextBox.Text;
                selectedLivestock.MilkProduced = int.TryParse(MilkProducedTextBox.Text, out int result) ? result : 0;

                // Gọi phương thức cập nhật trong BLL
                _context.UpdateLivestock(selectedLivestock);
                MessageBox.Show($"{selectedLivestock.GetType().Name} has been updated.");
                InitGrid();
            }
            else
            {
                MessageBox.Show("Please select an animal to update.");
            }
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (LivestockDataGrid.SelectedItem != null)
            {
                Livestock livestock = (Livestock)LivestockDataGrid.SelectedItem;
                _context.DeleteLivestock(livestock);
                MessageBox.Show($"{livestock.GetType().Name} has been deleted from the farm.");
                InitGrid();
            }
            else
            {
                MessageBox.Show("Please select an animal to delete.");
            }
        }

        private void LivestockDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LivestockDataGrid.SelectedItem is Livestock selectedLivestock)
            {
                // Hiển thị thông tin vật nuôi đã chọn trong TextBox
                NameTextBox.Text = selectedLivestock.Name;
                MilkProducedTextBox.Text = selectedLivestock.MilkProduced.ToString();
                // Gán loại vật nuôi cho ComboBox (Goat, Sheep, Cow)
                if (selectedLivestock is Goat)
                    TypeComboBox.SelectedIndex = 0;
                else if (selectedLivestock is Sheep)
                    TypeComboBox.SelectedIndex = 1;
                else if (selectedLivestock is Cow)
                    TypeComboBox.SelectedIndex = 2;
            }
        }

        private void View_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                // Ensure an item is selected
                if (comboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    // Retrieve the selected type based on the Content
                    string selectedType = selectedItem.Content.ToString();

                    // Get the corresponding livestock based on the selected type
                    ObservableCollection<Livestock> livestockList;

                    switch (selectedType)
                    {
                        case "Cow":
                            livestockList = new ObservableCollection<Livestock>(_context.GetAllCows());
                            break;
                        case "Sheep":
                            livestockList = new ObservableCollection<Livestock>(_context.GetAllSheeps());
                            break;
                        case "Goat":
                            livestockList = new ObservableCollection<Livestock>(_context.GetAllGoats());
                            break;
                        default:
                            livestockList = new ObservableCollection<Livestock>(_context.GetAllLiveStock());
                            break;
                    }

                    // Update the DataGrid with the filtered livestock
                    LivestockDataGrid.ItemsSource = livestockList;
                }
            }
        }

        private void MethodButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newWindow = new MainWindow();
            newWindow.Show();


        }
    }
}
