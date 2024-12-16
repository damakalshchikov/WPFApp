using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp.Utils;

namespace WpfApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private string? _currentInput = "";
    private ScientificWindow? _scientificWindow;
    
    // Запуск
    public MainWindow()
    {
        InitializeComponent();
        Closed += MainWindow_Closed;
    }
    
    // Обработчик кнопок калькулятора
    public void Button_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var content = button?.Content.ToString();

        if (content == "=")
        {
            try
            {   
                _currentInput = StringHelper.SanitizeInput(_currentInput);
                _currentInput = new DataTable().Compute(_currentInput, null).ToString();
                
                if (Regex.IsMatch(_currentInput, @"E-([1-9]|1[0-9])$"))
                {
                    Display.Text = "0";
                }
                else
                {
                    Display.Text = StringHelper.FormatOutput(_currentInput);   
                }
                
                _currentInput = "";
            }
            catch
            {
                Display.Text = "Error";
                _currentInput = "";
            }
        }
        else if (content == "AC")
        {
            _currentInput = "";
            Display.Text = "0";
        }
        else
        {
            if (content == "x²")
            {
                _currentInput = $"{_currentInput}²";
            }
            else if (content == "xⁿ")
            {
                _currentInput = $"{_currentInput}^";
            }
            else if (content == "x!")
            {
                _currentInput = $"{_currentInput}!";
            }
            else if (content == "1/x")
            {
                _currentInput = $"{_currentInput}/";
            }
            else if (content == "sin" || content == "cos" || content == "tg")
            {
                switch (content)
                {
                    case "sin":
                        _currentInput = $"sin({_currentInput})";
                        break;
                    case "cos":
                        _currentInput = $"cos({_currentInput})";
                        break;
                    case "tg":
                        _currentInput = $"tg({_currentInput})";
                        break;
                }
            }
            else if (content == "ln")
            {
                _currentInput = $"ln({_currentInput})";
            }
            else
            {
                _currentInput += content;
            }
            
            Display.Text = _currentInput;
        }
    }
    
    // Открытие или закрытие окна с доп. функциями
    private void OpenOrCloseScientificWindow(object sender, RoutedEventArgs e)
    {
        if (_scientificWindow == null) // Если окно ещё не открыто
        {
            _scientificWindow = new ScientificWindow();
            _scientificWindow.Closed += (s, args) => _scientificWindow = null;
            _scientificWindow.Show();
        }
        else
        {
            _scientificWindow.Close(); // Закрываем окно, если оно уже открыто
            _scientificWindow = null;
        }
    }

    // Перемещение окна ЛКМ
    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }
    
    // Закртыие доп. окна, если главное окно закрылось
    private void MainWindow_Closed(object? sender, EventArgs e)
    {
        if (_scientificWindow != null)
        {
            _scientificWindow.Close(); // Закрыть окно с математическими функциями
            _scientificWindow = null;
        }
    }
    
    // Обработчик кнокпки закрытия главного окна
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

}