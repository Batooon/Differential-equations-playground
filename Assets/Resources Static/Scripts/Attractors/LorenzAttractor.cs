using System.Collections;
using UnityEngine;

namespace Attractors
{
    public class LorenzAttractor : PhasePortrait
    {
        [SerializeField] private float _rho = 28f;
        [SerializeField] private float _sigma = 10f;
        [SerializeField] private float _beta = 8f / 3f;

        protected override void Start()
        {
            base.Start();
            X = .01f;
        }
        
        protected override IEnumerator PlotPortrait()
        {
            Dx = (_sigma * (Y - X)) * Dt;
            Dy = (X * (_rho - Z) - Y) * Dt;
            Dz = (X * Y - _beta * Z) * Dt;
            
            X += Dx;
            Y += Dy;
            Z += Dz;
            
            AddPointToPlot(X, Y, Z);
            
            yield return null;
        }
    }
}