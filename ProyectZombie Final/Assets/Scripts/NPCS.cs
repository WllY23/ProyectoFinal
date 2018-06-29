using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Ally;
using NPC.Enemies;

public class NPCS : MonoBehaviour
{
	public Zonbiedates _zonbi; //llamando ala estructura del zombie
    public datosciudadano _city; // llamando la estructura del ciudadano

	public bool react; // boleano de reaccion 
	public bool stoprect = true; //boleano de stopreacion en verdadero

    public float Speed; //variable flotante para velocidad
	public int age; //variable entera para los años
	public float VelAge; // variable flotante para la velocidad dependiendo de los años 

	int rnd; //entero que llame rnd para el random del caso

	Estado estado; // enum estado

	public int rndrota; // variable entera para el rabdom de la rotacion

	int moving; //idle es el estado en movimiento del zombie 

	public GameObject target;
	Vector3 direction;// variable con alcance de clase
	public float UnidaDist = 5.0f; //variable flotante de unidades de distancia con un valor de 5.0 flotantes

	//coorutina movimiento
	public IEnumerator Movimiento ()
	{
		yield return new WaitForSeconds(3); // estoy diciendo que me espere 5 segundos para llamar el ramdom de los estados
		estado = (Estado) Random.Range(0,3); // llamando los estados 
		rnd = Random.Range(0, 4);// realizado el ramdom de los estados se llama este que es el ramdom de los movimientos 
		rndrota = Random.Range (0, 2); // random para la rotacion
		StartCoroutine (Movimiento ()); // iniciando la corrutina Movimiento zombie
	}

	public virtual void Start () 
	{
    }
		// metodo virtual de niciar los estados 
	public virtual void Iniciarest ()
	{
		StartCoroutine(Movimiento());// iniciando la corrutina movimiento
	}
		
       void Awake ()                                                                              
    {
		StartCoroutine(Movimiento()); // iniciando la corrutina movimiento
        age = Random.Range(15, 100); // edad al azar entre 15 y 100
		Speed = 100f / age ; // a la velocidad le doy un  valor de 100f y los divido por los años 
		VelAge = Speed / age; // aqui estoy diciendo que velocidad age sea igual a speed dividido años 
    }

	void Update () 
	{
		// esta es una condicion que cuando el booleano este verdadero activa el estado reacting y cuando este falso esta en el estado idle
		if (react)
		{
			estado = Estado.reacting; //estado de reaccion
			stoprect = false;
		}
		else if (!stoprect)
		{
			stoprect = true;
			estado = Estado.idle; // estado idle
		}
	}
		
	public void Activarestados()
	{
		//switch que me controla los estados idle y moving del zombie 
		switch (estado) 
		{
		case Estado.idle:
			break;
		case Estado.rotating: // estado rotacion rotal con un movimiento al azar
			Rotating (); // llamando el metodo de rotacion
			break;
		case  Estado.moving:
			Moving (); //llamando el metodo del movimiento
			break;
			//estado de reaccion
		case Estado.reacting:
			break;
		}
	}
		
	public void Rotating() // metodo de rotacion
	{
		//switch de rotacion a la izaquierda o a la derecha
		switch (rndrota) 
		{
			case 0:
			transform.Rotate (0, 1 * Speed, 0);
	       	break;
			case 1:
			transform.Rotate (0, -1 * Speed, 0);
	        break;
		};
	}

	//metodo de movimiento
	public void Moving()
	{
		switch (rnd) 
		{
		case 0:
			transform.position += transform.forward * Speed * Time.deltaTime; //avanzar hacia adelante 
			break;
		case 1:
			transform.position -= transform.forward * Speed * Time.deltaTime;	//avanzar hacia atras
			break;
		case 2:
			transform.position += transform.right * Speed * Time.deltaTime; // avanzar hacia la izquierda
			break;
		case 3:
			transform.position -= transform.right * Speed * Time.deltaTime; // avanzar hacia la derecha
			break;
		}
	}
}
