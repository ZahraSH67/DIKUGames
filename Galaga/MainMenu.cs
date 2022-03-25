using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.State;

namespace Galaga.GalagaStates {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;

        public MainMenu() {
            
        }
        public static MainMenu GetInstance() {
            if (MainMenu.instance == null) {
                MainMenu.instance = new MainMenu();
                MainMenu.instance.InitializeGameState();
            }
            return MainMenu.instance;
        }

        public void RenderState() {
            backGroundImage.RenderEntity();
        }

        public void InitializeGameState () {
            
        }

        // GalagaBus.GetBus().RegisterEvent(
        //     new GameEvent {
        //         EventType = GameEventType.GameStateEvent,
        //         Message = "CHANGE_STATE",
        //         StringArg1 = "GAME_RUNNING"
        //     }
        // };

        /// <summary>
        /// Reset this state to set its private variables to their initial default
        /// values. This is useful when e.g. leaving a game and entering a new game,
        /// where calling this method could reset the player's position, reset the
        /// score counter, or perform other similar actions.
        /// </summary>
        public void ResetState() {

        }

        /// <summary>
        /// Update all variables that are being used by this GameState.
        /// </summary>
        public void UpdateState() {

        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {

        }

    }
}