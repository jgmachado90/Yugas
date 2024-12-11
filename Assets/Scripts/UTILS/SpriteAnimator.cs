using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimator : MonoBehaviour
{
    public Image imageComponent; // Referência ao SpriteRenderer do objeto
    public List<Sprite> sprites;         // Lista de sprites para animar
    public float frameDelay = 0.1f;      // Tempo entre cada frame em segundos

    private int currentFrame = 0;

    private void Start()
    {
        // Inicia a animação
        StartCoroutine(AnimateSprites());
    }

    private IEnumerator AnimateSprites()
    {
        while (true) // Loop infinito
        {
            imageComponent.sprite = sprites[currentFrame]; // Atualiza o sprite atual

            currentFrame = (currentFrame + 1) % sprites.Count; // Avança para o próximo frame (volta ao início no final)

            yield return new WaitForSeconds(frameDelay); // Espera o delay
        }
    }
}
