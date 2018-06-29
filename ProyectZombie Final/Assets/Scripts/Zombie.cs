using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Ally;

namespace NPC //namespace global NPC
{
	namespace Enemies // namespace de la clase del zombie llamado enemy
	{
		public sealed class Zombie : Enemy //clase sellada que hereda de Enemy
		{
			public override void Start()
			{
				base.Start ();
				_zonbi.col = new Color[] { Color.cyan, Color.green, Color.magenta }; // vector de colores para zombie 
				int col = Random.Range (0, 3); // ramdon de colores de 0 a 3
				this.gameObject.GetComponent<Renderer> ().material.color = _zonbi.col [col]; // al game object le doy el color que se le dio al azar a col 
				_zonbi.part = (body)Random.Range(0, 5); // partes del cuerpo al azar
				age = Random.Range(15, 101); //variable de años con un random range de 15 a 100
			}
			// metodo de recivir daño que desactiva el game object y lo disminuye en 1
			public override void ReceiveDamage ()
			{
				Debug.Log ("aaaargh!");
				gameObject.SetActive (false);
				Manager.Instance.ModificarNumeroDeNPCs (0, -1, 0); //dismminuyo en uno en el cambas al zombie
			}
			//metodo override que me inicia los estados
			public override void Iniciarest ()
			{
				base.Iniciarest ();
			}
			//variable flotante de la distancia del heroe
			float disthero;
			//este metodo buscar en un vector me guarda todos los gamaobjects y en un foreach me busca todos los gameobject que tengan el 
			//componente de ciudadano y de heroe
			void buscar()
			{
				GameObject[] AllGameObjects = GameObject.FindObjectsOfType (typeof(GameObject)) as GameObject[]; //
				foreach (GameObject go in AllGameObjects)
				{
					if(go.GetComponent<Ciudadano>())
					{
						float disten = Vector3.Distance(go.transform.position, transform.position);

						if (disten <= 15f && disthero > 8f)
						{
							target = go;
						}
					}
					else if (go.GetComponent<Hero>())
					{
						float disten = Vector3.Distance(go.transform.position, transform.position);
						disthero = disten;
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
			// en este oncollision le estoy diciendo que cuando el zombie colisione con el ciudadano el ciudadano tome parametros del zombie 
			private void OnCollisionEnter(Collision collision)                            
            {
                if (collision.gameObject.GetComponent<Ciudadano>())
                {
                    Ciudadano c = collision.gameObject.GetComponent<Ciudadano>();
                    Zombie z = c;
					Debug.Log ("convertido Desde zombie " + "age " + z.age);
                }
            }
        }
	}
}