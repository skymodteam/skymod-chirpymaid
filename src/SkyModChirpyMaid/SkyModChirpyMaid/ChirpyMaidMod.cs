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
        private static UIButton buttonInstance;

        public static UIButton ButtonInstance
        {
            get
            {
                return buttonInstance;
            }
            set
            {
                buttonInstance = value;
            }
        }

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
        public override void OnLevelLoaded(LoadMode mode)
        {
            if (ChirpPanel.instance == null) return;

            // The following was shamelessly ripped from:
            // http://www.reddit.com/r/CitiesSkylinesModding/comments/2ymwxe/example_code_using_the_colossal_ui_in_a_user_mod/
            // https://gist.github.com/reima/9ba51c69f65ae2da7909

            var buttonObject = new GameObject("SkyModChirpyMaidButton", typeof(UIButton));

            // Make the buttonObject a child of the uiView.
            buttonObject.transform.parent = ChirpPanel.instance.transform;

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

            ChirpyMaidMod.ButtonInstance = button;
        }

        private void ButtonClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            if (eventParam.buttons == UIMouseButton.Left && ChirpPanel.instance != null)
            {
                ChirpPanel.instance.ClearMessages();
                ChirpPanel.instance.Hide();
            }
        }
    }

    public class ChirpyMaidButtonMonitor : ThreadingExtensionBase
    {
        public override void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
            try
            {
                if (ChirpPanel.instance != null && ChirpyMaidMod.ButtonInstance != null)
                {
                    if (ChirpyMaidMod.ButtonInstance.isVisible && !ChirpPanel.instance.isShowing)
                    {
                        ChirpyMaidMod.ButtonInstance.Hide();
                    }
                    else if (!ChirpyMaidMod.ButtonInstance.isVisible && ChirpPanel.instance.isShowing)
                    {
                        ChirpyMaidMod.ButtonInstance.Show();
                    }
                }
            }
            catch
            {
                // Gulp.
            }

            base.OnUpdate(realTimeDelta, simulationTimeDelta);
        }
    }
}