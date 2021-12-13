using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using EquipmentReplacementProblem.Business.Dto;
using EquipmentReplacementProblem.Render.Models;
using EquipmentReplacementProblem.Render.Models.EqualityComparers;

namespace EquipmentReplacementProblem.Render.Helpers
{
    public static class ErpOutputToGraphRenderModelConverter
    {
        public static GraphRenderModel ConvertOutputToRenderModel(ErpOutputDto erpOutputDto)
        {
            var renderMultiplier = 50; 
            var lines = new List<CartesianEdge>();

            foreach (var edge in erpOutputDto.GraphEdges)
            {
                lines.Add(new CartesianEdge()
                {
                    From = new CartesianPoint()
                    {
                        X = edge.Source.YearOfDecision * renderMultiplier,
                        Y = edge.Source.EquipmentAge * renderMultiplier,
                    },
                    To = new CartesianPoint()
                    {
                        X = edge.Target.YearOfDecision * renderMultiplier,
                        Y = edge.Target.EquipmentAge * renderMultiplier,
                    }
                });
            }

            var points = new List<CartesianPoint>();
            foreach (var vertex in erpOutputDto.GraphVertices)
            {
                points.Add(new CartesianPoint()
                {
                    X = vertex.YearOfDecision * renderMultiplier,
                    Y = vertex.EquipmentAge * renderMultiplier
                });
            }

            var path = new List<CartesianEdge>();
            foreach (var edge in erpOutputDto.OptimalPath)
            {
                path.Add(new CartesianEdge()
                {
                    From = new CartesianPoint()
                    {
                        X = edge.Source.YearOfDecision * renderMultiplier,
                        Y = edge.Source.EquipmentAge * renderMultiplier,
                    },
                    To = new CartesianPoint()
                    {
                        X = edge.Target.YearOfDecision * renderMultiplier,
                        Y = edge.Target.EquipmentAge * renderMultiplier,
                    }
                });
            }

            var endPoint = erpOutputDto.OptimalPath.Last(); 

            var renderModel = new GraphRenderModel()
            {
                Lines = lines.Distinct(new CartesianEdgeEqualityComparer()).ToList(),
                Points = points.Distinct(new CartesianPointEqualityComparer()).ToList(),
                Path = path,
                SellPoint = new CartesianPoint()
                {
                    X = endPoint.Target.YearOfDecision * renderMultiplier,
                    Y = endPoint.Target.EquipmentAge * renderMultiplier,
                }
            };

            return renderModel;
        }
    }
}
