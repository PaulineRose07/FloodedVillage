using Data.Runtime;
using UnityEngine;


namespace Tiles.Runtime
{
    public class Sand : MonoBehaviour, CanBeModified
    {

        #region Publics

        #endregion

        #region Unity API

        private void Start()
        {
            _spriteRenderer.enabled = true;
            _isActive = true;
            
        }

        private void OnMouseEnter()
        {
            ActivateHighlight();
        }

        private void OnMouseExit()
        {
            //Debug.Log(_isActive);
            DeactivateHighlight();
        }

        private void OnMouseDown()
        {
            ActivateOrDeactivateSpriteRenderer();
            _updateMoves.Raise();
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

        [ContextMenu("Activate Sprite Renderer")]
        public void ActivateOrDeactivateSpriteRenderer()
        {
            if (_isActive == true)
            {
                _spriteRenderer.enabled = false;
                _isActive = false;
            }
            else if(_isActive == false)
            {
                _spriteRenderer.enabled = true;
                _isActive = true;
            }
 
        }

        public bool IsTheTileFull()
        {
            return _isActive;
        }

        public void DestroyIfWater()
        {
            Destroy(gameObject);
        }

        #endregion

        #region Utils

        #endregion

        #region Privates & Protected
        [SerializeField] private GameObject _highlight;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private bool _isActive = true;
        [SerializeField] GameEvent _updateMoves;

        #endregion
    }

}
