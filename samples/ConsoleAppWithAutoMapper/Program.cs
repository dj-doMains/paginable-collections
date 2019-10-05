namespace ConsoleAppWithAutoMapper
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using PaginableCollections;

    class Program
    {
        static void Main(string[] args)
        {
            var itemCountPerPage = 2;
            var pageNumber = 2;

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

            var paginable =
                infitiyStones
                    .ToPaginable<InfinityStone, InfinityStoneDto>(pageNumber, itemCountPerPage, mapper, o =>
                    {
                        o.Items["CraftedBy"] = "Eitri";
                    });

            foreach (var item in paginable)
                Console.WriteLine($"The {item.Item.Name} is {item.Item.Color}");

            Console.WriteLine(string.Format("page number {0}", pageNumber));
            Console.WriteLine(string.Format("item count per page {0}", itemCountPerPage));
            Console.WriteLine(string.Format("showing items {0} to {1} of {2}", paginable.FirstItemNumber, paginable.LastItemNumber, paginable.TotalItemCount));
            Console.ReadLine();
        }
    }
}
