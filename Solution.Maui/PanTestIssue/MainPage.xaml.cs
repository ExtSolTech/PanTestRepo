using System.Diagnostics;

namespace PanTestIssue
{
    public partial class MainPage : ContentPage
    {
        BindableObject bindableObject;
        Point lastPoint;

        public MainPage()
        {
            InitializeComponent();
        }

        private void PanGestureRecognizer_PanUpdatedRectangle(object sender, PanUpdatedEventArgs e)
        {
            Debug.WriteLine($"{nameof(PanGestureRecognizer_PanUpdatedRectangle)}: {e.StatusType}");

            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    bindableObject = (BindableObject)sender;
                    break;

                case GestureStatus.Completed:
                    bindableObject = null;
                    break;
            }
        }

        private void PanGestureRecognizer_PanUpdatedMain(object sender, PanUpdatedEventArgs e)
        {
            Debug.WriteLine($"{nameof(PanGestureRecognizer_PanUpdatedMain)}: {e.StatusType}");
            Point currentPoint = new Point(e.TotalX, e.TotalY);

            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    lastPoint = currentPoint;

                    if (bindableObject == null)
                        bindableObject = grid;

                    break;

                case GestureStatus.Running:
                    Rect layoutBounds = AbsoluteLayout.GetLayoutBounds(bindableObject);
                    layoutBounds.X += currentPoint.X - lastPoint.X;
                    layoutBounds.Y += currentPoint.Y - lastPoint.Y;
                    AbsoluteLayout.SetLayoutBounds(bindableObject, layoutBounds);
                    lastPoint = currentPoint;
                    break;

                case GestureStatus.Completed:
                    bindableObject = null;
                    break;
            }
        }
    }
}