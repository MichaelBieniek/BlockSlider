using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PincherController : MonoBehaviour {

    [SerializeField] Transform right;
    [SerializeField] Transform left;
    Rigidbody _right;
    Rigidbody _left;

    bool _closing = false;
    float _speed;

    float _switch = 3f;

	// Use this for initialization
	void Start () {
        _right = right.GetComponent<Rigidbody>();
        _left = left.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        if( _switch > 0 )
        {
            _switch -= Time.deltaTime;
        } else
        {
            _closing = !_closing;
            if( _closing )
            {
                _switch = 0.5f;
            } else
            {
                _switch = 3f;
            }
        }

        if (_closing)
        {
            _speed = -10.0f;
        } else
        {
            _speed = 1f;
        }
        _right.velocity = Vector3.right * _speed;;
        _left.velocity = Vector3.left * _speed;

        
        
    }
}
