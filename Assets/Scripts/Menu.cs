using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private static float tempo = 0.5f;
    [SerializeField] private AudioClip somBackgroundMenu;
    [SerializeField] private AudioClip somBotao;
    private SoundPlayer player;
    private WaitForSeconds tempoDeEspera = new WaitForSeconds(tempo);


    void Start()
    {
        player = GameObject.Find("Audio Source").GetComponent<SoundPlayer>();
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            if (somBackgroundMenu != null && player != null)
            {
                player.PlaySoundBackground(somBackgroundMenu);
            }
        }
    }

    public void CarregarCenaJogo(string nomeCena)
    {
        StartCoroutine(CarregarCena(nomeCena));
    }

    IEnumerator CarregarCena(string nomeCena)
    {
        if (player != null)
        {
            player.PlaySound(somBotao);
        }
        yield return tempoDeEspera;
        SceneManager.LoadScene(nomeCena);
    }

}
