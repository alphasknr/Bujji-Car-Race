using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Boosters : MonoBehaviour
{

    public Button sprintBtn, shieldOnBtn, fuelBtn;
    float prevSpeed, fullFuelSpeed;
    Coroutine fuelCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        FuelFill();
        sprintBtn.onClick.AddListener(NitroSprintOn);
        shieldOnBtn.onClick.AddListener(ShieldOn);
        fuelBtn.onClick.AddListener(FuelFill);
    }

    void NitroSprintOn(){
        sprintBtn.interactable = false;
        prevSpeed = Eternals.speedBoostValue;
        Eternals.speedBoostValue += 2f;
        StartCoroutine(NitroSprintOff());
    }

    IEnumerator NitroSprintOff(){
        while(prevSpeed < Eternals.speedBoostValue){
            Eternals.speedBoostValue  -= 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        sprintBtn.interactable = true;
        Eternals.speedBoostValue = prevSpeed;
    }

    void ShieldOn(){
        shieldOnBtn.interactable = false;
        Eternals.isShieldOn = true;
        StartCoroutine(ShieldOff());
    }

    IEnumerator ShieldOff(){
        yield return new WaitForSeconds(3f*Eternals.shieldMultiplier);
        shieldOnBtn.interactable = true;
        Eternals.isShieldOn = false;
    }

    void FuelFill(){
        // fuelBtn.interactable = false;
        Time.timeScale = 1f;
        if(Eternals.speedBoostValue > 0){prevSpeed = Eternals.speedBoostValue;}
        else{Eternals.speedBoostValue = prevSpeed;}
        if(fuelCoroutine != null){StopCoroutine(fuelCoroutine);}
        fuelCoroutine = StartCoroutine(FuelNill());
    }

    IEnumerator FuelNill(){
        yield return new WaitForSeconds(5f*Eternals.fuelMultiplier);
        while (Eternals.speedBoostValue > 0.5f)
        {
            Eternals.speedBoostValue -= 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
        Time.timeScale = 0f;
        Eternals.speedBoostValue = 0f;
        fuelBtn.interactable = true;
        Debug.Log("The fuel is empty");
    } 


}
