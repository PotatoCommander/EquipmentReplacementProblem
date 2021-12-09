using System;
using System.Collections.Generic;
using System.Text;
using DealerServiceSystem.Business.Core.Models;
using DealerServiceSystem.Business.Core.Models.Graph;

namespace DealerServiceSystem.Business.Core
{
    public class LongestPathInGraphAlgo
    {
        public void ExampleMethod()
        {
            var info = new EquipmentUsageInformation()
            {
                EquipmentInfo = new Dictionary<int, AtomicEquipmentInfo>()
                {
                    {
                        0, new AtomicEquipmentInfo()
                        {
                            Income = 20000,
                            UsageExpenses = 200,
                            SellEquipmentCost = 100000,
                            NewEquipmentCost = 100000
                        }
                    },
                    {
                        1, new AtomicEquipmentInfo()
                        {
                            Income = 19000,
                            UsageExpenses = 600,
                            SellEquipmentCost = 80000,
                            NewEquipmentCost = 100000
                        }
                    },
                    {
                        2, new AtomicEquipmentInfo()
                        {
                            Income = 18500,
                            UsageExpenses = 1200,
                            SellEquipmentCost = 60000,
                            NewEquipmentCost = 100000
                        }
                    },
                    {
                        3, new AtomicEquipmentInfo()
                        {
                            Income = 17200,
                            UsageExpenses = 1500,
                            SellEquipmentCost = 50000,
                            NewEquipmentCost = 100000
                        }
                    },
                    {
                        4, new AtomicEquipmentInfo()
                        {
                            Income = 15500,
                            UsageExpenses = 1700,
                            SellEquipmentCost = 30000,
                            NewEquipmentCost = 100000
                        }
                    },
                    {
                        5, new AtomicEquipmentInfo()
                        {
                            Income = 14000,
                            UsageExpenses = 1800,
                            SellEquipmentCost = 10000,
                            NewEquipmentCost = 100000
                        }
                    },
                    {
                        6, new AtomicEquipmentInfo()
                        {
                            Income = 12200,
                            UsageExpenses = 2200,
                            SellEquipmentCost = 5000,
                            NewEquipmentCost = 100000
                        }
                    }
                }
            };

            var graph = new GraphModel(info, 1, 3);
            var dec = new DecisionGraph(graph);
            dec.PrintShortestPath(graph.StartVertex.Name, "End");
        }
    }
}
