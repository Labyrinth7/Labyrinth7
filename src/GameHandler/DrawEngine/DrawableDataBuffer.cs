namespace GameHandler.DrawEngine
{
    using GameHandler.Interfaces;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DrawableDataBuffer : IEnumerable
    {
        public List<IDrawable> drawableObjects = new List<IDrawable>();

        public IEnumerator GetEnumerator()
        {
            foreach (IDrawable drawable in this.drawableObjects)
            {
                yield return drawable;
            }
        }

        public void AddData(IDrawable data)
        {
            this.drawableObjects.Add(data);
        }

        public List<IDrawable> GetData()
        {
            return this.drawableObjects;
        }

        public void EmptyBuffer()
        {
            this.drawableObjects.Clear();
        }
    }
}
