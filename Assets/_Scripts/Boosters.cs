using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Boosters : MonoBehaviour
{

    public Button sprintBtn, shieldOnBtn, fuelBtn, magnetBtn;
    float prevSpeed;
    Coroutine fuelCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        FuelFill();
        sprintBtn.onClick.AddListener(NitroSprintOn);
        shieldOnBtn.onClick.AddListener(ShieldOn);
        fuelBtn.onClick.AddListener(FuelFill);
        magnetBtn.onClick.AddListener(MagnetOn);
    }

    void NitroSprintOn(){
        if(Eternals.speedBoostValue < 1f){return; }
        Eternals.sprintSpeed = 50f;
        sprintBtn.interactable = false;
        prevSpeed = Eternals.speedBoostValue;
        FlowManager.Instance.UpdateSmokeLifetime(1.5f);
        Eternals.speedBoostValue += 2f;
        StartCoroutine(NitroSprintOff());
    }

    IEnumerator NitroSprintOff(){
        while(prevSpeed < Eternals.speedBoostValue){
            Eternals.speedBoostValue  -= 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        FlowManager.Instance.UpdateSmokeLifetime(1f);
        sprintBtn.interactable = true;
        Eternals.sprintSpeed = 0f;
        if(Eternals.isCarBroke == false){Eternals.speedBoostValue = prevSpeed;}
    }

    void ShieldOn(){
        FlowManager.Instance.shield.SetActive(true);
        shieldOnBtn.interactable = false;
        Eternals.isShieldOn = true;
        StartCoroutine(ShieldOff());
    }

    IEnumerator ShieldOff(){
        yield return new WaitForSeconds(3f*Eternals.shieldMultiplier);
        FlowManager.Instance.shield.SetActive(false);
        shieldOnBtn.interactable = true;
        Eternals.isShieldOn = false;
    }

    void FuelFill(){
        // FlowManager.Instance.fuelometer.fillAmount = 1f;
        FlowManager.Instance.smoke.gameObject.SetActive(true);
        FlowManager.Instance.UpdateSmokeLifetime(1f);
        StartCoroutine(Fuelometer());
        fuelBtn.interactable = false;
        Time.timeScale = 1f;
        if(Eternals.speedBoostValue > 0){prevSpeed = Eternals.speedBoostValue;}
        else{Eternals.speedBoostValue = prevSpeed;}
        if(fuelCoroutine != null){StopCoroutine(fuelCoroutine);}
        fuelCoroutine = StartCoroutine(FuelNill());
    }

    IEnumerator FuelNill(){
        // Debug.Log(Eternals.speedBoostValue);
        // while (Eternals.speedBoostValue < prevSpeed + 0.1f)
        // {
        //     Eternals.speedBoostValue += 0.05f;
        //     yield return new WaitForSeconds(0.1f);
        // }
        yield return new WaitForSeconds(50f*Eternals.fuelMultiplier);
        float gap = 0.05f/(Eternals.speedBoostValue/0.05f);
        FlowManager.Instance.UpdateSmokeLifetime(0f);
        while (Eternals.speedBoostValue > 0.5f)
        {
            FlowManager.Instance.fuelometer.fillAmount -= gap;
            Eternals.speedBoostValue -= 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
        FlowManager.Instance.smoke.gameObject.SetActive(false);
        FlowManager.Instance.fuelometer.fillAmount = 0f;
        Time.timeScale = 0f;
        Eternals.speedBoostValue = 0f;
        fuelBtn.interactable = true;
        Debug.Log("The fuel is empty");
    }

    IEnumerator Fuelometer(){

        while(FlowManager.Instance.fuelometer.fillAmount < 1f){
            FlowManager.Instance.fuelometer.fillAmount += 0.05f;
            yield return new WaitForSeconds(0.15f);
        }
        FlowManager.Instance.fuelometer.fillAmount = 1f;
        float gap = 1/(50f*Eternals.fuelMultiplier);
        // Debug.Log(gap);
        while(FlowManager.Instance.fuelometer.fillAmount > 0.05f){
            FlowManager.Instance.fuelometer.fillAmount -= gap/2f;
            yield return new WaitForSeconds(0.5f);
        }
    }

    void MagnetOn(){
        FlowManager.Instance.magnet.SetActive(true);
        magnetBtn.interactable = false;
        Eternals.isMagnetOn = true;
        StartCoroutine(MagnetOff());
    }

    IEnumerator MagnetOff(){
        yield return new WaitForSeconds(3f * Eternals.magnetMultiplier);
        FlowManager.Instance.magnet.SetActive(false);
        magnetBtn.interactable = true;
        Eternals.isMagnetOn = false;
    }
    
}
