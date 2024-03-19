using UniRx;

namespace _Project.Scripts.UI.Upgrades
{
    public interface IUpgradePopUpPresenter
    {
        public ReactiveCommand CloseCommand { get; }
        public CompositeDisposable CompositeDisposable { get;}
        
        public void Show();
        public void Subscribe();
        public void UnSubscribe();
    }
}