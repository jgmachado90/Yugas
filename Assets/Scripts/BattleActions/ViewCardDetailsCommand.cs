public class ViewCardDetailsCommand : ICommand
{
    private readonly CardData _card;

    public ViewCardDetailsCommand(CardData card)
    {
        _card = card;
    }

    public void Execute()
    {

    }
}
