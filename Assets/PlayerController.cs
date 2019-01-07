using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

	[SerializeField] float playerStrafeSpeed = 5f;
	[SerializeField] int bounds = 8;

	[SerializeField] GameObject fork;
	[SerializeField] GameObject slammer;
	[SerializeField] GameObject pincher;
	[SerializeField] GameObject ramp;

	[SerializeField] GameObject puSpeedBoost;

	[SerializeField] Transform slider;

	[SerializeField] Text scoreText;
	private int score = 0;
	private Vector3 baseForce = new Vector3(8, 0, 8);
	private float cooldown = 0;

	// Use this for initialization
	void Start()
	{
		ProceduralSpawn();
	}

	void Offset(Vector3 orig, Vector3 dest)
	{

	}

	// Update is called once per frame
	void FixedUpdate()
	{

		score += (int)(gameObject.GetComponent<Rigidbody>().velocity.magnitude * 0.5f);
		scoreText.text = "" + score;
		if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < bounds)
		{
			transform.Translate(Vector3.right * Time.deltaTime * playerStrafeSpeed);
		}
		if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -bounds)
		{
			transform.Translate(Vector3.left * Time.deltaTime * playerStrafeSpeed);
		}
		if (Input.GetKey(KeyCode.UpArrow))
		{
			gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 6f);
		}

		if (cooldown > 0)
		{
			cooldown -= Time.deltaTime;
		}
		else
		{
			//gameObject.GetComponent<ConstantForce>().force = baseForce;
		}

		float velocity = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
		float r = (gameObject.GetComponent<Rigidbody>().velocity.magnitude / 20f);
		float b = 1 - (gameObject.GetComponent<Rigidbody>().velocity.magnitude / 20f);

		//gameObject.GetComponent<Rigidbody>().velocity = Vector3.forward * 3f;
		if (RenderSettings.skybox.HasProperty("_Tint"))
			RenderSettings.skybox.SetColor("_Tint", new Color(r, 0, b));
		else if (RenderSettings.skybox.HasProperty("_SkyTint"))
			RenderSettings.skybox.SetColor("_SkyTint", new Color(r, 0, b));
	}

	private void OnGUI()
	{
		GUILayout.Label("Velocity " + gameObject.GetComponent<Rigidbody>().velocity.magnitude);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "SpeedBoost")
		{
			Destroy(other.gameObject);
			cooldown = 4f;
			gameObject.GetComponent<ConstantForce>().force = new Vector3(0, 0, 8);
		}
	}

	void ProceduralSpawn()
	{
		int maxLength = 1000;
		int baseInc = 15;
		int inc;
		int selected = 0;
		int offset = 0;

		inc = baseInc;
		for (int i = baseInc; i < maxLength; i = i + inc)
		{
			selected = Random.Range(0, 5);
			offset = Random.Range(-bounds, bounds);
			GameObject go = null;
			switch (selected)
			{
				case 0:
					go = Instantiate(slammer, Vector3.forward * i + Vector3.left * offset + Vector3.up * 2.5f, Quaternion.identity);
					break;
				case 1:
					go = Instantiate(pincher, Vector3.forward * i, Quaternion.identity);
					break;
				case 2:
					go = Instantiate(ramp, Vector3.forward * i + Vector3.right * offset, Quaternion.identity);
					break;
				case 3:
					go = Instantiate(fork, Vector3.forward * i, Quaternion.identity);
					break;
				default:
					break;
			}
			if (go != null)
				go.GetComponent<Transform>().SetParent(slider);

		}

		for (int i = baseInc + (baseInc / 2); i < maxLength; i = i + inc * 2)
		{
			selected = Random.Range(0, 10);
			offset = Random.Range(-bounds, bounds);
			GameObject go = null;
			switch (selected)
			{
				case 0:
					go = Instantiate(puSpeedBoost, Vector3.forward * i + Vector3.right * offset, Quaternion.identity);
					break;
				case 1:
					go = Instantiate(puSpeedBoost, Vector3.forward * i + Vector3.right * offset, Quaternion.identity);
					break;
				default:
					break;
			}
			if (go != null)
				go.GetComponent<Transform>().SetParent(slider);
		}

		//slider.Rotate(1, 0, 0);

	}
}
