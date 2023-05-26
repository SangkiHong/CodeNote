using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float lifeTime;
        [SerializeField] private float speed;

        private Transform _transform;
        // Start is called before the first frame update
        void Start()
        {
            _transform = this.transform;
            Destroy(gameObject, lifeTime);
        }

        // Update is called once per frame
        void Update()
        {
            _transform.position = Vector3.MoveTowards(_transform.position, _transform.forward * 100, Time.deltaTime * speed);
        }
    }
}
