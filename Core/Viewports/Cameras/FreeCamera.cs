using Godot;

namespace Taleteller.Core.Viewports.Cameras
{
    public class FreeCamera : Camera
    {
        private int _panSpeed = 20;
        private float _zoomSpeed = 0.5f;
        private Vector2 _maxZoomIn = new Vector2(0.5f, 0.5f);
        private Vector2 _maxZoomOut = new Vector2(3f, 3f);
        
        public int PanSpeed => _panSpeed;
        public float ZoomSpeed => _zoomSpeed;

        private bool _isMouseDragging = false;
        private bool _isZoomBlocked = false;
        private bool _isPanningBlocked = false;

        public bool IsMouseDragging => _isMouseDragging;
        public bool IsZoomBlocked => _isZoomBlocked;
        public bool IsPanningBlocked => _isPanningBlocked;

        private Tween _tween;
        
        public override void _Ready()
        {
            base._Ready();
            
            _tween = (Tween) GetNode("./Tween");
        }

        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);
            
            if (!IsPanningBlocked)
            {
                if (Input.IsActionPressed("cam_pan_right"))
                {
                    Translate(new Vector2(PanSpeed, 0) * (Zoom.x));
                }
                else if (Input.IsActionPressed("cam_pan_left"))
                {
                    Translate(new Vector2(-PanSpeed, 0) * (Zoom.x));
                }
            
                if (Input.IsActionPressed("cam_pan_up"))
                {
                    Translate(new Vector2(0, -PanSpeed) * (Zoom.x));
                }
                else if (Input.IsActionPressed("cam_pan_down"))
                {
                    Translate(new Vector2(0, PanSpeed) * (Zoom.x));
                }
            }
        }

        public override void _UnhandledInput(InputEvent inputEvent)
        {
            base._UnhandledInput(inputEvent);

            if (!IsPanningBlocked)
            {
                if (inputEvent is InputEventMouseButton click)
                {
                    if ((click.ButtonIndex == 1 && click.Pressed && !IsMouseDragging))
                    {
                        _isMouseDragging = true;
                    }
                    else if (IsMouseDragging)
                    {
                        _isMouseDragging = false;
                    }
                }
                else
                {
                    if (inputEvent.IsActionPressed("cam_pan_toggle"))
                    {
                        _isMouseDragging = true;
                    }
                    else if (inputEvent.IsActionReleased("cam_pan_toggle"))
                    {
                        _isMouseDragging = false;
                    }
                }

                if (inputEvent is InputEventMouseMotion motion && IsMouseDragging)
                {
                    Translate(-(motion.Relative / 20) * PanSpeed * (Zoom.x));
                }

                if (!IsZoomBlocked)
                {
                    if (inputEvent.IsAction("cam_zoom_in"))
                    {
                        Vector2 target = new Vector2(
                            Mathf.Clamp(Zoom.x - ZoomSpeed, _maxZoomIn.x, _maxZoomOut.x),
                            Mathf.Clamp(Zoom.y - ZoomSpeed, _maxZoomIn.y, _maxZoomOut.y));
                        _tween.InterpolateProperty(
                            this,
                            "zoom",
                            Zoom,
                            target,
                            0.2f,
                            Tween.TransitionType.Cubic,
                            Tween.EaseType.Out);
                        _tween.Start();
                    }
                    else if (inputEvent.IsAction("cam_zoom_out"))
                    {
                        Vector2 target = new Vector2(
                            Mathf.Clamp(Zoom.x + ZoomSpeed, _maxZoomIn.x, _maxZoomOut.x),
                            Mathf.Clamp(Zoom.y + ZoomSpeed, _maxZoomIn.y, _maxZoomOut.y));
                        _tween.InterpolateProperty(
                            this,
                            "zoom",
                            Zoom,
                            target,
                            0.2f,
                            Tween.TransitionType.Cubic,
                            Tween.EaseType.Out);
                        _tween.Start();
                    }
                }
            }
        }

        public void SetPanSpeed(int value)
        {
            _panSpeed = value;
        }

        public void SetZoomLocked(bool value)
        {
            _isZoomBlocked = value;
        }

        public void SetPanningLocked(bool value)
        {
            _isPanningBlocked = value;
        }
    }
}
