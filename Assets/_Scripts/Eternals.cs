using UnityEngine;

public class Eternals
{
    public static float bushSpeed;
    public static float gapValue = 1f;
    public static float speedBoostValue = 1f;
    public static float shieldMultiplier = 1f;
    public static float fuelMultiplier = 1f;
    public static float magnetMultiplier = 1f;
    public static float boostDurationMultiplier = 1f;
    public static float coinGapMultiplier = 1f;
    public static bool isShieldOn = false, isMagnetOn = false, isCarBroke = false, isSprinting = false;

    public static int coins, shields, magnets, sprints, fuels;
    public static float minSpeed = 225f, maxSpeed = 350f, maxFuel, sprintSpeed;

}

public class Codes
{
    public const string CoinsCode = "Coins";
    public const string ShieldsCode = "Shields";
    public const string MagnetsCode = "Magnets";
    public const string SprintCode = "Sprints";
    public const string FuelCode = "Fuels";
}
