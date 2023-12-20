using AlohaKit.Animations;
using Microsoft.Maui.Controls;

namespace BorderLabs.Maui;

public class StartPointAnimation : AnimationBase
{
    public static readonly BindableProperty FromProperty = BindableProperty.Create ("From", typeof (Point), typeof (StartPointAnimation), null, BindingMode.TwoWay);
    public static readonly BindableProperty ToProperty = BindableProperty.Create ("To", typeof (Point), typeof (StartPointAnimation), null, BindingMode.TwoWay);

    public Point From
    {
        get
        {
            return (Point)GetValue (FromProperty);
        }
        set
        {
            SetValue (FromProperty, value);
        }
    }

    public Point To
    {
        get
        {
            return (Point)GetValue (ToProperty);
        }
        set
        {
            SetValue (ToProperty, value);
        }
    }
    protected override Task BeginAnimation()
    {
        if (Target == null)
        {
            throw new NullReferenceException ("Null Target property.");
        }
        
        return Task.Run (() =>
        {
#pragma warning disable CS0612 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
            Device.BeginInvokeOnMainThread (() =>
            {
                Target.Animate ("Point1", Point (), 16, Convert.ToUInt32 (Duration));
            });
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning restore CS0612 // Type or member is obsolete
        });
    }

    internal Animation Point()
    {
        var animation = new Animation ();
        LinearGradientBrush grdientBrush = ((Border)Target).Stroke as LinearGradientBrush;
        animation.WithConcurrent ((f) => grdientBrush.StartPoint = new Microsoft.Maui.Graphics.Point(grdientBrush.StartPoint.X, f), From.Y, To.Y);
        animation.WithConcurrent ((f) => grdientBrush.StartPoint = new Microsoft.Maui.Graphics.Point (f, grdientBrush.StartPoint.Y), From.X, To.X);
        return animation;
    }
}
