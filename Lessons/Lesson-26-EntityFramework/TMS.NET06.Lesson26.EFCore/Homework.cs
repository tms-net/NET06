using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMSStudens
{
	[Index(nameof(Name /*, IsUnique = true*/))]
	[Table("homeworks")]
	public class Homework
	{
		[Key]
		public int HomeworkId { get; set; }

		public string Name { get; set; }

		public Mark Mark { get; set; }

		public bool? IsCompleted { get; set; }

		public DateTime DateRereived { get; }

		[NotMapped]
		public string PullRequest { get; set; }
		public DateTime Created { get; set; }
		public /*virtual*/ Student Student { get; set; } // Inverse Navigation Property

		public ICollection<Tag> Tags { get; set; }

		public HomeworkType Type { get; set; }

		public virtual Student Reviewer { get; set; }
	}

	public enum HomeworkType
	{
		GroupWork = 1,
		IndividualWork = 2,
		Report
	}

	public readonly struct Mark
	{
		public Mark(int? mark) => Value = mark;

		public int? Value { get; }

		public override string ToString() => Value switch
		{
			{ } v when v <= 3 => "neud",
			{ } v when v > 3 && v <= 7 => "good",
			{ } v when v > 7 => "excelent",
			_ => "undefined"
		};
	}
}
