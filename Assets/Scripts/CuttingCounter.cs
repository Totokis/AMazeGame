public class CuttingCounter : BaseCounter
{
    public override void Interact(CMPlayer player)
    {
        if (HasKitchenItem() == false)
        {
            if (player.HasKitchenItem())
            {
                player.GetKitchenItem().SetKitchenObjectParent(this);
            }
        }
        else
        {
            if (player.HasKitchenItem())
            {
            }
            else
            {
                GetKitchenItem().SetKitchenObjectParent(player);
            }
        }
    }
}