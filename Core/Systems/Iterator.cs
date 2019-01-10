using Godot;
using Taleteller.Core.Utils;

namespace Taleteller.Core.Systems
{
    public class Iterator : Node
    {
        private static int _currId = 100; // we'll reserve 0-99 to things that we might want to have as statically identified.. can't really say why, but it's here for the off-chance
        public static CoroutineHandler Coroutine;
        
        public override void _Ready()
        {
            if (Coroutine == null)
            {
                Coroutine = new CoroutineHandler();
            }
        }

        public override void _Process(float delta)
        {
            Coroutine.Update(delta);
        }

        public static int GetUniqueId()
        {
            _currId++;
            return _currId - 1;
        }
    }
}
