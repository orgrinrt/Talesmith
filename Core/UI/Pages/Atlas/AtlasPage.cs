using Talesmith.Core.Viewports.Cameras;

namespace Talesmith.Core.UI.Pages.Atlas
{
    public class AtlasPage : Page
    {
        public override void _Ready()
        {
        
        }

        public FreeCamera GetFreeCamera()
        {
            return GetNode<FreeCamera>("../../Atlas2D/FreeCam");
        }
    }
}
