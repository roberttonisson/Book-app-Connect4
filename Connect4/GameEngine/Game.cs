using System;
using System.Collections.Generic;

namespace GameEngine
{
    /// <summary>
    /// Tic-Tac-Toe 
    /// </summary>
    public class Game
    {
        public CellState[,] Board { get; set; } = default!;

        public int BoardWidth { get; set; }
        public int BoardHeight { get; set; }

        public bool _playerZeroMove { get; set; }

        public Dictionary<int, int> ColumnStatus = new Dictionary<int, int>();

        public Game()
        {
        }

        public Game(GameSettings settings)
        {
            if (settings.BoardHeight < 4 || settings.BoardWidth < 4)
            {
                throw new ArgumentException("Board size has to be at least 4x4!");
            }

            BoardHeight = settings.BoardHeight;
            BoardWidth = settings.BoardWidth;
            // initialize the board
            for (int i = 1; i <= BoardWidth; i++)
            {
                ColumnStatus[i] = 0;
            }

            Board = new CellState[BoardHeight, BoardWidth];
        }


        public CellState[,] GetBoard()
        {
            var result = new CellState[BoardHeight,BoardWidth];
            Array.Copy(Board, result, Board.Length);
            return result;
        }


        public void Move(int row)
        {
            Board[BoardHeight - ColumnStatus[row] - 1, row - 1] = _playerZeroMove ? CellState.O : CellState.X;

            ColumnStatus[row] = ColumnStatus[row] + 1;

            _playerZeroMove = !_playerZeroMove;
        }
    }
}