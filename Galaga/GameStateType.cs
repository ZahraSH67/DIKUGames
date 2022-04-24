using System;
namespace Galaga {
    //4.1 Galaga State
    public enum GameStateType {
            GameRunning, 
            GamePaused,
            MainMenu,
    }

    public class StateTransformer {
        /// <summary>
        /// Method that transforms a string to a GameStateType.
        /// </summary>
        /// <param name="state">The state is a string.</param>
        /// <returns>
        /// A GameStateType that is an enum and has three types.
        /// </returns>

        public static GameStateType TransformStringToState(string state) {
            switch (state) {
                case "GAME_RUNNING":
                    return GameStateType.GameRunning;
                case "GAME_PAUSED":
                    return GameStateType.GamePaused;
                case "MAIN_MENU":
                    return GameStateType.MainMenu;
                default:
                    throw new ArgumentException("It is Invalid");
            }
        }
        /// <summary>
        /// Method that transforms a GameStatetype to a string.
        /// </summary>
        /// <param name="state">The state is a GameStateType.</param>
        /// <returns>
        /// A string.
        /// </returns>

        public static string TransformStateToString(GameStateType state) {
            switch (state) {
                case GameStateType.GameRunning :
                    return "GAME_RUNNING";
                case GameStateType.GamePaused : 
                    return "GAME_PAUSED";
                case GameStateType.MainMenu :
                    return "MAIN_MENU";
                default :
                    throw new ArgumentException ("It is Invalid");          
            }
        }
    }
}