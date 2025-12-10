using UnityEngine;
using TMPro;
public class Tuomari : MonoBehaviour
{
    public TMP_Text resultText;

    public int kierostenMaara = 3;

    private bool winnerDeclared = false;
    
    private void Start()
    {
        resultText.text = "";
    }

    private void OnTriggerEnter(Collider car)
    {
        CarIdentify id = car.GetComponent<CarIdentify>();

        if(id == null)
        {
            return;
        }

        LapCounter lap = car.GetComponent<LapCounter>();


        //string winnerName = id.displayName;
        
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
            int tmpLap =lap.lapsCompleted;
            validator.UpdateLapsText(tmpLap +1,kierostenMaara);
            validator.ResetLap();
        }

        lap.lapsCompleted++;

        if(winnerDeclared == false && lap.lapsCompleted >= kierostenMaara )
        {
            string winnerName = id.displayName;
            winnerDeclared = true;
            resultText.text = $"WINNER: {winnerName}";
            GameManager.Instance.Phase = RacePhase.Finished;
            //Debug.Log($"WINNER: {winnerName}");
        }
    }
}
