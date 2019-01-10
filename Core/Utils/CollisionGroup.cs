using Godot;

namespace Taleteller.Core.Utils
{
    public class CollisionGroup
    {
        private string _name;
        private int[] _layers;
        private int[] _masks;

        public string Name => _name;
        public int[] Layers => _layers;
        public int[] Masks => _masks;

        public CollisionGroup(string name, int[] layers, int[] masks)
        {
            this._name = name;
            this._layers = layers;
            this._masks = masks;
        }

        public int GetCombinedMasks()
        {
            var temp = 0;
            //Godot.GD.Print("--");
            foreach (var value in _masks)
            {
                temp = temp | (1 << value);
                //Godot.GD.Print(value);
            }
            //Godot.GD.Print("--");
            
            return temp;
        }
        
        public int GetCombinedLayers()
        {
            int temp = 0;
            
            foreach (int value in _layers)
            {
                temp += (int)Mathf.Round(Mathf.Pow(2, value));
                
            }

            //Godot.GD.Print(temp);
            return temp;
        }
        
        // ======================================== Player ========================================
        private static int[] playerMasks = new int[1]
        {
            (int) CollisionLayer.MovementBlocks
        };
        private static int[] playerLayers = new int[1]
        {
            (int) CollisionLayer.Player,
        };
        public static CollisionGroup Player = new CollisionGroup("Player", playerLayers, playerMasks);
        
        // ==================================== ActorCharacters ========================================
        private static int[] actorMasks = new int[1]
        {
            (int) CollisionLayer.MovementBlocks
        };
        private static int[] actorLayers = new int[2]
        {
            (int) CollisionLayer.Player,
            (int) CollisionLayer.ActorCharacters
        };
        public static CollisionGroup ActorCharacters = new CollisionGroup("ActorCharacters", actorLayers, actorMasks);
        
        // ===================================== MovementBlocks ========================================
        private static int[] movementBlockMasks = new int[2]
        {
            (int) CollisionLayer.Player,
            (int) CollisionLayer.ActorCharacters
        };
        private static int[] movementBlockLayers = new int[1]
        {
            (int) CollisionLayer.MovementBlocks,
        };
        public static CollisionGroup MovementBlocks = new CollisionGroup("MovementBlocks", movementBlockLayers, movementBlockMasks);
        
    }
}