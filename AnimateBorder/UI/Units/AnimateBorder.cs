using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace AnimateBorder.UI.Units
{
    public class AnimateBorder : Border
    {
        public IEnumerable<SolidColorBrush> BorderBrushs
        {
            get { return (IEnumerable<SolidColorBrush>)GetValue(BorderBrushsProperty); }
            set { SetValue(BorderBrushsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderBrushsProperty =
            DependencyProperty.Register("BorderBrushs", typeof (IEnumerable<SolidColorBrush>), typeof(AnimateBorder), new PropertyMetadata(null, OnBorderBrushsCallBack));

        public Duration Interval
        {
            get { return (Duration)GetValue (IntervalProperty); }
            set { SetValue (IntervalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Interval.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register ("Interval", typeof (Duration), typeof (AnimateBorder), new PropertyMetadata (new Duration(new TimeSpan(0,0,1)), OnBorderBrushsCallBack));

        private static void OnBorderBrushsCallBack(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var a = (AnimateBorder)d;
            a.StartAnimation ();
        }
        public AnimateBorder()
        {
        }
        public override void BeginInit()
        {
            base.BeginInit ();
            //this.StartAnimation ();
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate ();
            this.StartAnimation ();
        }
        Storyboard sb;
        public void StartAnimation()
        {
            sb?.Pause ();            
            LinearGradientBrush linearGradient = new LinearGradientBrush ()
            {
                StartPoint = new Point (0, 0),
                EndPoint = new Point (1, 1),
            };
            double offset = BorderBrushs.Count () <= 1? 1.0 : 0.0;
            double nextoffect = 1.0 / (BorderBrushs.Count () -1);
            foreach (var brush in BorderBrushs)
            {
                linearGradient.GradientStops.Add (new GradientStop (((SolidColorBrush)brush).Color, offset));
                offset += nextoffect;
            }
          

            if (BorderBrushs.Count () <= 1)
                return;

            sb = new Storyboard ()
            {
                RepeatBehavior = RepeatBehavior.Forever
            };

            PointAnimation startPointAnimation1 = new PointAnimation (new Point (0, 0), new Point(0.5,0), Interval);
            PointAnimation endPointAnimation1 = new PointAnimation (new Point (1, 1), new Point(0.5, 1), Interval);
            PointAnimation startPointAnimation2 = new PointAnimation (new Point (0.5, 0), new Point (1, 1), Interval)
            {
                BeginTime = Interval.TimeSpan
            };
            PointAnimation endPointAnimation2 = new PointAnimation (new Point (0.5, 1), new Point (0,0), Interval)
            {
                BeginTime = Interval.TimeSpan
            };
            Storyboard.SetTarget (startPointAnimation1, this);
            Storyboard.SetTarget (endPointAnimation1, this);
            Storyboard.SetTarget (startPointAnimation2, this);
            Storyboard.SetTarget (endPointAnimation2, this);
            Storyboard.SetTargetProperty (startPointAnimation1, new PropertyPath ("BorderBrush.(LinearGradientBrush.StartPoint)"));
            Storyboard.SetTargetProperty (endPointAnimation1, new PropertyPath ("BorderBrush.(LinearGradientBrush.EndPoint)"));
            Storyboard.SetTargetProperty (startPointAnimation2, new PropertyPath ("BorderBrush.(LinearGradientBrush.StartPoint)"));
            Storyboard.SetTargetProperty (endPointAnimation2, new PropertyPath ("BorderBrush.(LinearGradientBrush.EndPoint)"));

            sb.Children.Add(startPointAnimation1);
            sb.Children.Add(endPointAnimation1);
            sb.Children.Add(startPointAnimation2);
            sb.Children.Add(endPointAnimation2);

            this.BorderBrush = linearGradient;
            sb.Begin ();
        }

        public void EndAnimation()
        {
            if (sb == null)
                return;
        }
    }
}