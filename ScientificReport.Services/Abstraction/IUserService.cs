﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScientificReport.DAL.Models;

namespace ScientificReport.Services.Abstraction {
  public interface IUserService {
    ApplicationUser GetCurrentUser(string userName);
  }
}
