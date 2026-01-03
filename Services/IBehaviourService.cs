using CatBreedingProgram.Models;

public interface IBehaviourService
{
    Task LogBehaviour(BehaviourLogRequest behaviourLogRequest);
}