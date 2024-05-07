using Demo6.Model;
using Demo6.ViewModel;
using MongoDB.Driver.Builders;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Demo6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private ShoppingCartViewModel _viewModel;
        private ICollectionView _collectionView;
        public ObservableCollection<Product> Products { get; set; }

        public MainWindow()
        {
            Products = new ObservableCollection<Product>();
            InitializeComponent();
            _viewModel = new ShoppingCartViewModel();
            DataContext = _viewModel;
            LoadProductsFromDatabase();
            ApplySorting();

        }


        private ImageSource LoadImageFromByteArray(byte[] imageData)
        {
            BitmapImage image = new BitmapImage();
            using (MemoryStream stream = new MemoryStream(imageData))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
            }
            return image;
        }


        private void LoadProductsFromDatabase()
        {
            string connectionString = "Data Source=(local);Initial Catalog=Trade_completed;Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Product";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        

                        Products.Add(new Product
                        {
                            ArticleNumber = reader["ArticleNumber"].ToString(),
                            Name = reader["Name"].ToString(),
                            Unit = reader["Unit"].ToString(),
                            Cost = Convert.ToDecimal(reader["Cost"]),
                            MaximumDiscountAmount = Convert.ToDecimal(reader["MaximumDiscountAmount"]),
                            Manufacturer = reader["Manufacturer"].ToString(),
                            Provider = reader["Provider"].ToString(),
                            Category = reader["Category"].ToString(),
                            CurrentDiscount = Convert.ToDecimal(reader["CurrentDiscount"]),
                            QuantityInStock = Convert.ToInt32(reader["QuantityInStock"]),
                            Description = reader["Description"].ToString(),
                            //Photo = image // Присваиваем изображение к свойству Photo типа ImageSource
                        });
                    }
                        reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }
        }


        public void ApplySorting()
        {
            // Установка свойства SortBy во ViewModel в соответствии с критериями сортировки
            _viewModel.SortBy = "Name"; // Пример: Сортировка по имени изначально

            // Вы можете обрабатывать различные критерии сортировки, устанавливая свойство SortBy соответственно

            // После установки SortBy, ViewModel автоматически применит сортировку
        }

        public void ResetSorting()
        {
            // Перезагрузка продуктов из базы данных для сброса сортировки
            LoadProductsFromDatabase();
            // Убедитесь, что свойство Products в ViewModel обновлено с перезагруженными продуктами
            _viewModel.Products = Products;
        }
    }
}