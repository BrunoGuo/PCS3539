using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// paredes de uma celula (em bits)
[Flags]
public enum WallPosition {
    LEFT = 1,
    RIGHT = 2,
    UP = 4,
    DOWN = 8,
    VISITED = 128
}

public struct Position {
    public int x;
    public int y;
}

public struct Neighbor {
    public Position cellPosition;
    public WallPosition sharedWalls;
}

public class MazeGenerator : MonoBehaviour {
    private static WallPosition GetOppositeWallPosition(WallPosition wallPosition) {
        switch (wallPosition) {
            case WallPosition.LEFT: return WallPosition.RIGHT;
            case WallPosition.RIGHT: return WallPosition.LEFT;
            case WallPosition.UP: return WallPosition.DOWN;
            case WallPosition.DOWN: return WallPosition.UP;
            default: return WallPosition.UP;
        }
    }

    private static WallPosition[,] ApplyBacktracker(WallPosition[,] maze, int width, int height) {
        var rng = new System.Random();
        var positionStack = new Stack<Position>();
        var position = new Position {
            x = rng.Next(0, width),
            y = rng.Next(0, height)
        };

        maze[position.x, position.y] |= WallPosition.VISITED; 
        positionStack.Push(position);

        while (positionStack.Count > 0) {
            var currentPosition = positionStack.Pop();
            var neighbors = GetUnvisitedNeighbors(currentPosition, maze, width, height);

            if (neighbors.Count > 0) {
                positionStack.Push(currentPosition);
                var randomIndex = rng.Next(0, neighbors.Count);
                var randomNeighbor = neighbors[randomIndex];
                var neighborPosition = randomNeighbor.cellPosition;

                // remover a parede que foi verificada
                maze[currentPosition.x, currentPosition.y] &= ~randomNeighbor.sharedWalls;
                maze[neighborPosition.x, neighborPosition.y] &= ~GetOppositeWallPosition(randomNeighbor.sharedWalls);

                maze[neighborPosition.x, neighborPosition.y] |= WallPosition.VISITED;

                positionStack.Push(neighborPosition);
            }
        }

        return maze;
    }

    public static List<Neighbor> GetUnvisitedNeighbors(Position position, WallPosition[,] maze, int width, int height) {
        var list = new List<Neighbor>();

        // need to check left wall
        if (position.x > 0) {
            if (!maze[position.x - 1, position.y].HasFlag(WallPosition.VISITED)) {
                list.Add(new Neighbor {
                    cellPosition = new Position {
                        x = position.x - 1,
                        y = position.y
                    },
                    sharedWalls = WallPosition.LEFT
                });
            }
        }

        // need to check bottom wall
        if (position.y > 0) {
            if (!maze[position.x, position.y - 1].HasFlag(WallPosition.VISITED)) {
                list.Add(new Neighbor {
                    cellPosition = new Position {
                        x = position.x,
                        y = position.y - 1
                    },
                    sharedWalls = WallPosition.DOWN
                });
            }
        }

        // need to check right wall
        if (position.x < width - 1) {
            if (!maze[position.x + 1, position.y].HasFlag(WallPosition.VISITED)) {
                list.Add(new Neighbor {
                    cellPosition = new Position {
                        x = position.x + 1,
                        y = position.y
                    },
                    sharedWalls = WallPosition.RIGHT
                });
            }
        }

        // need to check top wall
        if (position.y < height - 1) {
            if (!maze[position.x, position.y + 1].HasFlag(WallPosition.VISITED)) {
                list.Add(new Neighbor {
                    cellPosition = new Position {
                        x = position.x,
                        y = position.y + 1
                    },
                    sharedWalls = WallPosition.UP
                });
            }
        }

        return list;
    }

    public static WallPosition[,] GenerateMaze(int width, int height) {
        WallPosition[,] maze = new WallPosition[width, height];

        // inicializar as celulas do labirinto
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                maze[i, j] = WallPosition.LEFT | WallPosition.RIGHT | WallPosition.UP | WallPosition.DOWN;
            }
        }

        maze = ApplyBacktracker(maze, width, height);


        // maze[randomIndex, height - 1] &= ~WallPosition.UP;


        return maze;
    }
}
