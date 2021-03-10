using MSI2.Algorithms;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSI2.Helpers
{
    public static class SolversFactory
    {
        private static readonly Dictionary<AlgorithmType, Func<ICVRPSolver>> _solvers = new Dictionary<AlgorithmType, Func<ICVRPSolver>>
        {
            [AlgorithmType.Greedy] = () => new GreedySolver(),
            [AlgorithmType.BasicAnt] = () => new BasicAntSolver(),
            [AlgorithmType.LocalAnt] = () => new LocalAntSolver(),
            [AlgorithmType.EliteAnt] = () => new EliteAntSolver()
        };

        public static ICVRPSolver CreateSolver(AlgorithmType algorithmType) =>
            _solvers[algorithmType]();
    }
}
