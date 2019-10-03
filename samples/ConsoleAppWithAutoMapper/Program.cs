namespace ConsoleAppWithAutoMapper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using PaginableCollections;

    class Program
    {
        static void Main(string[] args)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new Profiles());
            });

            IMapper mapper = config.CreateMapper();

            var infitiyStones = new List<InfinityStone>
            {
                new InfinityStone { Name = "Space", Color = "Blue" },
                new InfinityStone { Name = "Reality", Color = "Red" },
                new InfinityStone { Name = "Power", Color = "Purple" },
                new InfinityStone { Name = "Mind", Color = "Yellow" },
                new InfinityStone { Name = "Time", Color = "Green" },
                new InfinityStone { Name = "Soul", Color = "Orange" }
            };

            var infinityStoneDtos = mapper.Map<List<InfinityStoneDto>>(infitiyStones);

            infinityStoneDtos
                .ForEach(x => Console.WriteLine($"The {x.Name} is {x.Color}"));

            Console.ReadLine();
        }
    }
}
