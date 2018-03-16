﻿using Evolution.Data.Entity;

namespace Evolution.Repo
{
    public interface IEvolutionRepo
    {
        string[] GetExecutedEvolutionFileNames();
        void AddEvolution(IEvolution evolution);
        void SaveChanges();
        void ExecuteEvolution(string content);
    }
}
