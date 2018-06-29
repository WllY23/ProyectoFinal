using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
	public GameObject Cube; // variable  gameobgect cubo
	public float limite; // un flotante de limite
	public Vector3 puntosalida; // un tipo vector3 de punto de salida  

	void Update ()
	{
		if (Cube.transform.position.y < limite) //aqui le digo que si la posicion en y del cubo es menor al limite 
		{
			Cube.transform.position = puntosalida;	 //la posicion del cubo sera igual a punto de salida 
		}
	}
}
