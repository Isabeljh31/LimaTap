using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitSystem.Core.Interfaces;
using TransitSystem.Shared.Models;

namespace TransitSystem.Core.Services
{
    public class TransitIssueService : ITransitIssueService
    {
        private static readonly List<TransitIssueDto> _issuesDatabase = new();

        public bool RegisterIssue(TransitIssueDto issue)
        {
            if (string.IsNullOrWhiteSpace(issue.CardNumber) || string.IsNullOrWhiteSpace(issue.Description))
            {
                return false;
            }

            // Simulación de flujo: se asigna al equipo de operaciones de LimaTap
            issue.Status = "Asignado a Soporte Técnico en Estación";
            _issuesDatabase.Add(issue);
            return true;
        }

        public List<TransitIssueDto> GetActiveIssues()
        {
            return _issuesDatabase;
        }
    }
}
