using System.Windows;
using System.Windows.Media.Animation;

namespace AnimateBorder.Local.Animation;

public class GradationLoopAnimation : BaseAnimation
{
    List<PointAnimation> startanimations;
    List<PointAnimation> endanimations;
    public GradationLoopAnimation(Duration Interval)
    {
        startanimations = new List<PointAnimation>();
        endanimations = new List<PointAnimation>();
        // StartPoint
        startanimations.Add (new PointAnimation (new Point (0, 0), new Point (1, 0), Interval));
        startanimations.Add (new PointAnimation (new Point (1, 0), new Point (1, 1), Interval));
        startanimations.Add (new PointAnimation (new Point (1, 1), new Point (0, 1), Interval));
        startanimations.Add (new PointAnimation (new Point (0, 1), new Point (0, 0), Interval));

        // EndPoint
        endanimations.Add (new PointAnimation (new Point (1, 1), new Point (0, 1), Interval));
        endanimations.Add (new PointAnimation (new Point (0, 1), new Point (0, 0), Interval));
        endanimations.Add (new PointAnimation (new Point (0, 0), new Point (1, 0), Interval));
        endanimations.Add (new PointAnimation (new Point (1, 0), new Point (1, 1), Interval));
        Duration tempduration = new Duration (new TimeSpan(0,0,0));

        for(int i=0; i<4; i++)
        {
            Storyboard.SetTargetProperty (GetStartPointAnimations[i], new PropertyPath ("BorderBrush.(LinearGradientBrush.StartPoint)"));
            Storyboard.SetTargetProperty (GetEndPointAnimations[i], new PropertyPath ("BorderBrush.(LinearGradientBrush.EndPoint)"));
        }
        for (int i=1; i<4; i++)
        {
            startanimations[i].BeginTime = Interval.TimeSpan + tempduration.TimeSpan;
            endanimations[i].BeginTime = Interval.TimeSpan + tempduration.TimeSpan;
            tempduration += Interval;
        }
    }

    private List<PointAnimation> GetStartPointAnimations => this.startanimations;
    private List<PointAnimation> GetEndPointAnimations => this.endanimations;

    public override Storyboard GetStoryboard()
    {
        Storyboard sb = new Storyboard ()
        {
            RepeatBehavior = RepeatBehavior.Forever
        };

        for (int i = 0; i < 4; i++)
        {
            sb.Children.Add (GetStartPointAnimations[i]);
            sb.Children.Add (GetEndPointAnimations[i]);
        }

        return sb;
    }
}
