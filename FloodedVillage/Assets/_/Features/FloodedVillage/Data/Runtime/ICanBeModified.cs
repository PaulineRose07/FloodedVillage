using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Runtime
{
    public interface ICanBeModified
    {
        #region Publics
        public bool IsTheTileFull();

        public void DestroyIfWater();
        #endregion
    }

}
