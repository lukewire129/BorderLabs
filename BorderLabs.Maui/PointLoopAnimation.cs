using AlohaKit.Animations;

namespace BorderLabs.Maui
{
    public class PointLoopAnimation : AnimationBase
    {
        bool isRunning = false;
        protected override Task BeginAnimation()
        {
            if (Target == null)
            {
                throw new NullReferenceException ("Null Target property.");
            }
            if (isRunning == true)
                return Task.CompletedTask;
            return Task.Run (() =>
            {
                isRunning = true;
#pragma warning disable CS0612 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
                Device.BeginInvokeOnMainThread (() =>
                {
                    Target.Animate ("LoopPoint", Point (), 16, Convert.ToUInt32 (Duration), finished: (s, e) => isRunning = false);
                });
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning restore CS0612 // Type or member is obsolete
            });
        }

        internal Animation Point()
        {
            LinearGradientBrush grdientBrush = ((Border)Target).Stroke as LinearGradientBrush;
            var animation = new Animation ()
                               .WithConcurrent ((f) => grdientBrush.StartPoint = new Microsoft.Maui.Graphics.Point (f, 0.0), start: 0.0, end: 1.0, beginAt: 0.0, finishAt: 0.25)
                               .WithConcurrent ((f) => grdientBrush.EndPoint = new Microsoft.Maui.Graphics.Point (f, 1.0), start: 1.0, end: 0.0, beginAt: 0.0, finishAt: 0.25)
                               .WithConcurrent ((f) => grdientBrush.StartPoint = new Microsoft.Maui.Graphics.Point (1.0, f), start: 0.0, end: 1.0, beginAt: 0.25, finishAt:0.5)
                               .WithConcurrent ((f) => grdientBrush.EndPoint = new Microsoft.Maui.Graphics.Point (0.0, f), start: 1.0, end: 0.0, beginAt: 0.25, finishAt: 0.5)
                               .WithConcurrent ((f) => grdientBrush.StartPoint = new Microsoft.Maui.Graphics.Point (f, 1.0), start: 1.0, end: 0.0, beginAt: 0.5, finishAt: 0.75)
                               .WithConcurrent ((f) => grdientBrush.EndPoint = new Microsoft.Maui.Graphics.Point (f, 0.0), start: 0.0, end: 1.0, beginAt: 0.5, finishAt: 0.75)
                               .WithConcurrent ((f) => grdientBrush.StartPoint = new Microsoft.Maui.Graphics.Point (0.0, f), start: 1.0, end: 0.0, beginAt: 0.75, finishAt: 1.0)
                               .WithConcurrent ((f) => grdientBrush.EndPoint = new Microsoft.Maui.Graphics.Point (1.0, f), start: 0.0, end: 1.0, beginAt: 0.75, finishAt: 1.0);
            return animation;
        }
    }
}
