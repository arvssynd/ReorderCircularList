using System;
using System.Collections.Generic;
using System.Linq;

namespace ReorderCircularList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new List<Analysis>
            {
                //new Analysis(){
                //    Name = "analisi 1",
                //    Position = 47,
                //    PositionInternal = 1,
                //},
                new Analysis(){
                    Name = "analisi 2",
                    Position = 49,
                    PositionInternal = 1,
                },
                new Analysis(){
                    Name = "analisi 3",
                    Position = 50,
                    PositionInternal = 2,
                },
                new Analysis(){
                    Name = "analisi 4",
                    Position = 1,
                    PositionInternal = 3,
                },
                new Analysis(){
                    Name = "analisi 5",
                    Position = 2,
                    PositionInternal = 4,
                },
                //new Analysis(){
                //    Name = "analisi 6",
                //    Position = 37,
                //    PositionInternal = 3,
                //},
                //new Analysis(){
                //    Name = "analisi 7",
                //    Position = 38,
                //    PositionInternal = 4,
                //},
                //new Analysis(){
                //    Name = "analisi 8",
                //    Position = 39,
                //    PositionInternal = 5,
                //},
            };


            // QUESTI VALORI SONO POSITION INTERNAL SEMPRE
            // muovo posizione 1 alla posizione 4
            var previousPositionInternal = 2;
            var newPositionInternal = 4;

            // muovo posizione 4 alla posizione 1
            //var previousPositionInternal = 4;
            //var newPositionInternal = 1;

            var found = false;
            var newStartIndex = 0;
            var indexWhereToStart = 0;
            var upperThreshold = 50;
            var lowerThreshold = 1;
            var positionToSubstitute = 0;

            var exceedThreshold = list.Any(x => x.Position == upperThreshold) && list.Any(x => x.Position == lowerThreshold);

            foreach (var item in list)
            {
                if (!found)
                {
                    if (item.PositionInternal == previousPositionInternal)
                    {
                        found = true;
                        item.PositionInternal = newPositionInternal;

                        if (newPositionInternal < previousPositionInternal || exceedThreshold)
                        {
                            newStartIndex = item.PositionInternal;
                            indexWhereToStart = item.Position;

                            if (exceedThreshold || newPositionInternal < previousPositionInternal)
                            {
                                positionToSubstitute = list.Where(x => x.PositionInternal == item.PositionInternal && x.Position != item.Position).First().Position;
                            }
                        }
                    }
                }
                else
                {
                    if (item.PositionInternal <= newPositionInternal)
                    {
                        item.PositionInternal--;
                    }
                }
            }

            if (!exceedThreshold && newPositionInternal > previousPositionInternal)
            {
                list = list.OrderBy(x => x.PositionInternal).ToList();
                var minPosition = list.Min(x => x.Position);
                var index = 0;
                foreach (var item in list)
                {
                    item.Position = minPosition + index;
                    index++;
                }
            }
            else
            {

                if (newStartIndex > 0)
                {
                    list = list.OrderBy(x => x.PositionInternal).ToList();
                    foreach (var item in list)
                    {
                        if (item.PositionInternal < previousPositionInternal &&
                            (
                                (item.PositionInternal == newPositionInternal && item.Position != indexWhereToStart) ||
                                item.PositionInternal != newPositionInternal
                            ) &&
                            newPositionInternal > previousPositionInternal
                           )
                        {
                            item.PositionInternal++;
                        }
                        else if (newPositionInternal < previousPositionInternal)
                        {
                            bool assigned = false;
                            if (item.PositionInternal == newPositionInternal && item.Position != positionToSubstitute)
                            {
                                item.Position = positionToSubstitute;
                                assigned = true;
                            }

                            if (item.PositionInternal < previousPositionInternal &&
                                item.PositionInternal >= newPositionInternal &&
                                (
                                    (item.PositionInternal == newPositionInternal && item.Position != indexWhereToStart) ||
                                    item.PositionInternal != newPositionInternal
                                ) &&
                                !assigned
                            )
                            {
                                item.PositionInternal++;
                                item.Position++;
                            }

                            if (item.Position == 51)
                            {
                                item.Position = 1;
                            }
                        }
                        else if (exceedThreshold && item.PositionInternal <= newPositionInternal)
                        {
                            if (item.Position == indexWhereToStart)
                            {
                                item.Position = positionToSubstitute;
                            }
                            else
                            {
                                item.Position--;
                            }

                            if (item.Position == 0)
                            {
                                item.Position = 50;
                            }
                        }
                    }
                }
            }




            foreach (var item in list.OrderBy(x => x.PositionInternal))
            {
                Console.WriteLine("Name: " + item.Name + ", Position: " + item.Position + ", PositionInternal: " + item.PositionInternal);
            }

        }

        public class Analysis
        {
            public string Name { get; set; }
            public int Position { get; set; }
            public int PositionInternal { get; set; }
        }
    }
}
