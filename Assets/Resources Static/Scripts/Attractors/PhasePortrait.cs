using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Attractors
{
    [RequireComponent(typeof(LineRenderer))]
    public abstract class PhasePortrait : MonoBehaviour, ICenterChange
    {
        [SerializeField, Range(.001f, .03f)] protected float Dt;
        [SerializeField] private KeyCode _stopPlottingKey;
        [SerializeField] private LineData _lineData;
        
        public event Action<Vector3> CenterChanged;

        protected float X, Y, Z;
        protected float Dx, Dy, Dz;
        private LineRenderer _plot;
        private readonly List<Vector3> _points = new List<Vector3>();
        private Vector3 _currentPoint;

        protected virtual void Awake()
        {
            _plot = GetComponent<LineRenderer>();
        }

        protected virtual void Start()
        {
            StartCoroutine(Draw());
        }

        protected void AddLine(Vector2 start, Vector2 end)
        {
            var arrow = new GameObject();
            var line = arrow.AddComponent<LineRenderer>();
            line.colorGradient = _lineData.GradientColor;
            line.materials = _lineData.Materials;
            line.textureMode = _lineData.TextureMode;
            line.endWidth = .05f;
            line.startWidth = .05f;
            line.positionCount = 2;
            line.SetPosition(0, new Vector3(start.x, 0, start.y));
            var ending = start + end;
            line.SetPosition(1, new Vector3(ending.x, 0, ending.y));
        }

        protected void AddPointToPlot(Vector3 point)
        {
            _currentPoint = point;
            _points.Add(point);

            ResizePlotArray();
        }

        protected void AddPointToPlot(float x, float y, float z = 0f)
        {
            _currentPoint.Set(x, y, z);
            _points.Add(_currentPoint);
            
            ResizePlotArray();
        }

        private void ResizePlotArray()
        {
            var positionCount = _plot.positionCount;
            positionCount += 1;
            _plot.positionCount = positionCount;
            _plot.SetPosition(positionCount - 1, _currentPoint);
            
            CenterChanged?.Invoke(_plot.bounds.center);
        }

        private IEnumerator Draw()
        {
            while (Input.GetKeyDown(_stopPlottingKey) == false)
            {
                yield return PlotPortrait();
            }
        }

        protected abstract IEnumerator PlotPortrait();
    }
}