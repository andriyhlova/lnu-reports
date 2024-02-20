﻿using SRS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SRS.Domain.Entities
{
    public class ThemeOfScientificWork : BaseValueEntity
    {
        public string ThemeNumber { get; set; }

        public string Code { get; set; }

        public string ScientificHead { get; set; }

        public DateTime PeriodFrom { get; set; }

        public DateTime PeriodTo { get; set; }

        public Financial Financial { get; set; }

        public string OtherProjectType { get; set; }

        public decimal? PlannedAmount { get; set; }

        public Currency? Currency { get; set; }

        public bool IsActive { get; set; } = true;

        public ScientificWorkSubCategory? SubCategory { get; set; }

        public virtual ICollection<ReportThemeOfScientificWork> Reports { get; set; }

        public string UserId { get; set; }

        public string SupervisorId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ApplicationUser Supervisor { get; set; }

        public virtual ICollection<ThemeOfScientificWorkFinancial> ThemeOfScientificWorkFinancials { get; set; }

        public virtual ICollection<ThemeOfScientificWorkCathedra> ThemeOfScientificWorkCathedras { get; set; }

        public virtual ICollection<ThemeOfScientificWorkSupervisor> ThemeOfScientificWorkSupervisors { get; set; }

        public virtual ICollection<CathedraReport> CathedraReports { get; set; }

        public string GetSupervisor()
        {
            if (Supervisor == null)
            {
                return ScientificHead;
            }

            return Supervisor.I18nUserInitials.FirstOrDefault(x => x.Language == Language.UA)?.FullName;
        }

        public string GetSupervisorWithTitles()
        {
            if (Supervisor == null)
            {
                return ScientificHead;
            }

            var supervisor = GetSupervisor();
            var titles = Supervisor.GetTitles();

            if (string.IsNullOrWhiteSpace(titles))
            {
                return supervisor;
            }

            return string.Join(", ", supervisor, titles);
        }
    }
}