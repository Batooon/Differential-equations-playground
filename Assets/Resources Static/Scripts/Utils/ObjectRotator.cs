using UnityEngine;

namespace Utils
{
    public class ObjectRotator : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Transform _camera;

        private float _x, _y;
        private Vector3 _right, _up, _direction;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (Input.GetMouseButton(Axes.LeftMouseButton))
            {
                _x = Input.GetAxis(Axes.HorizontalRotation) * _rotationSpeed;
                _y = Input.GetAxis(Axes.VerticalRotation) * _rotationSpeed;

                _direction = _transform.position - _camera.position;
                _right = Vector3.Cross(_camera.up, _direction);
                _up = Vector3.Cross(_direction, _right);
                var rotation = _transform.rotation;
                rotation = Quaternion.AngleAxis(-_x, _up) * rotation;
                rotation = Quaternion.AngleAxis(_y, _right) * rotation;
                _transform.rotation = rotation;
            }
        }
    }
}