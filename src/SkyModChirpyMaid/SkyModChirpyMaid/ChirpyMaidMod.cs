// SkyMod Chirpy Maid
// (c) 2015 SkyModTeam (http://skymod.io)
// http://github.com/skymodteam

namespace SkyMod.ChirpyMaid
{
    using ColossalFramework.UI;
    using ICities;
    using System;
    using System.Timers;
    using UnityEngine;

    public class ChirpyMaidMod : IUserMod
    {
        public string Name
        {
            get { return "SkyMod Chirpy Maid"; }
        }

        public string Description
        {
            get { return "Get rid of that backlog of chirps easily with a Clear Everything button!"; }
        }
    }

    public class ChirpyMaidLoader : LoadingExtensionBase
    {
        private ChirpPanel chirpPanel;

        public override void OnLevelLoaded(LoadMode mode)
        {
            this.chirpPanel = GameObject.FindObjectOfType<ChirpPanel>();
            if (this.chirpPanel == null) return;

            // The following was shamelessly ripped from:
            // http://www.reddit.com/r/CitiesSkylinesModding/comments/2ymwxe/example_code_using_the_colossal_ui_in_a_user_mod/
            // https://gist.github.com/reima/9ba51c69f65ae2da7909

            var buttonObject = new GameObject("MyButton", typeof(UIButton));

            // Make the buttonObject a child of the uiView.
            buttonObject.transform.parent = this.chirpPanel.transform;

            // Get the button component.
            var button = buttonObject.GetComponent<UIButton>();

            // Set the text to show on the button.
            button.text = "CE";

            // Set the button dimensions.
            button.width = 30;
            button.height = 30;

            // Style the button to look like a menu button.
            button.normalBgSprite = "ButtonMenu";
            button.disabledBgSprite = "ButtonMenuDisabled";
            button.hoveredBgSprite = "ButtonMenuHovered";
            button.focusedBgSprite = "ButtonMenuFocused";
            button.pressedBgSprite = "ButtonMenuPressed";
            button.textColor = new Color32(255, 255, 255, 255);
            button.disabledTextColor = new Color32(7, 7, 7, 255);
            button.hoveredTextColor = new Color32(7, 132, 255, 255);
            button.focusedTextColor = new Color32(255, 255, 255, 255);
            button.pressedTextColor = new Color32(30, 30, 44, 255);

            // Place the button.
            button.transformPosition = new Vector3(-1.65f, 0.97f);

            // Respond to button click.
            button.eventClick += ButtonClick;
        }

        private void ButtonClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            if (this.chirpPanel != null)
            {
                this.chirpPanel.ClearMessages();
                this.chirpPanel.Hide();
            }
        }
    }
}