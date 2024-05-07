using Demo6.Model;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices;
using System.Windows.Data;

namespace Demo6.ViewModel
{
    public class ShoppingCartViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged("Products");
            }
        }



        private string _sortBy;
        public string SortBy
        {
            get { return _sortBy; }
            set
            {
                if (_sortBy != value)
                {
                    _sortBy = value;
                    OnPropertyChanged("SortBy");
                    ApplyFilter(); // Применить сортировку при изменении SortBy
                }
            }
        }


        private string _filterText;
        public string FilterText
        {
            get { return _filterText; }
            set
            {
                if (_filterText != value)
                {
                    _filterText = value;
                    OnPropertyChanged("FilterText");
                    ApplyFilter(); // Применяем фильтр при изменении текста фильтра
                }
            }
        }

        private ICollectionView _collectionView;
        public ICollectionView CollectionView
        {
            get { return _collectionView; }
            set
            {
                _collectionView = value;
                OnPropertyChanged("CollectionView");
            }
        }

        // Свойство для текущей страницы в пагинации
        private int _currentPage;
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    OnPropertyChanged("CurrentPage");
                    UpdateCollectionView(); // Обновляем отображаемые товары при изменении текущей страницы
                }
            }
        }

        // Другие свойства и методы ViewModel

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ApplyFilter()
        {
            // Применяем фильтр к коллекции товаров в соответствии с FilterText
            if (CollectionView != null)
            {
                CollectionView.Filter = item =>
                {
                    if (string.IsNullOrEmpty(FilterText))
                        return true; // Если текст фильтра пустой, отображаем все товары

                    // Здесь реализуем логику фильтрации по наименованию товара
                    Product product = item as Product;
                    return product.Name.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0;
                };
            }
        }




  






        private void UpdateCollectionView()
        {
            // Обновляем отображаемые товары в соответствии с текущей страницей
            if (CollectionView != null)
            {
                // Устанавливаем номер текущей страницы и размер страницы для пагинации
                int pageSize = 10; // Указываем размер страницы
                int startIndex = (CurrentPage - 1) * pageSize;

                // Устанавливаем смещение начального элемента и количество элементов на странице
                CollectionView.Filter = item =>
                {
                    int index = Products.IndexOf((Product)item);
                    return index >= startIndex && index < (startIndex + pageSize);
                };

                // Обновляем коллекцию после применения фильтра
                CollectionView.Refresh();
            }
        }

        // Добавьте методы и свойства для сортировки, пагинации и т.д.
    }
}
