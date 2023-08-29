using System;
using System.Collections.Generic;
using _game.Scripts.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _game.Scripts.Core.Ui
{
    public class UiManager : MonoSingleton<UiManager>
    {
        [SerializeField] private Transform m_canvasParent;
        [PropertySpace(10), ReadOnly] private UiController m_currentUi;

        private Dictionary<Type, UiController> _uiCache;

        public static T Get<T>() where T : UiController
        {
            return Instance.GetUi<T>();
        }

        public void Initialize()
        {
            _uiCache = new Dictionary<Type, UiController>();
        }

        private T GetUi<T>() where T : UiController
        {
            var uiType = typeof(T);
            if (_uiCache.TryGetValue(uiType, out var value))
            {
                return (T)value;
            }

            var ui = m_canvasParent.GetComponentInChildren<T>();
            if (ui == null)
            {
                Debug.LogException(new Exception($"UI Not Exist {typeof(T)}"));
                return default;
            }

            _uiCache[uiType] = ui;
            return ui;
        }
    }
}