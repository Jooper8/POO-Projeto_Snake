using UnityEngine;

public class Food : MonoBehaviour
{
    public Collider2D gridArea;

    private void Start()
    {
        //Executa o método de aleatorizar a posição do spawn da comida.
        RandomizePosition();
    }

    public void RandomizePosition()
    {
        //Declaração dos limites da área de jogo.
        Bounds bounds = this.gridArea.bounds;

        //Declaração dos floats x e y, que se baseia em um valor aleatório dentro da área definida anteriormente.
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        //Arredondação das variaveis para números inteiros.
        x = Mathf.Round(x);
        y = Mathf.Round(y);

        this.transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Executa o método RandomizePosition.
        RandomizePosition();
    }

}
