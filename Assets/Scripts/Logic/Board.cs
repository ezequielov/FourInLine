using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Board {
    int[][] grid;
    const int EMPTY_SLOT = -1;
    int[] slotAviablePerRow;
    public Board(Vector2Int v) { SetGrid(v); }
    public void SetGrid(Vector2Int v) {
        slotAviablePerRow = new int[v.x];
        grid = new int[v.x][];
        for (int x = 0; x < v.x; x++) {
            slotAviablePerRow[x] = 0;
            grid[x] = new int[v.y];
            for (int y = 0; y < v.y; y++) {
                grid[x][y] = EMPTY_SLOT;
            }
        }
    }
    public void SetChipInSlot(int playerId, int index) {
        if(IsAnEmptySlotInRowAviable(index)) {
            grid[index][slotAviablePerRow[index]] = playerId;
            slotAviablePerRow[index]++;
        }
    }

    public int[][] GetGrid() { return grid; }
    public int GetChipPositionOnTopOfRow(int index) { return slotAviablePerRow[index] - 1; }
    public bool IsAnEmptySlotInBoardAviable() {
        bool b = false;
        for (int x = 0; x < grid.Length; x++) {
            if (IsAnEmptySlotInRowAviable(x)) {
                b = true;
                break;
            }
        }
        return b;
    }
    public bool IsAnEmptySlotInRowAviable(int index) { return (slotAviablePerRow[index] < grid[index].Length); }
    public int GetNumberOfRows() { return grid.Length; }
    public bool IsWinConditionAchieved(int playerId, int amount = 4) {
        return IsWinConditionInLine(playerId, amount) || IsWinConditionInRow(playerId, amount) || IsWinConditionInDiagonal(playerId, amount);
    }
    public bool IsWinConditionInLine(int playerId, int amount = 4) {
        foreach(int i in slotAviablePerRow) {
            if(i > 0) {
                int y = i - 1;
                for (int xPos = 0; xPos < grid.Length; xPos++) {
                    if (xPos <= grid.Length - amount) {
                        int newAmount = amount + xPos;
                        bool b = true;
                        for (int x = xPos; x < newAmount; x++) {
                            if (grid[x][y] != playerId) { b = false; }
                        }
                        if (b) { return true; }
                    }
                }
            }
        }
        return false;
    }
    public bool IsWinConditionInRow(int playerId, int amount = 4) {
        for(int x = 0; x < slotAviablePerRow.Length; x++) {
            int topY = slotAviablePerRow[x] -1;
            if (slotAviablePerRow[x] >= amount) {
                int newAmount = topY - amount;
                bool b = true;
                for (int y = topY; y > newAmount; y--) {
                    if (grid[x][y] != playerId) { b = false; }
                }
                if (b) { return true; }
            }
        }
        return false;
    }
    public bool IsWinConditionInDiagonal(int playerId, int amount = 4) {
        return IsWinConditionInDiagonal(playerId, true) || IsWinConditionInDiagonal(playerId, false);
    }
    public bool IsWinConditionInDiagonal(int playerId, bool toRight, int amount = 4) {
        for (int posX = 0; posX < slotAviablePerRow.Length; posX++) {
            int topY = slotAviablePerRow[posX] - 1;
            int direction = (toRight) ? 1 : -1;
            if(topY >= 0) {
                Vector2Int realPosition = new Vector2Int(posX, topY);
                Vector2Int startCountingPosition = new Vector2Int();
                Vector2Int endCountingPosition = new Vector2Int();

                startCountingPosition.y = realPosition.y - (amount - 1);
                if(startCountingPosition.y < 0) { startCountingPosition.y = 0; }
                if (toRight) {
                    startCountingPosition.x = realPosition.x - (amount - 1);
                    if (startCountingPosition.x < 0) { startCountingPosition.x = 0; }
                }
                else {
                    startCountingPosition.x = realPosition.x + (amount - 1);
                    if (startCountingPosition.x > slotAviablePerRow.Length - 1) { startCountingPosition.x = slotAviablePerRow.Length - 1; }
                }
                int yStartLength, xStartLength;
                yStartLength = realPosition.y - startCountingPosition.y;
                xStartLength = Math.Abs(realPosition.x - startCountingPosition.x);
                if(yStartLength < xStartLength) { 
                    startCountingPosition.x = (toRight) ? realPosition.x - yStartLength : realPosition.x + yStartLength;
                }
                else { startCountingPosition.y = realPosition.y - xStartLength; }

                endCountingPosition.y = realPosition.y + (amount - 1);
                if (endCountingPosition.y > grid[posX].Length -1) { endCountingPosition.y = grid[posX].Length - 1; }
                if (toRight) {
                    endCountingPosition.x = realPosition.x + (amount - 1);
                    if (endCountingPosition.x > grid.Length - 1) { endCountingPosition.x = grid.Length - 1; }
                }
                else {
                    endCountingPosition.x = realPosition.x - (amount - 1);
                    if (endCountingPosition.x < 0) { endCountingPosition.x = 0; }
                }
                int yEndLength, xEndLength;
                yEndLength =  endCountingPosition.y - realPosition.y;
                xEndLength = Math.Abs(realPosition.x - endCountingPosition.x);
                if (yEndLength < xEndLength) {
                    endCountingPosition.x = (toRight) ? realPosition.x + yEndLength : realPosition.x - yEndLength;
                }
                else { endCountingPosition.y = realPosition.y + xEndLength; }

                int diagonalLength = endCountingPosition.y - startCountingPosition.y;
                int counter = 0;
                for (int i = 0; i <= diagonalLength; i++) {
                    int xDir = i * direction;
                    if (grid[startCountingPosition.x + xDir][startCountingPosition.y + i] != playerId) { counter = 0; }
                    else { counter++; }
                    if (counter >= amount) { return true; }
                }
            }

            
        }
            return false;
    }

}
