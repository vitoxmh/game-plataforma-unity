using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arm : MonoBehaviour
{

	public float Speed;
	private Rigidbody2D rb;
	private Vector2 Direction;
	private GameObject player;
	private Transform playerTrans;
	public float TiempoDeVida;


	void Awake()
	{


		rb = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player");
		playerTrans = player.transform;

	}


	void Start()
	{

		
		if (playerTrans.localScale.x < 0)
		{

			rb.velocity = new Vector2(-Speed, rb.velocity.y);
			Debug.Log(-Speed);
			transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

		}
		else
		{

			rb.velocity = new Vector2(Speed, rb.velocity.y);
			transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			Debug.Log(Speed);

		}

	}





	void OnTriggerEnter2D(Collider2D collider)
	{

	}


	void Update()
	{
		Destroy(gameObject, TiempoDeVida);
	}



}
