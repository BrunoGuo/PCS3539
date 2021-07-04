// https://www.youtube.com/watch?v=ya1HyptE5uc

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour {
    [SerializeField]
    [Range(1, 50)]
    private int width = 10;
    private List<Color> colorList = new List<Color> {Color.red, new Color(1, 0.5f, 0, 1), Color.yellow, Color.green, Color.cyan, Color.blue, Color.magenta, Color.white};

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

    [SerializeField]
    private Transform candlePrefab = null;

    public struct Position {
        public int x;
        public int y;
    }

    void Start() {
        var maze = MazeGenerator.GenerateMaze(width, height);
        Draw(maze);
    }

    private void setRandomColor(Transform obj, int index){
        Light lt = obj.GetComponent<Light>();
        lt.color = colorList[index];
    }

    private void Draw(WallPosition[,] maze) {
        var floor = Instantiate(floorPrefab, transform);
        floor.localScale = new Vector3(cellSize * width * 2, 1, cellSize * height * 2);
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

        var rng = new System.Random();
        var positionList = new List<Position>();
        for (int i = 0; i < 2; i++) {
            float x = rng.Next(0, width/2) * cellSize;
            float y = rng.Next(0, height/2) * cellSize;

            var candle0 = Instantiate(candlePrefab, transform) as Transform;
            var candle1 = Instantiate(candlePrefab, transform) as Transform;
            var candle2 = Instantiate(candlePrefab, transform) as Transform;
            var candle3 = Instantiate(candlePrefab, transform) as Transform;

            candle0.position = new Vector3(x - cellSize/2, -5, y - cellSize/2);
            setRandomColor(candle0, i*4);

            x = rng.Next(width/2, width) * cellSize;
            y = rng.Next(0, height/2) * cellSize;
            candle1.position = new Vector3(x - cellSize/2, -5, y - cellSize/2);
            setRandomColor(candle1, i*4 + 1);

            x = rng.Next(0, width/2) * cellSize;
            y = rng.Next(height/2, height) * cellSize;
            candle2.position = new Vector3(x - cellSize/2, -5, y - cellSize/2);
            setRandomColor(candle2, i*4 + 2);

            x = rng.Next(width/2, width) * cellSize;
            y = rng.Next(height/2, height) * cellSize;
            candle3.position = new Vector3(x - cellSize/2, -5, y - cellSize/2);
            setRandomColor(candle3, i*4 + 3);

            BoxCollider box0 = candle0.GetComponent<BoxCollider>();
            BoxCollider box1 = candle1.GetComponent<BoxCollider>();
            BoxCollider box2 = candle2.GetComponent<BoxCollider>();
            BoxCollider box3 = candle3.GetComponent<BoxCollider>();

            box0.isTrigger = true;
            box1.isTrigger = true;
            box2.isTrigger = true;
            box3.isTrigger = true;

            box0.size = new Vector3(cellSize / 8, 5, cellSize / 8);
            box1.size = new Vector3(cellSize / 8, 5, cellSize / 8);
            box2.size = new Vector3(cellSize / 8, 5, cellSize / 8);
            box3.size = new Vector3(cellSize / 8, 5, cellSize / 8);
        }

        // colocar o objeto de fim de jogo
        var randomIndex = rng.Next(0, height - 1);
        var trigger = Instantiate(endGameTrigger, transform) as Transform;
        trigger.position = new Vector3(-width/2 + (width - 1) * cellSize, -5.4f, -height/2 + randomIndex * cellSize);
    }

    void Update() {
        
    }
}
