using FlowerShop.Models;
using FlowerShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
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

namespace FlowerShop.Views.CodeBehind
{
    /// <summary>
    /// Interaction logic for StorageWindow.xaml
    /// </summary>
    public partial class StorageWindowOld : Window
    {
        public StorageWindowOld()
        {
            InitializeComponent();
        }

        private void CreateButton(object sender, RoutedEventArgs e)
        {
            // Opens the storage window for creating the product
            StorageCreateWindowOld storageCreateWindow = new StorageCreateWindowOld()
            {
                Title = "StorageCreate",
                Topmost = true,
                ResizeMode = ResizeMode.NoResize,
                ShowInTaskbar = true,
                Owner = null
            };
            storageCreateWindow.ShowDialog();
        }
    }
}
