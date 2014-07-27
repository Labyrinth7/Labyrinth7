namespace GameHandler.DrawEngine
{
    using GameHandler.Interfaces;
    using System;
    using System.Collections.Generic;

    public class ConsoleUiDrawHandler : IDrawHandler
    {
        public void Draw(DrawableDataBuffer buffer)
        {
            List<IDrawable> drawableObjects = buffer.GetData();

            foreach (IDrawable obj in drawableObjects) {
                Console.WriteLine(obj.GetDrawableData());
            }
        }
    }
}
