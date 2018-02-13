using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

	[SerializeField] AudioMenuButton audioEnterButton;
	[SerializeField] AudioMenuButton audioClickButton;


	public Text buttonText;


	public void OnPointerEnter(PointerEventData eventData){
		buttonText.color = Color.red;
		audioEnterButton.Play ();
	}

	public void OnPointerExit(PointerEventData eventData){
		buttonText.color = Color.white;
	}

	public void OnPointerClick(PointerEventData eventData){
		audioClickButton.Play ();
	}
}
