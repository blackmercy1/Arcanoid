using UnityEngine;

namespace Portals
{
    public class TopDownPortal : MonoBehaviour
    {
        private Vector2 _newPosition;
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out ICollision other))
            {
                var otherPosition = other.GetPosition();
                _newPosition = otherPosition.y <= 0 
                    ? new Vector2(otherPosition.x, -otherPosition.y - .5f) 
                    : new Vector2(otherPosition.x, -otherPosition.y + .5f);
                other.SetPosition(_newPosition);
            }
        }
    }
}