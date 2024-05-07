using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Demo6.Model
{
    public class Product : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _articleNumber;
        public string ArticleNumber
        {
            get { return _articleNumber; }
            set
            {
                if (_articleNumber != value)
                {
                    _articleNumber = value;
                    OnPropertyChanged("ArticleNumber");
                }
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string _unit;
        public string Unit
        {
            get { return _unit; }
            set
            {
                if (_unit != value)
                {
                    _unit = value;
                    OnPropertyChanged("Unit");
                }
            }
        }

        private decimal _cost;
        public decimal Cost
        {
            get { return _cost; }
            set
            {
                if (_cost != value)
                {
                    _cost = value;
                    OnPropertyChanged("Cost");
                }
            }
        }

        private decimal _maximumDiscountAmount;
        public decimal MaximumDiscountAmount
        {
            get { return _maximumDiscountAmount; }
            set
            {
                if (_maximumDiscountAmount != value)
                {
                    _maximumDiscountAmount = value;
                    OnPropertyChanged("MaximumDiscountAmount");
                }
            }
        }

        private string _manufacturer;
        public string Manufacturer
        {
            get { return _manufacturer; }
            set
            {
                if (_manufacturer != value)
                {
                    _manufacturer = value;
                    OnPropertyChanged("Manufacturer");
                }
            }
        }

        private string _provider;
        public string Provider
        {
            get { return _provider; }
            set
            {
                if (_provider != value)
                {
                    _provider = value;
                    OnPropertyChanged("Provider");
                }
            }
        }

        private string _category;
        public string Category
        {
            get { return _category; }
            set
            {
                if (_category != value)
                {
                    _category = value;
                    OnPropertyChanged("Category");
                }
            }
        }

        private decimal _currentDiscount;
        public decimal CurrentDiscount
        {
            get { return _currentDiscount; }
            set
            {
                if (_currentDiscount != value)
                {
                    _currentDiscount = value;
                    OnPropertyChanged("CurrentDiscount");
                }
            }
        }

        private int _quantityInStock;
        public int QuantityInStock
        {
            get { return _quantityInStock; }
            set
            {
                if (_quantityInStock != value)
                {
                    _quantityInStock = value;
                    OnPropertyChanged("QuantityInStock");
                }
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private byte[] _photo;
        public byte[] Photo
        {
            get { return _photo; }
            set
            {
                if (_photo != value)
                {
                    _photo = value;
                    OnPropertyChanged("Photo");
                    OnPropertyChanged("ImageSource");
                }
            }
        }

        public ImageSource ImagePath
        {
            get
            {
                if (Photo == null || Photo.Length == 0)
                {
                    // Возвращаем изображение-заглушку, если изображение не задано
                    return new BitmapImage(new Uri("C:\\Users\\yurag\\source\\repos\\Demo6\\Demo6\\7111142d25bd0f62d5078b68bbfe40a0.jpeg"));
                }
                else
                {
                    // Загружаем изображение из массива байтов
                    return LoadImageFromByteArray(Photo);
                }
            }
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



        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
