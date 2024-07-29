
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelDesign.Runtime
{
    public class UIMenu : MonoBehaviour
    {
        #region Publics
	
        #endregion

        #region Unity API
		
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
        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }
        #endregion

        #region Utils

        #endregion

        #region Privates & Protected
        
        #endregion
    }

}
