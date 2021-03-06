﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZergScheduler.ViewModels;
using ZergScheduler.Models;

namespace ZergScheduler.Controllers
{
	public class CourseController : Controller
	{
		ZergRushEntities courseDB = new ZergRushEntities();

		// GET: /Course/
		public ActionResult Index()
		{
			// Create a list of genres
			var departments = from department in courseDB.Departments
							  orderby department.dept_title
							  select department;

			// Create our view model
			var viewModel = new CourseIndexViewModel
			{
				NumberOfDepartments = departments.Count(),
				Departments = departments.ToList()
			};

			return View(viewModel);
		}

		// GET: /Course/Browse?department=CMSC
		public ActionResult Browse(string dept)
		{
			var departmentModel = courseDB.Departments.Include("Courses").Single(g => g.dept_id == dept);

			var viewModel = new CourseBrowseViewModel
			{
				Department = departmentModel,
				Courses = departmentModel.Courses.ToList()
			};

			return View(viewModel);
		}

		// GET: /Course/Details/CMSC345
		public ActionResult Details(string id)
		{
			var course = courseDB.Courses.Single(a => a.course_id == id);

			return View(course);
		}
	}
}
