using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
