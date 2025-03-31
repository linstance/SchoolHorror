using UnityEngine;
using UnityEngine.SceneManagement;
public class NextScene : MonoBehaviour
{

    public void GoingClassroom()
    {
        SceneManager.LoadScene("Classroom");
    }
   
}
