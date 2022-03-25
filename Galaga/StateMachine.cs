using DIKUArcade.Events;
using DIKUArcade.State;
using Galaga;

namespace Galaga.GalagaStates {
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState { get; private set; }
        public StateMachine() {
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            ActiveState = MainMenu.GetInstance();
            //ActiveState = GameRunning.GetInstance();
            //ActiveState = GamePaused.GetInstance();
        }
        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
                case GameStateType.MainMenu:
                    ActiveState = MainMenu.GetInstance();
                    break;
                case GameStateType.GameRunning:
                    //ActiveState = GameRunning.GetInstance();
                    break;
                case GameStateType.GamePaused:
                    //ActiveState = GameRunning.GetInstance();
                    break;
            }
        }

        public void ProcessEvent (GameEvent gameEvent) {
            GalagaBus.GetBus().RegisterEvent(
                new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_RUNNING"
                }
            );}

    }
}
