using Data.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;

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
        public void FindWaterInCellCoordinates()
        {
            for(int i = 0; i < _cellsCoordinatesList.Count; i++)
            {
                if (_cellsCoordinatesList[i].m_cellObject.TryGetComponent<IAmWater>(out IAmWater water))
                {
                    Debug.Log("I Am Water");
                    CheckIfOutOfBounds(i);
                    //FindAdjacentCellFromGrid(i, -10);
                }
            }
        }

        public void FindAdjacentCellFromGrid(int i, int location)
        {
            if (_cellsCoordinatesList[i + location].m_cellObject.TryGetComponent<CanBeModified>(out CanBeModified modify))
            {
                if(modify.IsTheTileFull() == false)
                {
                    var CellCoordinatesOfNeighbor = _cellsCoordinatesList[i + location];
                    SwitchTilesInGrid(i + location);
                    Debug.Log($"You can flood {CellCoordinatesOfNeighbor} ");
                }
            }



            
        }

        private void SwitchTilesInGrid(int i)
        {
            var pos = GetCellPosition(i);
            var cell = Instantiate(_cells[2], pos, Quaternion.identity, _foregroundTransform);
            _cellsCoordinatesList[i].m_cellObject.GetComponent<CanBeModified>().DestroyIfWater();
            
            var myStruct = _cellsCoordinatesList[i];
            myStruct.m_cellObject = cell;
            _cellsCoordinatesList[i] = myStruct;
        }

        private void CheckIfOutOfBounds(int i)
        {
            //Check if bottom of 1DGrid
            bool bottom = _cellsCoordinatesList[i].m_cellLocation < _gridDimensions.x;
            //Check if top of 1DGrid
            bool top = _cellsCoordinatesList[i].m_cellLocation > _gridCellCount - _gridDimensions.x;
            //Check if Left of 1DGrid
            bool left = _cellsCoordinatesList[i].m_cellLocation % _gridDimensions.x == 0;
            //Check if right of 1DGrid
            bool right = _cellsCoordinatesList[i].m_cellLocation % _gridDimensions.x == _gridDimensions.x - 1;
            

            if (bottom)
            {
                if (right) FindAdjacentCellFromGrid(i, -1);

                if(left) FindAdjacentCellFromGrid(i, +1);

                FindAdjacentCellFromGrid(i, +10);
            }
            if(top)
            {
                FindAdjacentCellFromGrid(i, -10);

                if (right) FindAdjacentCellFromGrid(i, -1);

                if (left) FindAdjacentCellFromGrid(i, +1);
            }
            
            if (left)
            {
                FindAdjacentCellFromGrid(i, +1);

            }
           
            if (right)
            {
                FindAdjacentCellFromGrid(i, -1);
            }
            else if(!bottom && !top && !right && !left)
            {
                FindAdjacentCellFromGrid(i, +10);
                FindAdjacentCellFromGrid(i, -10);
                FindAdjacentCellFromGrid(i, +1);
                FindAdjacentCellFromGrid(i, -1);
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
