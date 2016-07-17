using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Locale {

	Dictionary<string, string> entries = new Dictionary<string,string> 
	{
		{"intro.my_truck.0", "Отличная машина!"},
		{"intro.my_truck.1", "40 тонн, 295 л.с!"},
		{"intro.my_truck.2", "30 литров на сотню жрет!"},
		{"intro.truck.fuel_first", "Для начала надо заправиться, а то не доеду."},
		{"intro.truck.pay_first", "Стоп. Заплатить надо, мы же честные люди!"},
		{"intro.pump_look.0", "Дизель - 50 за литр, уроды!"},
		{"intro.pump_look.1", "50 на 200 получается... эээ... много, короче.\nНу вроде денег хватает, а ехать уже недалеко."},
		{"intro.fuel_guy_look.0", "Этот тип заправляет машины. Заправит и мою."},
		{"intro.fuel_guy_use.0", "Что ты имеешь в виду?"},
		{"intro.fuel_guy_use.1", "На что ты меня толкаешь?"},
		{"intro.fuel_guy_use.2", "Ок. Хотя... нет!"},
		{"intro.fuel_guy_talk", "Дизель. Полный бак, пожалуста."},
		{"intro.box_look.0", "Эта штука связана с электричеством!"},
		{"intro.box_look.1", "Я бы это не трогал..."},
	};


	public string GetText (string key)
	{
		string result;
		if (entries.TryGetValue (key, out result))
			return result;
		else
			return key;
	}

	public string GetRandomText(string key, int n)
	{
		string result;
		if (entries.TryGetValue (key+"."+Random.Range(0,n).ToString(), out result))
			return result;
		else
			return key;
	}

}
