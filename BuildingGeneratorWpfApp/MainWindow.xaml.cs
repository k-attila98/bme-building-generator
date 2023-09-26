using BuildingGenerator.BuildingGenerator.Generation_Settings;
using BuildingGenerator.Prefabs.Floors;
using BuildingGenerator.Prefabs.Roofs;
using BuildingGenerator.Prefabs.Walls;
using BuildingGenerator.Serialization;
using BuildingGenerator.Shared;
using HelixToolkit.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
        private string selectedWingsStrat, selectedWingStrat, selectedStoriesStrat, selectedStoryStrat, selectedWallsStrat, selectedRoofStrat;

        private List<string> wingsStrategies = StrategyResolver.GetAllWingsStrats();
        private List<string> wingStrategies = StrategyResolver.GetAllWingStrats();
        private List<string> wallsStrategies = StrategyResolver.GetAllWallsStrats();
        private List<string> storiesStrategies = StrategyResolver.GetAllStoriesStrats();
        private List<string> storyStrategies = StrategyResolver.GetAllStoryStrats();
        private List<string> roofStrategies = StrategyResolver.GetAllRoofStrats();

        private string objStr = string.Empty;
        private BuildingGenerationOrchestrator generator = new BuildingGenerationOrchestrator();
        private BuildingSettings settings = new BuildingSettings();

        private bool isWingsNumsValid = true, isStoriesNumsvalid = true, isSizeNumsValid = true;

        string selectedTexturingPrefabPath = "";

        public MainWindow()
        {
            InitializeComponent();

            cbWingsStrat.ItemsSource = wingsStrategies;
            cbWingStrat.ItemsSource = wingStrategies;
            cbStoriesStrat.ItemsSource = storiesStrategies;
            cbStoryStrat.ItemsSource = storyStrategies;
            cbWallsStrat.ItemsSource = wallsStrategies;
            cbRoofStrat.ItemsSource = roofStrategies;

            aplWalls.PrefabSelectionChanged += new AddedPrefabsList.PrefabSelectionChangedHandler(_SetViewPortToSelectedPrefab);
            aplRoofs.PrefabSelectionChanged += new AddedPrefabsList.PrefabSelectionChangedHandler(_SetViewPortToSelectedPrefab);
            aplFloor.PrefabSelectionChanged += new AddedPrefabsList.PrefabSelectionChangedHandler(_SetViewPortToSelectedPrefab);

            aplWallsTexturing.PrefabSelectionChanged += new AddedPrefabsList.PrefabSelectionChangedHandler(_SetTexturingViewPortToSelectedPrefab);
            aplRoofsTexturing.PrefabSelectionChanged += new AddedPrefabsList.PrefabSelectionChangedHandler(_SetTexturingViewPortToSelectedPrefab);
            aplFloorTexturing.PrefabSelectionChanged += new AddedPrefabsList.PrefabSelectionChangedHandler(_SetTexturingViewPortToSelectedPrefab);
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
            //TODO: a fájl nevét kicseérlni hogy ne relatív path legyen (lehet így nem működik még nem próbáltam)
            string path = $"../../../../BuildingGenerator/Generated/building-{DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss")}.obj";

            _SaveViewPortContent(ref viewPort3d, path);
            //TODO: ugyanez igaz importra is
        }

        private void _RenameMtl(string dir, ObjExporter objExporter)
        {
            
        }

        private void _SaveViewPortContent(ref HelixViewport3D viewPort, string pathOfSave)
        {
            viewPort.Children.Remove(viewPort.Children.FirstOrDefault(c => c.GetType() == typeof(SunLight)));
            viewPort.Children.Remove(viewPort.Children.FirstOrDefault(c => c.GetType() == typeof(GridLinesVisual3D)));

            var dir = System.IO.Path.GetDirectoryName(pathOfSave) ?? ".";
            System.IO.Directory.CreateDirectory(dir);
            var filename = System.IO.Path.GetFileName(pathOfSave);

            Guid guidName = Guid.NewGuid();

            var objExporter = new CustomObjExporter()
            {
                TextureFolder = dir,
                FileCreator = f => File.Create(System.IO.Path.Combine(dir, f)),
                SwitchYZ = true,
                TextureFileName = guidName.ToString()
            };


            using (var stream = File.Create(pathOfSave))
            {
                objExporter.MaterialsFile = System.IO.Path.ChangeExtension(filename, ".mtl");
                objExporter.Export(viewPort.Viewport, stream);
            }

            btnSave.IsEnabled = false;

            viewPort.Children.Add(new SunLight());
            viewPort.Children.Add(new GridLinesVisual3D() { Width = 8.0, Length = 8.0, MinorDistance = 1.0, MajorDistance = 1.0, Thickness = 0.01 });
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

        private void btnLoadWall_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Wavefront OBJ files (*.obj)|*.obj";
            ofd.Title = "Select Wavefront OBJ to load Wall prefab";

            if (ofd.ShowDialog() == true)
            {
                txtBlockSelectedWall.Text = ofd.FileName;
                ObjReader reader = new ObjReader();

                _LoadObjIntoViewPortByPath(ref prefabViewer, ofd.FileName);
                btnAddWall.IsEnabled = true;
            }
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

        private void btnAddWall_Click(object sender, RoutedEventArgs e)
        {
            aplWalls.Prefabs.Add(txtBlockSelectedWall.Text);
            aplWallsTexturing.Prefabs.Add(txtBlockSelectedWall.Text);
        }

        private void btnAddRoof_Click(object sender, RoutedEventArgs e)
        {
            aplRoofs.Prefabs.Add(txtBlockSelectedRoof.Text);
            aplRoofsTexturing.Prefabs.Add(txtBlockSelectedRoof.Text);
        }

        private void btnAddFloor_Click(object sender, RoutedEventArgs e)
        {
            aplFloor.Prefabs.Add(txtBlockSelectedFloor.Text);
            aplFloorTexturing.Prefabs.Add(txtBlockSelectedFloor.Text);
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

        private void btnLoadRoof_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Wavefront OBJ files (*.obj)|*.obj";
            ofd.Title = "Select Wavefront OBJ to load Roof prefab";

            if (ofd.ShowDialog() == true)
            {
                txtBlockSelectedRoof.Text = ofd.FileName;

                _LoadObjIntoViewPortByPath(ref prefabViewer, ofd.FileName);
                btnAddRoof.IsEnabled = true;
            }
        }

        private void btnLoadFloor_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Wavefront OBJ files (*.obj)|*.obj";
            ofd.Title = "Select Wavefront OBJ to load Floor prefab";

            if (ofd.ShowDialog() == true)
            {
                txtBlockSelectedFloor.Text = ofd.FileName;

                _LoadObjIntoViewPortByPath(ref prefabViewer, ofd.FileName);
                selectedTexturingPrefabPath = ofd.FileName;
                btnAddFloor.IsEnabled = true;
            }
        }

        private void cbWallsStrat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedWallsStrat = cbWallsStrat.SelectedItem.ToString() ?? string.Empty;
        }

        private void btnLoadTexture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            ofd.Title = "Select picture to be used as a texture on the selected prefab";

            if (ofd.ShowDialog() == true)
            {
                txtBlockSelectedTexture.Text = ofd.FileName;
                _LoadObjIntoViewPortByObjStringAndTexturePath(ref prefabTextureViewer, File.ReadAllText(selectedTexturingPrefabPath), ofd.FileName);
                btnAddTexture.IsEnabled = true;
            }
        }

        private void btnAddTexture_Click(object sender, RoutedEventArgs e)
        {
            string textureFilename = System.IO.Path.GetFileNameWithoutExtension(txtBlockSelectedTexture.Text);
            string prefabFilename = System.IO.Path.GetFileNameWithoutExtension(selectedTexturingPrefabPath);
            string prefabDir = System.IO.Path.GetDirectoryName(selectedTexturingPrefabPath) ?? ".";
            string texturedPrefabPath = System.IO.Path.Combine(prefabDir, $"{prefabFilename}-{textureFilename}/{prefabFilename}-{textureFilename}.obj") ;

            _SaveViewPortContent(ref prefabTextureViewer, texturedPrefabPath);
        }

        private void cbRoofStrat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedRoofStrat = cbRoofStrat.SelectedItem.ToString() ?? string.Empty;
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            settings.wingsStrategy = StrategyResolver.ResolveWingsStratFromName(selectedWingsStrat);
            settings.wingStrategy = StrategyResolver.ResolveWingStratFromName(selectedWingStrat);
            settings.storiesStrategy = StrategyResolver.ResolveStoriesStratFromName(selectedStoriesStrat);
            settings.storyStrategy = StrategyResolver.ResolveStoryStratFromName(selectedStoryStrat);
            settings.wallsStrategy = StrategyResolver.ResolveWallsStratFromName(selectedWallsStrat);
            settings.roofStrategy = StrategyResolver.ResolveRoofStratFromName(selectedRoofStrat);


            settings.Size = new Vector2Int(Int32.Parse(tbSizeWidth.Text), Int32.Parse(tbSizeHeight.Text));
            settings.Wings = new Vector2Int(Int32.Parse(tbWingFrom.Text), Int32.Parse(tbWingTo.Text));
            settings.Stories = new Vector2Int(Int32.Parse(tbStoryFrom.Text), Int32.Parse(tbStoryTo.Text));

            generator.AddSettings(settings);

            if (checkBoxUsePrefabs.IsChecked ?? false)
            {
                generator.DeserializeMultipleWallTransformsFromObj(aplWalls.Prefabs);
                generator.DeserializeMultipleRoofTransformsFromObj(aplRoofs.Prefabs);
                generator.DeserializeFloorTransformFromObj(aplFloor.Prefabs.ElementAt(0));

            }

            //objStr = generator.GenerateBuildingToDisplay(checkBoxUsePrefabs.IsChecked ?? false);
            var objsToTexture = generator.GenerateBuildingToDisplayForTexturing(checkBoxUsePrefabs.IsChecked ?? false);

            //VertexIdProvider.Reset();
            _ResetViewPort(ref viewPort3d, 8.0);
            foreach (var key in objsToTexture.Keys)
            {
                _AddObjIntoViewPortByObjStringAndTexturePath(ref viewPort3d, objsToTexture[key], key);
            }

            btnSave.IsEnabled = true;
        }

        public void DisplayGeneratedBuilding(string objString)
        {
            _ResetViewPort(ref viewPort3d, 8.0);
            _LoadObjIntoViewPortByObjString(ref viewPort3d, objString);
        }

        private void _LoadObjIntoViewPortByObjString(ref HelixViewport3D viewPort, string objString)
        {
            _ResetViewPort(ref viewPort, 8.0);
            if (objString == null || objString.Length == 0)
            {
                return;
            }

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
                viewPort.Children.Add(device3D);

            }
        }

        private void _LoadObjIntoViewPortByPath(ref HelixViewport3D viewPort, string path)
        {
            _ResetViewPort(ref viewPort, 8.0);
            if (path == null || path.Length == 0)
            {
                return;
            }

            var dispatcher = Dispatcher.CurrentDispatcher;

            ModelVisual3D device3D = new ModelVisual3D();
            ObjReader objReader = new ObjReader(dispatcher)
            {
                SwitchYZ = true
            };
            Model3DGroup model3DGroup = objReader.Read(path);
            device3D.Content = model3DGroup;
            viewPort.Children.Add(device3D);


        }

        private void _LoadObjIntoViewPortByObjStringAndTexturePath(ref HelixViewport3D viewPort, string objString, string texturePath)
        {
            _ResetViewPort(ref viewPort, 8.0);
            if (objString == null || objString.Length == 0)
            {
                return;
            }

            using (var strStream = new MemoryStream(Encoding.UTF8.GetBytes(objString)))
            {
                var dispatcher = Dispatcher.CurrentDispatcher;

                ModelVisual3D device3D = new ModelVisual3D();
                ObjReader objReader = new ObjReader(dispatcher)
                {
                    SwitchYZ = true,
                    DefaultMaterial = new DiffuseMaterial(
                            new ImageBrush(
                                new BitmapImage(
                                    new Uri(texturePath, UriKind.Absolute)
                                    )
                                )
                            { TileMode = TileMode.None, Stretch = Stretch.Fill, ViewportUnits = BrushMappingMode.Absolute }
                        )

                };
                Model3DGroup model3DGroup = objReader.Read(strStream);
                
                device3D.Content = model3DGroup;
                viewPort.Children.Add(device3D);

            }
        }

        private void _AddObjIntoViewPortByObjStringAndTexturePath(ref HelixViewport3D viewPort, string objString, string texturePath)
        {

            if (objString == null || objString.Length == 0)
            {
                return;
            }

            using (var strStream = new MemoryStream(Encoding.UTF8.GetBytes(objString)))
            {
                var dispatcher = Dispatcher.CurrentDispatcher;
                ObjReader objReader = null;
                ModelVisual3D device3D = new ModelVisual3D();
                if (texturePath != "null")
                    objReader = new ObjReader(dispatcher)
                    {
                        SwitchYZ = true,
                        DefaultMaterial = new DiffuseMaterial(
                                new ImageBrush(
                                    new BitmapImage(
                                        new Uri(texturePath, UriKind.Absolute)
                                        )
                                    )
                                { TileMode = TileMode.None, Stretch = Stretch.Fill, ViewportUnits = BrushMappingMode.Absolute }
                            )

                    };
                else
                    objReader = new ObjReader(dispatcher)
                    {
                        SwitchYZ = true
                    };
                Model3DGroup model3DGroup = objReader.Read(strStream);

                device3D.Content = model3DGroup;
                viewPort.Children.Add(device3D);
            }
        }

        private void _ResetViewPort(ref HelixViewport3D viewPort, double sideLength)
        {
            viewPort.Children.Clear();
            viewPort.Children.Add(new SunLight());
            viewPort.Children.Add(new GridLinesVisual3D() { Width = sideLength, Length = sideLength, MinorDistance = 1.0, MajorDistance = 1.0, Thickness = 0.01 });
        }

        private void _SetViewPortToSelectedPrefab(object sender, string newPath)
        {
            if (newPath == null || newPath.Length == 0)
            {
                _ResetViewPort(ref prefabViewer, 8.0);
                return;
            }
            _LoadObjIntoViewPortByPath(ref prefabViewer, newPath);
        }

        private void _SetTexturingViewPortToSelectedPrefab(object sender, string newPath)
        {
            if (newPath == null || newPath.Length == 0)
            {
                _ResetViewPort(ref prefabViewer, 8.0);
                return;
            }
            _LoadObjIntoViewPortByPath(ref prefabTextureViewer, newPath);
            selectedTexturingPrefabPath = newPath;
        }

        /*
        private void asd()
        { 
            HelixToolkit.Wpf.ObjExporter asd = new HelixToolkit.Wpf.ObjExporter();
            asd.Export(viewPort3d.Children.ElementAt(0).Content, "C:\\Users\\michal\\Desktop\\test.obj");
        }
            */

    }
}
