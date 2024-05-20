using UnityEngine;

namespace OctanGames.Services.Input
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis
        {
            get
            {
                Vector2 axis = SimpleInputAxis();
                if (axis == Vector2.zero)
                {
                    axis = UnityAxis();
                }
                
                return axis;
            }
        }

        private static Vector2 UnityAxis() =>
            new(UnityEngine.Input.GetAxis(HORIZONTAL), UnityEngine.Input.GetAxis(VERTICAL));
    }
}