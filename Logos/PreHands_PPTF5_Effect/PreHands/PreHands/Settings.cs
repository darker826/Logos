using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Scope = "member", Target = "Microsoft.Samples.Kinect.AdaptiveUI.Settings.#.cctor()", Justification = "Complexity is caused by long list of static properties. This is not real complexity.")]

namespace PreHands 
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;

    public class Settings : DependencyObject
    {
        public static readonly DependencyProperty DisplayWidthInMetersProperty =
           DependencyProperty.Register("DisplayWidthInMeters", typeof(double), typeof(Settings), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (o, args) => ((Settings)o).OnParameterChanged()), checkValue => ValidateDouble(checkValue, MinDisplayDimensionInMeters, MaxDisplayDimensionInMeters));

        public static readonly DependencyProperty DisplayHeightInMetersProperty =
            DependencyProperty.Register("DisplayHeightInMeters", typeof(double), typeof(Settings), new FrameworkPropertyMetadata(0.56, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (o, args) => ((Settings)o).OnParameterChanged()), checkValue => ValidateDouble(checkValue, MinDisplayDimensionInMeters, MaxDisplayDimensionInMeters));

        public static readonly DependencyProperty DisplayWidthInPixelsProperty =
            DependencyProperty.Register("DisplayWidthInPixels", typeof(double), typeof(Settings), new FrameworkPropertyMetadata(1280.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (o, args) => ((Settings)o).OnParameterChanged()), checkValue => ValidateDouble(checkValue, MinDisplayDimensionInPixels, MaxDisplayDimensionInPixels));

        public static readonly DependencyProperty DisplayHeightInPixelsProperty =
            DependencyProperty.Register("DisplayHeightInPixels", typeof(double), typeof(Settings), new FrameworkPropertyMetadata(1024.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (o, args) => ((Settings)o).OnParameterChanged()), checkValue => ValidateDouble(checkValue, MinDisplayDimensionInPixels, MaxDisplayDimensionInPixels));

        public static readonly DependencyProperty FullScreenProperty =
            DependencyProperty.Register("FullScreen", typeof(bool), typeof(Settings), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (o, args) => ((Settings)o).OnParameterChanged()));

        private const double MinDisplayDimensionInMeters = 0.001;
        private const double MaxDisplayDimensionInMeters = 20;

        private const double MinDisplayDimensionInPixels = 1;
        private const double MaxDisplayDimensionInPixels = 10000;

        private const double MaxHysteresis = 10.0;

        public event EventHandler<EventArgs> ParameterChanged;

        public double DisplayWidthInMeters
        {
            get
            {
                return (double)this.GetValue(DisplayWidthInMetersProperty);
            }

            set
            {
                this.SetValue(DisplayWidthInMetersProperty, value);
            }
        }

        public double DisplayHeightInMeters
        {
            get
            {
                return (double)this.GetValue(DisplayHeightInMetersProperty);
            }

            set
            {
                this.SetValue(DisplayHeightInMetersProperty, value);
            }
        }

        public double DisplayWidthInPixels
        {
            get
            {
                return (double)this.GetValue(DisplayWidthInPixelsProperty);
            }

            set
            {
                this.SetValue(DisplayWidthInPixelsProperty, value);
            }
        }

        public bool FullScreen
        {
            get
            {
                return (bool)this.GetValue(FullScreenProperty);
            }

            set
            {
                this.SetValue(FullScreenProperty, value);
            }
        }

        public double DisplayHeightInPixels
        {
            get
            {
                return (double)this.GetValue(DisplayHeightInPixelsProperty);
            }

            set
            {
                this.SetValue(DisplayHeightInPixelsProperty, value);
            }
        }
        private static bool ValidateDouble(object checkValue, double minValue, double maxValue)
        {
            var doubleValue = (double)checkValue;
            return doubleValue >= minValue && doubleValue <= maxValue;
        }

        private void OnParameterChanged()
        {
            if (this.ParameterChanged != null)
            {
                this.ParameterChanged(this, new EventArgs());
            }
        }

    }
}

