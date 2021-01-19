using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TMS.FitnessTracker
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Starting Fitness Tracker...");
			IEnumerable<Training> trainings = TrainingGenerator.GetTrainings();
			//var pulses1 = TrainingGenerator.GetPulses(true);
			//var pulses2 = TrainingGenerator.GetPulses(false);

			Console.WriteLine("Started Fitness Tracker.");
			var averageDistance = GetAverageDistance(trainings);
			var allTimeDistance = GetAllTimeDistance(trainings);
			var maxDistance = GetMaxDistance(trainings);

			// TODO: Output results
		}

		private static double GetMaxDistance(IEnumerable<Training> trainings)
		{
			//TODO: return max distance for all trainings
			return 0;
		}

		private static double GetAllTimeDistance(IEnumerable<Training> trainings)
		{
			//TODO: return distance for all trainings
			return 0;
		}

		private static double GetAverageDistance(IEnumerable<Training> trainings)
		{
			//TODO: return avarage distance for all trainings
			return 0;
		}
	}

	internal class TrainingGenerator
	{
		public static IEnumerable<Training> GetTrainings()
		{
			return Enumerable.Range(0, 100).Select(CreateTraining);
		}

		private static Training CreateTraining(int num)
		{
			throw new NotImplementedException();
		}

		public static IEnumerable<int> GetPulses(bool hasThird) // true -> [0, 1, 2, 3]
																// 1. Current = 0 -> MoveNext()
																// 2. Current = 1 -> MoveNext()
																// 3. hasThird 
																//		? Current = 2 -> MoveNext()
																//		: Current = 3 -> MoveNext();
		{
			yield return 0;

			yield return 1;

			if (hasThird)
				yield return 2;
			// else
			// 	yield break;

			yield return 3;
		}
	}

	internal class Training
	{
		public TimeSpan Duration { get; set; }
		public DateTime StartDate { get; set; }
		public double Distance { get; set; }
		public int Steps { get; set; }
		public int AveragePulse { get; set; }
	}
}
