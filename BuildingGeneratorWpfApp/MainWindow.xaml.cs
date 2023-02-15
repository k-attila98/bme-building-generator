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
using System.Text.RegularExpressions;
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

        private string objStr = string.Empty;
        private BuildingGenerationOrchestrator generator;
        private BuildingSettings settings = new BuildingSettings();

        private bool isWingsNumsValid = true, isStoriesNumsvalid = true, isSizeNumsValid = true;

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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            generator.SerializeBuildingFromStr(objStr);
            btnSave.IsEnabled = false;
        }

        private void cbStoryStrat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedStoryStrat = cbStoryStrat.SelectedItem.ToString() ?? string.Empty;
        }

        private bool _IsTextboxValueNumber(TextBox tb)
        {
            return Regex.IsMatch(tb.Text, "^[0-9]*$");
        }

        private void _DisplayErrorInLabel(Label label, string errorMessage)
        {
            label.Content = errorMessage;
            btnGenerate.IsEnabled = false;
            btnSave.IsEnabled = false;
        }

        private void _ResetErrorInLabel(Label label)
        {
            label.Content = "";
            btnGenerate.IsEnabled = isWingsNumsValid && isStoriesNumsvalid && isSizeNumsValid;
        }

        private void tbStoriesFromTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!this.IsLoaded)
            {
                return;
            }

            if (tbStoryFrom.Text == "" || tbStoryTo.Text == "")
            {
                _DisplayErrorInLabel(labelErrorStories, "Fill all textboxes!");
                isStoriesNumsvalid = false;
                return;
            }

            if (!_IsTextboxValueNumber(tbStoryFrom) || !_IsTextboxValueNumber(tbStoryTo))
            {
                _DisplayErrorInLabel(labelErrorStories, "Stories From or To is not a number!");
                isStoriesNumsvalid = false;
                return;
            }

            if (Int32.Parse(tbStoryFrom.Text) < 1 || Int32.Parse(tbStoryTo.Text) < 1)
            {
                _DisplayErrorInLabel(labelErrorStories, "Stories From or To cannot be smaller than 1!");
                isStoriesNumsvalid = false;
                return;
            }

            if (Int32.Parse(tbStoryFrom.Text) > Int32.Parse(tbStoryTo.Text))
            {
                _DisplayErrorInLabel(labelErrorStories, "Stories From cannot be greater than To!");
                isStoriesNumsvalid = false;
                return;
            }

            
            isStoriesNumsvalid = true;
            _ResetErrorInLabel(labelErrorStories);
        }

        private void tbSizeWidthHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!this.IsLoaded)
            {
                return;
            }

            if (tbSizeWidth.Text == "" || tbSizeHeight.Text == "")
            {
                _DisplayErrorInLabel(labelErrorWidthHeight, "Fill all textboxes!");
                isSizeNumsValid = false;
                return;
            }

            if (!_IsTextboxValueNumber(tbSizeWidth) || !_IsTextboxValueNumber(tbSizeHeight))
            {
                _DisplayErrorInLabel(labelErrorWidthHeight, "Size Width or Height is not a number!");
                isSizeNumsValid = false;
                return;
            }

            if (Int32.Parse(tbSizeWidth.Text) < 1 || Int32.Parse(tbSizeHeight.Text) < 1)
            {
                _DisplayErrorInLabel(labelErrorWidthHeight, "Size Width or Height cannot be smaller than 1!");
                isSizeNumsValid = false;
                return;
            }

            
            isSizeNumsValid = true;
            _ResetErrorInLabel(labelErrorWidthHeight);
        }

        private void tbWingFromTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!this.IsLoaded)
            {
                return;
            }

            if (tbWingFrom.Text == "" || tbWingTo.Text == "")
            {
                _DisplayErrorInLabel(labelErrorWings, "Fill all textboxes!");
                isWingsNumsValid = false;
                return;
            }

            if (!_IsTextboxValueNumber(tbWingFrom) || !_IsTextboxValueNumber(tbWingTo))
            {

                _DisplayErrorInLabel(labelErrorWings, "Wing From or To is not a number!");
                isWingsNumsValid = false;
                return;
            }

            if (Int32.Parse(tbWingFrom.Text) < 1 || Int32.Parse(tbWingTo.Text) < 1)
            {
                _DisplayErrorInLabel(labelErrorWings, "Wings From or To cannot be smaller than 1!");
                isWingsNumsValid = false;
                return;
            }

            if (Int32.Parse(tbWingFrom.Text) > Int32.Parse(tbWingTo.Text))
            {
                _DisplayErrorInLabel(labelErrorWings, "Wing From cannot be greater than To!");
                isWingsNumsValid = false;
                return;
            }

            
            isWingsNumsValid = true;
            _ResetErrorInLabel(labelErrorWings);
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

            // TODO: vhogy feketére színezni az összeset

            viewPort3d.Children.Clear();
            viewPort3d.Children.Add(new SunLight());
            viewPort3d.Children.Add(new GridLinesVisual3D() { Width = 10.0, Length = 10.0, MinorDistance = 1.0, MajorDistance = 1.0, Thickness = 0.01 });
            
            settings.storiesStrategy = StrategyResolver.ResolveStoriesStratFromName(selectedStoriesStrat);
            settings.wingsStrategy = StrategyResolver.ResolveWingsStratFromName(selectedWingsStrat);

            settings.Size = new Vector2Int(Int32.Parse(tbSizeWidth.Text), Int32.Parse(tbSizeHeight.Text));
            settings.Wings = new Vector2Int(Int32.Parse(tbWingFrom.Text), Int32.Parse(tbWingTo.Text));
            settings.Stories = new Vector2Int(Int32.Parse(tbStoryFrom.Text), Int32.Parse(tbStoryTo.Text));

            generator = new BuildingGenerationOrchestrator(settings);

            objStr = generator.GenerateBuildingToDisplay();

            DisplayGeneratedBuilding(objStr);

            btnSave.IsEnabled = true;
        }

        public void DisplayGeneratedBuilding(string objString)
        {
            using (var strStream = new MemoryStream(Encoding.UTF8.GetBytes(objString)))
            {
                var dispatcher = Dispatcher.CurrentDispatcher;

                ModelVisual3D device3D = new ModelVisual3D();
                ObjReader objReader = new ObjReader(dispatcher)
                {
                    SwitchYZ = true
                };
                Model3DGroup model3DGroup = objReader.Read(strStream);
                device3D.Content = model3DGroup;
                viewPort3d.Children.Add(device3D);

            }
        }
    }
}
