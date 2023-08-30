using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _game.Scripts.Components.Grid
{
    public class GridCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler,
        IDragHandler, IEndDragHandler
    {
        [SerializeField] private RectTransform m_rectTransform;
        [SerializeField, ReadOnly] private Vector2Int m_cord;

        private GridManager _gridManager;
        private GridCellEvents _gridCellEvents;
        private IGridObject _gridObject;

        [Button]
        public void Initialize(GridManager gridManager, Vector2Int cord, Vector2 localPosition,
            GridCellEvents gridCellEvents)
        {
            name = GetIndex(cord.x, cord.y);
            m_rectTransform.anchoredPosition = localPosition;

            m_cord = cord;

            _gridManager = gridManager;
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

            if (_gridObject != null)
            {
                _gridObject.OnInteract();
            }
        }

        public void SetGridObject(IGridObject newGridObject)
        {
            newGridObject?.UpdateCell(this);
            newGridObject?.SetPosition(transform.position);

            _gridObject = newGridObject;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_gridObject == null) return;
            var offset = GetSize() / 2;

            var canvas = _gridManager.GetCanvas();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                eventData.position, canvas.worldCamera, out Vector2 localPos);
            var position = canvas.transform.TransformPoint(localPos + new Vector2(-offset.x, offset.y));

            _gridObject.SetPosition(position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_gridObject == null) return;
            _gridObject.SetPosition(transform.position);

            var hoveredList = eventData.hovered;
            foreach (var hovered in hoveredList)
            {
                if (hovered == gameObject) continue;

                var hoveredName = hovered.name;

                // TODO can be implemented with layers or tags to be more robust
                if (hoveredName.Contains("x_"))
                {
                    var targetGridCell = hovered.GetComponent<GridCell>();
                    if (!targetGridCell.IsFilled())
                    {
                        // Move
                        targetGridCell.SetGridObject(_gridObject);
                        SetGridObject(null);
                    }
                    else
                    {
                        var targetGridObject = targetGridCell.GetGridObject();
                        if (targetGridObject.CanMerge(_gridObject))
                        {
                            // Merge
                            targetGridObject.Merge(_gridObject);
                            SetGridObject(null);
                        }
                        else
                        {
                            // Switch
                            targetGridCell.SetGridObject(_gridObject);
                            SetGridObject(targetGridObject);
                        }
                    }

                    break;
                }

                if (hoveredName.Contains("btn_inventory"))
                {
                    Debug.Log("Inventory Drop");
                    break;
                }
            }
        }
    }
}