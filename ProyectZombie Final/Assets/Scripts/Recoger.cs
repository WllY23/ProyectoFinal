using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Recoger : Hero
{
	// en este oncollicionenter estey diciendo que si coliciona con un gameobject tagiado como Life la vida aumentara en 30 
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Life") // si coliciona con un gameobject tagiado con Life
		{
			life += 30; // la vida es igual a 30
			vida.value = 30; // la vida en el canvas el valor aumenta en 30 
			Destroy (gameObject); // destruye el game object
			Debug.Log ("tengo 30 +"); //imprime que tiene 30 mas de vida
		}
	}
}
