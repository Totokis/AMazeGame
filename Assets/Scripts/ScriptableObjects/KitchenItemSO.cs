using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class KitchenItemSO : ScriptableObject
    {
        public Transform prefab;
        public Sprite sprite;
        public string objectName;
    }
}