 using UnityEngine;
 using UnityEngine.EventSystems;
 
 public class Button_Quit : MonoBehaviour, IPointerDownHandler
 {
 
     public void OnPointerEnter(PointerEventData eventData)
     {
         GetComponent<Renderer>().material.color = Color.blue;    
     }
     
     public void OnPointerExit(PointerEventData eventData)
     {
         GetComponent<Renderer>().material.color = Color.white;
     }
     
     public void OnPointerDown(PointerEventData eventData)
     {
		Application.Quit();
     }
 }