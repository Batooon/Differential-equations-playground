using Attractors;
using UnityEngine;

namespace Utils
{
    public class Entry : MonoBehaviour
    {
        [SerializeField] private LorenzAttractor _attractor;
        [SerializeField] private CameraMovement _cameraMovement;

        private void Awake()
        {
            _cameraMovement.Init(_attractor);
        }
    }
}