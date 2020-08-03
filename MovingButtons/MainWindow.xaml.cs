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

namespace MovingButtons
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Button[] buttons = new Button[8];

        public MainWindow()
        {
            InitializeComponent();
            buttons[0] = button1;
            buttons[1] = button2;
            buttons[2] = button3;
            buttons[3] = button4;
            buttons[4] = button5;
            buttons[5] = button6;
            buttons[6] = button7;
            buttons[7] = button8;
        }

        public void MoveButton(KeyEventArgs e)
        {
            Dictionary<(int, int), bool> values = new Dictionary<(int, int), bool>()
            {
                { (0, 0),  false}, { (0, 1),  false}, { (0, 2),  false},
                { (1, 0),  false}, { (1, 1),  false}, { (1, 2),  false},
                { (2, 0),  false}, { (2, 1),  false}, { (2, 2),  false}
            };
            Dictionary<(int, int), Button> buttonsSaved = new Dictionary<(int, int), Button>();

            foreach (var button in buttons)
            {
                var pos = (Grid.GetRow(button), Grid.GetColumn(button));
                values[pos] = true;
                buttonsSaved.Add(pos, button);
            }

            ValueTuple<int, int> free = (0, 0);

            foreach (var kvp in values)
            {
                if (!values[kvp.Key])
                {
                    free = kvp.Key;
                    break;
                }
            }

            if (e.Key == Key.Up)
            {
                if (free.Item1 < 2)
                {
                    Grid.SetRow(buttonsSaved[(free.Item1 + 1, free.Item2)], free.Item1);
                }
            }
            else if (e.Key == Key.Down)
            {
                if (free.Item1 > 0)
                {
                    Grid.SetRow(buttonsSaved[(free.Item1 - 1, free.Item2)], free.Item1);
                }
            }
            else if (e.Key == Key.Left)
            {
                if (free.Item2 < 2)
                {
                    Grid.SetColumn(buttonsSaved[(free.Item1, free.Item2 + 1)], free.Item2);
                }
            }
            else if (e.Key == Key.Right)
            {
                if (free.Item2 > 0)
                {
                    Grid.SetColumn(buttonsSaved[(free.Item1, free.Item2 - 1)], free.Item2);
                }
            }
        }

        public bool CheckForWin()
        {
            bool won = true;
            foreach (var button in buttons)
            {
                if ((Grid.GetRow(button), Grid.GetColumn(button)) == (0, 2))
                {
                    won = false;
                }
            }

            return won;
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            MoveButton(e);
            if (CheckForWin())
            {
                foreach (var button in buttons)
                {
                    button.Content = "Yes!";
                }
            }
        }
    }
}
