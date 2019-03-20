using System.Collections.Generic;

namespace WFC {
    public class WaveFormCollapse {
        private const int TILE_SIZE  = 3;
        private const int TILE_SHIFT = 1;

        // todo: add init pattern + every pixel of pattern gets a weight
        // => sum of weights that build the tile => weight of tile

        class Tile {
            private int[,] points;
            private double weight;
            private List<Tile> neighbours;

            public override bool Equals(object obj) {
                return false;
            }
        }
    }
}