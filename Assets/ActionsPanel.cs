using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Fate {
    
    public class ActionsPanel : MonoBehaviour {

        public Button[] actionButtons;
        Hero hero;

    	// Use this for initialization
    	void Start () {
            for (int a=0;a<actionButtons.Length;a++)
            {
                Interactable.Action act = (Interactable.Action)a;
                actionButtons[a].onClick.RemoveAllListeners();
                actionButtons[a].onClick.AddListener(delegate
                    {                        
                        OnActionClicked(act);
                    });
            } 
    	}
    	
    	// Update is called once per frame
    	void Update () {
    	
    	}

        public void OnActionClicked(Interactable.Action act)
        {
            hero.GetComponent<Hero>().OnActionSelected(act);
        }

        public void ShowOn(GameObject selected, Hero hero)
        {
            this.hero = hero;
            Camera camera = Camera.main;

            Vector3 menuPos = selected.transform.position;
            foreach (Transform child in selected.transform)
            {
                if (child.gameObject.tag == "MenuPoint")
                {
                    menuPos = child.transform.position;
                }
            }

            Vector3 screenPos = camera.WorldToScreenPoint(menuPos);

            Interactable.Action[] acts = selected.GetComponent<Interactable>().Actions;

            foreach (var b in actionButtons) {
                b.gameObject.SetActive(false);
            }

            int x = 0, y = 0, w = 96;                        

            foreach (var act in acts)                       
            {
                if (act == Interactable.Action.Use)
                {
                    if (!string.IsNullOrEmpty(hero.Tool))
                        continue;
                }
                if (act == Interactable.Action.UseItem)
                {
                    if (string.IsNullOrEmpty(hero.Tool))
                        continue;
                }
                actionButtons[(int)act].GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
                x += w;
                actionButtons[(int)act].gameObject.SetActive(true);

                if (act == Interactable.Action.UseItem)
                {
                    Texture t = Resources.Load<Texture>(@"Items\Icons\" + hero.Tool);
                    actionButtons[(int)act].GetComponentInChildren<RawImage>().texture = t;
                }
            }

            screenPos.x -= (x-96) / 2;
            GetComponent<RectTransform>().anchoredPosition = screenPos;


        }
    }
}