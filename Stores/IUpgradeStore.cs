using ASPClicker.Models;

namespace ASPClicker.Stores
{
    public interface IUpgradeStore
    {
        List<Upgrade> GetUpgrades();
        Upgrade GetUpgrade(string id);
        void PurchaseUpgrade(ICircle payingCircle, ICircle upgradedCircle, string id, double discount);
    }
}
