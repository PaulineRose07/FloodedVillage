using Data.Runtime;
using UnityEngine;

namespace Tiles.Runtime
{
    public class Bridge : MonoBehaviour, IAmBridge
    {
        public void UpdateSprite()
        {
            _spriteRenderer.sprite = _sprites[1];
        }

        #region Private & Protected
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _sprites;

        #endregion
    }

}
