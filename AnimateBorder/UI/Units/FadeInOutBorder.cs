using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace AnimateBorder.UI.Units
{
    public class FadeInOutBorder : ContentControl
    {
        public new double Opacity
        {
            get { return (double)GetValue (OpacityProperty); }
            set { SetValue (OpacityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty OpacityProperty =
            DependencyProperty.Register ("Opacity", typeof (double), typeof (FadeInOutBorder), new PropertyMetadata (1.0));

        public double CornerRadius
        {
            get { return (double)GetValue (CornerRadiusProperty); }
            set { SetValue (CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register ("CornerRadius", typeof (double), typeof (FadeInOutBorder), new PropertyMetadata (0.0, OnBorderBrushCallBack));


        public new Brush? BorderBrush
        {
            get { return (Brush)GetValue (BorderBrushProperty); }
            set { SetValue (BorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BorderBrush.  This enables animation, styling, binding, etc...
        public static readonly new DependencyProperty BorderBrushProperty =
            DependencyProperty.Register ("BorderBrush", typeof (Brush), typeof (FadeInOutBorder), new PropertyMetadata (null, OnBorderBrushCallBack));

        public Duration Interval
        {
            get { return (Duration)GetValue (IntervalProperty); }
            set { SetValue (IntervalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Interval.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register ("Interval", typeof (Duration), typeof (FadeInOutBorder), new PropertyMetadata (new Duration (new TimeSpan (0, 0, 1)), OnBorderBrushCallBack));

        static FadeInOutBorder()
        {
            DefaultStyleKeyProperty.OverrideMetadata (typeof (FadeInOutBorder), new FrameworkPropertyMetadata (typeof (FadeInOutBorder)));
        }

        private static void OnBorderBrushCallBack(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var a = (FadeInOutBorder)d;
            a.StartAnimation ();
        }

        Storyboard sb;
        public void StartAnimation()
        {
            Opacity = 1.0;
            if (BorderBrush == null)
                return;
            sb?.Pause ();
            sb = new Storyboard ()
            {
                RepeatBehavior = RepeatBehavior.Forever
            };
            DoubleAnimation FadeOutAnimation = new DoubleAnimation ()
            {
                From = 1.0,
                To = 0.0,
                Duration = Interval.TimeSpan
            };
            Storyboard.SetTargetProperty (FadeOutAnimation, new PropertyPath ("Opacity"));
            //Storyboard.SetTargetProperty(FadeInAnimation, new PropertyPath ("Opacity"));

            sb.Children.Add (FadeOutAnimation);
            //sb.Children.Add (FadeInAnimation);

            BeginStoryboard (sb);
        }

        public void EndAnimation()
        {
            if (sb == null)
                return;
        }
    }
}
