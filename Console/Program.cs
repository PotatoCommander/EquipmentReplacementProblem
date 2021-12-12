using System;
using System.Collections.Generic;
using DealerServiceSystem.Business;
using DealerServiceSystem.Business.Core;
using DealerServiceSystem.Business.Dto;
using DealerServiceSystem.Business.Services;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var info = new ErpInputDto()
            {
                EquipmentAgeAtStart = 3,
                YearStart = 1,
                EquipmentServiceInformation = new Dictionary<int, EquipmentServiceInformation>()
                {
                    {
                        0, new EquipmentServiceInformation()
                        {
                            Income = 20000,
                            ServiceExpenses = 200,
                            SellEquipmentCost = 100000,
                            NewEquipmentCost = 100000
                        }
                    },
                    {
                        1, new EquipmentServiceInformation()
                        {
                            Income = 19000,
                            ServiceExpenses = 600,
                            SellEquipmentCost = 80000,
                            NewEquipmentCost = 100000
                        }
                    },
                    {
                        2, new EquipmentServiceInformation()
                        {
                            Income = 18500,
                            ServiceExpenses = 1200,
                            SellEquipmentCost = 60000,
                            NewEquipmentCost = 100000
                        }
                    },
                    {
                        3, new EquipmentServiceInformation()
                        {
                            Income = 17200,
                            ServiceExpenses = 1500,
                            SellEquipmentCost = 50000,
                            NewEquipmentCost = 100000
                        }
                    },
                    {
                        4, new EquipmentServiceInformation()
                        {
                            Income = 15500,
                            ServiceExpenses = 1700,
                            SellEquipmentCost = 30000,
                            NewEquipmentCost = 100000
                        }
                    },
                    {
                        5, new EquipmentServiceInformation()
                        {
                            Income = 14000,
                            ServiceExpenses = 1800,
                            SellEquipmentCost = 10000,
                            NewEquipmentCost = 100000
                        }
                    },
                    {
                        6, new EquipmentServiceInformation()
                        {
                            Income = 12200,
                            ServiceExpenses = 2200,
                            SellEquipmentCost = 5000,
                            NewEquipmentCost = 100000
                        }
                    }
                }
            };

            var erpBellmanFordSolution = new ErpBellmanFordSolutionService();
            erpBellmanFordSolution.FindOptimalStrategy(info);
            var b = erpBellmanFordSolution.FindOptimalStrategy(info);
        }
    }
}
