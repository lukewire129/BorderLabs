using AnimateBorder.Local.Animation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace AnimateBorder.UI.Units
{
    public enum AnimationType
    {
        Loop,
        FadeInOut
    }
    public class AnimateBorder : Border
    {
        public AnimationType AnimationType
        {
            get { return (AnimationType)GetValue (AnimationTypeProperty); }
            set { SetValue (AnimationTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationTypeProperty =
            DependencyProperty.Register ("AnimationType", typeof (AnimationType), typeof (AnimateBorder), new PropertyMetadata (AnimationType.Loop, OnBorderBrushsCallBack));


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
        Brush tempBrush;
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged (e);

            if (e.Property.Name == nameof (BorderBrush))
            {
                if (AnimationType != AnimationType.FadeInOut)
                    return;
                if (tempBrush == this.BorderBrush)
                    return;
                this.StartAnimation ();
                this.tempBrush = BorderBrush;
            }
        }
        Storyboard sb;
        public void StartAnimation()
        {
            sb?.Pause ();            
            BaseAnimation ani = null;
            
            if(AnimationType == AnimationType.Loop)
            {
                if (BorderBrushs == null)
                    return;
                if (BorderBrushs.Count () <= 1)
                    return;
                LinearGradientBrush linearGradient = new LinearGradientBrush ()
                {
                    StartPoint = new Point (0, 0),
                    EndPoint = new Point (1, 1),
                };
                double offset = BorderBrushs.Count () <= 1 ? 1.0 : 0.0;
                double nextoffect = 1.0 / (BorderBrushs.Count () - 1);
                foreach (var brush in BorderBrushs)
                {
                    linearGradient.GradientStops.Add (new GradientStop (((SolidColorBrush)brush).Color, offset));
                    offset += nextoffect;
                }

                this.BorderBrush = linearGradient;
                ani = new GradationLoopAnimation (Interval);
            }
            else if (AnimationType == AnimationType.FadeInOut)
            {
                if (BorderBrush == null)
                    return;
                ani = new FadeInoutLoopAnimation (this.BorderBrush, Interval);
            }
            sb = ani.GetStoryboard ();
            BeginStoryboard (sb);
        }

        public void EndAnimation()
        {
            if (sb == null)
                return;
        }
    }
}