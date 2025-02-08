using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene(1); // Carga la escena con el índice 1, el indice 1 es  el orden en el que pones las escenas en la build de unity, el menu principal es la 0, 
                                   //la siguiente es la 1,2,3.... lo he hecho asi porque ya decidiremos cual es la escena uno, de esta manera podemos cambiar el orden en todo momento
                                   //En el (on Click, teneis que buscar LoadScene() )
    }
}