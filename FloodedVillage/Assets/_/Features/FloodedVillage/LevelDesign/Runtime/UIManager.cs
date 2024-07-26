using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelDesign.Runtime
{
    public class UIManager : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity API

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }

            _gameManager.UpdateMovesText(_movesText);

        }

        #endregion

        #region Main methods
        public void RetryLevel()
        {
            string currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentScene);
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void OpenGameOverPanel()
        {
            _gameOverPanel.SetActive(true);
        }

        public void CloseGameOverPanel()
        {
            _gameOverPanel.SetActive(false);
        }

        public void OpenYouWonPanel()
        {
            _levelWonPanel.SetActive(true);
        }

        public void CloseYouWonPanel()
        {
            _levelWonPanel.SetActive(false);
        }
        #endregion

        #region Utils

        #endregion

        #region Privates & Protected
        [SerializeField] private TMP_Text _movesText;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private GameObject _levelWonPanel;
        [SerializeField] private GameManager _gameManager;
        #endregion
    }

}
