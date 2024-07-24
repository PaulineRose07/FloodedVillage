using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign.Runtime
{
    public class GridMaker : MonoBehaviour
    {
        #region Publics
        public int[] m_firstLevel = new int[]
        {
            1,1,1,1,1,1,1,1,1,3,
            1,0,0,0,0,0,0,0,0,0,
            1,0,1,1,1,1,3,1,1,1,
            1,0,1,1,1,3,1,1,1,1,
            1,1,1,1,1,1,1,4,1,1,
            2,2,2,2,2,2,2,2,2,2,
        };
        #endregion

        #region Unity API

        private void Awake()
        {
            _gridCellCount = _gridDimensions.x * _gridDimensions.y;
            BackgroundGenerator();
            ForegroundGenerator();
        }
       

        #endregion

        #region Main methods

        #endregion

        #region Utils
        private void BackgroundGenerator()
        {
            for (int i = 0; i < _gridCellCount; i++)
            {
                var pos = GetCellPosition(i);
                var cell = Instantiate(_cells[0], pos, Quaternion.identity, _backgroundTransform);

                cell.name = $"BackGround cell {pos}";
            }

           
        }

        private void ForegroundGenerator()
        {
            for (int i = 0; i < _gridCellCount; i++)
            {
                var pos = GetCellPosition(i);
                Debug.Log(i);
                var cell = Instantiate(_cells[m_firstLevel[i]], pos, Quaternion.identity, _foregroundTransform);
                cell.name = $"cell {pos}";
                CellCoordinates coordinates = new CellCoordinates()
                {
                    name = $"cell {pos}",
                    m_cellObject = cell,
                    m_cellLocation = i,
                };
                _cellsList.Add(coordinates);
                
            }
        }

        private Vector2 GetCellPosition(int i)
        {
            int x = i % _gridDimensions.x;
            int y = i / _gridDimensions.x;
            
            return new Vector2Int(x, y);

        }
        #endregion

        #region Privates & Protected
        [Header("--- Grid Settings ---")]
        [SerializeField] Vector2Int _gridDimensions;

        
        [SerializeField] GameObject[] _cells;
        [SerializeField] List<CellCoordinates> _cellsList;
        [SerializeField] Transform _backgroundTransform;
        [SerializeField] Transform _foregroundTransform;
        private int _gridCellCount;
        
        #endregion
    }

}
