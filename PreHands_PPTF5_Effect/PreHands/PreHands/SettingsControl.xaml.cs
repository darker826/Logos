using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PreHands
{
    /// <summary>
    /// SettingsControl.xaml에 대한 상호 작용 논리
    /// </summary>
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

    }
}
