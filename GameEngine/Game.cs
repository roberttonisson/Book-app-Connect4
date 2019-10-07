using System;
using System.Collections.Generic;

namespace GameEngine
{
    /// <summary>
    /// Tic-Tac-Toe 
    /// </summary>
    public class Game
    {
        // null, X, O
        private CellState[,] Board { get; set; }

        public int BoardWidth { get; }
        public int BoardHeight { get; }

        public Dictionary<int, int> RowStatus = new Dictionary<int, int>();

        private bool _playerZeroMove;

        public Game(int boardHeight = 4, int boardWidth = 4)
        {
            if (boardHeight < 4 || boardWidth < 4)
            {
                throw new ArgumentException("Board size has to be at least 3x3!");
            }

            BoardHeight = boardHeight;
            BoardWidth = boardWidth;
            // initialize the board
            for (int i = 1; i <= boardWidth; i++)
            {
                RowStatus[i] = 0;
            }

            Board = new CellState[boardHeight, boardWidth];
        }

        public CellState[,] GetBoard()
        {
            var result = new CellState[BoardHeight, BoardWidth];
            Array.Copy(Board, result, Board.Length);
            return result;
        }


        public void Move(int row)
        {
            Board[BoardHeight - RowStatus[row] - 1, row - 1] = _playerZeroMove ? CellState.O : CellState.X;

            RowStatus[row] = RowStatus[row] + 1;

            _playerZeroMove = !_playerZeroMove;
        }
    }
}