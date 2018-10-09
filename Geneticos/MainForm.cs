/*
 * Created by SharpDevelop.
 * User: lelouch
 * Date: 7/10/2018
 * Time: 15:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.IO;

namespace Geneticos
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			var historico = new List<Individuo>();
			var poblacion = generarPoblacionInicial();
			
			for(int i=0;i<100000;i++)
			{
				var seleccion = seleccionTorneo(poblacion);
				
				var nuevaGeneracion = new List<Individuo>();
				for(int j=0;j<seleccion.Count;j++)
				{
					var r1 = MyRandom.Next() % seleccion.Count;
					var r2 = MyRandom.Next() % seleccion.Count;
					nuevaGeneracion.AddRange(seleccion[r1].cruzar(seleccion[r2]));
					//nuevaGeneracion.AddRange(seleccion[j].cruzar(seleccion[j+1]));
				}
				
				if(i%100 == 0)
					mutar(nuevaGeneracion);
				
				poblacion = nuevaGeneracion;
				
				//historico.Add(poblacion.OrderByDescending(o => o.fitness()).First());
				historico.AddRange(poblacion);
			}
			
			var mejorIndividuo = historico.OrderByDescending(i => i.fitness()).First();
			var mejorFitness = mejorIndividuo.fitness();
			
			//MessageBox.Show("Mejor individuo: " + mejorIndividuo.cromosomaString() );
			
			writeCSV(historico);
			
			this.Close();
		}
		
		public List<Individuo> generarPoblacionInicial()
		{
			var list = new List<Individuo>();
			
			for (int i=0; i<20; i++){
				
				var cromosoma = new Individuo.Gen[24];
				
				for(int j=0; j<cromosoma.Length; j++){
					cromosoma[j] = Individuo.RandomGen();
				}
			
				list.Add(new Individuo(cromosoma));
			}
			
			return list;
		}
		
		List<Individuo> seleccionTorneo(List<Individuo> poblacion)
		{
			var nuevaPoblacion = new List<Individuo>();
			for(int i=0;i<poblacion.Count;i=i+2)
			{
				if(poblacion[i].fitness() > poblacion[i+1].fitness()){
					nuevaPoblacion.Add(poblacion[i]);
				}else nuevaPoblacion.Add(poblacion[i+1]);
			}
			
			return nuevaPoblacion;
		}
		
		void mutar(List<Individuo> poblacion)
		{
			var random = new Random();
			var r = random.Next() % poblacion.Count;
			
			poblacion[r].mutar();
		}
		
		void writeCSV(List<Individuo> historico)
		{
			var sb = new StringBuilder();
			
			sb.Append("Fitness,CantTrabajo,CantEstudio,CantOcio,CantAlimentacion,CantDormir");
			sb.Append(Environment.NewLine);
			
			foreach(var i in historico){
				sb.Append(i.getCSV());
				sb.Append(Environment.NewLine);
			}
			
			File.WriteAllText("E:\\geneticos.csv",sb.ToString());
		}
	}
}
