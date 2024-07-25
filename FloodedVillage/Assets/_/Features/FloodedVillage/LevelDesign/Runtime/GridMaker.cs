using Data.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

       
        public TextAsset m_levelMapText;
        #endregion

        #region Unity API

        private void Awake()
        {
            
            _gridCellCount = _gridDimensions.x * _gridDimensions.y;
            _levelDesign = new int[_gridCellCount];
            Initialize();
            BackgroundGenerator();
            ForegroundGenerator();
        }






        #endregion

        #region Main methods

        private void Initialize()
        {
            string[] textContent = m_levelMapText.ToString().Split(',');
            for(int i = 0; i < textContent.Length; i++)
            {
                //Debug.Log(i);
                int intified = int.Parse(textContent[i]);
                _levelDesign[i] = intified;
                //Debug.Log($"index : {i} -- Value: {textContent[i]}");
            }
        }

        #endregion

        #region Utils
        private void BackgroundGenerator()
        {
            for (int i = 0; i < _gridCellCount; i++)
            {
                var pos = GetCellPosition(i);
                var cell = Instantiate(_backgroundPrefab, pos, Quaternion.identity, _backgroundTransform);

                cell.name = $"BackGround cell {pos}";
            }
        }

        private void ForegroundGenerator()
        {
            for (int i = 0; i < _gridCellCount; i++)
            {
                var pos = GetCellPosition(i);
                //Debug.Log(i);
                var cell = Instantiate(_cells[_levelDesign[i]], pos, Quaternion.identity, _foregroundTransform);
                cell.name = $"cell {i}";
                CellCoordinates coordinates = new CellCoordinates()
                {
                    name = $"cell {pos}",
                    m_cellObject = cell,
                    m_cellLocation = i,
                };
                _cellsCoordinatesList.Add(coordinates);
            }
        }

        private Vector2 GetCellPosition(int i)
        {
            int x = i % _gridDimensions.x;
            int y = i / _gridDimensions.x;
            
            return new Vector2Int(x, y);

        }

        private void OnDrawGizmosSelected()
        {
            for (int i = 0; i < _gridDimensions.x * _gridDimensions.y; i++)
            {
                var pos = GetCellPosition(i);
                Handles.Label(pos, i.ToString());
            }
        }
        #endregion

        [ContextMenu("Find Water")]
        private void FindWaterInCellCoordinates()
        {
            for(int i = 0; i < _cellsCoordinatesList.Count; i++)
            {
                if (_cellsCoordinatesList[i].m_cellObject.TryGetComponent<CanBeModified>(out CanBeModified modify))
                {
                    Debug.Log("Modified");
                }
            }
        }


        #region Privates & Protected
        [Header("--- Grid Settings ---")]
        [SerializeField] Vector2Int _gridDimensions;

        
        [SerializeField] GameObject[] _cells;
        [SerializeField] GameObject _backgroundPrefab;
        [SerializeField] List<CellCoordinates> _cellsCoordinatesList;
        [SerializeField] Transform _backgroundTransform;
        [SerializeField] Transform _foregroundTransform;
        private int _gridCellCount;
        [SerializeField] private int[] _levelDesign;
        
        #endregion
    }

}
