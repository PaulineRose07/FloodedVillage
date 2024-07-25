using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Runtime
{
    [CreateAssetMenu(menuName = "Scriptables/ Int")]
    public class IntScriptable : ScriptableObject
    {
        #region Publics
        public int _current;
        public int _max;
        public int _min;
        #endregion
    }
}
