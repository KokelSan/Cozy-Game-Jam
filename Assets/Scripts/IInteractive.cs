public interface IInteractive
{
    public void Select();
    public void UnSelect();
    public void Drag(bool isHeld);

    public bool IsActive();
    public void SetActive(bool flag);
}
