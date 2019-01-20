using System.Collections.Generic;
using Godot;
using GDMechanic.Wiring;
using GDMechanic.Wiring.Attributes;
using Talesmith.Core;
using Talesmith.Core.Systems;
using Talesmith.Core.UI.Inspector;

public class DockDragger : Control
{
    private bool _isHandling = false;
    private float _inspectorMaxSize = -650;
    private float _inspectorMinSize = -150;
    
	public override void _Ready()
    {
        this.Wire();
        
        Connect("mouse_entered", this, nameof(OnMouseEnter));
        Connect("mouse_exited", this, nameof(OnMouseExit));
        CallDeferred(nameof(DeferredInit));
        SetFocusMode(FocusModeEnum.All);
    }   
    
    private IEnumerator<float> MonitorForClick()
    {
        GrabFocus();
        while (true)
        {
            if (Input.IsMouseButtonPressed(1) && !_isHandling)
            {
                Iterator.Coroutine.Run(HandleDragging());
                break;
            }
            if (!IsWithinBounds())
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
        Dock dock = GetDock();
        Control pageParent = App.Self.GetWorkspaceController();
        while (true)
        {
            float dist = (RectGlobalPosition.y - GetGlobalMousePosition().y);
            
            dock.MarginTop = dock.MarginTop - dist;
            if (dock.MarginTop > _inspectorMinSize)
            {
                dock.MarginTop = _inspectorMinSize;
            }
            else if (dock.MarginTop < _inspectorMaxSize)
            {
                dock.MarginTop = _inspectorMaxSize;
            }

            pageParent.MarginBottom = dock.MarginTop;
            
            if (!Input.IsMouseButtonPressed(1) || _isHandling == false)
            {
                _isHandling = false;
                break;
            }
            
            yield return 0.000001f;
        }
        
        OnMouseExit();
        
        if (IsWithinBounds())
        {
            OnMouseEnter();
        }
    }

    private bool IsWithinBounds()
    {
        return GetGlobalMousePosition().y > RectGlobalPosition.y &&
               GetGlobalMousePosition().y < RectGlobalPosition.y + RectSize.y;
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

    private void DeferredInit()
    {
        //App.Self.Preferences.Connect(nameof(Preferences.InspectorToggled), this, nameof(OnInspectorToggled));
    }

    private Dock GetDock()
    {
        return GetNode<Dock>("..");
    }
}