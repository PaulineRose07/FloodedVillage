using UnityEngine;
using Data.Runtime;

namespace Game.Runtime
{
    public class MouseInput : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity API
        private void Start()
        {
            _camera = Camera.main;
           
        }
        private void Update()
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);


            if (Input.GetMouseButtonDown(0))
            {
                if(hit.collider != null)
                {
                    if(hit.collider.TryGetComponent<ICanBeHighlighted>(out ICanBeHighlighted component))
                    {

                    }

                    
                }
            }

          
        }

        #endregion

        #region Main methods

        #endregion

        #region Utils

        #endregion

        #region Privates & Protected
        private Camera _camera;
        #endregion
    }

}
