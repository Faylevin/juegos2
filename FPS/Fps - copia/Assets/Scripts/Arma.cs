using UnityEngine;

public class Arma 
{
    public float alcance;
	public float frecuenciaDisparo;
	public int danoCausado;
	public int capacidad;
	
	public Arma(float a, float f, int d, int c){
		this.alcance = a;
		this.frecuenciaDisparo = f;
		this.danoCausado = d;
		this.capacidad = c;
	}
}
