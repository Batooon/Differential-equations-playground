using Attractors;
using UnityEngine;

namespace Utils
{
    public class Entry : MonoBehaviour
    {
        [SerializeField] private PhasePortrait _portrait;
        [SerializeField] private CameraMovement _cameraMovement;

        private void Awake()
        {
            _cameraMovement.Init(_portrait);
        }
    }
}