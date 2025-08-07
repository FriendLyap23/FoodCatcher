using Zenject;

public class GameplayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Timer>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Score>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Lose>().FromComponentInHierarchy().AsSingle();

        Container.Bind<ScreenLimitation>().FromComponentInHierarchy().AsSingle();

        Container.Bind<MainCharacterMovement>().FromComponentInHierarchy().AsSingle();
    }
}
