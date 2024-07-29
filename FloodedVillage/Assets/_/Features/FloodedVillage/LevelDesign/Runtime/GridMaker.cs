using Data.Runtime;
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

        [ContextMenu("Find Water")]
        public void FindWaterInCellCoordinates()
        {
            for (int i = 0; i < _cellsCoordinatesList.Count; i++)
            {
                if (_cellsCoordinatesList[i].m_cellObject.TryGetComponent<IAmBridge>(out IAmBridge bridge))
                {
                    bridge.UpdateSprite();
                    CheckIfOutOfBoundsBridge(i);
                }
                if (_cellsCoordinatesList[i].m_cellObject.TryGetComponent<IAmWater>(out IAmWater water))
                {
                    CheckIfOutOfBounds(i);
                    //FindAdjacentCellFromGrid(i, -10);
                }
            }
        }

 
        public void FindAdjacentCellFromGrid(int i, int location)
        {
            int newLocation = i + location;

            if (newLocation >= 0 && newLocation < _cellsCoordinatesList.Count)
            {
                if (_cellsCoordinatesList[newLocation].m_cellObject.TryGetComponent<ICanBeModified>(out ICanBeModified modify))
                {
                    if (modify.IsTheTileFull() == false)
                    {
                        var CellCoordinatesOfNeighbor = _cellsCoordinatesList[newLocation];
                        SwitchTilesSandInGrid(newLocation);
                        //Debug.Log($"You can flood {CellCoordinatesOfNeighbor} ");
                    }
                }
                if (_cellsCoordinatesList[newLocation].m_cellObject.TryGetComponent<IAmAffectedByWater>(out IAmAffectedByWater affected))
                {
                    affected.Flood();
                    if(_cellsCoordinatesList[newLocation].m_cellObject.TryGetComponent<IAmZombie>(out IAmZombie zombie))
                    {
                        SwitchTilesZombieInGrid(newLocation);
                    }
                }
            }
        }

        private void SwitchTilesZombieInGrid(int i)
        {
            var pos = GetCellPosition(i);
            var cell = Instantiate(_cells[2], pos, Quaternion.identity, _foregroundTransform);
            var myStruct = _cellsCoordinatesList[i];
            myStruct.m_cellObject = cell;
            _cellsCoordinatesList[i] = myStruct;
            cell.TryGetComponent<IAmWater>(out IAmWater water);
            water.ChangeToZombie();
            water.OnWaterInitialization();
        }

        private void SwitchTilesSandInGrid(int i)
        {
            var pos = GetCellPosition(i);
            var cell = Instantiate(_cells[2], pos, Quaternion.identity, _foregroundTransform);
            _cellsCoordinatesList[i].m_cellObject.GetComponent<ICanBeModified>().DestroyIfWater();
            var myStruct = _cellsCoordinatesList[i];
            myStruct.m_cellObject = cell;
            _cellsCoordinatesList[i] = myStruct;
            cell.TryGetComponent<IAmWater>(out IAmWater water);
            water.OnWaterInitialization();
        }

        private void CheckIfOutOfBoundsBridge(int i)
        {
            FindAdjacentCellFromGrid(i, +_gridDimensions.x);
            FindAdjacentCellFromGrid(i, -_gridDimensions.x);
        }


        private void CheckIfOutOfBounds(int i)
        {
            int cellLocation = _cellsCoordinatesList[i].m_cellLocation;
            //Check if bottom of 1DGrid
            bool bottom = cellLocation < _gridDimensions.x;
            //Check if top of 1DGrid
            bool top = cellLocation > _gridCellCount - _gridDimensions.x;
            //Check if Left of 1DGrid
            bool left = cellLocation % _gridDimensions.x == 0;
            //Check if right of 1DGrid
            bool right = cellLocation % _gridDimensions.x == _gridDimensions.x - 1;


            if (bottom)
            {
                if (!right) FindAdjacentCellFromGrid(i, +1);
                if (!left) FindAdjacentCellFromGrid(i, -1);
                FindAdjacentCellFromGrid(i, +_gridDimensions.x);

            }
            if (top)
            {
                if (!right) FindAdjacentCellFromGrid(i, +1);
                if (!left) FindAdjacentCellFromGrid(i, -1);
                FindAdjacentCellFromGrid(i, -_gridDimensions.x);
            }

            if (left && !bottom && !top)
            {
                if (!bottom) FindAdjacentCellFromGrid(i, +_gridDimensions.x);
                if (!top) FindAdjacentCellFromGrid(i, -_gridDimensions.x);

                FindAdjacentCellFromGrid(i, +1);
            }

            if (right && !bottom && !top)
            {
                if (!bottom) FindAdjacentCellFromGrid(i, +_gridDimensions.x);
                if (!top) FindAdjacentCellFromGrid(i, -_gridDimensions.x);
                FindAdjacentCellFromGrid(i, -1);

            }
            else if (!bottom && !top && !right && !left)
            {
                FindAdjacentCellFromGrid(i, +_gridDimensions.x);
                FindAdjacentCellFromGrid(i, -_gridDimensions.x);
                FindAdjacentCellFromGrid(i, +1);
                FindAdjacentCellFromGrid(i, -1);
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

        /*private void OnDrawGizmosSelected()
        {
            for (int i = 0; i < _gridDimensions.x * _gridDimensions.y; i++)
            {
                var pos = GetCellPosition(i);
                Handles.Label(pos, i.ToString());
            }
        }*/
    

        #endregion


        #region Privates & Protected
        [Header("--- Grid Settings ---")]
        [SerializeField] Vector2Int _gridDimensions;
        [SerializeField] Transform _backgroundTransform;
        [SerializeField] Transform _foregroundTransform;
        [SerializeField] GameObject[] _cells;
        [SerializeField] GameObject _backgroundPrefab;
        private int _gridCellCount;
        [Header("--- Instantiated List ---")]
        [SerializeField] List<CellCoordinates> _cellsCoordinatesList;
        [SerializeField] private int[] _levelDesign;
        

        #endregion
    }

}
