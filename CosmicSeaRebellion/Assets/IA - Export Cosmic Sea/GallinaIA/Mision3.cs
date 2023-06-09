using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Mision3 : MonoBehaviour
{
    public Text questionText;
    public Button[] answerButtons;
    public Text resultText;

    public int numberA;
    public int numberB;
    public int correctAnswer;

    public MisionManager misionManager;

    private void Start()
    {
        misionManager = FindObjectOfType<MisionManager>();
        GenerateNumbers();
        GenerateQuestion();
        AssignAnswers();
    }

    public void CheckAnswer(int buttonIndex)
    {
        if (buttonIndex == correctAnswer)
        {
            resultText.text = "�Respuesta correcta!";
            misionManager.CorrectAnswer("Mision_3");
            gameObject.SetActive(false);
        }
        else
        {
            resultText.text = "Respuesta incorrecta. Int�ntalo de nuevo.";
        }
    }

    private void GenerateNumbers()
    {
        numberA = Random.Range(5, 7); // Rango de n�meros aleatorios para A (puedes ajustarlos seg�n tus necesidades)
        numberB = Random.Range(1, 4); // Rango de n�meros aleatorios para B (puedes ajustarlos seg�n tus necesidades)
    }

    private void GenerateQuestion()
    {
        correctAnswer = Random.Range(0, 3); // �ndice del bot�n con la respuesta correcta (0, 1 o 2)

        int incorrectAnswer1 = Random.Range(1, 10); // Respuestas incorrectas (puedes ajustar los rangos seg�n tus necesidades)
        int incorrectAnswer2 = Random.Range(1, 10);

        questionText.text = string.Format("�Podrias ayudarme a buscar a {0} - {1} gallinas que han escapado? ", numberA, numberB); // Puedes personalizar el formato de la pregunta seg�n tus necesidades

        // Asigna las respuestas a los botones
        answerButtons[correctAnswer].GetComponentInChildren<Text>().text = (numberA - numberB).ToString();
        answerButtons[(correctAnswer + 1) % 3].GetComponentInChildren<Text>().text = incorrectAnswer1.ToString();
        answerButtons[(correctAnswer + 2) % 3].GetComponentInChildren<Text>().text = incorrectAnswer2.ToString();
    }

    private void AssignAnswers()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int buttonIndex = i; // Variable temporal para evitar que el �ndice siempre tenga el valor m�ximo
            answerButtons[i].onClick.AddListener(() => CheckAnswer(buttonIndex));
        }
    }
}
