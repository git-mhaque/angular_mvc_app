﻿using AngularArchitecture.Services.Interface;
using AngularArchitecture.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularArchitecture.Services
{
    public class AppService : IAppService 
    {

        private List<ProjectViewModel> _projectList = new List<ProjectViewModel>();

        public AppService() 
        {
            for (int i = 1; i <= 23; i++) 
            {

                ProjectViewModel vm = new ProjectViewModel()
                {
                    ProjectID = i,
                    Name = "Project " + i,
                    Description = "Description for project " + i,
                    StartDate = new DateTime(2014, 1, 12),
                    EndDate = new DateTime(2014, 1, 12).AddDays(120)
                };

                _projectList.Add(vm);
            }
        }


        public ResponseViewModel GetProjectList()
        {
            ResponseViewModel rvm = new ResponseViewModel();

            rvm.Data = _projectList;

            return rvm;
        }

        public ResponseViewModel SaveProject(ProjectViewModel vm)
        {
            ResponseViewModel rvm = new ResponseViewModel("The record has been successfully saved.");

            if (vm.ProjectID == 0)
            {
                var existingProject = from rec in _projectList
                                      where rec.Name.Equals(vm.Name,StringComparison.InvariantCultureIgnoreCase)
                                      select rec;

                if (existingProject != null && existingProject.Count()  > 0)
                {
                    rvm.AddError("A project with the same naem already exists, please choose a different name.");
                }
                else
                {
                    vm.ProjectID = _projectList.Count + 1;
                    _projectList.Add(vm);
                    rvm.Data = vm;
                }
                
            }
            else
            {
                var vmNew = _projectList.SingleOrDefault(q => q.ProjectID == vm.ProjectID);

                if (vmNew != null)
                {
                    vmNew.Name = vm.Name;
                    vmNew.Description = vm.Description;
                    vmNew.StartDate = vm.StartDate;
                    vmNew.EndDate = vm.EndDate;

                    rvm.Data = vmNew;

                }
                else 
                {
                    rvm.AddError("Couldn't find the project.");
                }

            }

            return rvm;

        }

        public ResponseViewModel GetProject(int id)
        {
            ResponseViewModel rvm = new ResponseViewModel();

            var vm = _projectList.SingleOrDefault(q => q.ProjectID == id);

            rvm.Data = vm;

            return rvm; 
        }


   
    }
}