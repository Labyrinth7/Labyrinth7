namespace GameHandler.Interfaces
{
    using System.Collections.Generic;

    public interface IUserInterfaceHandler
    {
        void Draw(List<IDrawable> drawableObjects);
    }
}
