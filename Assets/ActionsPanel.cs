using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Fate {
    
    public class ActionsPanel : MonoBehaviour {

        public Button[] actionButtons;

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
            GameObject hero = GameObject.FindGameObjectWithTag("Player");
            if (hero != null)
            {
                hero.GetComponent<Hero>().OnActionSelected(act);
            }
        }

        public void ShowOn(GameObject selected)
        {
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
            GetComponent<RectTransform>().anchoredPosition = screenPos;

            Interactable.Action[] acts = selected.GetComponent<Interactable>().Actions;

            foreach (var b in actionButtons) {
                b.gameObject.SetActive(false);
            }

            int x = 0, y = 0, w = 96;

            x = -(w * (acts.Length-1)) / 2;

            foreach (var act in acts)
            {
                actionButtons[(int)act].GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
                x += w;
                actionButtons[(int)act].gameObject.SetActive(true);

            }

        }
    }
}