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
        private Text NewGame;
        private Text Quit;
        private int activeMenuButton;
        private int maxMenuButtons;

        public MainMenu() {
            NewGame = new Text ("NEW GAME", new Vec2F(0.2f,0.5f), new Vec2F(0.2f,0.5f));
            Quit = new Text ("Quit", new Vec2F(0.0f,0.0f), new Vec2F(0.0f,0.0f));
            Text[] menuButtons = new Text[] {NewGame, Quit};

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
            menuButtons[0].RenderText();
            menuButtons[1].RenderText();
        }

        public void InitializeGameState () {
            
        }

        

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
            GalagaBus.GetBus().ProcessEventsSequentially();

        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            GameEvent gameEvent = new GameEvent();
            gameEvent.EventType = GameEventType.InputEvent;
            switch (action) {
                case KeyboardAction.KeyPress:
                    gameEvent.Message = "KeyPress";
                    break;
                case KeyboardAction.KeyRelease:
                    gameEvent.Message = "KeyRelease";
                    break;
            }
        }

        void KeyPress(KeyboardKey key) {
            switch (key) {
                    case KeyboardKey.Space :
                    GalagaBus.GetBus().RegisterEvent(
                        new GameEvent {
                            EventType = GameEventType.GameStateEvent,
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_RUNNING"
                        }
                    );
                    break;
            //     case KeyboardKey.Up :
            //         Text[i] = Text[i]--;
            //         break;
            //     case KeyboardKey.Down :
            //         Text[i] = Text[i]++;
            //         break;
            }
        }

    }
}