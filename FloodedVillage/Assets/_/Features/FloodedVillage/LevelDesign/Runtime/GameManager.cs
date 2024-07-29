using Data.Runtime;
using TMPro;
using UnityEngine;

namespace LevelDesign.Runtime
{
    public class GameManager : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity API

        // Start is called before the first frame update
        void Start()
        {
            _CurrentMoves = _MaxMoves;
        }

        // Update is called once per frame
        void Update()
        {
            if(_currentCrops >= _maxCrops && _currentAliveZombies <= 0)
            {
                YouWon(); 
            }
            if (_CurrentMoves == 0)
            {
                GameOver();
            }
            
        }

        #endregion

        #region Main methods
        public void UpdateCurrentMoves()
        {
            _CurrentMoves--;
        }

        public void UpdateMovesText(TMP_Text movesText)
        {
            movesText.text = $"Moves left: {_CurrentMoves.ToString()}";
            if( _CurrentMoves <= 0)
            {
                movesText.text = $"Moves left: 0";
            }
        }

        public void UpdateZombieCount()
        {
            _currentAliveZombies--;
        }
        public void UpdateCurrentGrownSeeds()
        {
            _currentCrops++;
        }
        public void GameOver()
        {
            _onGameOver.Raise();
        }
        #endregion

        #region Utils

        private void YouWon()
        {
            _onLevelWon.Raise();
        }
        #endregion

        #region Privates & Protected
        [SerializeField] private int _MaxMoves;
        [SerializeField] private int _CurrentMoves;
        [SerializeField] private int _maxCrops;
        [SerializeField] private int _currentCrops;
        [SerializeField] private int _currentAliveZombies;
        [SerializeField] GameEvent _onGameOver;
        [SerializeField] private GameEvent _onLevelWon;


        #endregion
    }

}
