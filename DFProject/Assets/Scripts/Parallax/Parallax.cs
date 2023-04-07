using Cinemachine;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float _startPosition;

    [SerializeField]
    private CinemachineVirtualCamera _camera;
    [Range(0f, 1f)]
    [SerializeField]
    private float _parallaxEffect;

    private void Start()
    {
        _startPosition = _camera.transform.position.x;
    }

    private void LateUpdate()
    {
        float distance = _camera.transform.position.x * _parallaxEffect;
        transform.position = new Vector3(_startPosition + distance, transform.position.y, transform.position.z);
 
    }

}
