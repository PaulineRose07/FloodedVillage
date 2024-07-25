using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelDesign.Runtime
{
    public class UIManager : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity API

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
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
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private GameObject _levelWonPanel;
        #endregion
    }

}
