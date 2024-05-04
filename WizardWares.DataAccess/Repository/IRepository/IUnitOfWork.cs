﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardWares.DataAccess.Repositiory.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }

        void Save();
    }
}
