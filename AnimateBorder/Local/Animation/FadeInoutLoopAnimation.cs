using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace AnimateBorder.Local.Animation
{
    public class FadeInoutLoopAnimation : BaseAnimation
    {
        ColorAnimation ColorAnimation { get; set; }
        public FadeInoutLoopAnimation(Brush baseColor,
                                      Duration Interval)
        {
            ColorAnimation = new ColorAnimation ()
            {
                From = ((SolidColorBrush)baseColor).Color,
                To = Brushes.Transparent.Color,
                Duration = Interval,
            };
        }
        public override Storyboard GetStoryboard()
        {
            Storyboard sb = new Storyboard ()
            {
                RepeatBehavior = RepeatBehavior.Forever,
            };
            Storyboard.SetTargetProperty (ColorAnimation, new PropertyPath ("BorderBrush.Color"));

            sb.Children.Add(ColorAnimation);
            return sb;
        }
    }
}
