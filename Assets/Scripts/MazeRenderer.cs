// https://www.youtube.com/watch?v=ya1HyptE5uc

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour {
    [SerializeField]
    [Range(1, 50)]
    private int width = 10;

    [SerializeField]
    [Range(1, 50)]
    private int height = 10;

    private float cellSize = 12f;

    [SerializeField]
    private Transform wallPrefab = null;

    [SerializeField]
    private Transform floorPrefab = null;

    [SerializeField]
    private Transform endGameTrigger = null;

    void Start() {
        var maze = MazeGenerator.GenerateMaze(width, height);
        Draw(maze);
    }

    private void Draw(WallPosition[,] maze) {
        var floor = Instantiate(floorPrefab, transform);
        floor.localScale = new Vector3(cellSize * width, 1, cellSize * height);
        floor.Translate(cellSize * (width - 2) / 2, - cellSize / 2, cellSize * (height - 2) / 2);

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                var cell = maze[i, j];
                var cellPosition = new Vector3(-width/2 + i * cellSize, 0, -height/2 + j * cellSize);
                
                if (cell.HasFlag(WallPosition.UP)) {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.position = cellPosition + new Vector3(0, 0, cellSize/2);
                    topWall.localScale = new Vector3(cellSize, topWall.localScale.y, topWall.localScale.z);
                }


                if (cell.HasFlag(WallPosition.LEFT)) {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = cellPosition + new Vector3(-cellSize/2, 0, 0);
                    leftWall.eulerAngles = new Vector3(0, 90, 0);
                    leftWall.localScale = new Vector3(cellSize, leftWall.localScale.y, leftWall.localScale.z);
                }

                if (i == (width - 1)) {
                    if (cell.HasFlag(WallPosition.RIGHT)) {
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.position = cellPosition + new Vector3(cellSize/2, 0, 0);
                        rightWall.eulerAngles = new Vector3(0, 90, 0);
                        rightWall.localScale = new Vector3(cellSize, rightWall.localScale.y, rightWall.localScale.z);
                    }
                }

                if (j == 0) {
                    if (cell.HasFlag(WallPosition.DOWN)) {
                        var bottomWall = Instantiate(wallPrefab, transform) as Transform;
                        bottomWall.position = cellPosition + new Vector3(0, 0, -cellSize/2);
                        bottomWall.localScale = new Vector3(cellSize, bottomWall.localScale.y, bottomWall.localScale.z);
                    }
                }
            }
        }

        // colocar o objeto de fim de jogo
        var rng = new System.Random();
        var randomIndex = rng.Next(0, height - 1);
        var trigger = Instantiate(endGameTrigger, transform) as Transform;
        trigger.position = new Vector3(-width/2 + (width - 1) * cellSize, -4, -height/2 + randomIndex * cellSize);
    }

    void Update() {
        
    }
}
