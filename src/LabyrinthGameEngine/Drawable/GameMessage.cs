namespace LabyrinthGameEngine
{
    using GameHandler.Interfaces;
    using System;

    internal class GameMessage : IDrawable
    {
        private String message = null;

        internal GameMessage(String message)
        {
            this.message = message;
        }
        
        public object GetDrawableData()
        {
            return message;
        }
    }
}
