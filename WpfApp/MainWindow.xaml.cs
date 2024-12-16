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
                Display.Text = StringHelper.FormatOutput(_currentInput);
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
            _currentInput += content;
            Display.Text = _currentInput;
        }
    }
    
    private void OpenScientificWindow(object sender, RoutedEventArgs e)
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