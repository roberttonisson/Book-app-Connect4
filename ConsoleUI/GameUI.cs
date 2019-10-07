﻿using System;
using System.ComponentModel;
using GameEngine;

namespace ConsoleUI
{
    public static class GameUI
    {
        private static readonly string _verticalSeparator = "|";
        private static readonly string _horizontalSeparator = "-";
        private static readonly string _centerSeparator = "+";
        
        public static void PrintBoard(Game game)
        {
            var board = game.GetBoard();
            for (int yIndex = 0; yIndex < game.BoardHeight; yIndex++)
            {
                var line = "";
                for (int xIndex = 0; xIndex < game.BoardWidth; xIndex++)
                {
                    
                    line = line + " " + GetSingleState(board[yIndex, xIndex]) + " ";
                    if (xIndex < game.BoardWidth - 1)
                    {
                        line = line + _verticalSeparator;
                    }
                }
                
                Console.WriteLine(line);

                if (yIndex < game.BoardHeight - 1)
                {
                    line = "";
                    for (int xIndex = 0; xIndex < game.BoardWidth; xIndex++)
                    {
                        line = line + _horizontalSeparator+ _horizontalSeparator+ _horizontalSeparator;
                        if (xIndex < game.BoardWidth - 1)
                        {
                            line = line +_centerSeparator;
                        }
                    }
                    Console.WriteLine(line);
                }

                
            }
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