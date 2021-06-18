using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {
    int[][] grid;
    public Board(Vector2Int v) { SetGrid(v); }
    public void SetGrid(Vector2Int v) {
        grid = new int[v.x][];
        for (int x = 0; x < v.x; x++) {
            grid[x] = new int[v.y];
            for (int y = 0; y < v.y; y++) {
                grid[x][y] = 0;
            }
        }
    }
    public void SetChipInSlot(int playerId, int index) {
        for (int y = 0; y < grid[index].Length; y++) {
            if (grid[index][y] == 0) {
                grid[index][y] = playerId;
                return;
            }
        }
    }

    public int[][] GetGrid() { return grid; }
    public int GetChipPositionOnTopOfRow(int index) {
        int i = -1;
        for (int y = grid[index].Length -1; y >= 0; y--) {
            if (grid[index][y] != 0) {
                i = y;
                break;
            }
        }
        return i;
    }
    public bool IsAnEmptySlotInBoardAviable() {
        bool b = false;
        for (int x = 0; x < grid.Length; x++) {
            Debug.Log("CHECKING ROW " + x);
            if (grid[x][grid[x].Length - 1] != 0) {
                b = true;
                break;
            }
        }
        return b;
    }
    public bool IsAnEmptySlotInRowAviable(int index) {
        bool b = false;
        for (int y = 0; y < grid[index].Length; y++) {
            if (grid[index][y] == 0) {
                b = true;
                break;
            }
        }
        return b;
    }

}
