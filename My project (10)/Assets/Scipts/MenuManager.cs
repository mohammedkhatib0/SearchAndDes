using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
	public static MenuManager Instance;

	[SerializeField] menu[] menus;

	void Awake()
	{
		Instance = this;
	}

	public void OpenMenu(string menuName)
	{
		for(int i = 0; i < menus.Length; i++)
		{
			if(menus[i].menuName == menuName)
			{
				menus[i].Open();
			}
			else if(menus[i].open)
			{
				CloseMenu(menus[i]);
			}
		}
	}

	public void OpenMenu(menu Menu)
	{
		for(int i = 0; i < menus.Length; i++)
		{
			if(menus[i].open)
			{
				CloseMenu(menus[i]);
			}
		}
		Menu.Open();
 }

	public void CloseMenu(menu Menu)
	{
		Menu.close();
		
	}
}