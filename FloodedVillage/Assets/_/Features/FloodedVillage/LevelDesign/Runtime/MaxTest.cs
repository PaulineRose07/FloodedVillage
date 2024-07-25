using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign.Runtime
{
    public class MaxTest : MonoBehaviour
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
            _maxLevel = new int[_gridDimensions.x, _gridDimensions.y];
            _levelDesign = new int[_gridCellCount];
            TestMax();
            //Initialize();
            //BackgroundGenerator();
            //ForegroundGenerator();
        }

        #endregion

        #region Main methods

        private void Initialize()
        {
            string[] textContent = m_levelMapText.ToString().Split(',');
            for (int i = 0; i < textContent.Length; i++)
            {
                //Debug.Log(i);
                int intified = int.Parse(textContent[i]);
                _levelDesign[i] = intified;
                //Debug.Log($"index : {i} -- Value: {textContent[i]}");
            }
        }

        private void TestMax()
        {
            var lines = m_levelMapText.ToString().Split("\n"[0]);
            foreach(string soloLine in lines)
            {
               for(int i = 0; i < lines.Length; i++)
                {
                    for (int j = 0; j < lines[i].Length; j++)
                    {
                        if (lines[i][j] == "#"[0])
                        {
                            _maxLevel[i,j] = lines[i][j];
                        }
                        else if (lines[i][j] == "."[0])
                        {
                            _maxLevel[i, j] = lines[i][j];
                        }
                        Debug.Log(_maxLevel[i,j]);
                    }
                }

            }
            
        }


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
                //Debug.Log(i);
                var cell = Instantiate(_cells[_levelDesign[i]], pos, Quaternion.identity, _foregroundTransform);
                cell.name = $"cell {i}";
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
        [SerializeField] private int[] _levelDesign;
        [SerializeField] private int[,] _maxLevel;

        #endregion
    }

}
