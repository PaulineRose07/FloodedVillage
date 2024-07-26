using Data.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiles.Runtime
{
    public class Zombie : MonoBehaviour, IAmAffectedByWater, IAmWater
    {
       
        
        #region Publics

        #endregion

        #region Unity API

        // Start is called before the first frame update
        void Start()
        {
			_spriteRenderer.sprite = _zombieSprites[0];
            _isDrowned = false;
    	}

        #endregion

        #region Main methods
        public void Flood()
        {
            if (_isDrowned) return;
            if (!_isDrowned)
            {
                 _spriteRenderer.sprite = _zombieSprites[1];
                _onZombieDrowned.Raise();
                _isDrowned = true;
            }
        }

        public void OnWaterInitialization()
        {
            if (!_isDrowned) return;
        }

        public bool AmIWater()
        {
            if (!_isDrowned) { return false; }
            else return true;
        }

        #endregion

        #region Utils

        #endregion

        #region Privates & Protected
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _zombieSprites;
        [SerializeField] private GameEvent _onZombieDrowned;
        [SerializeField] private GameEvent _onWaterInitialization;
        private bool _isDrowned;
        #endregion
    }

}
