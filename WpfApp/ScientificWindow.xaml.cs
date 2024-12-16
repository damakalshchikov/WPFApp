using System.Windows;
using System.Windows.Input;

namespace WpfApp;

public partial class ScientificWindow : Window
{
    // Запуск
    public ScientificWindow()
    {
        InitializeComponent();
    }

    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }
    
    // Обработчик кнопки закрытия окна с доп. функциями
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}