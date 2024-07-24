using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign.Runtime
{
    [System.Serializable]
    public struct CellCoordinates
    {
        #region Publics
        public string name;
        public GameObject m_cellObject;
        public int m_cellLocation;
        #endregion
    }

}
