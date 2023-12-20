using AnimateBorder.Local.Animation;
using System.Collections.ObjectModel;
using System.Reflection;
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
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate ();
            this.StartAnimation ();
        }
        Storyboard sb;
        public void StartAnimation()
        {
            if (BorderBrushs == null)
                return;
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

            this.BorderBrush = linearGradient;
            sb = new Storyboard ()
            {
                RepeatBehavior = RepeatBehavior.Forever
            };
            var ani = new GradationLoopAnimation (Interval);
            for(int i=0; i<ani.GetStartPointAnimations.Count; i++)
            {
                Storyboard.SetTargetProperty (ani.GetStartPointAnimations[i], new PropertyPath ("BorderBrush.(LinearGradientBrush.StartPoint)"));
                sb.Children.Add (ani.GetStartPointAnimations[i]);
            }
            for (int i = 0; i < ani.GetEndPointAnimations.Count; i++)
            {
                Storyboard.SetTargetProperty (ani.GetEndPointAnimations[i], new PropertyPath ("BorderBrush.(LinearGradientBrush.EndPoint)"));
                sb.Children.Add (ani.GetEndPointAnimations[i]);
            }

            BeginStoryboard (sb);
        }

        public void EndAnimation()
        {
            if (sb == null)
                return;
        }
    }
}