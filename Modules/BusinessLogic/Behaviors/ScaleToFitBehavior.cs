using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.PanAndZoom;
using Avalonia.Xaml.Interactivity;

namespace BusinessLogic.Behaviors
{
    public class ScaleToFitBehavior : Behavior<ZoomBorder>
    {
        /// <inheritdoc />
        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject != null)
            {
                AssociatedObject.LayoutUpdated += OnLayoutUpdated;
            }
        }

        /// <inheritdoc />
        protected override void OnDetaching()
        {
            if (AssociatedObject != null)
            {
                AssociatedObject.KeyUp -= OnLayoutUpdated;
            }

            base.OnDetaching();
        }

        private void OnLayoutUpdated(object? sender, EventArgs e)
        {
            OnPointChanged();
        }

        private void OnPointChanged()
        {
            if (AssociatedObject?.Child is not ContentControl contentControl)
                return;

            if (contentControl.Content is not ItemsControl or ItemsControl {ItemCount: 0})
            {
                return;
            }

            Rect element = contentControl.Bounds;
            Rect panel = AssociatedObject.Bounds;

            AssociatedObject.Uniform(panel.Width, panel.Height, element.Width, element.Height);
        }
    }
}