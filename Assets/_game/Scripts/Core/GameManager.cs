using System.Collections;
using _game.Scripts.Core.Ui;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _game.Scripts.Core
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            InitializeAwake();
        }

        private void Start()
        {
            InitializeStart();
        }

        private void InitializeAwake()
        {
            UiManager.Instance.Initialize();
        }

        private void InitializeStart()
        {
            StartCoroutine(StartGame());
        }

        [Button]
        private IEnumerator StartGame()
        {
            yield return new WaitForEndOfFrame();
        }
    }
}
