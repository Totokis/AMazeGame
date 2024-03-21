namespace Counters
{
    public class TrashCounter : BaseCounter
    {
        public override void Interact(CMPlayer cmPlayer)
        {
            if (cmPlayer.HasKitchenItem())
            {
                cmPlayer.GetKitchenItem().DestroySelf();
            }
        }
    }
}