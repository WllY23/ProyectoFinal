using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Vampire : Enemy //clase sellada que hereda de Enemy
{
	public Color cols; // variable de color para el vampire
	public override void Start()
	{
		base.Start ();
		cols = Color.black; // dandole al vampiro el color negro
		this.gameObject.GetComponent<Renderer> ().material.color = cols;//accediendo al renderer del vampiro eigualandolo al cols"color"
		age = Random.Range(150, 300);// edad al azar del vampiro de 150 a 300
		FindObjectOfType<Manager> ().GetComponent<Manager> ();
	}
	//metodo override de iniciar estados
	public override void Iniciarest ()
	{
		base.Iniciarest ();
	}
	//metodo override de recivirdaño donde disminuyo al vampiro
	public override void ReceiveDamage ()
	{
		Debug.Log ("aaaargh!");
		gameObject.SetActive (false);
		Manager.Instance.ModificarNumeroDeNPCs (0, 0, -1); //disminuyo en uno a los vampiros en el cambas
	}
	//este metodo buscar en un vector me guarda todos los gamaobjects y en un foreach me busca los game object que tengan el 
	//componente de zombie
	void buscar()
	{
		GameObject[] AllGameObjects = GameObject.FindObjectsOfType (typeof(GameObject)) as GameObject[];
		foreach (GameObject go in AllGameObjects)
		{
			if (go.GetComponent<Hero>())
			{
				float disten = Vector3.Distance(go.transform.position, transform.position);
				if (disten <= 15f)
				{
					target = go;
				}
			}
		}
	}

	void Update()
	{
		Activarestados ();//activa los estados 
		buscar ();//llamando el metodo de busqueda
		// y le digo que al que el target se alege de la pocicion del zombie mas cercano 
		if (target) 
		{
			Vector3 myVector = target.transform.position - transform.position;

			float distanceToPlayer = myVector.magnitude;

			if (distanceToPlayer < UnidaDist) 
			{
				transform.position += Vector3.Normalize (target.transform.position - transform.position) * Speed * Time.deltaTime;
			}
		}
	}
}

