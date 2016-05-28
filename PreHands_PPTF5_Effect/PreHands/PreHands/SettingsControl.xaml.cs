using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;


namespace PreHands
{
    /// <summary>
    /// SettingsControl.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    public partial class SettingsControl
    {


        public static readonly DependencyProperty SettingsProperty =
           DependencyProperty.Register("Settings", typeof(Settings), typeof(SettingsControl), new PropertyMetadata(null));

        public SettingsControl()
        {
            this.InitializeComponent();
            this.LayoutRoot.DataContext = this;
        }

        public Settings Settings
        {
            get
            {
                return (Settings)this.GetValue(SettingsProperty);
            }

            set
            {
                this.SetValue(SettingsProperty, value);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("PPT Mode가 시작됩니다.");
            e.Handled = true;
            MainWindow.isPPTActive = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Mouse Mode가 시작됩니다.");
            e.Handled = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("녹화를 시작합니다.");
            e.Handled = true;
        }
    }
}
