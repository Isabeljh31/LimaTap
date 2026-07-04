using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitSystem.Shared.Models;


namespace TransitSystem.Core.Interfaces
{
    public interface ITransitIssueService
    {
        bool RegisterIssue(TransitIssueDto issue);
        List<TransitIssueDto> GetActiveIssues();
    }
}
