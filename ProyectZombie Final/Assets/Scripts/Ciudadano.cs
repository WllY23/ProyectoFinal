using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Enemies;

namespace NPC //namespace gloval 
{
	namespace Ally // namespace del ally (ciudadano)
	{
		public sealed class Ciudadano : NPCS //la clase ciudadano ereda de la clace NPCS
		{
			public datosciudadano _citi; // llamando la estructura de los ciudadanos 

			public override void Start () 
			{
				base.Start();
				_citi.name = (nomciu)Random.Range(1, 20); //random para los nombres de los ciudadanos 
				age = Random.Range(15, 101);// ramdom para las edades de los cuudadanos 
			}
			// metodo override de iniciar los estados.
			public override void Iniciarest ()
			{
				base.Iniciarest ();
			}
			//este metodo buscar en un vector me guarda todos los gamaobjects y en un foreach me busca los game object que tengan el 
			//componente de zombie
			void buscar()
			{
				GameObject[] AllGameObjects = GameObject.FindObjectsOfType (typeof(GameObject)) as GameObject[];
				foreach (GameObject go in AllGameObjects)
				{
					if(go.GetComponent<Zombie>()) 
					{
						float disten = Vector3.Distance(go.transform.position, transform.position); //al disten le doy la posicion
						if (disten <= 15f)  //aqui le digo que si target es menor o igual a 15 float target sea igual a go
						{
							target = go; // a target le doy el valor del go
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
						transform.position += Vector3.Normalize (target.transform.position + transform.position) * Speed * Time.deltaTime;
					}
				}
			}

			// esta es la comvercion explicita de ciudadano a zombie 
			public static implicit operator Zombie(Ciudadano citi) 
			{
				Zombie z = citi.gameObject.AddComponent<Zombie>(); //agregandocomponente de zombie
				z.tag="Zombie";//agregando tag de zombie
			    z.age = citi.age;//la edad del zombie combertido quedara igual a cuando era ciudadano
				z.Speed = citi.Speed;//la velocidad del zombie combertido quedara igual a cuando era ciudadano
				Destroy(citi);//destruye el ciudadano 
				Manager.Instance.ModificarNumeroDeNPCs (-1, 1, 0);//en el cambas disminuyendo un ciudadano y aumentando un zombie 
			    return z;//retorna un zombie
			 }
		}
	}
}
