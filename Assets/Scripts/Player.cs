using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private string nomePersonagem;
    [SerializeField] private int vida;
    [SerializeField] private int ataque;
    [SerializeField] private int defesa;
    [SerializeField] private int especial;
    [SerializeField] private bool estahVivo = true;
    [SerializeField] private DiretorBatalha dB;
    [SerializeField] private Sprite spriteDerrota;
    [SerializeField] private bool ehHeroi;
    [SerializeField] private GameObject pDesefa;
    [SerializeField] private GameObject pSangrar;
    [SerializeField] private AudioClip[] somAtaque;
    [SerializeField] private AudioClip[] somDefesa;
    [SerializeField] private AudioClip[] somEspecial;
    [SerializeField] private AudioClip[] somErroAtaque;
    [SerializeField] private AudioClip[] somDano;
    [SerializeField] private AudioClip somVitoria;
    [SerializeField] private AudioClip somMorte;
    [SerializeField] private AudioClip somEspecialPronto;
    [SerializeField] private Camera camera;

    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private SoundPlayer audioSource;
    private FalasPersonagem falasPersonagem;

    private void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundPlayer>();

        if (!falasPersonagem && !ehHeroi)
        {
            falasPersonagem = GetComponent<FalasPersonagem>();
        }
    }

    public void FalaDoPersonagem(string tipoDeFala)
    {
        if(falasPersonagem != null && !ehHeroi && tipoDeFala == "Ataque")
        {
            falasPersonagem.FalaDeAtaque();
        }
        else if (falasPersonagem != null && !ehHeroi && tipoDeFala == "Defesa")
        {
            falasPersonagem.FalaDeDefesa();
        }
    }

    public string GetNomePersonagem()
    {
        return nomePersonagem;
    }

    public int GetVida()
    {
        return vida;
    }

    public bool VerificaVida() //retorna true se o jogador esta vivo e false se esta morto
    {
        return estahVivo;
    }

    public bool VerificaEspecial() //retorna true se o jogador tem especial e false se nao tem
    {
        if (especial >= 3)
        {
            dB.RecebeTexto($"{nomePersonagem} especial pronto!");
            audioSource.PlaySound(somEspecialPronto);
            return true;
        }
        else
        {
            return false;
        }
    }

    public int ValorEspecial()
    {
        return especial;
    }

    public int Ataque()
    {
        int valorAtaque = Random.Range(0,ataque);

        especial++;

        AnimaAtaque();

        if (valorAtaque > 0)
        {
            FalaDoPersonagem("Ataque");
            dB.RecebeTexto($"{nomePersonagem} ataca com {valorAtaque}");
            PlaySomAtaque();
        }
        else
        {
            dB.RecebeTexto($"{nomePersonagem} erra o ataque.");
            PlaySomErroAtaque();
        }


        return valorAtaque;
    }

    public int Defesa()
    {
        int valorDefesa = Random.Range(0, defesa);

        if(valorDefesa > 0)
        {
            FalaDoPersonagem("Defesa");
            dB.RecebeTexto($"{nomePersonagem} defende com {valorDefesa}");
        }
        else
        {
            dB.RecebeTexto($"{nomePersonagem} nao consegue defender.");
        }
        

        return valorDefesa;
    }

    public int Especial()
    {
        int valorEspecial = Random.Range(20, ataque);
        int chanceDeDobrar = Random.Range(0, 100);
        int fatorMultiplicador = especial;

        AnimaAtaque();

        if (chanceDeDobrar >= 90 && especial >= 3)
        {
            int valorEspecialDobrado = (valorEspecial * 2) + fatorMultiplicador;
            FalaDoPersonagem("Ataque");
            dB.RecebeTexto($"{nomePersonagem} ataca com {valorEspecialDobrado}");
            PlaySomEspecial();
            especial = 0;
            return valorEspecialDobrado;
        }
        else if (chanceDeDobrar < 90 && especial >= 3)
        {
            FalaDoPersonagem("Ataque");
            dB.RecebeTexto($"{nomePersonagem} ataca com {valorEspecial}");
            PlaySomAtaque();
            especial = 0;
            return valorEspecial;
        }
        else
        {
            dB.RecebeTexto("Seu especial nao esta carregado!");
            return 0;
        }
    }

    public void LevarDano(int dano)
    {
        int danoFinal = dano - Defesa();

        if (danoFinal <= 0)
        {
            StartCoroutine(TocarDefesa());
        }
        else if (danoFinal <= 25)
        {
            StartCoroutine(TocarDanoNormal(danoFinal));
        }
        else
        {
            StartCoroutine(TocarDanoMaximo(danoFinal));
        }

        if (estahVivo)
        {
            Debug.Log("");
            Debug.Log($"{nomePersonagem}, vida: {vida}");
        }
        else
        {
            dB.RecebeTexto($"{nomePersonagem}, morreu!");
        }

    }
    private void DefineVida() //Verifica o valor da vida e define como morto
    {
        if (vida <= 0)
        {
            spriteRenderer.sprite = spriteDerrota;
            vida = 0;
            estahVivo = false; //Ta morto
        }
    }

    private void AnimaAtaque()
    {
        if (ehHeroi)
        {
            anim.SetTrigger("AtaqueInimigo");
        }
        else
        {
            anim.SetTrigger("AtaqueHeroi");
        }
    }

    private void PlaySomDano()
    {
        int som = Random.Range(0, somDano.Length);
        audioSource.PlaySound(somDano[som]);
    }

    private void ParticulaDefesa()
    {
        pDesefa.GetComponent<ParticleSystem>().Play();
    }

    private void ParticulaSangrar()
    {
        pSangrar.GetComponent<ParticleSystem>().Play();
    }

    private void PlaySomAtaque()
    {
        int som = Random.Range(0, somAtaque.Length);
        audioSource.PlaySound(somAtaque[som]);
    }
    private void PlaySomErroAtaque()
    {
        int som = Random.Range(0, somErroAtaque.Length);
        audioSource.PlaySound(somErroAtaque[som]);
    }

    private void PlaySomDefesa()
    {
        int som = Random.Range(0, somDefesa.Length);
        audioSource.PlaySound(somDefesa[som]);
    }

    private void PlaySomEspecial()
    {
        int som = Random.Range(0, somEspecial.Length);
        audioSource.PlaySound(somEspecial[som]);
    }

    public void PlaySomMorte()
    {
        audioSource.PlaySound(somMorte);
    }

    public void PlaySomVitoria()
    {
        audioSource.PlaySound(somVitoria);
    }

    IEnumerator TocarDefesa()
    {
        dB.RecebeTexto($"{nomePersonagem} consegue se defender!");
        anim.SetTrigger("Defesa");
        yield return new WaitForSeconds(0.5f);
        PlaySomDefesa();
        ParticulaDefesa();
    }

    IEnumerator TocarDanoNormal(int danoFinal)
    {
        dB.RecebeTexto($"{nomePersonagem} leva dano de {danoFinal}.");
        anim.SetTrigger("Dano");
        yield return new WaitForSeconds(0.5f);
        PlaySomDano();
        ParticulaSangrar();
        vida -= danoFinal; //vida = vida - danoFinal;
        DefineVida();
    }

    IEnumerator TocarDanoMaximo(int danoFinal)
    {
        dB.RecebeTexto($"{nomePersonagem} toma uma porrada de {danoFinal}.");
        anim.SetTrigger("Dano");
        yield return new WaitForSeconds(0.5f);
        CameraTreme(0.5f);
        PlaySomDano();
        ParticulaSangrar();
        vida -= danoFinal;
        DefineVida();
    }

    IEnumerator timerSomVitoria()
    {
        yield return new WaitForSeconds(1.5f);
        audioSource.PlaySound(somVitoria);
    }

    private void CameraTreme(float magnitude)
    {
        audioSource.PlaySound(somVitoria);
        camera.GetComponent<CameraShake>().ShakeCamera(0.5f, magnitude);
    }
}
