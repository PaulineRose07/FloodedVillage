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

        public bool AmIWater()
        {
            return true;
        }
        #endregion

        #region Privates & Protected
        [SerializeField] private GameEvent _onWaterInitialization;
        #endregion
    }

}
