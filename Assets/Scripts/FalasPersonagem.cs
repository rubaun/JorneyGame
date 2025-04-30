using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FalasPersonagem : MonoBehaviour
{
    [SerializeField] private GameObject falaTexto;
    [SerializeField] private GameObject balaoFala;
    [Header("Falas do Personagem")]
    [Header("0 - Fala inicial | 1-3 - Defesa | 4-6 - Ataque")]
    [SerializeField] private List<string> falas = new List<string>();
    private Player personagem;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        personagem = GetComponent<Player>();
        StartCoroutine(FalaInicial());
    }

    private IEnumerator FalaInicial()
    {
        yield return new WaitForSeconds(5f);
        falaTexto.GetComponent<TextMeshProUGUI>().text = falas[0];
        balaoFala.GetComponent<Animator>().SetTrigger("FalaAgora");
        falaTexto.GetComponent<Animator>().SetTrigger("FalaAgora");
        yield return new WaitForSeconds(5f);
        balaoFala.GetComponent<Animator>().SetTrigger("CalaBoca");
        falaTexto.GetComponent<Animator>().SetTrigger("CalaBoca");
        falaTexto.GetComponent<TextMeshProUGUI>().text = "";
    }

    public void FalaDeAtaque()
    {
        StartCoroutine(FalaAtaque());
    }
    private IEnumerator FalaAtaque()
    {
        falaTexto.GetComponent<TextMeshProUGUI>().text = falas[Random.Range(4,6)];
        balaoFala.GetComponent<Animator>().SetTrigger("FalaAgora");
        falaTexto.GetComponent<Animator>().SetTrigger("FalaAgora");
        yield return new WaitForSeconds(3.5f);
        balaoFala.GetComponent<Animator>().SetTrigger("CalaBoca");
        falaTexto.GetComponent<Animator>().SetTrigger("CalaBoca");
        falaTexto.GetComponent<TextMeshProUGUI>().text = "";
    }

    public void FalaDeDefesa()
    {
        StartCoroutine(FalaDefesa());
    }

    private IEnumerator FalaDefesa()
    {
        falaTexto.GetComponent<TextMeshProUGUI>().text = falas[Random.Range(1, 3)];
        balaoFala.GetComponent<Animator>().SetTrigger("FalaAgora");
        falaTexto.GetComponent<Animator>().SetTrigger("FalaAgora");
        yield return new WaitForSeconds(3.5f);
        balaoFala.GetComponent<Animator>().SetTrigger("CalaBoca");
        falaTexto.GetComponent<Animator>().SetTrigger("CalaBoca");
        falaTexto.GetComponent<TextMeshProUGUI>().text = "";
    }
}
