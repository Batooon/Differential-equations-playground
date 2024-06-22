using System.Collections;
using UnityEngine;

namespace Attractors
{
    public class UsualEquation : PhasePortrait
    {
        [SerializeField] private float _dk;
        private float _k1;
        private float _k2;
        private float _t;

        protected override void Start()
        {
            base.Start();

            Dx = .5f;
            Dy = .5f;
        }
        
        protected override IEnumerator PlotPortrait()
        {
            for (var i = -25; i < 15; i++)
            {
                for (var j = -25; j < 15; j++)
                {
                    var x = Dx * i;
                    var y = Dy * j;
                    var prime = F(new Vector2(x, y));
                    AddLine(new Vector2(x, y), prime);
                }
            }

            // AddPointToPlot(X, Y);

            yield break;
        }

        private Vector2 F(Vector2 coordinates)
        {
            return new Vector2(coordinates.y, -Mathf.Sin(coordinates.x));
        }
    }
}