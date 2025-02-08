using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Para el jogo
        #else
            Application.Quit(); // Cierra la joguina ( Es la funcion que teneis que buscar para agregar en el (on Click) )
        #endif
    }
}

//Gracias chatgpt por hacerme un script sin tener que pensar ni esforzarme, te quiero baby