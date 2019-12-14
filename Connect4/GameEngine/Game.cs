using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    /// <summary>
    /// Connect4
    /// </summary>
    public class Game
    {
        public CellState[,] Board { get; set; } = default!;

        public int BoardWidth { get; set; }
        public int BoardHeight { get; set; }

        public bool _playerZeroMove { get; set; }
        public bool _computerPlays { get; set; }

        public Dictionary<int, int> ColumnStatus = new Dictionary<int, int>();

        public Game()
        {
        }

        public Game(GameSettings settings, bool computerPlays = false, bool computerStarts = false)
        {
            if (settings.BoardHeight < 4 || settings.BoardWidth < 4)
            {
                throw new ArgumentException("Board size has to be at least 4x4!");
            }

            if (computerPlays)
            {
                _computerPlays = true;
            }

            _playerZeroMove = computerStarts;

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
            var result = new CellState[BoardHeight, BoardWidth];
            Array.Copy(Board, result, Board.Length);
            return result;
        }


        public bool Move(int column)
        {
            Board[BoardHeight - ColumnStatus[column] - 1, column - 1] = _playerZeroMove ? CellState.O : CellState.X;

            ColumnStatus[column] = ColumnStatus[column] + 1;

            _playerZeroMove = !_playerZeroMove;

            return CheckForWin(column);
        }

        public bool ComputerMove()
        {
            var fullColumns = ColumnStatus.Keys.ToArray().Where(key => ColumnStatus[key] < BoardWidth).ToList();
            var random = new Random();
            var column = fullColumns[random.Next(fullColumns.Count)];

            Board[BoardHeight - ColumnStatus[column] - 1, column - 1] = _playerZeroMove ? CellState.O : CellState.X;

            ColumnStatus[column] = ColumnStatus[column] + 1;

            _playerZeroMove = !_playerZeroMove;

            return CheckForWin(column);
        }

        public bool CheckForWin(int column)
        {
            var startWidth = column - 1;
            var startHeight = BoardHeight - ColumnStatus[column];
            var cell = !_playerZeroMove ? CellState.O : CellState.X;
            var total = 1;
            //Check horizontal win
            for (var i = 1; i < 4; i++)
            {
                if (startWidth + i <= BoardWidth - 1 && Board[startHeight, startWidth + i] == cell)
                {
                    total += 1;
                    continue;
                }

                break;
            }

            for (var i = 1; i < 4; i++)
            {
                if (startWidth - i >= 0 && Board[startHeight, startWidth - i] == cell)
                {
                    total += 1;
                    continue;
                }

                break;
            }

            if (total >= 4)
            {
                return true;
            }

            //Check Vertical win
            total = 1;
            for (var i = 1; i < 4; i++)
            {
                if (startHeight + i <= BoardHeight - 1 && Board[startHeight + i, startWidth] == cell)
                {
                    total += 1;
                    continue;
                }

                break;
            }

            for (var i = 1; i < 4; i++)
            {
                if (startHeight - i >= 0 && Board[startHeight - i, startWidth] == cell)
                {
                    total += 1;
                    continue;
                }

                break;
            }

            if (total >= 4)
            {
                return true;
            }

            //Check diagonal win
            total = 1;
            for (var i = 1; i < 4; i++)
            {
                if (startHeight + i <= BoardHeight - 1 && startWidth + i <= BoardWidth - 1 &&
                    Board[startHeight + i, startWidth + i] == cell)
                {
                    total += 1;
                    continue;
                }

                break;
            }

            for (var i = 1; i < 4; i++)
            {
                if (startHeight - i >= 0 && startWidth - i >= 0 && Board[startHeight - i, startWidth - i] == cell)
                {
                    total += 1;
                    continue;
                }

                break;
            }

            if (total >= 4)
            {
                return true;
            }

            //Check other diagonal win
            total = 1;
            for (var i = 1; i < 4; i++)
            {
                if (startHeight + i <= BoardHeight - 1 && startWidth - i >= 0 &&
                    Board[startHeight + i, startWidth - i] == cell)
                {
                    total += 1;
                    continue;
                }

                break;
            }

            for (var i = 1; i < 4; i++)
            {
                if (startHeight - i >= 0 && startWidth + i <= BoardWidth - 1 && Board[startHeight - i, startWidth + i] == cell)
                {
                    total += 1;
                    continue;
                }

                break;
            }

            if (total >= 4)
            {
                return true;
            }

            return false;
        }
    }
}