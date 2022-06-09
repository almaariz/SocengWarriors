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
            locDetail();
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

    void locDetail()
    {
        if (gameObject.name == "IntroScene")
            {
                GameController.Instance.locintro.SetActive(true);
                GameController.Instance.lochall.SetActive(false);
                GameController.Instance.loc1.SetActive(false);
                GameController.Instance.loc2.SetActive(false);
                GameController.Instance.loc3.SetActive(false);
                GameController.Instance.loc4.SetActive(false);
                GameController.Instance.loc5.SetActive(false);
                GameController.Instance.loc6.SetActive(false);
                GameController.Instance.loc7.SetActive(false);
            }
            else if(gameObject.name == "HallScene")
            {
                GameController.Instance.locintro.SetActive(false);
                GameController.Instance.lochall.SetActive(true);
                GameController.Instance.loc1.SetActive(false);
                GameController.Instance.loc2.SetActive(false);
                GameController.Instance.loc3.SetActive(false);
                GameController.Instance.loc4.SetActive(false);
                GameController.Instance.loc5.SetActive(false);
                GameController.Instance.loc6.SetActive(false);
                GameController.Instance.loc7.SetActive(false);
            }
            else if(gameObject.name == "PhisingPlaza")
            {
                GameController.Instance.locintro.SetActive(false);
                GameController.Instance.lochall.SetActive(false);
                GameController.Instance.loc1.SetActive(true);
                GameController.Instance.loc2.SetActive(false);
                GameController.Instance.loc3.SetActive(false);
                GameController.Instance.loc4.SetActive(false);
                GameController.Instance.loc5.SetActive(false);
                GameController.Instance.loc6.SetActive(false);
                GameController.Instance.loc7.SetActive(false);
            }
            else if(gameObject.name == "SSurfingPlaza")
            {
                GameController.Instance.locintro.SetActive(false);
                GameController.Instance.lochall.SetActive(false);
                GameController.Instance.loc1.SetActive(false);
                GameController.Instance.loc2.SetActive(true);
                GameController.Instance.loc3.SetActive(false);
                GameController.Instance.loc4.SetActive(false);
                GameController.Instance.loc5.SetActive(false);
                GameController.Instance.loc6.SetActive(false);
                GameController.Instance.loc7.SetActive(false);
            }
            else if(gameObject.name == "DumpsterDPlaza")
            {
                GameController.Instance.locintro.SetActive(false);
                GameController.Instance.lochall.SetActive(false);
                GameController.Instance.loc1.SetActive(false);
                GameController.Instance.loc2.SetActive(false);
                GameController.Instance.loc3.SetActive(true);
                GameController.Instance.loc4.SetActive(false);
                GameController.Instance.loc5.SetActive(false);
                GameController.Instance.loc6.SetActive(false);
                GameController.Instance.loc7.SetActive(false);
            }
            else if(gameObject.name == "DTheftPlaza")
            {
                GameController.Instance.locintro.SetActive(false);
                GameController.Instance.lochall.SetActive(false);
                GameController.Instance.loc1.SetActive(false);
                GameController.Instance.loc2.SetActive(false);
                GameController.Instance.loc3.SetActive(false);
                GameController.Instance.loc4.SetActive(true);
                GameController.Instance.loc5.SetActive(false);
                GameController.Instance.loc6.SetActive(false);
                GameController.Instance.loc7.SetActive(false);
            }
            else if(gameObject.name == "QuidProQuoPlaza")
            {
                GameController.Instance.locintro.SetActive(false);
                GameController.Instance.lochall.SetActive(false);
                GameController.Instance.loc1.SetActive(false);
                GameController.Instance.loc2.SetActive(false);
                GameController.Instance.loc3.SetActive(false);
                GameController.Instance.loc4.SetActive(false);
                GameController.Instance.loc5.SetActive(true);
                GameController.Instance.loc6.SetActive(false);
                GameController.Instance.loc7.SetActive(false);
            }
            else if(gameObject.name == "PretextingPlaza")
                {
                GameController.Instance.locintro.SetActive(false);
                GameController.Instance.lochall.SetActive(false);
                GameController.Instance.loc1.SetActive(false);
                GameController.Instance.loc2.SetActive(false);
                GameController.Instance.loc3.SetActive(false);
                GameController.Instance.loc4.SetActive(false);
                GameController.Instance.loc5.SetActive(false);
                GameController.Instance.loc6.SetActive(true);
                GameController.Instance.loc7.SetActive(false);
            }
            else if(gameObject.name == "TailgatingPlaza")
                {
                GameController.Instance.locintro.SetActive(false);
                GameController.Instance.lochall.SetActive(false);
                GameController.Instance.loc1.SetActive(false);
                GameController.Instance.loc2.SetActive(false);
                GameController.Instance.loc3.SetActive(false);
                GameController.Instance.loc4.SetActive(false);
                GameController.Instance.loc5.SetActive(false);
                GameController.Instance.loc6.SetActive(false);
                GameController.Instance.loc7.SetActive(true);
            }
    }

    // List<SavableEntity> GetSavableEntities()
    // {
    //     var currScene = SceneManager.GetSceneByName(gameObject.name);
    //     var savableEntities = FindObjectOfType<SavableEntity>().Where(x => x.gameObject.scene == currScene).ToList();
    // }
}
