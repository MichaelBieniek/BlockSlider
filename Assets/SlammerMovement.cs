using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SlammerMovement : MonoBehaviour
{

	[SerializeField] float offset = 2f;
	Vector3 direction = Vector3.up;
	float speed = 1f;

	[SerializeField] float max = 5;
	[SerializeField] float min = 1.5f;

	// Use this for initialization
	void Start()
	{
		_audio = GetComponent<AudioSource>();
		int _offset = (int)Math.Round(UnityEngine.Random.Range(-offset, offset));
		transform.Translate(Vector3.right * offset);
	}

	AudioSource _audio;
	// Update is called once per frame
	void Update()
	{

		if (direction == Vector3.down)
		{
			speed = 10.0f;
		}

		else if (direction == Vector3.up)
		{
			speed = 1.0f;
		}

		// move object locally in direction
		transform.Translate(direction * speed * Time.deltaTime, Space.Self);

		if (transform.localPosition.y >= max)
		{
			direction = Vector3.down;
		}

		if (transform.localPosition.y <= min)
		{
			direction = Vector3.up;
			_audio.Play();
		}
	}
}
