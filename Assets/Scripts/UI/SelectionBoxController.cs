using MyBox;
using UnityEngine;

namespace UI
{
    [ExecuteAlways]
    public class SelectionBoxController : MonoBehaviour
    {
        [SerializeField] [PositiveValueOnly] protected float width = 1f;
        [SerializeField] [PositiveValueOnly] protected float height = 1f;

        [Foldout("Graphics")] [SerializeField] protected Transform topLeft;
        [Foldout("Graphics")] [SerializeField] protected Transform topRight;
        [Foldout("Graphics")] [SerializeField] protected Transform bottomLeft;
        [Foldout("Graphics")] [SerializeField] protected Transform bottomRight;
        

        [ButtonMethod()]
        private void UpdateGraphics()
        {
            topLeft.localPosition = new Vector3(width / -2, height/2);
            topRight.localPosition = new Vector3(width / 2, height/2);
            bottomLeft.localPosition = new Vector3(width / -2, height/-2);
            bottomRight.localPosition = new Vector3(width / 2, height/-2);
        }

        public void SetSize(Vector2 colliderSize)
        {
            width = colliderSize.x;
            height = colliderSize.y;
            UpdateGraphics();
        }
    }
}
