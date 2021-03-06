﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using XCase.Model;

namespace XCase.Gui
{
    /// <summary>
    /// Interaction logic for ExceptionWindow.xaml
    /// </summary>
    public partial class ExceptionWindow : Window
    {
        private Exception exception; 

        public ExceptionWindow()
        {
            InitializeComponent();
        }

        public ExceptionWindow(Exception exception)
            : this()
        {
            this.exception = exception;
			
			XCaseException xe = exception as XCaseException;
			
            tbExMsg.Content = exception.Message;
			if (xe != null)
			{
				tbExInner.Content = String.Empty;
				expander1.Visibility = Visibility.Collapsed;
				textBlock1.Content = xe.ExceptionTitle;
				this.Title = xe.ExceptionTitle;
				button1.Content = "Ok";
				button2.Visibility = Visibility.Collapsed;
			}
			else
			{
				if (exception.InnerException != null)
				{
					tbExInner.Content = "Inner exception: " + exception.InnerException.Message;
				}
				else
				{
					tbExInner.Content = String.Empty;
				}
			}
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void expander1_Expanded(object sender, RoutedEventArgs e)
        {
            if (expander1.IsExpanded)
            {
                tbExStack.Content = exception.StackTrace;
            }
            else
            {
                tbExStack.Content = String.Empty;
            }
        }
    }
}
