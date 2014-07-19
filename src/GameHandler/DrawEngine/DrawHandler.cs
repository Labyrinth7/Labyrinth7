namespace GameHandler.DrawEngine
{
    using GameHandler.Interfaces;
    using System.Collections.Generic;

    public class DrawHandler : IDrawHandler
    {
        public List<IDrawable> DrawableObject = new List<IDrawable>();

        private UserInterface userInterface;
        private IUserInterfaceHandler userInterfaceHandler;

        public DrawHandler(UserInterface currentUi)
        {
            this.userInterface = currentUi;

            switch (this.userInterface)
            {
                case UserInterface.Console:
                    this.userInterfaceHandler = new UiHandlerConsole();
                    break;
            }
        }

        /// <summary>
        /// Draws all drawable objects.
        /// </summary>
        public void Draw()
        {
            userInterfaceHandler.Draw(DrawableObject);
        }

        /// <summary>
        /// Returns the current selected UI.
        /// </summary>
        public UserInterface UserInterface
        {
            get
            {
                return this.userInterface;
            }
        }
    }
}
