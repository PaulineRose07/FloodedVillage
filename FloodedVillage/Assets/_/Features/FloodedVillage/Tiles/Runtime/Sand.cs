using UnityEngine;
using Data.Runtime;

namespace Tiles.Runtime
{
    public class Sand : MonoBehaviour, ICanBeHighlighted
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
			
    		}

        #endregion

        #region Main methods

        public void ActivateHighlight()
        {
            _highlight.SetActive(true);
        }

        public void DeactivateHighlight()
        {
            _highlight.SetActive(false);
        }

        public void ActivateOrDeactivateSpriteRenderer()
        {
            if (_active == true)
            {
                _spriteRenderer.enabled = false;
                _active = false;
            }
            if(_active == false)
            {
                _spriteRenderer.enabled = true;
                _active = true;
            }
        }

        #endregion

        #region Utils

        #endregion

        #region Privates & Protected
        [SerializeField] private GameObject _highlight;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private bool _active = true;
        #endregion
    }

}
