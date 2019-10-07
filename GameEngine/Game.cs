using System;

namespace GameEngine
{
    /// <summary>
    /// Tic-Tac-Toe 
    /// </summary>
    public class Game
    {
        // null, X, O
        private CellState[,] Board { get;  set; }

        public int BoardWidth { get; }
        public int BoardHeight { get; }

        private bool _playerZeroMove;
        
        public Game(int boardHeight = 3, int boardWidth = 3)
        {
            if (boardHeight < 3 || boardWidth < 3)
            {
                throw new ArgumentException("Board size has to be at least 3x3!");
            }

            BoardHeight = boardHeight;
            BoardWidth = boardWidth;
            // initialize the board
            Board = new CellState[boardHeight, boardWidth];
        }
        
        public CellState[,] GetBoard()
        {
            var result = new CellState[BoardHeight, BoardWidth];
            Array.Copy(Board, result, Board.Length);
            return result;
        }


        public void Move(int posY, int posX)
        {
            if (Board[posY, posX] != CellState.Empty)
            {
                return;
            }

            Board[posY, posX] = _playerZeroMove ? CellState.X : CellState.O;
            
            _playerZeroMove = !_playerZeroMove;
        }

    }
    
    
}