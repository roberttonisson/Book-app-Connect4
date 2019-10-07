using System;
using System.ComponentModel;
using GameEngine;

namespace ConsoleUI
{
    public static class GameUI
    {
        private static readonly string _verticalSeparator = "│";
        private static readonly string _horizontalSeparator = "─";
        private static readonly string _crossSeparator = "┼";
        private static readonly string _leftCrossSeparator = "├";
        private static readonly string _rightCrossSeparator = "┤";

        public static void PrintBoard(Game game)
        {
            var line = "";
            var board = game.GetBoard();
            for (int yIndex = 0; yIndex < game.BoardHeight; yIndex++)
            {
                line = _verticalSeparator;
                for (int xIndex = 0; xIndex < game.BoardWidth; xIndex++)
                {
                    line = line + " " + GetSingleState(board[yIndex, xIndex]) + " " + _verticalSeparator;
                }

                Console.WriteLine(line);

                if (yIndex < game.BoardHeight)
                {
                    line = yIndex < game.BoardHeight - 1 ? _leftCrossSeparator : "└";

                    for (int xIndex = 0; xIndex < game.BoardWidth; xIndex++)
                    {
                        line = line + _horizontalSeparator + _horizontalSeparator + _horizontalSeparator;
                        if (xIndex < game.BoardWidth - 1)
                        {
                            line += yIndex < game.BoardHeight - 1 ? _crossSeparator : "┴";
                        }
                        else
                        {
                            line += yIndex < game.BoardHeight - 1 ? _rightCrossSeparator : "┘";
                        }
                    }

                    Console.WriteLine(line);
                }
            }

            line = "";
            for (int i = 1; i <= game.BoardWidth; i++)
            {
                line = line + "  " + i + " ";
            }

            Console.WriteLine(line);
        }

        public static string GetSingleState(CellState state)
        {
            switch (state)
            {
                case CellState.Empty:
                    return " ";
                case CellState.O:
                    return "O";
                case CellState.X:
                    return "X";
                default:
                    throw new InvalidEnumArgumentException("Unknown enum option!");
            }
        }
    }
}