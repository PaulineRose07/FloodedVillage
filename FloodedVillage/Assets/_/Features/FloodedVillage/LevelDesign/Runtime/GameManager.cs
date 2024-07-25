using Data.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
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
        #endregion

        #region Utils
        private void GameOver()
        {
            _onGameOver.Raise();
        }
        #endregion

        #region Privates & Protected
        [SerializeField] private int _MaxMoves;
        [SerializeField] private int _CurrentMoves;
        [SerializeField] GameEvent _onGameOver;
        
        
        #endregion
    }

}
