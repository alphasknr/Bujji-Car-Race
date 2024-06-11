using UnityEngine;

public class ProximitySensor : MonoBehaviour
{
    
   private void OnTriggerEnter2D(Collider2D otherCar) {
        GameObject parentCar = transform.parent.gameObject;
        if (otherCar.tag == "car" && otherCar != parentCar){
            otherCar.GetComponent<CarMovement>().speed = 0.4f * Eternals.speedBoostValue;
            parentCar.GetComponent<CarMovement>().speed = 1f * Eternals.speedBoostValue;
            // Debug.Log(otherCar.name);
        }
   }
}
