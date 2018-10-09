/*
 * Created by SharpDevelop.
 * User: lelouch
 * Date: 7/10/2018
 * Time: 15:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geneticos
{
	/// <summary>
	/// Description of Individuo.
	/// </summary>
	public class Individuo
	{
		public enum Gen{
			TRABAJO,
			ESTUDIO,
			OCIO,
			ALIMENTACION,
			DORMIR
		}
		
		static Random random = new Random();
		public static Gen RandomGen()
		{
			//var random = new Random();
			var r = random.Next() % 5;
			
			if(r==0)
				return Individuo.Gen.ALIMENTACION;
			if(r==1)
				return Individuo.Gen.DORMIR;
			if(r==2)
				return Individuo.Gen.ESTUDIO;
			if(r==3)
				return Individuo.Gen.OCIO;
			
			return Individuo.Gen.TRABAJO;
		}
		
		Gen[] cromosoma;
		
		public Individuo(Gen[] cromosoma){
			this.cromosoma = cromosoma;
		}
		
		public int fitness()
		{
			int retValue = 100;
			
			int cantTrabajo 		= cromosoma.Count(g=>g==Gen.TRABAJO);
			int cantEstudio			= cromosoma.Count(g=>g==Gen.ESTUDIO);
			int cantOcio			= cromosoma.Count(g=>g==Gen.OCIO);
			int cantAlimentacion    = cromosoma.Count(g=>g==Gen.ALIMENTACION);
			int cantDormir          = cromosoma.Count(g=>g==Gen.DORMIR);
			
			if(cantDormir < 7 || cantDormir > 10){
				retValue -= 30;
			}
			
			if(cantEstudio < 4){
				retValue -= 10;
			}
			
			if(cantOcio < 2 || cantOcio > 5){
				retValue -= 20;
			}
			
			if(cantAlimentacion < 1 || cantAlimentacion > 3)
				retValue -= 15;
			
			if(cantTrabajo < 8)
				retValue -= 10;
			
			return retValue;
		}
		
		public string cromosomaString()
		{
			int cantTrabajo 		= cromosoma.Count(g=>g==Gen.TRABAJO);
			int cantEstudio			= cromosoma.Count(g=>g==Gen.ESTUDIO);
			int cantOcio			= cromosoma.Count(g=>g==Gen.OCIO);
			int cantAlimentacion    = cromosoma.Count(g=>g==Gen.ALIMENTACION);
			int cantDormir          = cromosoma.Count(g=>g==Gen.DORMIR);
			
			return "Cantidad Trabajo: " + cantTrabajo.ToString() + Environment.NewLine +
				"Cantidad Estudio: " 	+ cantEstudio.ToString() + Environment.NewLine +
				"Cantidad Ocio: " 		+ cantOcio.ToString() + Environment.NewLine +
				"Cantidad Alimentacion: " + cantAlimentacion.ToString() + Environment.NewLine +
				"Cantidad Dormir: " 	+ cantDormir.ToString();
		}
		
		public string getCSV()
		{
			string cantTrabajo 		= cromosoma.Count(g=>g==Gen.TRABAJO).ToString();
			string cantEstudio			= cromosoma.Count(g=>g==Gen.ESTUDIO).ToString();
			string cantOcio			= cromosoma.Count(g=>g==Gen.OCIO).ToString();
			string cantAlimentacion    = cromosoma.Count(g=>g==Gen.ALIMENTACION).ToString();
			string cantDormir          = cromosoma.Count(g=>g==Gen.DORMIR).ToString();
			
			return fitness().ToString()+','+cantTrabajo + ',' + cantEstudio +','+cantOcio+','+cantAlimentacion+','+cantDormir;
		}
		
		public void mutar(){
			//var random = new Random();
			var r = random.Next() % cromosoma.Length;
			
			this.cromosoma[r] = RandomGen();
		}
		
		public List<Individuo> cruzar(Individuo otro){
			
			var genesHijo1 = new List<Gen>();
			var genesHijo2 = new List<Gen>();
			
			genesHijo1.AddRange(this.cromosoma.Take(12));
			genesHijo1.AddRange(otro.cromosoma.Skip(12).Take(12));
			
			genesHijo2.AddRange(otro.cromosoma.Take(12));
			genesHijo2.AddRange(this.cromosoma.Skip(12).Take(12));
			
			return new List<Individuo>(){new Individuo(genesHijo1.ToArray()),new Individuo(genesHijo2.ToArray())};
		}
	}
}
