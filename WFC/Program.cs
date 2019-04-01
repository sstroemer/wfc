using System.Collections.Generic;

namespace WFC {
    internal class Program {
        public static void Main(string[] args) {    
            int[,] pattern = {
                { 1, 1, 1, 0, 0, 1, 1 },
                { 1, 0, 2, 0, 0, 0, 0 },
                { 1, 1, 1, 0, 0, 1, 0 },
                { 0, 0, 0, 1, 1, 1, 0 },
                { 1, 1, 0, 0, 0, 0, 0 }
            };
    
            Dictionary<int, double> weights = new Dictionary<int, double>() {
                { 0, 1.0 },
                { 1, 1.5 },
                { 2, 0.1 }
            };

            WeightedWaveFunctionCollapse wwfc = new WeightedWaveFunctionCollapse();
            wwfc.Parse(pattern, weights);
        }
    }
}



