using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionController : MonoBehaviour
{
    private bool animationFinished = false;

    // Llamaremos a esta funci�n cuando la animaci�n haya terminado
    public void AnimationFinished()
    {
        animationFinished = true;
    }

    private void Update()
    {
        if (animationFinished)
        {
            ChangeScene();
        }
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("End");
    }
}
