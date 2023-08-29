using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _game.Scripts.Components.Grid
{
    public class GridCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private RectTransform m_rectTransform;
        [SerializeField, ReadOnly] private Vector2Int m_cord;

        private GridCellEvents _gridCellEvents;
        private IGridObject _gridObject;

        [Button]
        public void Initialize(Vector2Int cord, Vector2 localPosition, GridCellEvents gridCellEvents)
        {
            name = GetIndex(cord.x, cord.y);
            m_rectTransform.anchoredPosition = localPosition;
            m_cord = cord;

            _gridCellEvents = gridCellEvents;
        }

        public static string GetIndex(int x, int y)
        {
            return $"x_{x}_y_{y}";
        }

        [Button]
        public Vector2 GetSize()
        {
            var rect = m_rectTransform.rect;
            return new Vector2(rect.width, rect.height);
        }

        public string GetIndex()
        {
            return name;
        }
        
        public Vector2Int GetCord()
        {
            return m_cord;
        }

        public bool IsFilled()
        {
            return _gridObject != null;
        }
        
        public IGridObject GetGridObject()
        {
            return _gridObject;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _gridCellEvents.OnCellEnter?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _gridCellEvents.OnCellExit?.Invoke(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _gridCellEvents.OnCellClick?.Invoke(this, (int)eventData.button);
        }
    }
}