using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MinecraftTreeGenerator
{
    public partial class MainWindow : Window
    {
        private const int MapWidth = 20;
        private const int MapHeight = 20;
        private const int CellSize = 20;
        private int[,] map;
        private const double TreeProbability = 0.2;

        public MainWindow()
        {
            InitializeComponent();
            InitializeMap();
            DrawMap();
        }

        private void InitializeMap()
        {
            map = new int[MapWidth, MapHeight];
            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    map[x, y] = 0;
                }
            }
        }

        private void GenerateMap()
        {
            Random random = new Random();

            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    if (random.NextDouble() < TreeProbability)
                    {
                        map[x, y] = random.Next(6, 17);
                    }
                    else
                    {
                        map[x, y] = 0;
                    }
                }
            }
        }

        private void DrawMap()
        {
            MapCanvas.Children.Clear();

            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    if (map[x, y] > 0)
                    {
                        DrawTree(x, y, map[x, y]);
                    }
                    else
                    {
                        DrawGround(x, y);
                    }
                }
            }
        }

        private void DrawGround(int x, int y)
        {
            Rectangle groundBlock = new Rectangle
            {
                Width = CellSize,
                Height = CellSize,
                Fill = Brushes.SaddleBrown,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            Canvas.SetLeft(groundBlock, x * CellSize);
            Canvas.SetTop(groundBlock, y * CellSize);
            MapCanvas.Children.Add(groundBlock);

            TextBlock textBlock = new TextBlock
            {
                Text = "0",
                Foreground = Brushes.Black,
                FontWeight = FontWeights.Bold,
                Width = CellSize,
                Height = CellSize,
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            Canvas.SetLeft(textBlock, x * CellSize);
            Canvas.SetTop(textBlock, y * CellSize);
            MapCanvas.Children.Add(textBlock);
        }

        private void DrawTree(int x, int y, int height)
        {
            Color treeColor = GetColorFromHeight(height);

            Rectangle treeBlock = new Rectangle
            {
                Width = CellSize,
                Height = CellSize,
                Fill = new SolidColorBrush(treeColor),
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            Canvas.SetLeft(treeBlock, x * CellSize);
            Canvas.SetTop(treeBlock, y * CellSize);
            MapCanvas.Children.Add(treeBlock);

            TextBlock textBlock = new TextBlock
            {
                Text = height.ToString(),
                Foreground = Brushes.Black,
                FontWeight = FontWeights.Bold,
                Width = CellSize,
                Height = CellSize,
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            Canvas.SetLeft(textBlock, x * CellSize);
            Canvas.SetTop(textBlock, y * CellSize);
            MapCanvas.Children.Add(textBlock);
        }

        private Color GetColorFromHeight(int height)
        {
            double ratio = (height - 6) / 10.0;
            byte red = (byte)(255 * ratio);
            byte green = (byte)(255 * (1 - ratio));
            return Color.FromRgb(red, green, 110);
        }

        private void GenerateTrees_Click(object sender, RoutedEventArgs e)
        {
            GenerateMap();
            DrawMap();
        }
    }
}
