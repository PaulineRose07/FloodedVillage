using Data.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiles.Runtime
{
    public class Seeds : MonoBehaviour, IAmAffectedByWater
    {
        #region Unity API
        private void Start()
        {
            _canGrow = true;
        }
        #endregion

        #region Main methods
        public void Flood()
        {
            if(!_canGrow) return;
            else if (_canGrow)
            {
                _spriteRenderer.sprite = _crops[1];
                _onGrownCrops.Raise();
                _canGrow = false;
            }
        }
        #endregion

        #region Utils

        #endregion

        #region Privates & Protected
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _crops;
        [SerializeField] private GameEvent _onGrownCrops;
        private bool _canGrow;
        #endregion
    }

}
