using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// esta clase abstracta me hereda de npc 
// y contiene un metodo que se llama ReceiveDamage "recivir daño" 
public abstract class Enemy : NPCS 
{
	public abstract void ReceiveDamage (); //metodo abstracto de recivir daño
}
