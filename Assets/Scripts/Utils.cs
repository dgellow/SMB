using System;
using System.Collections;
using System.Collections.Generic;

public static class Utils {

	private static readonly Random rng = new Random();

	/// <summary>
	/// Randomize the specified list with side effect.
	/// </summary>
	/// <param name="list">List.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static void Randomize<T>(this IList<T> list) {
		int n = list.Count;  
		while (n > 1) {  
			n--;  
			int k = rng.Next(n + 1);  
			T value = list[k];  
			list[k] = list[n];  
			list[n] = value;  
		}  
	}

	public static T GetRandomValue<T>(this IList<T> list) {
		var index = rng.Next (0, list.Count);
		return list [index];
	}
}
