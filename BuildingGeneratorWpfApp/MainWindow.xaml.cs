using BuildingGenerator.BuildingGenerator.Generation_Settings;
using BuildingGenerator.Prefabs.Floors;
using BuildingGenerator.Prefabs.Roofs;
using BuildingGenerator.Prefabs.Walls;
using BuildingGenerator.Shared;
using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BuildingGeneratorWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string MODEL_PATH = "E:\\Attila\\egyetem cuccok\\9. felev\\onlab2\\BuildingGenerator\\BuildingGenerator\\Generated\\building-2022-12-11T00-01-02.obj";
        private string selectedWingsStrat, selectedWingStrat, selectedStoriesStrat, selectedStoryStrat, selectedWallsStrat, selectedRoofStrat;

        private List<string> wingsStrategies = StrategyResolver.GetAllWingsStrats();
        private List<string> wingStrategies = StrategyResolver.GetAllWingStrats();
        private List<string> wallsStrategies = StrategyResolver.GetAllWallsStrats();
        private List<string> storiesStrategies = StrategyResolver.GetAllStoriesStrats();
        private List<string> storyStrategies = StrategyResolver.GetAllStoryStrats();
        private List<string> roofStrategies = StrategyResolver.GetAllRoofStrats();

        

        public MainWindow()
        {
            InitializeComponent();
            //Model3DGroup MdlGrp = null;

            //ModelVisual3D device3D = new ModelVisual3D();
            //device3D.Content = Display3d(MODEL_PATH);
            // Add to view port
            //viewPort3d.Children.Add(device3D);

            cbWingsStrat.ItemsSource = wingsStrategies;
            cbWingStrat.ItemsSource = wingStrategies;
            cbStoriesStrat.ItemsSource = storiesStrategies;
            cbStoryStrat.ItemsSource = storyStrategies;
            cbWallsStrat.ItemsSource = wallsStrategies;
            cbRoofStrat.ItemsSource = roofStrategies;
        }

        private void cbWingStrat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedWingStrat = cbWingStrat.SelectedItem.ToString() ?? string.Empty;
        }

        private void cbWingsStrat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedWingsStrat = cbWingsStrat.SelectedItem.ToString() ?? string.Empty;
        }

        private void cbStoriesStrat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedStoriesStrat = cbStoriesStrat.SelectedItem.ToString() ?? string.Empty;
        }

        private void cbStoryStrat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedStoryStrat = cbStoryStrat.SelectedItem.ToString() ?? string.Empty;
        }

        private void cbWallsStrat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedWallsStrat = cbWallsStrat.SelectedItem.ToString() ?? string.Empty;
        }

        private void cbRoofStrat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedRoofStrat = cbRoofStrat.SelectedItem.ToString() ?? string.Empty;
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {

            // TODO: megtisztítani az összes textboxos inputot
            // TODO: vhogy feketére színezni az összeset

            viewPort3d.Children.Clear();
            viewPort3d.Children.Add(new SunLight());
            viewPort3d.Children.Add(new GridLinesVisual3D());

            var settings = new BuildingSettings();
            settings.Size = new Vector2Int(Int32.Parse(tbSizeWidth.Text), Int32.Parse(tbSizeHeight.Text));
            settings.Stories = Int32.Parse(tbStoryTo.Text);
            settings.Wings = Int32.Parse(tbWingTo.Text);
            settings.storiesStrategy = StrategyResolver.ResolveStoriesStratFromName(selectedStoriesStrat);
            settings.wingsStrategy = StrategyResolver.ResolveWingsStratFromName(selectedWingsStrat);

            var generator = new BuildingGenerationOrchestrator(settings);

            var objStr = generator.GenerateBuildingToDisplay();

            DisplayGeneratedBuilding(objStr);

            btnSave.IsEnabled = true;
        }

        /*
        private Model3D Display3d(string modelPath)
        {
            Model3D device = null;
            try
            {
                ModelImporter import = new ModelImporter();
                device = import.Load(modelPath);
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception Error : " + e.StackTrace);
            }
            return device;
        }
        */
        public void DisplayGeneratedBuilding(string objString)
        {
            using (var strStream = new MemoryStream(Encoding.UTF8.GetBytes(objString)))
            {
                var dispatcher = Dispatcher.CurrentDispatcher;
                //var strStream = new MemoryStream(Encoding.UTF8.GetBytes(objString));

                ModelVisual3D device3D = new ModelVisual3D();
                ObjReader objReader = new ObjReader(dispatcher)
                {
                    SwitchYZ = true
                };
                //objReader.IgnoreErrors = true;
                // TODO: ha új modelt akarok generálni akkor elszáll ennél a sornál a téma, javítani
                Model3DGroup model3DGroup = objReader.Read(strStream);
                device3D.Content = model3DGroup;
                viewPort3d.Children.Add(device3D);

            }


            

            

            //device3D.Content = Display3d(MODEL_PATH);
            // Add to view port
            //viewPort3d.Children.Add(device3D);
        }
    }
}
