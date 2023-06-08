using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MisionManager : MonoBehaviour
{
    public int enemyTarget = 10;            // Número de enemigos que se deben eliminar
    public int objectTarget = 5;            // Número de objetos que se deben recoger
    public int petsTarget = 3;

    [SerializeField]
    private int enemiesEliminated = 0;      // Contador de enemigos eliminados
    [SerializeField]
    private int objectsCollected = 0;       // Contador de objetos recogidos
    [SerializeField]
    private int petsReached = 0;

    public Text missionTexxt;                // Referencia al Texto que muestra la misión actual        
    private bool missionAccepted = false;

    public Mision1 mision1target;
    public Mision2 mision2target;
    public Mision3 mision3target;
    public GameObject missionPanel;// Panel que contiene el texto de la misión
    public GameObject dialoguePanel; // Panel que contiene el texto del dialogo
    public GameObject misionObjects;
    public GameObject misionBattle;
    public GameObject misionPets;

    public bool mision1 = false;

    public Text textMision;
    public string missionText;       // Texto de la misión a mostrar
    public int enemiesToKill;        // Cantidad de enemigos que deben ser eliminados para cumplir la misión
    private int enemiesKilled;

    public bool aceptarMision;

    public GameObject currentPanel;
    public GameObject[] Enemies;
    public GameObject[] Pets;
    public GameObject[] Objects;
    // Start is called before the first frame update
    
    void Start()
    {

        mision1target = FindObjectOfType<Mision1>();
        mision2target = FindObjectOfType<Mision2>();
        mision3target = FindObjectOfType<Mision3>();
        enemiesToKill = Enemies.Length;
        //textMision.text = "Elimina a todos los guardias" + "\n Guardias Restantes " + enemiesToKill;
        dialoguePanel.SetActive(false);
        missionPanel.SetActive(false);
        misionBattle.SetActive(false);
        misionObjects.SetActive(false);
        misionPets.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        enemiesToKill = mision1target.numberA + mision1target.numberB;
        enemyTarget = mision1target.numberA + mision1target.numberB;
        petsTarget = mision2target.numberA - mision2target.numberB;
    }
    public void EnemyKilled()
    {
        // Llamado cuando un enemigo es eliminado por el jugador
        enemiesKilled++;
        if (enemiesKilled >= enemiesToKill)
        {
            // Misión cumplida
            missionPanel.SetActive(true);
            missionPanel.GetComponentInChildren<Text>().text = "¡Misión cumplida!";
        }
    }
    public void UpdateMissionProgress(string missionType, int amount)
    {
        if (!missionAccepted)
            return;

        switch (missionType)
        {
            case "Enemy":
                enemiesEliminated += amount;
                missionTexxt.text = "Elimina " + enemiesEliminated + "/" + enemyTarget + " enemigos";
                Debug.Log("Mate a un enemigo");
                if (enemiesEliminated >= enemyTarget)
                    CompleteMission();
                break;

            case "Object":
                objectsCollected += amount;
                Debug.Log("Sume un objeto");
                if (objectsCollected >= objectTarget)
                    CompleteMission();
                break;

            case "Pets":
                petsReached += amount;
                missionTexxt.text = "Alcanza las " + petsReached + "/" + petsTarget + " Gallinas";
                Debug.Log("Sume un pollo");
                if (petsReached >= petsTarget)
                    CompleteMission();
                break;
        }
    }

    private void CompleteMission()
    {
        Debug.Log("¡Misión completada!");
        ResetMission();
    }

    // Método para reiniciar la misión actual
    private void ResetMission()
    {
        enemiesEliminated = 0;
        objectsCollected = 0;
        petsReached = 0;
        missionAccepted = false;
        missionTexxt.text = "No hay misiones disponibles";
    }
    public void SetMission(string missionType)
    {
        ResetMission();
        missionAccepted = false;

        switch (missionType)
        {
            case "Enemy":
                missionTexxt.text = "Elimina " + enemiesEliminated + "/" + enemyTarget + " enemigos";
                missionPanel.SetActive(true);
                break;

            case "Object":
                missionTexxt.text = "Recoge " + objectTarget + " objetos";
                missionPanel.SetActive(true);
                break;

            case "Pet":
                missionTexxt.text = "Alcanza las " + petsReached + "/" + petsTarget + " Gallinas";
                missionPanel.SetActive(true);
                break;
        }
    }
    public void AcceptMission()
    {
        missionAccepted = true;
    }
    public void MisionAcepted()
    {
        dialoguePanel.SetActive(false);
        missionPanel.SetActive(true);
    }
    public void CorrectAnswer(string missionActivated)
    {
        switch (missionActivated)
        {
            case "Mision_1":
                SetMission("Enemy");
                AcceptMission();
                break;
            case "Mision_2":
                SetMission("Pet");
                AcceptMission();
                break;
            case "Mision_3":
                SetMission("Object");
                AcceptMission();
                break;
        }
    }
    public void EnabledMisionPanel(string panelIA)
    {
        switch(panelIA)
        {
            case "Bots":
                misionBattle.SetActive(true);
                currentPanel = misionBattle;
                break;
            case "Objects":
                misionObjects.SetActive(true);
                currentPanel = misionObjects;
                break;
            case "Pets":
                misionPets.SetActive(true);
                currentPanel = misionPets;
                break;
        }
    }
}
