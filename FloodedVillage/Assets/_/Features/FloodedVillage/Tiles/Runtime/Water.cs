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

        #endregion

        #region Privates & Protected
        [SerializeField] private GameEvent _onWaterInitialization;
        #endregion
    }

}
