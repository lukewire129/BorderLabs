using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AnimateBorder.UI.Units
{
    public class AnimateBorderTest : ContentControl
    {
        static AnimateBorderTest()
        {
            DefaultStyleKeyProperty.OverrideMetadata (typeof (AnimateBorderTest), new FrameworkPropertyMetadata (typeof (AnimateBorderTest)));
        }

        public new Brush BorderBrush
        {
            get { return (Brush)GetValue(BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BorderBrush.  This enables animation, styling, binding, etc...
        public static readonly new DependencyProperty BorderBrushProperty =
            DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(AnimateBorderTest), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));


        Grid grd;
        public AnimateBorderTest()
        {
            this.grd = GetTemplateChild ("PART_Border") as Grid;
        }
        protected override Size MeasureOverride(Size constraint)
        {
            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            return base.ArrangeOverride(finalSize);
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);



        }
    }
}