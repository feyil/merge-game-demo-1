using _game.Scripts.Components.Grid.Objects.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _game.Scripts.Components.Grid.Objects.View
{
    public class ApplianceGridObjectView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_text;
        [SerializeField] private Image m_image;
        
        public void Render(ApplianceGridObjectData data)
        {
            m_text.SetText(data.Number.ToString());
            m_image.color = data.Color;
        }
    }
}