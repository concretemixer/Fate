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
		{"intro.truck.go", "Поехали!"},
		{"intro.pump_look.0", "Дизель - 50 за литр, уроды!"},
		{"intro.pump_look.1", "50 на 200 получается... эээ... много, короче.\nНу вроде денег хватает, а ехать уже недалеко."},
		{"intro.fuel_guy_look.0", "Этот тип заправляет машины. Заправит и мою."},
		{"intro.fuel_guy_use.0", "Что ты имеешь в виду?"},
		{"intro.fuel_guy_use.1", "На что ты меня толкаешь?"},
		{"intro.fuel_guy_use.2", "Ок. Хотя... нет!"},
		{"intro.fuel_guy_talk", "- Дизель. Полный бак, пожалуста."},
		{"intro.fuel_guy_talk_ok", "- Как скажете!"},
		{"intro.box_look.0", "Эта штука связана с электричеством!"},
		{"intro.box_look.1", "Я бы это не трогал..."},
        {"intro.dumpster_look.0", "Мусорка"},
        {"intro.dumpster_look.1", "Возможно, там живут крысы или бомжи"},
        {"intro.dumpster_use.0", "Я не буду рыться в мусоре."},
        {"intro.dumpster_use.1", "Я пока не голоден."},
        {"intro.dumpster_no_waste.0", "Зачем мне это выкидывать?"},
        {"intro.dumpster_no_waste.1", "Это мне еще пригодится."},
        {"intro.shop_look", "Внутри кассы и магазин - все как обычно."},
        {"intro.shop_look_pay", "Надо зайти внутрь и заплатить."},
        {"intro.shop_look_leave", "Я там уже был. Надо ехать дальше."},
        {"intro.diesel_look.0", "Тепловоз."},
        {"intro.diesel_look.1", "ТЭП-60 или похожий."},
        {"intro.girl_look.0", "Симпатичная, но какая-то квадратная..."},
        {"intro.girl_look.1", "Похожа на Сашу Грей. Или нет..."},
        {"intro.girl_look.2", "Кассирша. Скучная работа, должно быть."},

        {"intro.girl_pickup", "- Девушка, как вас зовут?"},
        {"intro.camera_look.0", "Понавешали камер, епт!"},
        {"intro.extinguisher_look.0","Если вдруг пожар, эта штука нам поможет. Хотя вряд ли."},
        {"intro.camera_look.1", "Видеонаблюдение в туалете ведется для вашей безопасности!\nХа-ха."},
        {"intro.extinguisher_look.1","Если вдруг заправка загорится, пользы от него немного будет."},
		{"intro.girl_pickup.answer.0", "- Света"},
		{"intro.girl_pickup.answer.1", "- Маша"},
		{"intro.girl_pickup.answer.2", "- Лена"},

		{"intro.girl_pickup.0", "- А меня Сигизмунд"},
		{"intro.girl_pickup.1", "- А меня Мухамед"},
		{"intro.girl_pickup.2", "- Бонд. Джеймс Бонд."},

        {"intro.girl_what", "- Это не является платежным средством на территории нашей страны, мистер."},

		{"intro.girl_answer", "- Там парень на улице, скажите ему, что вам заправить."},
		{"intro.girl_pay", "- С вас 3499.00. Можете оплатить карточкой или наличными."},
        {"intro.girl_no_cash", "- Этого недостаточно, простите."},
        {"intro.girl_pin", "- Пин-код, пожалуйста."},
        {"intro.girl_pin_wrong_1", "- Неверный код."},
        {"intro.girl_pin_wrong_2", "- Странно. Забыл..."},
        {"intro.girl_pin_good_1", "- Секунду... оплачено. Счастливого пути!"},

		{"intro.girl_else_1", "- Что-то еще?"},
        {"intro.girl_else_2", "- Нет-нет, мне уже пора ехать."},
		{"intro.biker_talk.0", "- Сколько жрет, сколько прет?"},
		{"intro.biker_talk.1", "- Дай прокатиться, бро!"},
		{"intro.biker_talk.2", "- Давно катаешься? Сколько переломов уже?"},
		{"intro.biker_fuck_off", "- Отвали!"},
        {"intro.vending_cash","Это аппарат принимает только наличные"},
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
        return GetText(key + "." + Random.Range(0, n).ToString());
	}

}
