using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Ally;   // llamando el namespace de aliado (ciudadano)
using NPC.Enemies; // llamando el namespace de  enemygo(zombie)
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
	datosciudadano _citi; // llamando la estructura del ciudadano
	Zonbiedates _zonbi; // llamando la estructura del zombie
	Manager m; //llamando al manager

	public float life = 100; //flotante de vida llamado life con un valor de 100
	public Slider vida;//slaider de vida del cambas 

	void Start () 
	{
		vida.value = life; //al valor de la vida la igualo a life
		m = FindObjectOfType<Manager> ();//llamando la clase Manager
		Camera.main.gameObject.AddComponent<FPSAim>(); // añade el script fpsaim a la main camara
	}

	void Update()
	{
		//aqui le digo que si la vida es menor o igual a 0 me llame la esena gameover
		if (life <= 0)
		{
			SceneManager.LoadScene("GameOver");// llamando la esena gameover
		}
	}

	// metodo de colision del zombie y el ciudadano con el heroe
	void OnCollisionEnter(Collision collision)
	{
		//  si que me dice que si el heroe choca con el zombie me imprima un mensaje
		if (collision.gameObject.tag == "Zombie")
		{
			m.Dialog.text = "Waaaarrr quiero comer " + collision.gameObject.GetComponent<Zombie>()._zonbi.part; // mensaje que se imprime en caso de collicion con el zombie
			life -= 30; //life disminuya 30 con la colision
			vida.value = life;
		}
		//  si que me dice que si el heroe choca con el ciudadano me imprima un mensaje
		else if (collision.gameObject.tag == "Ciudadano")
		{ 
			m.Dialog.text = "hola soy " + collision.gameObject.GetComponent<Ciudadano>()._citi.name + " y tengo " + collision.gameObject.GetComponent<Ciudadano>().age.ToString() + " años"; // mensaje que se imprime en caso de collicion con el ciudadano 
		}
		if (collision.gameObject.tag == "Life")
		{
			m.Dialog.text = "RECUPERE VIDA "; // mensaje que se imprime en caso de collicion con el zombie
			life += 30; //si coliciona con Life aumentara en 30
			vida.value = life;//al valor de la vida lo igualo a life
			collision.gameObject.SetActive(false);//desactivando el game object
			Vector3 posLife = new Vector3 (Random.Range(-20, 20), 0, Random.Range(-20, 20)); // un vector3 posicion que llame pos que coloca al azar las primitivas en la escena   
			collision.transform.position = posLife; // a collision le doy la posicion de pos life 
			collision.transform.gameObject.SetActive(true);//activando el game object
		}
		//aqui le estoy diciendo que si colicionan con un gameobject de tag vampire la vida del heroe disminuye en 60 
		if (collision.gameObject.tag == "Vampire")
		{
			m.Dialog.text = "Te chupare toda tu sangre "; // mensaje que se imprime en caso de collicion con el vampire
			life -= 60; // la vida del heroe disminuye en 60
			vida.value = life;
		}
	}
}
