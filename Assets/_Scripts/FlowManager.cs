using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class FlowManager : MonoBehaviour
{
    public static FlowManager Instance { get; private set; }

    public TMP_Text shieldText, magnetText, sprintText, fuelText, speedometerText, coinsText;
    public Image fuelometer, speedometer;
    public ParticleSystem smoke;
    public GameObject shield, magnet;
    public Color smokeColor, sprintColor, burnColor;

    void Awake()
    {
        Instance = this;
        LoadPlayerPrefs();
    }


    public void LoadPlayerPrefs()
    {
        Eternals.coins = PlayerPrefs.GetInt(Codes.CoinsCode);
        Eternals.shields = PlayerPrefs.GetInt(Codes.ShieldsCode);
        Eternals.sprints = PlayerPrefs.GetInt(Codes.SprintCode);
        Eternals.fuels = PlayerPrefs.GetInt(Codes.FuelCode);
        Eternals.magnets = PlayerPrefs.GetInt(Codes.MagnetsCode);
        UpdateValues();
    }

    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt(Codes.CoinsCode, Eternals.coins);
        PlayerPrefs.SetInt(Codes.ShieldsCode, Eternals.shields);
        PlayerPrefs.SetInt(Codes.SprintCode, Eternals.sprints);
        PlayerPrefs.SetInt(Codes.FuelCode, Eternals.fuels);
        PlayerPrefs.SetInt(Codes.MagnetsCode, Eternals.magnets);
        UpdateValues();
    }

    public void UpdateValues()
    {
        shieldText.text = Eternals.shields.ToString();
        magnetText.text = Eternals.magnets.ToString();
        sprintText.text = Eternals.sprints.ToString();
        fuelText.text = Eternals.fuels.ToString();
        coinsText.text = Eternals.coins.ToString();
    }

    public void UpdateSmokeLifetime(float lifetime)
    {
        var mainModule = smoke.main;
        mainModule.startLifetime = lifetime;
        if(lifetime > 1f){mainModule.startColor = sprintColor;}
        else{mainModule.startColor = smokeColor;}
        smoke.gameObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        smoke.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void AfterExplosion(){
        var mainModule = smoke.main;
        mainModule.startColor = burnColor;
        smoke.gameObject.transform.rotation = Quaternion.Euler(180f, 0f, 0f);
        smoke.gameObject.transform.localScale = new Vector3(1f, 2f, 1f);
    }
}
