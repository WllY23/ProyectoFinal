using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Ally;   // llamando el namespace de aliado (ciudadano)
using NPC.Enemies; // llamando el namespace de  enemygo(zombie)
using UnityEngine.SceneManagement;
using UnityEngine.UI; //inportando la libreria de unity UI
//  NOTE: Copy and paste from Line 8 to line 38 in the class you need the singleton
//  and replace all the words "Singleton" with the name of the class
public class Manager : MonoBehaviour 
{
	// esto es un singleton para instanciar la clase manager 
	private static Manager instance = null;

	public static Manager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<Manager>(); 
			}
			return instance;
		}
	}
	Zonbiedates _zonbi; // llamando la estructura zombie 
	datosciudadano _citi; // llamando la estructura ciudadano 
	Person _pers; // llamando la estrcuctura Person

	public Text NZ; // texto para el canvas de numeros de zombies 
	public Text NC; // texto para el canvas de numeros de ciudadanos 
	public Text NV; // texto para el canvas de numeros de vampiros 

	public Text Dialog; // texto del cambas para los dialogos 

	public const int MAX = 25; // variable constante max para el maximo de cubos 

	public int NumZonbies; // contador de zombies
	public int NumCiudaanos; // contador de ciudadanos
	public int NumVampire;//contador de vampiros

	public GameObject [] npcss; // vector de game objects 
	// bloquear el cursor para que no se salga de la pantalla
	CursorLockMode wantedMode;
	void SetCursorState ()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = true;
	}

	void Start () 
	{
		SetCursorState (); //llamando el metodo de bloquear el cursos
		int P = Random.Range (5, 16); //velocidad con un valor aleatorio de 5 a 5 flotante
		_pers = new Person (P); // instancia de Person
		int rnd2 = Random.Range (_pers.Min, MAX); // random para el numero de cubos que apareceran en la escena
		npcss  = new GameObject[rnd2]; // un nuevo vector que esta recibiendo el rnd2 de los cubos 

		for (int i = 0; i < rnd2; i++)  // for que inicia en cero y me aumenta de a uno hasta la cantidad al azar que se hizo en el 
		{
			GameObject go = GameObject.CreatePrimitive (PrimitiveType.Cube);    // al gameobject lo llame go y le agrege la primitiva de cubo
			go.AddComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // agregandole el rigidbody a las primitivas y congelando su rotagion en x y z
			Vector3 pos = new Vector3 (Random.Range(-20, 20), 0, Random.Range(-20, 20)); // un vector3 posicion que llame pos que coloca al azar las primitivas en la escena   
			go.transform.position = pos;  //al gameobject go le estoy dando la posicion de pos 
			int caractr = Random.Range(1, 4);  // ramdom del switch caracter que coje un rango al azar y crea primitivas en tipo zombie 
			switch (caractr)                                                           
			{
			case 1: // si el caso es uno me crea un ciudadano                                                               
				go.name = "Ciudadano"; // game object tipo heroe
				go.AddComponent<Ciudadano> (); // agrego la clase ciudadano
				go.tag = "Ciudadano"; // al gameobject go de este caso le agrego el tag de ciudadano
				break;// detiene el siclo en caso de cumplirse 		
			case 2:  // si el caso es dos me crea un zombie                                                      
				go.name = "Zombie"; //game object tipo zombie
				go.AddComponent<Zombie> (); // agrego la clase zombie 
				go.tag = "Zombie"; // al gameobject go de este caso le agrego el tag de zombie
				break; // detiene el siclo en caso de cumplirse 
			case 3:
				go.name = "Vampire"; // game object tipo heroe
				go.AddComponent<Vampire> (); // agrego la clase ciudadano
				go.tag = "Vampire"; // al gameobject go de este caso le agrego el tag de ciudadano
				break;
			}

			npcss [i] = go; //guardando los cubos en un vector y lo igualo a go (los game obgects)
		}
		// aumento de los numeros de zombies y ciudadanos que aumentan si tienen tag de zombie y ciudadano
		foreach (GameObject pry in npcss)
		{
			GameObject go = pry as GameObject;

			if (go.tag == "Zombie") //si el go tiene el tag de zombie
			{
				NumZonbies += 1; // numero de zombies aumenta de a uno
			}
			if (go.tag == "Ciudadano") // si el go tiene tag de ciudadano
			{
				NumCiudaanos += 1; // numero de ciudadanos aumenta de a uno
			}
			if(go.tag == "Vampire")//si el go tiene tag de vampiro
			{
				NumVampire += 1; // numero de Vampire aumenta de a uno
			}
		}
		NZ.text = NumZonbies.ToString (); //convercion de entero a string contador de los  zombies
		NC.text = NumCiudaanos.ToString (); // convercion de entero a string contador de los ciudadanos 
		NV.text = NumVampire.ToString(); // convercion de entero a string contador de los vampiros 
	}

	void Update()
	{
		// un entero de suma para sumar el numero de vampiros y el numero de zombies 
		int suma = NumVampire + NumZonbies;
	
		if (suma <= 0) //aqui le estoy diciendo que si suma es menor o igual a 0 llama la esena de ganaste 
		{
			SceneManager.LoadScene("Ganaste");
		}
	}
	//metodo de modificar los numeros del npsc  donde le digo que aumente un zombie,ciudadano y vampire
	public void ModificarNumeroDeNPCs (int c, int z, int v) // un metodo que contiene en un entero de c"ciudadano",z"zombie",v"vampiere"
	{
		NumCiudaanos += c; //numero de ciudadanos aumento en uno 
		NC.text = NumCiudaanos.ToString(); // el texto NC numero de ciudadanos es igual a numciudadanos convirtiendo el entero a string
		NumZonbies += z; //el numero de zombies aumenta en uno
		NZ.text = NumZonbies.ToString(); // el texto NZ numero de zombies es igual a numzombies convirtiendo el entero a string
		NumVampire += v; // el numero de vampiros aumenta en uno
		NV.text = NumVampire.ToString(); // el texto NV numero de ciudadanos es igual a numvampiros convirtiendo el entero a string
	}
}



