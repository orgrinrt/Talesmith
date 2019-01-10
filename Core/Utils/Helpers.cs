using System.Collections.Generic;
using Godot;

namespace Taleteller.Core.Utils
{
    public static class Helpers
    {
        public static Vector2 Zero2 = new Vector2(0, 0);
        public static Vector3 Zero3 = new Vector3(0, 0, 0);

        public static void ReParent(this Node child, Node newParent)
        {
            child.GetParent().RemoveChild(child);
            newParent.AddChild(child);
        }

        public static Node[] GetChildrenRecursive(this Node parent)
        {
            List<Node> result = new List<Node>();

            foreach (Node node in parent.GetChildren())
            {
                result.Add(node);

                if (node.GetChildCount() > 0)
                {
                    foreach (Node child in GetChildrenRecursive(node))
                    {
                        result.Add(child);
                    }
                }
            }

            return result.ToArray();
        }

        private static void GetAllNodes(Node parent)
        {
            // ? do we need this
        }

        public static T GetParentOfType<T>(this Node node) where T : Node
        {
            Node parentCandidate = node.GetParent();
            
            while (parentCandidate != null && parentCandidate != node.GetTree().GetRoot())
            {
                if (parentCandidate is T match)
                {
                    return match as T;
                }
                
                parentCandidate = parentCandidate.GetParent();
            }
            
            Debug.Log.Error("Helpers.GetParentOfType<T>()", "Couldn't find a parent with type " + typeof(T) + " to node " + node.Name, true);

            return null;
        }

        public static Theme GetThemeUsed(this Control control)
        {
            Control currentControl = control;
            
            while (currentControl != null)
            {
                if (currentControl.Theme != null)
                {
                    return  currentControl.Theme;
                }
                else
                {
                    currentControl = currentControl.GetParentOfType<Control>();
                }
            }

            Debug.Log.Error("Helpers.GetThemeUsed()", "Couldn't find a theme for control: " + control.Name, true);
            return null;
        }
    }
}