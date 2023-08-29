using _game.Scripts.Components.Grid.Objects.Data;
using TMPro;
using UnityEngine;

namespace _game.Scripts.Components.Grid.Objects.View
{
    public class ApplianceGridObjectView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_text;
        
        public void Render(ApplianceGridObjectData data)
        {
            m_text.SetText(data.Number.ToString());
        }
    }
}