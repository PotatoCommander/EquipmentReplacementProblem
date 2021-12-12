using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EquipmentReplacementProblem.Business.Dto;
using EquipmentReplacementProblem.Business.Services;
using EquipmentReplacementProblem.Render.Drawing;
using EquipmentReplacementProblem.Render.Helpers;

namespace Clientss.EquipmentReplaceProblemWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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
            var _erpSolutionService = new ErpBellmanFordSolutionService();
            var solution = _erpSolutionService.FindOptimalStrategy(info);
            var converted = ErpOutputToGraphRenderModelConverter.ConvertOutputToRenderModel(solution);
            var renderer = new GraphRender();
            var bmp = renderer.DrawGraph(converted);
            this.pictureBox1.Image = bmp;
        }
    }
}
