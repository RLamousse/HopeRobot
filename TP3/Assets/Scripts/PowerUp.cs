using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public float rotationSpeed = 99.0f;


	void Update ()
	{
			transform.Rotate(new Vector3(0f,0f,1f) * Time.deltaTime * this.rotationSpeed);
	}

	public void SetRotationSpeed(float speed)
	{
		this.rotationSpeed = speed;
	}
}
