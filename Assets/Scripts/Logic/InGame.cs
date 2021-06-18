using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : MonoBehaviour
{
    int[][] board;
    [SerializeField] BoardView view;
    MatchData data;
    void Start(){
        data = GetMatchData("MatchData_1");
        Vector2Int grid = new Vector2Int(data.rows, data.lines);
        InitBoard(grid);
        view.Init(grid, OnChipRowSelected);
    }
    void Update(){
        
    }
    void InitBoard(Vector2Int grid) {
        board = new int[grid.x][];
        for (int x = 0; x < grid.x; x++) {
            board[x] = new int[grid.y];
            for(int y = 0; y < grid.y; y++) {
                board[x][y] = 0;
            }
        }
    }
    MatchData GetMatchData(string st) {
        string jsonObj = Resources.Load<TextAsset>("Data/MatchData/" + st).ToString();
        return JsonUtility.FromJson <MatchData> (jsonObj);
    }
    void OnChipRowSelected(int index) {
        Debug.Log("IN GAME... ROW::: " + index);
    }
}
