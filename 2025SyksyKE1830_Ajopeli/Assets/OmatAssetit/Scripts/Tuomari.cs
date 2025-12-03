using UnityEngine;
using TMPro;
public class Tuomari : MonoBehaviour
{
    public TMP_Text resultText;

    private bool winnerDeclared = false;
    
    private void Start()
    {
        resultText.text = "";
    }

    private void OnTriggerEnter(Collider car)
    {
        CarIdentify id = car.GetComponent<CarIdentify>();

        string winnerName = id.displayName;
        
        if(id.kind == CarKind.Player)
        {
            var validator = car.GetComponent<PelaajanKierrostarkastus>();
            if (validator == null)
            {
                Debug.LogError("Puuttuu PelaajanKierrostarkastus scripti");
                return;
            }

            if (!validator.AllVisitedThisLap)
            {
                Debug.Log("Pelaaja ylitti maaliviivan, mutta kaikki checkpointit eivät ole kunnossa -> ei voittoa!");
                return;
            }
        }
        if(winnerDeclared == false)
        {
            winnerDeclared = true;
            resultText.text = $"WINNER: {winnerName}";
            //Debug.Log($"WINNER: {winnerName}");
        }
    }
}
