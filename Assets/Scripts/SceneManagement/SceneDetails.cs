using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDetails : MonoBehaviour
{
    [SerializeField] List<SceneDetails> connectedScenes;
    [SerializeField] AudioClip sceneMusic;
    public bool IsLoaded { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            LoadScene(); 
            GameController.Instance.SetCurrentScene(this);

            if (sceneMusic != null)
                AudioManager.i.PlayMusic(sceneMusic, fade: true);

            foreach (var scene in connectedScenes)
            {
                scene.LoadScene();
            }

            if (GameController.Instance.PrevScene != null)
            {
                var prevLoadedScenes = GameController.Instance.PrevScene.connectedScenes;
                foreach (var scene in prevLoadedScenes)
                {
                    if (!connectedScenes.Contains(scene) && scene != this)
                        scene.UnloadScene();
                }
            }
        }
    }

    public void LoadScene()
    {
        if (!IsLoaded){
            SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            IsLoaded = true;
        }
    }

    public void UnloadScene()
    {
        if (IsLoaded){
            SceneManager.UnloadSceneAsync(gameObject.name);
            IsLoaded = false;
        }
    }

    // List<SavableEntity> GetSavableEntities()
    // {
    //     var currScene = SceneManager.GetSceneByName(gameObject.name);
    //     var savableEntities = FindObjectOfType<SavableEntity>().Where(x => x.gameObject.scene == currScene).ToList();
    // }
}
