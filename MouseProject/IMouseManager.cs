namespace MouseProject
{
    public interface IMouseManager
    {
        void MoveCursor(int x, int y);

        int GetX();

        int GetY();

        void Click();

        void RightClick();

        void DoubleClick();

        void Scroll(int y);

        void ClickDown();

        void ClickUp();
    }
}
