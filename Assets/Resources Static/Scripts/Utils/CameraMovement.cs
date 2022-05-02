using System;
using UnityEngine;

namespace Utils
{
    public interface ICenterChange
    {
        event Action<Vector3> CenterChanged;
    }

    public static class Axes
    {
        public const int LeftMouseButton = 0;
        public const string VerticalRotation = "Mouse Y";
        public const string HorizontalRotation = "Mouse X";
        public const string ScrollWheel = "Mouse ScrollWheel";
    }

    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Camera _seizedCamera;
        [SerializeField] private Transform _furnitureCenterPoint;
        [SerializeField] private float _zoomSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField, Range(.1f, 4f)] private float _min3DZoom;
        [SerializeField, Range(5f, 90f)] private float _max3DZoom;
        [SerializeField] private float _zoomAmount = 1f;

        private ICenterChange _centerChanger;
        
        private Transform _transform;
    
        private float _zoomPosition;

        private Transform _cameraFollow;

        private Vector3 _rotateStartPosition;
        private Vector3 _rotateCurrentPosition;
    
        private Vector3 _2dMouseStartPosition;
        private Vector3 _2dMouseCurrentPosition;
    
        private Vector3 _new3DZoom;

        private Quaternion _newRotation;
    
        private bool _moving;
        private bool _changingState;

        private float _xRotation;
        private float _yRotation;

        public void Init(ICenterChange centerChange)
        {
            _centerChanger = centerChange;
            _transform = transform;

            var cameraFollowObject = new GameObject
            {
                transform =
                {
                    position = _furnitureCenterPoint.localPosition,
                    rotation = _furnitureCenterPoint.localRotation
                }
            };
            _cameraFollow = cameraFollowObject.transform;
            _transform.position = _cameraFollow.position;
            _new3DZoom = _seizedCamera.transform.localPosition;
        }

        private void OnEnable()
        {
            _centerChanger.CenterChanged += OnCenterChanged;
        }

        private void OnDisable()
        {
            _centerChanger.CenterChanged -= OnCenterChanged;
        }

        private void Update()
        {
            HandleZoom();
        }

        private Vector3 GetNewZoom(float zoom)
        {
            var lookAtVector = -_seizedCamera.transform.localPosition;
            var lookAtNormalized = lookAtVector.normalized;
        
            return lookAtNormalized * zoom;
        }

        private void HandleZoom()
        {
            var scroll = Input.GetAxis(Axes.ScrollWheel);
            if (scroll != 0)
            {
                var lookAtVector = -_seizedCamera.transform.localPosition;
                var lookAtNormalized = lookAtVector.normalized;
                var zoom = _zoomAmount * scroll;
                _new3DZoom += GetNewZoom(zoom);
                var magnitudeUnsigned = Mathf.Sqrt(_new3DZoom.sqrMagnitude);
                if (magnitudeUnsigned < _min3DZoom)
                {
                    _new3DZoom = lookAtNormalized * -_min3DZoom;
                }
                else if (magnitudeUnsigned > _max3DZoom)
                {
                    _new3DZoom = lookAtNormalized * -_max3DZoom;
                }
            }

            if (_seizedCamera.transform.localPosition.Equals(_new3DZoom))
                return;
            _seizedCamera.transform.localPosition = Vector3.Lerp(_seizedCamera.transform.localPosition, _new3DZoom,
                _zoomSpeed * Time.deltaTime);
        }
        
        private void HandleRotation()
        {
            if (Input.GetMouseButton(Axes.LeftMouseButton))
            {
                _xRotation = Input.GetAxis(Axes.HorizontalRotation) * _rotationSpeed;
                _yRotation = Input.GetAxis(Axes.VerticalRotation) * _rotationSpeed;

                // var rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
                // _newRotation = rotation;
                // _transform.rotation = _newRotation;

                _transform.Rotate(_xRotation, _yRotation, 0, Space.World);
            }
        }

        private void OnCenterChanged(Vector3 newCenter)
        {
            _transform.position = newCenter;
        }
    }
}