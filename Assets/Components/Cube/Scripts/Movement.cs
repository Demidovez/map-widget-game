using UnityEngine;

namespace MyNamespace
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _step = 100f;
        private Transform _transform;

        private void Start()
        {
            _transform = GetComponent<Transform>();
        }

        private void Update()
        {
            Vector3 direction = Vector3.zero;
            
            if (Input.GetKey(KeyCode.W))
            {
                direction += Vector3.forward;
            }
            
            if (Input.GetKey(KeyCode.S))
            {
                direction += Vector3.back;
            }
            
            if (Input.GetKey(KeyCode.A))
            {
                direction += Vector3.left;
            }
            
            if (Input.GetKey(KeyCode.D))
            {
                direction += Vector3.right;
            }

            if (direction != Vector3.zero)
            {
                _transform.position = Vector3.Lerp(_transform.position, _transform.position + direction.normalized * _step, Time.deltaTime * _speed);
            }
        }
    }
}
