using BLL;
using DAL;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly FarmManager _context;

        public MainWindow()
        {
            InitializeComponent();
            _context = new FarmManager();  // Khởi tạo DbContext mà không dùng `using`
        }

        private void AddLivestock(Livestock livestock)
        {
            _context.AddLivestock(livestock);  
            MessageBox.Show($"{livestock.GetType().Name} has been added to the farm."); 
            Display(); 
        }

        private void BtnAddCow_Click(object sender, RoutedEventArgs e)
        {
            var cow = new Cow { Name = "New Cow" };
            AddLivestock(cow);
        }
        private void BtnAddSheep_Click(object sender, RoutedEventArgs e)
        {
            var sheep = new Sheep { Name = "New Sheep" };
            AddLivestock(sheep);
        }

        private void BtnAddGoat_Click(object sender, RoutedEventArgs e)
        {
            var goat = new Goat { Name = "New Goat" };
            AddLivestock(goat);
        }
        private void BtnFeedAnimals_Click(object sender, RoutedEventArgs e)
        {
            _context.FeedAnimals();
            MessageBox.Show("All animals have been fed.");
            Display();
        }

        private void BtnCollectMilk_Click(object sender, RoutedEventArgs e)
        {

            int milk = _context.CollectMilk();
            MessageBox.Show($"Total Milk Collected: {milk} liters");
            Display();
        }

        private void BtnReproduce_Click(object sender, RoutedEventArgs e)
        {
            int quantity= _context.ReproduceAnimals();
            MessageBox.Show("All animals have reproduced.");
            MessageBox.Show($"New offspring: {quantity}", "Reproduction Status", MessageBoxButton.OK, MessageBoxImage.Information);

            Display();
        }

        private void BtnStatistics_Click(object sender, RoutedEventArgs e)
        {
            Display();
        }
        private void ClearStatistics_Click(object sender, RoutedEventArgs e)
        {
            StatisticsText.Text = "";
        }
        private void Display()
        {
            using (var context = new FarmContext())
            {
                int cows = context.Cows.Count();
                int sheeps = context.Sheeps.Count();
                int goats = context.Goats.Count();
                int totalMilk = _context.CollectMilk();

                StatisticsText.Text = $"Cows: {cows}\nSheeps: {sheeps}\nGoats: {goats}\nTotal Milk Produced: {totalMilk} liters";
            }
        }

        private void BtnGetLiveStock_Click(object sender, RoutedEventArgs e)
        {
            List<Cow> liveStocks = _context.GetAllCows();
            int count = 0;
            foreach (var cow in liveStocks)
            {
                count++;
                MessageBox.Show($"{count}. {cow.Name} - {cow.GetType().Name}");
            }
        }
    }
}