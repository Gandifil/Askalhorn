﻿using System;
using Askalhorn.Common.Interpetators;
using Askalhorn.Map.Local;
using Newtonsoft.Json;

namespace Askalhorn.Map.Spawners
{
    internal class Spawner: ISpawner
    {
        public IExpression<Position> PositionExpression { get;}

        public IGameObjectBuilder Builder { get;}

        [JsonConstructor]
        public Spawner(IExpression<Position> positionExpression, IGameObjectBuilder builder)
        {
            PositionExpression = positionExpression;
            Builder = builder;
        }
        
        public void Initialize(Location location, Random random, int[] args, bool isLoading)
        {
            var obj = Builder.Build(PositionExpression.Generate(new LocationExpressionArgs
            {
                Location = location,
                Random = random,
            }));
            
            if (!isLoading || obj.IsStatic)
                location.Add(obj);
        }
    }
}