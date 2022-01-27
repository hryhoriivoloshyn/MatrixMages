using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatrixMages.Data;
using MatrixMages.Models;
using MatrixMages.ViewModels;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;

namespace MatrixMages.Controllers
{
    public class MatrixController : Controller
    {
        private readonly MatrixMagesdbContext _context;

        public MatrixController(MatrixMagesdbContext context)
        {
            _context = context;
        }

        public  IEnumerable<BattleMageStrategy> CalculateMaxMin(List<Victory> victories, List<BattleMageStrategy> battleMageStrategies)
        {
            int maxVictory=0;
            foreach (var victory in victories)
            {
                if (maxVictory < victory.Victory1)
                {
                    maxVictory = victory.Victory1;
                }
            }

            foreach (var victory in victories)
            {
                victory.Victory1 = maxVictory - victory.Victory1;
            }

            Victory minVictory;
            List<Victory> maxMinList = new List<Victory>();
            for (int i = 0; i < battleMageStrategies.Count(); i++)
            {
                List<Victory> strategyVictories =
                    victories.Where(e => e.BattleMageId == battleMageStrategies[i].Id).ToList();

                minVictory= victories[0];
                foreach (var victory in strategyVictories)
                {
                    if (minVictory.Victory1 > victory.Victory1)
                    {
                        minVictory = victory;
                    }
                }

                maxMinList.Add(minVictory);
            }

            Victory maxMinVictory=maxMinList[0];
            foreach (var victory in maxMinList)
            {
                if (maxMinVictory.Victory1 < victory.Victory1)
                {
                    maxMinVictory = victory;
                }
            }

            List<Victory> result = maxMinList.Where(e => e.Victory1 == maxMinVictory.Victory1).ToList();
            List<BattleMageStrategy> firstPlayerResult = new List<BattleMageStrategy>();
            foreach (var victory in result)
            {
                firstPlayerResult.Add(victory.BattleMage);
            }
            return firstPlayerResult;
        }
        public async Task<IActionResult> Index()
        {
            var victories = await _context.Victories.Include(v => v.BattleMage).Include(v => v.SpaceMage).ToListAsync();
            var battlemageStrategies = await _context.BattleMageStrategies.ToListAsync();
            var spacemageStrategies = await _context.SpaceMageStrategies.ToListAsync();

            MatrixViewModel viewModel = new MatrixViewModel()
            {
                Victories = victories,
                BattleMageStrategies = battlemageStrategies,
                SpaceMageStrategies = spacemageStrategies,
                MaxMinResult = CalculateMaxMin(victories.ToList(),battlemageStrategies.ToList())
            };
            return View(viewModel);
        }
    }
}
