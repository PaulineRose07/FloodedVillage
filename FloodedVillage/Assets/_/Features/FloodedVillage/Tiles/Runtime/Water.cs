using Data.Runtime;
using UnityEngine;

namespace Tiles.Runtime
{
    public class Water : MonoBehaviour, IAmWater
    {
        #region Unity API
        private void Start()
        {
            
        }
        #endregion

        #region Main Methods

        public void OnWaterInitialization()
        {
            _onWaterInitialization.Raise();
        }
        public void ChangeToZombie()
        {
            _spriteRenderer.sprite = _sprites[1];
        }



        #endregion

        #region Privates & Protected
        [SerializeField] private GameEvent _onWaterInitialization;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _sprites;
        #endregion
    }

}
