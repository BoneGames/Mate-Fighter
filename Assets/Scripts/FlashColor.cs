
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashColor : MonoBehaviour {

	public Image background;
	Color a,b;
	public float colorChangeSpeed;
	[Range(0.1f, 1f)]
	public float colorMax;
	[Range(0f, 0.9f)]
	public float colorMin;
	float colorOneVal = 1f;
	float colorTwoVal = 0;
	float colorPoint;
	int direction = -1;
	public enum ColorSelection{ Red, Green, Blue, Trans, Full };
	public ColorSelection colorOne;
	public ColorSelection colorTwo;
	void Start () {
		StartCoroutine(ColorChange());
		while(colorMax < colorMin)
		{
			colorMin -= 0.1f;
		}
	}

	Vector4 FirstColor()
	{
		switch(colorOne)
		{
			case ColorSelection.Red:
			a = Color.red;
			break;

			case ColorSelection.Green:
			a = Color.green;
			break;

			case ColorSelection.Blue:
			a = Color.blue;
			break;

			case ColorSelection.Full:
			a = Color.white;
			break;

			case ColorSelection.Trans:
			a = Vector4.zero;
			break;
		}
		return a;
	}

	Vector4 SecondColor()
	{
		switch(colorTwo)
		{
			case ColorSelection.Red:
			b = Color.red;
			break;

			case ColorSelection.Green:
			b = Color.green;
			break;

			case ColorSelection.Blue:
			b = Color.blue;
			break;

			case ColorSelection.Full:
			b = Color.white;
			break;

			case ColorSelection.Trans:
			b = Vector4.zero;
			break;
		}
		return b;
	}
	

	IEnumerator ColorChange()
	{
		while(true)
		{
			while(colorOne == colorTwo)
			{
				background.color = FirstColor();
				yield return new WaitForSeconds(1f);
			}

			colorPoint += Time.deltaTime * colorChangeSpeed * direction;
			if(colorPoint > colorMax)
			{
				colorPoint = colorMax;
				direction *= -1;
			}
			if(colorPoint < colorMin)
			{
				colorPoint = colorMin;
				direction *= -1;
			}
			FirstColor();
			SecondColor();
			
			background.color = Color.Lerp(a,b,colorPoint);
			yield return null;
		}
	}
}
