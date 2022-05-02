using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Attractors
{
    [RequireComponent(typeof(LineRenderer))]
    public class LorenzAttractor : MonoBehaviour, ICenterChange
    {
        [SerializeField] private float _rho = 28f;
        [SerializeField] private float _sigma = 10f;
        [SerializeField] private float _beta = 8f / 3f;
        [SerializeField, Range(.001f, .03f)] private float _dt;
        [SerializeField] private KeyCode _stopPlottingKey;

        public event Action<Vector3> CenterChanged;

        private float _x = .01f, _y, _z;
        private float _dx, _dy, _dz;
        private readonly List<Vector3> _points = new List<Vector3>();
        private LineRenderer _plot;
        private Vector3 _currentPoint;

        private void Awake()
        {
            _plot = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            StartCoroutine(Draw());
        }

        private IEnumerator Draw()
        {
            while (Input.GetKeyDown(_stopPlottingKey) == false)
            {
                _dx = (_sigma * (_y - _x)) * _dt;
                _dy = (_x * (_rho - _z) - _y) * _dt;
                _dz = (_x * _y - _beta * _z) * _dt;
            
                _x += _dx;
                _y += _dy;
                _z += _dz;
            
                _currentPoint.Set(_x, _y, _z);
                _points.Add(_currentPoint);

                var positionCount = _plot.positionCount;
                positionCount += 1;
                _plot.positionCount = positionCount;
                _plot.SetPosition(positionCount - 1, _currentPoint);

                CenterChanged?.Invoke(_plot.bounds.center);
            
                yield return null;
            }
        }

        
    }
}