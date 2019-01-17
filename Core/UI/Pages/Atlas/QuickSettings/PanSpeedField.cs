using Godot;
using Talesmith.Core.Viewports.Cameras;

namespace Talesmith.Core.UI.Pages.Atlas.QuickSettings
{
    public class PanSpeedField : HBoxContainer
    {
        private bool _isSpeedTogglePressed = false;

        public bool IsSpeedTogglePressed => _isSpeedTogglePressed;
    
        public override void _Ready()
        {
            GetSlider().Value = GetAtlasPage().GetFreeCamera().PanSpeed;

            GetSlider().Connect("value_changed", this, nameof(OnValueChanged));
        }

        public override void _Input(InputEvent @event)
        {
            base._Input(@event);

            if (@event.IsActionPressed("cam_pan_speed_toggle"))
            {
                _isSpeedTogglePressed = true;
            }
            else if (@event.IsActionReleased("cam_pan_speed_toggle"))
            {
                _isSpeedTogglePressed = false;
            }

            if (IsSpeedTogglePressed && GetAtlasPage().HasFocus())
            {
                HSlider slider = GetSlider();
                FreeCamera cam = GetAtlasPage().GetFreeCamera();
            
                if (!cam.IsZoomBlocked)
                {
                    cam.SetZoomLocked(true);
                }
            
                if (@event.IsActionPressed("cam_pan_speed_up"))
                {
                    slider.Value = slider.Value + slider.Step;
                }
                else if (@event.IsActionPressed("cam_pan_speed_down"))
                {
                    slider.Value = slider.Value - slider.Step;
                }
            }
            else
            {
                if (GetAtlasPage().GetFreeCamera().IsZoomBlocked)
                {
                    GetAtlasPage().GetFreeCamera().SetZoomLocked(false);
                }
            }
        }

        private void OnValueChanged(float value)
        {
            GetAtlasPage().GetFreeCamera().SetPanSpeed((int)value);
        }

        private HSlider GetSlider()
        {
            return GetNode<HSlider>("./HSlider");
        }

        private AtlasPage GetAtlasPage()
        {
            return GetNode<AtlasPage>("../..");
        }
    }
}
