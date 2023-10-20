﻿using DevFreela.Domain.Domain.Entities;

namespace DevFreela.Infrastructure.Persistence;
public class DevFreelaDbContext
{

    public List<User> Users { get; set; }
    public List<Project> Projects { get; set; }
    public List<Skill> Skills { get; set; }

    public List<ProjectComment> ProjectComments { get; set; }

}
