using System.Collections.Generic;
using Godot;
using Taleteller.Core.Systems;
using Taleteller.Core.Utils;

namespace Taleteller.Core.UI.Inspector
{
    public class Dragger : Control
    {
        private bool _isHandling = false;
        private float _inspectorMaxSize = -600;
        private float _inspectorMinSize = -225;
        
        public override void _Ready()
        {
            base._Ready();
            
            Connect("mouse_entered", this, nameof(OnMouseEnter));
            Connect("mouse_exited", this, nameof(OnMouseExit));
        }

        private IEnumerator<float> MonitorForClick()
        {
            while (true)
            {
                if (Input.IsMouseButtonPressed(1) && !_isHandling)
                {
                    Iterator.Coroutine.Run(HandleDragging());
                    break;
                }
                if (GetGlobalMousePosition().x < RectGlobalPosition.x ||
                    GetGlobalMousePosition().x > RectGlobalPosition.x + RectSize.x)
                {
                    OnMouseExit();
                    break;
                }
                yield return 0.000001f;
            }
        }

        private IEnumerator<float> HandleDragging()
        {
            _isHandling = true;
            Inspector inspector = GetInspector();
            while (true)
            {
                float dist = (RectGlobalPosition.x - GetGlobalMousePosition().x);
                inspector.MarginLeft = inspector.MarginLeft - dist;
                if (inspector.MarginLeft > _inspectorMinSize)
                {
                    inspector.MarginLeft = _inspectorMinSize;
                }
                else if (inspector.MarginLeft < _inspectorMaxSize)
                {
                    inspector.MarginLeft = _inspectorMaxSize;
                }
                
                if (!Input.IsMouseButtonPressed(1) || _isHandling == false)
                {
                    _isHandling = false;
                    break;
                }
                
                yield return 0.000001f;
            }
            
            OnMouseExit();
            
            if (GetGlobalMousePosition().x > RectGlobalPosition.x &&
                GetGlobalMousePosition().x < RectGlobalPosition.x + RectSize.x)
            {
                OnMouseEnter();
            }
        }

        private void OnMouseEnter()
        {
            if (!Iterator.Coroutine.IsRunning(MonitorForClick()))
            {
                Iterator.Coroutine.Run(MonitorForClick());
            }
        }

        private void OnMouseExit()
        {
            _isHandling = false;
            if (Iterator.Coroutine.IsRunning(MonitorForClick()))
            {
                Iterator.Coroutine.Stop(MonitorForClick());
            }
        }

        private Inspector GetInspector()
        {
            return GetNode<Inspector>("..");
        }
    }
}