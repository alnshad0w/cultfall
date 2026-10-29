using UnityEngine;

public class TransitionerBehavior : MonoBehaviour
{
    [SerializeField] private string objectName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var sceneManager = GameObject.Find("GameManager").GetComponent<SceneController>();
            if (sceneManager != null)
            {
                switch (gameObject.tag.ToString())
                {
                    case "Note":
                        sceneManager.ActivatePanel(FindChildObject(objectName));
                        break;
                    case "Pentagram":
                        FindChildObject(objectName).gameObject.SetActive(true);
                        break;
                }
            }
        }
    }

    private GameObject FindChildObject(string note)
    {
        var canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            foreach (Transform child in canvas.transform)
            {
                if (child.name == note)
                {
                    return child.gameObject;
                }
            }
        }
        return null;
    }
}
