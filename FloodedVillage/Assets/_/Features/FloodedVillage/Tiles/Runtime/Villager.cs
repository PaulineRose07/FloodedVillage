using Data.Runtime;
using UnityEngine;

namespace Tiles.Runtime
{
    public class Villager : MonoBehaviour, IAmAffectedByWater
    {
        public void Flood()
        {
            _onVillagerDrowned.Raise();
            _spriteRenderer.sprite = _sprites[1];
        }
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

        #endregion

        #region Utils

        #endregion

        #region Privates & Protected
        [SerializeField] private GameEvent _onVillagerDrowned;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _sprites;
        #endregion
    }

}
