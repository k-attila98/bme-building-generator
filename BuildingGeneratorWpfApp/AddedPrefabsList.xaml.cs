using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BuildingGeneratorWpfApp
{
    /// <summary>
    /// Interaction logic for AddedPrefabsList.xaml
    /// </summary>
    public partial class AddedPrefabsList : UserControl
    {

        public delegate void PrefabSelectionChangedHandler(object sender, string newPath);
        public event PrefabSelectionChangedHandler PrefabSelectionChanged;

        public ObservableCollection<string> Prefabs { get; set; }
        public AddedPrefabsList()
        {
            InitializeComponent();
            this.DataContext = this;
            Prefabs = new ObservableCollection<string>();
        }

        private void btnRemoveSelectedPrefab_Click(object sender, RoutedEventArgs e)
        {
            Prefabs.Remove((string)listBoxAddedPrefabs.SelectedItem);

        }

        private void listBoxAddedPrefabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PrefabSelectionChanged == null) return;

            var pathOfPrefab = (string)listBoxAddedPrefabs.SelectedItem;
            PrefabSelectionChanged(this, pathOfPrefab);
        }
    }
}
