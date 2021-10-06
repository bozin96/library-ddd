using Library.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.SyncedAggregates.Commands.CheckCommand
{
    public class CheckCommand : BaseCommandWithoutResult
    {
        public int Test { get; set; }

        public string Proba { get; set; }

        public string Proba2 { get; set; }

        public override bool IsValid()
        {
            return true;
        }
    }
}
