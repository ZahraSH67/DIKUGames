using System;
namespace Galaga {
    //4.1 Galaga State
    public class StateTransformer {
        public enum GameStateType {
            GameRunning, 
            GamePaused,
            MainMenu,
        }
        /// <summary>
        /// Method that transforms a string to a GameStateType.
        /// </summary>
        /// <param name="state">The state is a string.</param>
        /// <returns>
        /// A GameStateType that is a num and has three types.
        /// </returns>

        public static GameStateType TransformStringToState(string state) {
            switch (state) {
                case "Game_Runing":
                    return GameStateType.GameRunning;
                case "Game_Paused":
                    return GameStateType.GamePaused;
                case "Main_Menu":
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
                    return "Game_Running";
                case GameStateType.GamePaused : 
                    return "Game_Paused";
                case GameStateType.MainMenu :
                    return "Main_Menu";
                default :
                    throw new ArgumentException ("Ii is Invalid");          
            }
        }
    }
}