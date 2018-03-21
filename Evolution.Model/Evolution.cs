﻿using Ardalis.GuardClauses;
using System;

namespace Evolution.Model
{
    public class Evolution
    {
        public Guid Guid { get; }
        public string FileName { get; }
        public string Name { get; }
        public IDate Created { get; }

        public Evolution(IDate created, string evolutionName) :
            this(Guid.NewGuid(), evolutionName, created) { }

        public Evolution(Guid guid, string evolutionName, IDate created, string fileName = null)
        {
            Guard.Against.Null(guid, nameof(guid));
            Guard.Against.Null(created, nameof(created));
            Guard.Against.NullOrWhiteSpace(evolutionName, nameof(evolutionName));

            if (string.IsNullOrEmpty(fileName))
                fileName = CreateFileName(created, evolutionName);
            else
                Guard.Against.NullOrWhiteSpace(fileName, nameof(fileName));

            Guid = guid;
            Name = evolutionName;
            FileName = fileName;
            Created = created;
        }

        private static string CreateFileName(IDate created, string evolutionName)
        {
            return string.Format("{0}_{1}.evo.sql", created.ToString(), evolutionName);
        }
    }
}