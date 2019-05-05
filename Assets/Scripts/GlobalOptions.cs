using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalOptions{

	public static int lives = 3;
	public static string message = "Probando probando...";
	public static bool stage1 = false;
	public static bool stage2 = false;
	public enum Language {
		English, Español
	}
	public static Language language = Language.English;


}
