using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void MoveToScene1()
    {
        SceneManager.LoadScene("Sofie");
    }

    public void MoveToScene2()
    {
        SceneManager.LoadScene("2D map move");
    }
}