using _game.Scripts.Core.Ui;
using _game.Scripts.Ui.Controllers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _game.Scripts.Components.Grid
{
    public class GridCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IEndDragHandler
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

        public void SetGridObject(IGridObject gridObject)
        {
            _gridObject = gridObject;
            if (_gridObject == null) return;
            _gridObject.SetPosition(transform.position);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            var offset = GetSize() / 2;

            var canvas = UiManager.Get<GameUiController>().GetComponent<Canvas>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                eventData.position, canvas.worldCamera, out Vector2 localPos);
            var position = canvas.transform.TransformPoint(localPos + new Vector2(-offset.x, offset.y));

            _gridObject.SetPosition(position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _gridObject.SetPosition(transform.position);

            var hoveredList = eventData.hovered;
            foreach (var hovered in hoveredList)
            {
                if (hovered.name.Contains("x_"))
                {
                    var gridCell = hovered.GetComponent<GridCell>();
                    if (!gridCell.IsFilled())
                    {
                        gridCell.SetGridObject(_gridObject);
                        SetGridObject(null);
                    }
                    else
                    {
                    }
                    break;
                }
            }
        }
    }
}