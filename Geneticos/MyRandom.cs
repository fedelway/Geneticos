/*
 * Created by SharpDevelop.
 * User: lelouch
 * Date: 7/10/2018
 * Time: 18:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Geneticos
{
	/// <summary>
	/// Description of MyRandom.
	/// </summary>
	public static class MyRandom
	{
		static Random instance = new Random();
		public static int Next(){
			return instance.Next();
		}
	}
}
