using UnityEngine;

namespace Common
{
    public class Instantiater : MonoBehaviour
    {
        public static T Spawn<T>(T original, Vector3 position, Quaternion rotation) where T : Object
        {
            var obj = Instantiate(original, position, rotation);
            return obj;
        }
    }
}