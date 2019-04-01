using System;
using System.Collections.Generic;
using System.Linq;

namespace WFC {
    public class WeightedWaveFunctionCollapse {
        // todo: this should be adjustable from the outside
        private const int TILE_SIZE  = 2;
        // this is fixed in the current implementation
        private const int TILE_SHIFT = 1;

        private List<Tile> tiles;

        public void Parse(int[,] pattern, Dictionary<int, double> weights) {
            int ph = (pattern.GetLength(0) - (TILE_SIZE - 1));
            int pw = (pattern.GetLength(1) - (TILE_SIZE - 1));
            
            tiles = new List<Tile>();
            
            for (int x = 0; x < pw; x += TILE_SHIFT) {
                for (int y = 0; y < ph; y += TILE_SHIFT) {
                    int[,] points = new int[TILE_SIZE, TILE_SIZE];
                    double weight = 0;
                    for (int _x = 0; _x < TILE_SIZE; _x++) {
                        for (int _y = 0; _y < TILE_SIZE; _y++) {
                            points[_y, _x] = pattern[y + _y, x + _x];
                            weight += weights[points[_y, _x]];
                        }
                    }
                    
                    // LEFT, RIGHT, UP, DOWN
                    int n = y * pw + x;
                    int[] neighbours = {
                        (x == 0) ? (-1) : (n - 1), (x == (pw - 1)) ? (-1) : (n + 1),
                        (y == 0) ? (-1) : (n - pw), (y == (ph - 1)) ? (-1) : (n + pw)
                    };
                    tiles.Add(new Tile(points, weight, neighbours));
                }
            }
        }

        /*
         * width & height need to be multiples of TILE_SIZE
         */
        public int[,] GenerateMap(int width, int height) {
            // todo: check if width and height are "correct"
            
            int[,] tmap = new int[height/TILE_SIZE, width/TILE_SIZE];
            int[,] map  = new int[height, width];

            int tw = width/TILE_SIZE;
            int th = height/TILE_SIZE;
            
            // Clear map
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    tmap[y, x] = -1;
                }
            }

            // see here:
            // https://robertheaton.com/2018/12/17/wavefunction-collapse-algorithm/
            while (true) {
                // STEP 1: Calculate shannon entropy for every empty tile
                for (int x = 0; x < tw; x++) {
                    for (int y = 0; y < th; y++) {
                        if (tmap[y, x] != -1) continue;

                        double shannonEntropy = 0;
                        
                        int[] nb = {
                            (x == 0) ? (-1) : tmap[y, x-1], (x == (tw-1)) ? (-1) : tmap[y, x+1],
                            (y == 0) ? (-1) : tmap[y-1, x], (y == (th-1)) ? (-1) : tmap[y+1,x]
                        };
                        foreach (var t in tiles) {
                            if (t.IsPossible(nb)) {
                                
                            }
                        }
                    }
                }
            }

            return map;
        }
        
        class Tile {
            private int[,] points;
            private double weight;
            private int[] neighbours;

            public const int DIR_LEFT  = 0;
            public const int DIR_RIGHT = 1;
            public const int DIR_UP    = 3;
            public const int DIR_DOWN  = 4;
            
            public Tile(int[,] points, double weight, int[] neighbours) {
                this.points = points;
                this.weight = weight;     
                this.neighbours = neighbours;
            }
            
            public int GetP(int x, int y) {
                return points[x, y];
            }

            public double GetW() {
                return weight;
            }

            public bool HasNeighbour(int neighbour, int direction) {
                return (neighbours[direction] == neighbour);
            }

            public bool IsPossible(int[] neighbours) {
                for (int i = 0; i < 4; i++) {
                    if (neighbours[i] != -1 && this.neighbours[i] != -1 && neighbours[i] != this.neighbours[i]) {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}