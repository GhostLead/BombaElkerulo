using System;
using System.Collections.Generic;
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

namespace WpfAppAknaVagyJegkeresoBabuLepegetoCucc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GenerateGrid(30,10);
            grid.Background = Brushes.LightGreen;
            PlaceBombs(150);
            PlacePlayers(10);
        }

        public void GenerateGrid(int rows, int cols)
        {
            for (int i = 0; i < rows; i++)
            {
                RowDefinition row= new RowDefinition();
                grid.RowDefinitions.Add(row);
            }
            for (int i = 0; i < cols; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                grid.ColumnDefinitions.Add(col);
            }

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < cols; column++)
                {
                    Border cellBorder = new Border
                    {
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(1)
                    };

                    Grid.SetRow(cellBorder, row);
                    Grid.SetColumn(cellBorder, column);
                    grid.Children.Add(cellBorder);
                }
            }

        }

        public void PlaceBombs(int db)
        {
            Random rnd = new Random();

            for (int i = 0; i < db; i++)
            {
                int row = rnd.Next(grid.RowDefinitions.Count);
                int col = rnd.Next(grid.ColumnDefinitions.Count);

                Ellipse bomb = new Ellipse();
                bomb.Width = 22;
                bomb.Height = 22;
                bomb.Fill = Brushes.Black;
                Grid.SetRow(bomb, row);
                Grid.SetColumn(bomb, col);
                grid.Children.Add(bomb);

            }
        }

        public void PlacePlayers(int db)
        {
            Random rnd = new Random();

            List<int[]> occupiedCells = new List<int[]>();

            foreach (var child in grid.Children)
            {
                if (child is Border border && border.Child is Ellipse)
                {
                    int row = Grid.GetRow(border);
                    int col = Grid.GetColumn(border);
                    occupiedCells.Add(new int[] { row, col });
                }
            }

            for (int i = 0; i < db; i++)
            {
                int row = rnd.Next(grid.RowDefinitions.Count);
                int col = rnd.Next(grid.ColumnDefinitions.Count);

                bool isOccupied = false;

                foreach (var cell in occupiedCells)
                {
                    if (cell[0] == row && cell[1] == col)
                    {
                        isOccupied = true;
                        break;
                    }
                }

                if (!isOccupied)
                {
                    Button Player = new Button();
                    Player.Content = i + 1;
                    Player.Background = Brushes.Yellow;
                    Player.Foreground = Brushes.Black;
                    Player.Opacity = 0.7;
                    Grid.SetRow(Player,row);
                    Grid.SetColumn(Player,col);
                    grid.Children.Add(Player);
                }

            }

            

        }

    }
}
