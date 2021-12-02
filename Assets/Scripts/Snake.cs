using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    //Declaração de uma lista de Transforms chamada _segments. Ela compõe os segmentos do jogador.
    private List<Transform> _segments = new List<Transform>();
    //Declaração do Transform segmentPrefab que servirá como prefab para instanciar ao executar o Grow.
    public Transform segmentPrefab;
    //Declaração do Vector 2 direction para a direita.
    public Vector2 direction = Vector2.right;
    //Declaração da variável int initialSize como 4. Ela será usada no ResetState para fazer com que o jogador sempre comece com 4 segmentos.
    public int initialSize = 4;
        
    //Qualquer código dentro do Start é executado antes da primeira execução de quaisquer códigos colocado em Update.
    private void Start()
    {
        //Executa o método ResetState no começo do script.
        ResetState();
    }
    
    //Qualquer código dentro do Update é executado a quantidade máxima de frames por segundo na qual a máquina do jogador conseguir executar.
    private void Update()
    {
        //Caso a direção do eixo x for diferente de 0, o jogador pode pressionar W ou S para mover-se para cima e para baixo.
        if (this.direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                this.direction = Vector2.up;
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                this.direction = Vector2.down;
            }
        }
        //Entretanto, caso a direção do eixo x não for diferente de 0, o jogador pode pressionar D ou A para mover-se para a direita ou esquerda.
        else if (this.direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                this.direction = Vector2.right;
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                this.direction = Vector2.left;
            }
        }
    }

    private void FixedUpdate()
    {
        //Cada segmento acompanha a posição anterior do segmento que está em sua frente.
        for (int i = _segments.Count - 1; i > 0; i--) {
            _segments[i].position = _segments[i - 1].position;
        }

        //Declaração dos floats x e y arredondados, que são utilizadas para declarar o novo eixo x e y do jogador.
        float x = Mathf.Round(this.transform.position.x) + this.direction.x;
        float y = Mathf.Round(this.transform.position.y) + this.direction.y;

        this.transform.position = new Vector2(x, y);
    }
    
    //Método que faz o jogador crescer um segmento, utilizando o prefab de segmento.
    public void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }
    
    //Método que recomeça o jogo.
    public void ResetState()
    {
        //Faz com que o jogador comece mirado para a direita, e coloca-o na posição 0 do mapa.
        this.direction = Vector2.right;
        this.transform.position = Vector3.zero;

        //Destrói todos os segmentos do jogador.
        for (int i = 1; i < _segments.Count; i++) {
            Destroy(_segments[i].gameObject);
        }

        //Limpa a lista _segments e adiciona um item, que é o transform encontrado no gameobject que possui este script.
        _segments.Clear();
        _segments.Add(this.transform);

        //Enquanto i for menor do que o tamanho especificado no initialSize, ele executa o método grow.
        for (int i = 0; i < this.initialSize - 1; i++) {
            Grow();
        }
    }

    //Caso o jogador colida com algo que tenha um rigidbody e collider com trigger, ele executa o código abaixo.
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Checa se a tag da colisão é "Food". Caso seja, executa o método Grow.
        if (other.tag == "Food") {
            Grow();
        } 
        //Caso a tag não seja "Food" e seja "Obstacle", executa o método ResetState.
        else if (other.tag == "Obstacle") {
            ResetState();
        }
    }

}
