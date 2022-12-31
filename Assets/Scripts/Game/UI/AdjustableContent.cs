using UnityEngine;
using UnityEngine.Assertions;

namespace FourPics
{
    public class AdjustableContent : MonoBehaviour
    {
        [SerializeField]
        private Canvas canvas;
        [SerializeField]
        private RectTransform rectTransform;

        [SerializeField]
        private bool adjustWidth = true;
        [SerializeField]
        private bool adjustHeight = true;

        protected virtual void Awake()
        {
            Assert.IsNotNull(canvas);
            Assert.IsNotNull(rectTransform);
        }

        protected virtual void Start()
        {
            Adjust();
        }

        protected virtual void Adjust()
        {
            Rect safeArea = GetSafeArea();
            Vector2 anchorMin = safeArea.position;

            Vector2 anchorMax = safeArea.position + safeArea.size;
            anchorMin.x /= canvas.pixelRect.width;
            anchorMin.y /= canvas.pixelRect.height;
            anchorMax.x /= canvas.pixelRect.width;
            anchorMax.y /= canvas.pixelRect.height;

            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
        }

        private Rect GetSafeArea()
        {
            Rect safeArea = Screen.safeArea;

            if (!adjustHeight)
            {
                safeArea.y = 0;
                safeArea.height = Screen.height;
            }
            if (!adjustWidth)
            {
                safeArea.x = 0;
                safeArea.width = Screen.width;
            }

            return safeArea;
        }
    }
}