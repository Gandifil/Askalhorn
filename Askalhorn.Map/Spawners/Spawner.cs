﻿using System;
using Askalhorn.Common.Interpetators;
using Askalhorn.Map.Local;
using Newtonsoft.Json;

namespace Askalhorn.Map.Spawners
{
    internal class Spawner: ISpawner
    {
        public IExpression<Position> PositionExpression { get;}

        public IGameObject GameObject { get;}

        [JsonConstructor]
        public Spawner(IExpression<Position> positionExpression, IGameObject gameObject)
        {
            PositionExpression = positionExpression;
            GameObject = gameObject;
        }
        
        public void Initialize(Location location, Random random, int[] args, bool isLoading)
        {
            GameObject.Position = PositionExpression.Generate(location, random);
            
            if (!isLoading || GameObject.IsStatic)
                location.Add(GameObject);
        }
    }
}