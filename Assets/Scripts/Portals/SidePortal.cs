using UnityEngine;

namespace Portals
{
    public class SidePortal : MonoBehaviour
    {
        private Vector2 _newPosition;
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out ICollision other))
            {
                var otherPosition = other.GetPosition();
                
                _newPosition = otherPosition.x <= 0 
                    ? new Vector2(-otherPosition.x - .5f, otherPosition.y) 
                    : new Vector2(-otherPosition.x + .5f, otherPosition.y);
                
                other.SetPosition(_newPosition);
            }
        }
    }
}
