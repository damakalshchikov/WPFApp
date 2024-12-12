﻿using System.Windows;
using System.Windows.Input;

namespace WpfApp;

public partial class ScientificWindow : Window
{
    public ScientificWindow()
    {
        InitializeComponent();
    }

    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}