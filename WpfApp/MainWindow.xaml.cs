using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private string? _currentInput = "";
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var content = button?.Content.ToString();

        if (content == "=")
        {
            try
            {
                _currentInput = new DataTable().Compute(_currentInput, null).ToString();
                Display.Text = _currentInput;
            }
            catch
            {
                Display.Text = "Error";
            }
        }
        else if (content == "C")
        {
            _currentInput = "";
            Display.Text = "";
        }
        else
        {
            _currentInput += content;
            Display.Text = _currentInput;
        }
    }
}