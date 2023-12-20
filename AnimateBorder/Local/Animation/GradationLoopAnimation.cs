using System.Windows;
using System.Windows.Media.Animation;

namespace AnimateBorder.Local.Animation;

public class GradationLoopAnimation
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
        for (int i=1; i<4; i++)
        {
            startanimations[i].BeginTime = Interval.TimeSpan + tempduration.TimeSpan;
            endanimations[i].BeginTime = Interval.TimeSpan + tempduration.TimeSpan;
            tempduration += Interval;
        }
    }

    public List<PointAnimation> GetStartPointAnimations => this.startanimations;
    public List<PointAnimation> GetEndPointAnimations => this.endanimations;
}
