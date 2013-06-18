using System;
using MBHEngine.Behaviour;
using Microsoft.Xna.Framework;
using MBHEngine.GameObject;
using MBHEngine.Math;
using System.Diagnostics;
using BumpSetSpikeContentDefs;
using Microsoft.Xna.Framework.Input.Touch;
using MBHEngine.Render;
using MBHEngine.Debug;
using System.Collections.Generic;
using BumpSetSpike.Gameflow;
using MBHEngine.Input;

namespace BumpSetSpike.Behaviour
{
    /// <summary>
    /// Simple class for waiting for the user to tap the screen to start the game.
    /// </summary>
    class MainMenu : MBHEngine.Behaviour.Behaviour
    {
        /// <summary>
        /// Manages the state of the object.
        /// </summary>
        private enum State
        {
            OnTitle = 0,
            MoveToCourt,
        }

        /// <summary>
        /// The current state of the object.
        /// </summary>
        private State mCurrentState;

        /// <summary>
        /// We don't have a great way of knowing when the camera has reached its desination, so
        /// instead we just start a timer that takes the same amount of time and wait for it to
        /// expire.
        /// </summary>
        private StopWatch mWatch;

        /// <summary>
        /// Preallocated to avoid GC.
        /// </summary>
        private Player.OnGameRestartMessage mGameRestartMsg;

        /// <summary>
        /// Constructor which also handles the process of loading in the Behaviour
        /// Definition information.
        /// </summary>
        /// <param name="parentGOH">The game object that this behaviour is attached to.</param>
        /// <param name="fileName">The file defining this behaviour.</param>
        public MainMenu(GameObject parentGOH, String fileName)
            : base(parentGOH, fileName)
        {
        }

        /// <summary>
        /// Call this to initialize a Behaviour with data supplied in a file.
        /// </summary>
        /// <param name="fileName">The file to load from.</param>
        public override void LoadContent(String fileName)
        {
            base.LoadContent(fileName);

            mCurrentState = State.OnTitle;

            mWatch = StopWatchManager.pInstance.GetNewStopWatch();

            // Make the timer last the same amount of time it will take the camera to reach
            // its destination.
            mWatch.pLifeTime = CameraManager.pInstance.pNumBlendFrames;
            mWatch.pIsPaused = true;

            mGameRestartMsg = new Player.OnGameRestartMessage();
        }

        /// <summary>
        /// See parent.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // If we are in the main menu, start looking for button presses.
            // TODO: Move this to update passes.
            if (GameflowManager.pInstance.pState == GameflowManager.State.MainMenu)
            {
                GestureSample gesture = new GestureSample();

                if (InputManager.pInstance.CheckGesture(GestureType.Tap, ref gesture) || InputManager.pInstance.CheckAction(InputManager.InputActions.A, true))
                {
                    if (mCurrentState == State.OnTitle)
                    {
                        // Move down to the gameplay camera position.
                        CameraManager.pInstance.pTargetPosition = new Vector2(0, -30.0f);
                        mCurrentState = State.MoveToCourt;

                        mWatch.pIsPaused = false;
                    }
                }

                // Once the timer expires the camera should be in place and the game can start.
                if (mCurrentState == State.MoveToCourt && mWatch.IsExpired())
                {
                    GameObjectManager.pInstance.BroadcastMessage(mGameRestartMsg, mParentGOH);
                    GameflowManager.pInstance.pState = GameflowManager.State.GamePlay;

                    mWatch.Restart();
                    mWatch.pIsPaused = true;
                }
            }
        }
    }
}
